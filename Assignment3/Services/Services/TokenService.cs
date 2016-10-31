using System;
using Assignment3.Services.DataAccess;
using Assignment3.Services.Entities;

namespace Assignment3.Services
{
    public class TokenService : ITokenService {
        private  ITokenDataMapper _mapper;

        public TokenService(ITokenDataMapper mapper) {
            _mapper = mapper;
        }

        private static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public bool validateUserToken(string token, int userID) {
            Token userToken = _mapper.getTokenByUserID(userID);
            if (userToken == null) {
                return false;
            }
            DateTime expiry = Convert.ToDateTime(userToken.expires);
            if (DateTime.Compare(DateTime.Now, expiry) > 0) {
                return false;
            }
            if (userToken.token != token) {
                return false;
            }
            return true;
        }
        public string createUserToken(int userID, string username) {
            string tokenString = username + DateTime.Now.ToString();
            string tokenStringBase64 = Base64Encode(tokenString);
            System.TimeSpan duration = new System.TimeSpan(1, 0, 0, 0);
            string expires = (DateTime.Now.Add(duration)).ToString();
            Token newToken = new Token {
                userID = userID,
                token = tokenStringBase64,
                expires = expires
            };
            _mapper.createOrUpdateUserToken(newToken);
            return tokenStringBase64;
        }
    }
}