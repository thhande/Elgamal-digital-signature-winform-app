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

    enum VerificationStatus
    {
        Valid,
        InvalidSignatureValues,   // R hoặc S nằm ngoài phạm vi hợp lệ (có thể xác định chắc chắn)
        VerificationFailed        // Phương trình thất bại — không thể biết thành phần nào bị sửa
    }

    class VerificationResult
    {
        public bool IsValid { get; set; }
        public VerificationStatus Status { get; set; }
        public string Message { get; set; }
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

        // ── Detailed Verification ────────────────────────────────────────
        // ElGamal verification checks: g^h ≡ y^R · R^S (mod p)
        //
        // IMPORTANT: It is mathematically IMPOSSIBLE to determine from the
        // equation alone whether the message, the key (y), or the signature
        // (R/S) was tampered with — all three appear in one equation and any
        // one of them being wrong produces the same result (LHS ≠ RHS).
        //
        // What we CAN detect reliably:
        //   1. R is structurally out of range (R ≤ 0 or R ≥ p) — this is a
        //      hard mathematical constraint, not a guess.
        //   2. S is structurally out of range (S ≤ 0 or S ≥ p-1) — same.
        //   3. The full equation fails — meaning at least one of {message,
        //      key, signature} does not match what was used when signing.
        //
        // We do NOT attempt to guess which component was tampered with from
        // the equation result alone, as that would be unreliable.
        public VerificationResult DiagnoseVerification(string message, Signature signature, Key key)
        {
            BigInteger p = key.P;
            BigInteger g = key.G;
            BigInteger y = key.Y;
            BigInteger r = signature.R;
            BigInteger s = signature.S;

            // ── Step 1: Hard structural checks on R and S ─────────────────
            // These are cheap and definitive — values outside range can NEVER
            // have been produced by a valid signing operation.
            bool rOutOfRange = (r <= 0 || r >= p);
            bool sOutOfRange = (s <= 0 || s >= p - 1);

            if (rOutOfRange || sOutOfRange)
            {
                string detail = "";
                if (rOutOfRange && sOutOfRange)
                    detail = "Cả R lẫn S đều nằm ngoài phạm vi hợp lệ.";
                else if (rOutOfRange)
                    detail = string.Format("R = {0} nằm ngoài phạm vi hợp lệ (cần: 0 < R < p).", r);
                else
                    detail = string.Format("S = {0} nằm ngoài phạm vi hợp lệ (cần: 0 < S < p-1).", s);

                return new VerificationResult
                {
                    IsValid = false,
                    Status = VerificationStatus.InvalidSignatureValues,
                    Message =
                        "Chữ ký KHÔNG HỢP LỆ — Cấu trúc chữ ký sai.\n\n" +
                        detail + "\n\n" +
                        "Một chữ ký hợp lệ luôn phải có:\n" +
                        "  • 0 < R < p\n" +
                        "  • 0 < S < p-1\n\n" +
                        "Kết luận: Giá trị R hoặc S chắc chắn đã bị sửa đổi hoặc nhập sai."
                };
            }

            // ── Step 2: Verify the equation ───────────────────────────────
            BigInteger h = HashMessage(message, p);
            BigInteger lhs = BigInteger.ModPow(g, h, p);
            BigInteger rhs = BigInteger.ModPow(y, r, p) * BigInteger.ModPow(r, s, p) % p;

            if (lhs == rhs)
            {
                return new VerificationResult
                {
                    IsValid = true,
                    Status = VerificationStatus.Valid,
                    Message = "Chữ ký HỢP LỆ — thông điệp xác thực và chưa bị sửa đổi."
                };
            }

            // ── Step 3: Equation failed — report honestly ─────────────────
            // We cannot determine which component was tampered with.
            // All three (message, public key y, signature R/S) feed into one
            // equation. Any one of them being wrong gives the same failure.
            return new VerificationResult
            {
                IsValid = false,
                Status = VerificationStatus.VerificationFailed,
                Message =
                    "Chữ ký KHÔNG HỢP LỆ.\n\n" +
                    "Phương trình xác minh thất bại:\n" +
                    "  g^h mod p        = " + lhs + "\n" +
                    "  y^R · R^S mod p  = " + rhs + "\n\n" +
                    "Có thể do một trong các nguyên nhân sau:\n" +
                    "  • Thông điệp đã bị thay đổi sau khi ký\n" +
                    "  • Khoá công khai (p, g, y) không khớp với khoá đã dùng để ký\n" +
                    "  • Giá trị chữ ký R hoặc S đã bị sửa đổi\n\n" +
                    "Về mặt toán học, không thể xác định chính xác thành phần nào\n" +
                    "bị sửa chỉ từ kết quả phương trình này."
            };
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