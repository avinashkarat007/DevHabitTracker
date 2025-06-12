using DevHabitTracker.Database;
using DevHabitTracker.DTOs.Auth;
using DevHabitTracker.DTOs.User;
using DevHabitTracker.Entities;
using DevHabitTracker.Extensions;
using DevHabitTracker.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DevHabitTracker.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationIdentityDbContext _identityDbContext;
        private readonly TokenProvider _tokenProvider;

        public UserService(ApplicationDbContext context, UserManager<IdentityUser> userManager,
        ApplicationIdentityDbContext identityDbContext,
        ApplicationDbContext applicationDbContext,
        TokenProvider tokenProvider)
        {
            _applicationDbContext = context;
            this._userManager = userManager;
            this._identityDbContext = identityDbContext;
            this._tokenProvider = tokenProvider;
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _applicationDbContext.Users.ToListAsync();
            return users.Select(u => u.ToDto()).ToList();
        }

        public async Task<UserDto?> GetUserByIdAsync(string id)
        {
            var user = await _applicationDbContext.Users.FindAsync(id);
            return user?.ToDto();
        }

        public async Task<AccessTokensDto?> Login(LoginUserDto loginUserDto)
        {

            var identityUser  = await _userManager.FindByEmailAsync(loginUserDto.Email);

            if (identityUser == null || !await _userManager.CheckPasswordAsync(identityUser, loginUserDto.Password))
            {
                return null;
            }

            var tokenRequest = new TokenRequest(identityUser.Id, identityUser.Email!);
            AccessTokensDto accessToeken = _tokenProvider.Create(tokenRequest);
            return accessToeken;
        }

        public async Task<(bool Succeeded, IdentityResult IdentityResult, string? UserId, AccessTokensDto accessToeken)> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            using var identityTransaction = await _identityDbContext.Database.BeginTransactionAsync();
            using var appTransaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                var identityUser = new IdentityUser
                {
                    Email = registerUserDto.Email,
                    UserName = registerUserDto.Email
                };

                // 1.  Creating the identity user
                var identityResult = await _userManager.CreateAsync(identityUser, registerUserDto.Password);
                if (!identityResult.Succeeded)
                {
                    await identityTransaction.RollbackAsync();
                    return (false, identityResult, null, null);
                }

                var appUser = registerUserDto.ToEntity();
                appUser.IdentityId = identityUser.Id;

                // 2. Creating the app user.
                _applicationDbContext.Users.Add(appUser);
                await _applicationDbContext.SaveChangesAsync();

                await identityTransaction.CommitAsync();
                await appTransaction.CommitAsync();

                AccessTokensDto accessToeken = _tokenProvider.Create(new TokenRequest(identityUser.Id, identityUser.Email));

                return (true, identityResult, appUser.Id, accessToeken);
            }
            catch
            {
                await identityTransaction.RollbackAsync();
                await appTransaction.RollbackAsync();
                throw;
            }
        }

    }

}
