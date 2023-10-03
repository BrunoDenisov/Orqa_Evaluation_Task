using OrqaDB.EntityFramework.Queries;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace OrqaDB.AuthenticationService
{
    public class AuthService
    {
        private static AuthService instance;

        private readonly Dictionary<string, DateTime> _accessTokenExpiry = new Dictionary<string, DateTime>();

        private readonly AuthenticationQuery _authenticationQuery;

        private readonly OrqaDbContext _dbContext;

        private Timer tokenValidationTimer;

        private readonly int tokenValidationIntervalInMinutes = 60;

        public AuthService()
        {
            _dbContext = new OrqaDbContext();
            _authenticationQuery = new AuthenticationQuery(_dbContext);
            instance = this;

            tokenValidationTimer = new Timer(TokenValidationCallback, null, 0, tokenValidationIntervalInMinutes * 60 * 1000); // Token validation timer
        }


        public static AuthService GetInstance()
        {
            if (instance == null)
            {
                instance = new AuthService();
            }
            return instance;
        }

        private void TokenValidationCallback(object state)
        {
            try
            {
                List<string> keysToRemove = new List<string>(); // List for storing keys to remove

                foreach (var accessToken in _accessTokenExpiry.Keys)
                {
                    if (!IsAccessTokenValid(accessToken))
                    {
                        keysToRemove.Add(accessToken); // Adds toke to removal list if its invalid
                    }
                }

                foreach (var key in keysToRemove) // Removing invalid tokens
                {
                    _accessTokenExpiry.Remove(key);
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        public string Authenticate(string username, string password)
        {
            try
            {
                string hashedPassword = HashPassword(password);

                string role = _authenticationQuery.AuthenticateUser(username, hashedPassword);

                if (role != null)
                {
                    string accessToken = TokenService.GenerateToken(username, role);

                    DateTime expirationTime = DateTime.UtcNow.AddMinutes(60);

                    StoreAccessToken(accessToken, expirationTime);

                    return accessToken;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Authentication failed.", ex);
            }
        }

        public string RefreshAccessToken(string oldAccessToken) // Method for refreshing a access token Not fully implmented
        {
            if (_accessTokenExpiry.TryGetValue(oldAccessToken, out DateTime expirationTime) && DateTime.UtcNow < expirationTime)
            {
                string username = TokenService.ExtractUsernameFromAccessToken(oldAccessToken); // Extract the username and role from the old access token
                string role = TokenService.ExtractRoleFromAccessToken(oldAccessToken);

                string newAccessToken = TokenService.GenerateToken(username, role); // Generate a new access token with the same username and role

                DateTime newExpirationTime = DateTime.UtcNow.AddMinutes(60); // Update the expiration time for the new access token
                UpdateAccessTokenExpiration(newAccessToken, newExpirationTime);

                return newAccessToken;
            }

            return null;
        }

        public bool IsAccessTokenValid(string accessToken)
        {
            if (_accessTokenExpiry.TryGetValue(accessToken, out DateTime expirationTime))
            {
                return DateTime.UtcNow < expirationTime;
            }

            return false;
        }

        public void Logout(string accessToken)
        {
            InvalidateAccessToken(accessToken);
        }

        private void StoreAccessToken(string accessToken, DateTime expirationTime)
        {
            _accessTokenExpiry[accessToken] = expirationTime;
        }

        private void InvalidateAccessToken(string accessToken)
        {
            _accessTokenExpiry.Remove(accessToken);
        }

        private void UpdateAccessTokenExpiration(string accessToken, DateTime expirationTime)
        {
            _accessTokenExpiry[accessToken] = expirationTime;
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "");
            }
        }
    }

}
