using System.Text;

namespace Rehydrated.Bnet.Utils
{
    public static class HashUtility
    {
        /// <summary>
        /// Hashes the provided input <see cref="string"/> using the 32-bit variant of the FNV-1a algorithm.
        /// </summary>
        public static uint Fnv1a(string input)
        {
            const uint FnvOffsetBasis = 0x811C9DC5;
            const uint FnvPrime = 0x1000193;

            uint hash = FnvOffsetBasis;

            int numBytes = Encoding.UTF8.GetByteCount(input);
            Span<byte> bytes = stackalloc byte[numBytes];
            Encoding.UTF8.GetBytes(input, bytes);

            foreach (byte b in bytes)
                hash = (hash ^ b) * FnvPrime;

            return hash;
        }
    }
}
