using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace rh_admin
{
    public class Util
    {
        private Util()
        {
        }

        public static HashSalt hashPassword(string password)
        {
            ;

            // generate a 128-bit salt using a secure PRNG
            var salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var key = KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            var base64 = Convert.ToBase64String(key);

            /* Fetch the stored value */
            return new HashSalt
            {
                Hash = base64,
                Salt = Convert.ToBase64String(salt)
            };
        }

        public static bool check(string userEnteredPassword, HashSalt hashSalt)
        {
            return check(userEnteredPassword, hashSalt.Salt, hashSalt.Hash);
        }

        public static bool check(string userEnteredPassword, string passwordSalt, string passwordHash)
        {
            var hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                userEnteredPassword,
                Convert.FromBase64String(passwordSalt), ///Encoding.ASCII.GetBytes(dbPasswordSalt),
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));
            Console.WriteLine(hashedPassword);
            return passwordHash == hashedPassword;
        }
    }

    public class HashSalt
    {
        public string Hash { get; set; }
        public string Salt { get; set; }
    }

    //
    // public sealed class PasswordHasher : IPasswordHasher
    // {
    //     private const int SaltSize = 16; // 128 bit 
    //     private const int KeySize = 32; // 256 bit
    //
    //     public PasswordHasher(IOptions<HashingOptions> options)
    //     {
    //         Options = options.Value;
    //     }
    //
    //     private HashingOptions Options { get; }
    // }
    //
    // public interface IPasswordHasher
    // {
    //     string Hash(string password);
    //
    //     (bool Verified, bool NeedsUpgrade) Check(string hash, string password);
    // }
    //
    // public sealed class HashingOptions
    // {
    //     public int Iterations { get; set; } = 10000;
    // }
    //
    // public interface IPasswordHasher
    // {
    //     string Hash(string password);
    //
    //     (bool Verified, bool NeedsUpgrade) Check(string hash, string password);
    // }
}