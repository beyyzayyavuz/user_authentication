using System;
using System.Security.Cryptography;//secure random number generation and hash algorithms
using System.Text;

public class PasswordHelper
{
    //enhance the security of password hashing.
    public static string GenerateSalt(int size = 16)
    {
        //cryptographic random number generator RNGC
        var rng = new RNGCryptoServiceProvider();
        var buff = new byte[size]; //hold the random bytes.
        rng.GetBytes(buff); //filling the array with cryptographically secure random bytes
        return Convert.ToBase64String(buff);
    }


    public static string HashPassword(string password, string salt)
    {
        var sha256 = SHA256.Create();//Creates an instance of the SHA-256 cryptographic hash algorithm.
        var combinedPassword = password + salt;
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedPassword));
        return Convert.ToBase64String(hashedBytes);
    }
}
