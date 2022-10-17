using System.Security.Cryptography;
using System.Text;
using HashHandler.Shared.Models.Domain;

namespace HashHandler.HashProvider
{
    public class HashProvider : IHashProvider
    {
        public Task<HashesData> GenerateAsync(int hashQuantity, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var hashes = Enumerable
                .Range(0, hashQuantity)
                .Select(_ => GetHash(new Random().Next().ToString()))
                .ToArray();

            var result = new HashesData
            {
                Date = DateTime.UtcNow,
                Hashes = hashes
            };

            return Task.FromResult(result);
        }


        private static string GetHash(string value)
        {
            var sb = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(value));

                foreach (var b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}