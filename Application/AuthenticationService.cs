using System;
using System.Collections.Generic;
using Application.Interface;
using Application.Model;

namespace Application
{
    public class AuthenticationService : IAuthenticationService
    {
        private Dictionary<string, string> _tokens = new Dictionary<string, string>();

        public AuthenticationService() { }

        public AuthenticationService(Dictionary<string, string> tokens)
        {
            _tokens = tokens;
        }

        public string Authenticate(UserAuthentication user)
        {
            if (_tokens.ContainsKey(user.Email))
                return _tokens[user.Email];

            var newToken = GenerateToken(user);
            _tokens.Add(user.Email, newToken);

            return newToken;
        }

        private string GenerateToken(UserAuthentication user)
        {
            var encodeBytes = System.Text.Encoding.Unicode.GetBytes(user.Email + user.Password);
            return Convert.ToBase64String(encodeBytes);
        }
    }
}
