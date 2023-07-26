using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace IntegrationLibrary.Core.Service.Generators
{
    public class RandomStringGenerator
    {
        public string GenerateBase64(int length)
        {
            byte[] key = new byte[length];
            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            return Convert.ToBase64String(key);
        }

        public string GenerateBase62(int length)
        {
            byte[] key = new byte[length];
            using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
                generator.GetBytes(key);
            return ToBase62String(key);
        }

        private string ToBase62String(byte[] key)
        {
            const string alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            BigInteger dividend = new BigInteger(key);
            var builder = new StringBuilder();
            while (dividend != 0)
            {
                dividend = BigInteger.DivRem(dividend, alphabet.Length, out BigInteger remainder);
                builder.Insert(0, alphabet[Math.Abs((int)remainder)]);
            }
            return builder.ToString();
        }
    }
}
