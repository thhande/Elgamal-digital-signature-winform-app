using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;
namespace ElGamalApp
{

    class Key
    {
        public BigInteger P { get; set; }   // prime modulus
        public BigInteger G { get; set; }   // generator
        public BigInteger X { get; set; }   // private key (secret)
        public BigInteger Y { get; set; }   // public key: Y = G^X mod P
    }

    class Signature
    {
        public BigInteger R { get; private set; }
        public BigInteger S { get; private set; }

        public Signature(BigInteger r, BigInteger s)
        {
            this.R = r;
            this.S = s;
        }

        public override string ToString()
        {
            return string.Format("(R={0}, S={1})", R, S);
        }
    }

    class ElGamal
    {
        // ── Key Generation ───────────────────────────────────────────────
        public Key GenerateKey(BigInteger p, BigInteger g)
        {
            // x must be in [1, p-2]
            BigInteger x = RandomInRange(1, p - 2);

            // y = g^x mod p
            BigInteger y = BigInteger.ModPow(g, x, p);

            return new Key { P = p, G = g, X = x, Y = y };
        }

        // ── Signing ──────────────────────────────────────────────────────
        public Signature Sign(string message, Key key)
        {
            BigInteger p = key.P;
            BigInteger g = key.G;
            BigInteger x = key.X;
            BigInteger h = HashMessage(message, p);

            BigInteger r, s, k;

            int attempts = 0;
            do
            {
                // k must be random in [1, p-2] and coprime with (p-1)
                do
                {
                    k = RandomInRange(1, p - 2);
                }
                while (BigInteger.GreatestCommonDivisor(k, p - 1) != 1);

                // r = g^k mod p
                r = BigInteger.ModPow(g, k, p);

                // s = k^-1 * (h - x*r) mod (p-1)
                BigInteger kInv = ModInverse(k, p - 1);
                BigInteger numerator = ((h - x * r) % (p - 1) + (p - 1)) % (p - 1);
                s = (kInv * numerator) % (p - 1);

                attempts++;
            }
            while (s == 0 && attempts < 200);

            return new Signature(r, s);
        }

        // ── Verification ─────────────────────────────────────────────────
        // Valid if: g^h ≡ y^r * r^s (mod p)
        public bool Verify(string message, Signature signature, Key key)
        {
            BigInteger p = key.P;
            BigInteger g = key.G;
            BigInteger y = key.Y;
            BigInteger r = signature.R;
            BigInteger s = signature.S;

            if (r <= 0 || r >= p) return false;

            BigInteger h = HashMessage(message, p);

            BigInteger lhs = BigInteger.ModPow(g, h, p);
            BigInteger rhs = BigInteger.ModPow(y, r, p) * BigInteger.ModPow(r, s, p) % p;

            return lhs == rhs;
        }

        // ── Helpers ──────────────────────────────────────────────────────

        // Maps a message string to a BigInteger hash in [1, p-1]
        private BigInteger HashMessage(string message, BigInteger p)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(message));

                // Convert hash bytes to positive BigInteger
                // Append 0x00 to ensure it's treated as positive
                byte[] positiveBytes = new byte[bytes.Length + 1];
                Array.Copy(bytes, positiveBytes, bytes.Length);
                positiveBytes[bytes.Length] = 0x00;

                BigInteger hash = new BigInteger(positiveBytes);
                return (hash % (p - 1)) + 1;   // keep in [1, p-1]
            }
        }

        // Extended Euclidean — modular inverse of a mod m
        private BigInteger ModInverse(BigInteger a, BigInteger m)
        {
            BigInteger m0 = m;
            BigInteger x0 = 0;
            BigInteger x1 = 1;

            if (m == 1) return 0;

            while (a > 1)
            {
                BigInteger q = a / m;
                BigInteger tempM = m;
                BigInteger tempX0 = x0;

                m = a % m;
                a = tempM;
                x0 = x1 - q * x0;
                x1 = tempX0;
            }

            return x1 < 0 ? x1 + m0 : x1;
        }

        // Cryptographically secure random BigInteger in [min, max]
        private BigInteger RandomInRange(BigInteger min, BigInteger max)
        {
            if (min > max) throw new ArgumentException("min must be <= max");

            BigInteger range = max - min + 1;
            int byteCount = range.ToByteArray().Length;

            using (var rng = new RNGCryptoServiceProvider())
            {
                BigInteger result;
                byte[] bytes = new byte[byteCount + 1];
                do
                {
                    rng.GetBytes(bytes);
                    bytes[bytes.Length - 1] = 0x00; // ensure positive
                    result = new BigInteger(bytes) % range;
                }
                while (result < 0);

                return result + min;
            }
        }
    }




}

