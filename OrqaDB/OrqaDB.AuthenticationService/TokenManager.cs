using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OrqaDB.AuthenticationService;

namespace OrqaDB.AuthenticationService
{
    public class TokenManager
    {
        private Timer tokenRefreshTimer;
        private readonly AuthService authService;
        private readonly int tokenRefreshIntervalInMinutes = 60; 
        private readonly Dictionary<string, DateTime> _accessTokenExpiry = new Dictionary<string, DateTime>();
        private string currentAccessToken;

        public TokenManager(AuthService authService, string initialAccessToken)
        {
            this.authService = authService;
            tokenRefreshTimer = new Timer(TokenRefreshCallback, null, 0, tokenRefreshIntervalInMinutes * 60 * 1000);
            currentAccessToken = initialAccessToken;
        }

        private void TokenRefreshCallback(object state)
        {
            try
            {
                foreach (var token in _accessTokenExpiry.Keys.ToList()) // Cheking tokens and refreshing them if valid
                {
                    if (authService.IsAccessTokenValid(token))
                    {
                        authService.RefreshAccessToken(token);
                        _accessTokenExpiry[token] = DateTime.UtcNow.AddMinutes(60); 
                    }
                    else
                    {

                        Logout(token);
                    }
                }
            }
            catch (Exception ex)
            {
             
            }
        }

        public void RefreshToken(string accessToken) // Method for refreshing token, not fully implemented
        {
            try
            {
                if (authService.IsAccessTokenValid(accessToken))
                {
                    string newAccessToken = authService.RefreshAccessToken(accessToken);
                    if (!string.IsNullOrEmpty(newAccessToken))
                    {
                        currentAccessToken = newAccessToken;

                        _accessTokenExpiry[newAccessToken] = DateTime.UtcNow.AddMinutes(60); 
                    }
                    else
                    {
                        Logout(accessToken);
                    }
                }
                else
                {
                    Logout(accessToken);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public void Logout(string accessToken)
        {
            authService.Logout(accessToken);

            _accessTokenExpiry.Remove(accessToken);
        }
    }
}
