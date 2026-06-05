using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ElGamalApp
{

    class Key
    {
        public int P { get; set; }
        public int G { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Signature
    {
        public int R { get; private set; }
        public int S { get; private set; }

        public Signature(int r, int s)
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
        private Random _rng = new Random();

        public Key GenerateKey(int p, int g)
        {
            int x = _rng.Next(1, p - 1);
            int y = ModPow(g, x, p);

            return new Key { P = p, G = g, X = x, Y = y };
        }

        public Signature Sign(string message, Key key)
        {
            int p = key.P;
            int g = key.G;
            int x = key.X;
            int h = HashMessage(message, p);

            int r, s, k;

            do
            {
                do { k = _rng.Next(1, p - 1); }
                while (GCD(k, p - 1) != 1);

                r = ModPow(g, k, p);

                long kInv = ModInverse(k, p - 1);
                long sLong = (kInv * ((long)(h - (long)x * r % (p - 1)) % (p - 1) + (p - 1))) % (p - 1);
                s = (int)sLong;

            } while (s == 0);

            return new Signature(r, s);
        }

        public bool Verify(string message, Signature signature, Key key)
        {
            int p = key.P;
            int g = key.G;
            int y = key.Y;
            int r = signature.R;
            int s = signature.S;

            if (r <= 0 || r >= p) return false;

            int h = HashMessage(message, p);

            long lhs = ModPow(g, h, p);
            long rhs = (long)ModPow(y, r, p) * ModPow(r, s, p) % p;

            return lhs == rhs;
        }

        private int HashMessage(string message, int p)
        {
            using (var sha = SHA256.Create())
            {
                byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(message));
                long hash = Math.Abs(BitConverter.ToInt32(bytes, 0));
                return (int)(hash % (p - 1)) + 1;
            }
        }

        private int ModPow(long b, long exp, long mod)
        {
            long result = 1;
            b %= mod;
            while (exp > 0)
            {
                if ((exp & 1) == 1) result = result * b % mod;
                exp >>= 1;
                b = b * b % mod;
            }
            return (int)result;
        }

        private long ModInverse(long a, long m)
        {
            long m0 = m;
            long x0 = 0;
            long x1 = 1;

            if (m == 1) return 0;

            while (a > 1)
            {
                long q = a / m;
                long tempM = m;
                long tempX0 = x0;

                m = a % m;
                a = tempM;
                x0 = x1 - q * x0;
                x1 = tempX0;
            }

            return x1 < 0 ? x1 + m0 : x1;
        }

        private int GCD(int a, int b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }
    }




}

