using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Identity.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IStorageService storageService;


        public IdentityService(IStorageService storageService)
        {
            this.storageService = storageService;
        }


        public async Task<User> CreateUserAsync(string username, string password)
        {
            var (salt, hash) = Hash(password);

            var user = new User
            {
                Username = username, 
                Salt = salt,
                Hash = hash,
            };

            await storageService.InsertUserAsync(user);

            return user;
        }

        public async Task<string> IssueUserJwtAsync(string username, string password)
        {
            /*var user = await storageService.FindUserAsync(username);
            if (user == null || Hash(password, user.Salt) != user.Hash)
            {
                throw new ArgumentException();
            }*/

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("DV6y28wLptRaLh8XGyTeRlUTBI4biP8e"));
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            });

            return tokenHandler.WriteToken(token);
        }

        
        private (string salt, string hash) Hash(string password)
        {
            // Based on https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-2.2.

            // Generate a 128-bit salt using a secure PRNG:
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations):
            return (salt: Convert.ToBase64String(salt), hash: Hash(password, salt));
        }

        private string Hash(string password, byte[] salt) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));

        private string Hash(string password, string salt) =>
            Hash(password, Convert.FromBase64String(salt));
    }
}
