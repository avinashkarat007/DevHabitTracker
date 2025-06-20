using DevHabitTracker.Database;
using DevHabitTracker.Extensions;
using DevHabitTracker.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DevHabitTracker.Services
{
    public class UserContext(IHttpContextAccessor httpContextAccessor,
        ApplicationDbContext applicationDbContext,
        IMemoryCache memoryCache) : IUserContext
    {
        private const string CacheKeyPrefix = "users:id";
        private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

        // Here the primary key of the "Users" table, is returned as the user id. It is not the aspnet_users table's Id.
        public async Task<string?> GetUserIdAsync(CancellationToken cancellationToken)
        {
            string? identityId = httpContextAccessor.HttpContext?.User.GetIdentityId();

            if (identityId == null) {
                return null;
            }

            string cacheKey = CacheKeyPrefix + identityId;

            string? userId = await memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SetSlidingExpiration(CacheDuration);

                string? userId = await applicationDbContext.Users
                                .Where(u => u.IdentityId == identityId)
                                .Select(u => u.Id)
                                .FirstOrDefaultAsync(cancellationToken);

                return userId;
            });

            return userId;
        }
    }
}
