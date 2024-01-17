using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SplitWiseAPI.Services.Interface;
using System.Security.Cryptography;

namespace SplitWiseAPI.Services.Implementations
{
    public class EncryptionService:IEncryptionService
    {
        public EncryptionService()
        {
            
        }
        public string EcncryptPassword(string password,string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);

            // Hash the password using PBKDF2 with HMAC-SHA-256
            string hashedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                )
            );

            return hashedPassword;
        }
        public string GenerateSalt(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
    }
}
