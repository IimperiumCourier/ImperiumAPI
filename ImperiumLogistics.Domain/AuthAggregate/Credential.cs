using ImperiumLogistics.SharedKernel;
using ImperiumLogistics.SharedKernel.DDDSharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ImperiumLogistics.Domain.AuthAggregate
{
    public class Credential : ValueObject<Credential>
    {
        public string PasswordHash { get; private set; } = string.Empty;
        public int LoginAttempt { get; private set; }
        public DateTime LastDateChanged { get; private set; }

        internal static Credential Add(string password)
        {
            using SHA256 sha256Hash = SHA256.Create();

            return new Credential { PasswordHash = GetHash(sha256Hash, password), LoginAttempt = 0 };
        }

        internal void ChangePassword(string password)
        {
            using SHA256 sha256Hash = SHA256.Create();

            PasswordHash = GetHash(sha256Hash, password);
            LastDateChanged = Utility.GetNigerianTime();
        }

        public bool ValidatePassword(string password)
        {
            using SHA256 sha256Hash = SHA256.Create();

            return VerifyHash(sha256Hash, password, PasswordHash);
        }

        private static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {
            string _input = input.Trim();
            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(_input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        // Verify a hash against a string.
        private bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
        {
            // Hash the input.
            var hashOfInput = GetHash(hashAlgorithm, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }
    }
}

