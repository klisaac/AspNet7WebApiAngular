using System;
using System.Text;
using System.Security.Cryptography;
using AspNet7.Application.Common.Exceptions;

namespace AspNet7.Application.Common.Helpers
{
    public static class Password
    {
        public static Tuple<byte[], byte[]> CreatePasswordHash(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                return Tuple.Create(hmac.Key, hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != 64) throw new AspNet7Exception("Invalid length of password hash (64 bytes expected).");
            if (storedSalt.Length != 128) throw new AspNet7Exception("Invalid length of password salt (128 bytes expected).");

            using (var hmac = new HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}
