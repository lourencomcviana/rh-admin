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

        // public static void main(String[] args)
        // {
        //    String hashed = hashPassword("1234");
        //    check("1234", hashed);
        // }
        public static String hashPassword(string password)
        {;
 
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
 
            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            return  Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            
            /* Fetch the stored value */
            
        }

        private static Boolean check(String password, String savedPasswordHash)
        {

            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);

            byte[] salt = new byte[128 / 8];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            for (int i=0; i < 20; i++)
                if (hashBytes[i+16] != hash[i])
                    return false;
            return true;
        }
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