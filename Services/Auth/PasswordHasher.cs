using System.Security.Cryptography;

namespace WebApplication1.Services.Auth
{
    public class PasswordHasher: IPasswordHasher
    {
        byte[]? salt;
        SHA512 SHA512;
        public PasswordHasher()
        {
            SHA512 = SHA512.Create();
        }
        byte[] saltSum(in byte[] input)
        {
            byte[] res;
            if (salt == null) return input;
            else if (salt.Length >= input.Length)
            {
                res = new byte[salt.Length];
                for(int i = 0; i<input.Length; i++)
                {
                    res[i] = (byte)(salt[i] + input[i]);
                }
            }
            else
            {
                res = new byte[input.Length];
                for (int i = 0; i < salt.Length; i++)
                {
                    res[i] = (byte)(salt[i] + input[i]);
                }
            }
            return res;
        }
        public string Generate(string password)
        {
            byte[] passInBytes = Convert.FromBase64String(password);
            passInBytes = saltSum(passInBytes);
            byte[] hashed = SHA512.HashData(passInBytes);
            return Convert.ToBase64String(hashed);
        }
        public void SetSalt(string salt)
        {
            this.salt = Convert.FromBase64String(salt);
        }
    }
}
