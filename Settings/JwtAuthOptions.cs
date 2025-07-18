﻿namespace DevHabitTracker.Settings
{
    public class JwtAuthOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Key { get; set; }

        public int ExpirationInMinutes { get; set; }

        public int RefreshTokenExpirationInDays { get; set; }
    }
}
