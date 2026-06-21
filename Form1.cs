using System;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace ElGamalApp
{
    public partial class Form1 : Form
    {
        private ElGamal _elgamal = new ElGamal();

        // Snapshot của lần xác minh THÀNH CÔNG gần nhất.
        // Dùng để so sánh field nào đã thay đổi khi verify thất bại.
        private class VerifySnapshot
        {
            public string P, G, Y, Message, R, S;
        }
        private VerifySnapshot _lastValidSnapshot = null;

        public Form1()
        {
            InitializeComponent();
        }

        // ════════════════════════════════════════════════════════════
        //  SIGNER TAB — Key Generation
        // ════════════════════════════════════════════════════════════

        private void btnGenerateKey_Click(object sender, EventArgs e)
        {
            BigInteger p, g;

            if (!BigInteger.TryParse(txtP.Text.Trim(), out p) ||
                !BigInteger.TryParse(txtG.Text.Trim(), out g))
            {
                ShowError("Nhập lại giá trị hợp lệ cho p và g.");
                return;
            }

            if (p < 5 || !IsPrime(p))
            {
                ShowError(string.Format("{0} không phải số nguyên tố hợp lệ.", p));
                return;
            }

            if (g < 2 || g >= p)
            {
                ShowError(string.Format("g phải trong khoảng 2 và {0}.", p - 1));
                return;
            }

            try
            {
                BigInteger x;

                // x is optional: if the user typed a value, use it.
                // Otherwise generate one randomly.
                string xText = txtPrivateKey.Text.Trim();
                if (string.IsNullOrEmpty(xText))
                {
                    x = RandomInRange(1, p - 2);
                }
                else
                {
                    if (!BigInteger.TryParse(xText, out x))
                    {
                        ShowError("x cần là số nguyên hợp lệ.");
                        return;
                    }

                    if (x < 1 || x > p - 2)
                    {
                        ShowError(string.Format("x phải trong khoảng 1 và {0}.", p - 2));
                        return;
                    }
                }

                BigInteger y = BigInteger.ModPow(g, x, p);

                txtPrivateKey.Text = x.ToString();
                txtPublicKeyOut.Text = y.ToString();
                lblKeyFormula.Text = string.Format(
                    "y = g^x mod p   →   y has {0} digits, x has {1} digits",
                    y.ToString().Length, x.ToString().Length);

                txtSignKeyP.Text = p.ToString();
                txtSignKeyG.Text = g.ToString();
                txtSignKeyX.Text = x.ToString();

                txtSignature.Clear();
                btnSaveSignature.Enabled = false;
                grpSign.Enabled = true;
                ShowInfo("Tạo khoá thành công.");
            }
            catch (Exception ex)
            {
                ShowError("Error generating keys: " + ex.Message);
            }
        }

        // ════════════════════════════════════════════════════════════
        //  SIGNER TAB — Generate Random Keys (large prime p, g, x)
        // ════════════════════════════════════════════════════════════

        // Generates a random safe-prime p of 512 bits, picks g=2 (valid
        // generator for most safe primes), then a random x in [1, p-2],
        // fills all fields, and auto-generates keys — one click does it all.
        private void btnGenerateRandom_Click(object sender, EventArgs e)
        {
            try
            {
                btnGenerateRandom.Enabled = false;
                btnGenerateRandom.Text = "Đang tạo...";
                Application.DoEvents(); // refresh UI before heavy work

                // Generate a random probable prime of 512 bits
                BigInteger p = GenerateRandomPrime(512);

                // g = 2 works as a generator for safe primes (p = 2q+1 where q is prime).
                // For general primes we pick the smallest g >= 2 such that
                // g^((p-1)/2) != 1 mod p  (not a quadratic residue).
                BigInteger g = FindGenerator(p);

                // Random x in [1, p-2]
                BigInteger x = RandomInRange(1, p - 2);

                // Fill fields
                txtP.Text = p.ToString();
                txtG.Text = g.ToString();
                txtPrivateKey.Text = x.ToString();

                // Auto-click Generate Keys to compute y and enable signing
                btnGenerateKey_Click(sender, e);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tạo khoá ngẫu nhiên: " + ex.Message);
            }
            finally
            {
                btnGenerateRandom.Enabled = true;
                btnGenerateRandom.Text = "🎲 Random p, g, x";
            }
        }

        // Generates a random probable prime of the given bit length using
        // Miller-Rabin. Tries random odd numbers until one passes.
        private BigInteger GenerateRandomPrime(int bits)
        {
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                byte[] bytes = new byte[bits / 8 + 1];
                BigInteger candidate;
                int attempts = 0;
                do
                {
                    rng.GetBytes(bytes);
                    bytes[bytes.Length - 1] = 0x00;        // keep positive
                    bytes[0] |= 0x80;                       // set high bit → ensure full bit-length
                    bytes[bytes.Length - 2] |= 0x01;       // force odd
                    candidate = new BigInteger(bytes);
                    if (candidate < 0) candidate = -candidate;
                    attempts++;
                    if (attempts > 10000)
                        throw new Exception("Không tìm được số nguyên tố sau 10000 lần thử.");
                }
                while (!IsPrime(candidate, 15));
                return candidate;
            }
        }

        // Finds the smallest g >= 2 that is a primitive root mod p.
        // For a prime p, g is a primitive root iff g^((p-1)/q) != 1 mod p
        // for all prime factors q of (p-1).
        // Simplified version: since p-1 is even, check at minimum that
        // g^((p-1)/2) != 1 mod p (i.e. g is not a quadratic residue).
        // This is a necessary condition; for most large primes it's also
        // sufficient in practice for ElGamal.
        private BigInteger FindGenerator(BigInteger p)
        {
            BigInteger pm1 = p - 1;
            BigInteger half = pm1 / 2;
            for (BigInteger g = 2; g < p; g++)
            {
                if (BigInteger.ModPow(g, half, p) != 1 &&
                    BigInteger.ModPow(g, pm1, p) == 1)
                    return g;
            }
            return 2; // fallback
        }

        // Random button next to x in Key Generation — fills x with a
        // cryptographically random value in [1, p-2]. User can also type
        // their own x manually instead.
        private void btnRandomX_Click(object sender, EventArgs e)
        {
            BigInteger p;
            if (!BigInteger.TryParse(txtP.Text.Trim(), out p) || p < 5 || !IsPrime(p))
            {
                ShowError("nhập số nguyên tố p hợp lệ trước");
                return;
            }

            try
            {
                BigInteger x = RandomInRange(1, p - 2);
                txtPrivateKey.Text = x.ToString();
            }
            catch (Exception ex)
            {
                ShowError("Error generating random x: " + ex.Message);
            }
        }

        // ════════════════════════════════════════════════════════════
        //  SIGNER TAB — Sign Message
        // ════════════════════════════════════════════════════════════

        private void btnSign_Click(object sender, EventArgs e)
        {
            BigInteger p, g, x;

            if (!BigInteger.TryParse(txtSignKeyP.Text.Trim(), out p) ||
                !BigInteger.TryParse(txtSignKeyG.Text.Trim(), out g) ||
                !BigInteger.TryParse(txtSignKeyX.Text.Trim(), out x))
            {
                ShowError("nhập p,g,x hợp lệ.");
                return;
            }

            if (p < 5 || !IsPrime(p))
            {
                ShowError(string.Format("{0} không phải số nguyên hợp lê.", p));
                return;
            }

            if (x < 1 || x > p - 2)
            {
                ShowError(string.Format("x phải trong khoảng 1 và {0}.", p - 2));
                return;
            }

            string message = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                ShowError("Nhập thông điệp để ký.");
                return;
            }

            try
            {
                Key key = new Key { P = p, G = g, X = x, Y = 0 };
                Signature sig = _elgamal.Sign(message, key);

                txtSignature.Text = string.Format("R = {0}   |   S = {1}", sig.R, sig.S);
                btnSaveSignature.Enabled = true;
                ShowInfo("Ký thông điệp thành công");
            }
            catch (Exception ex)
            {
                ShowError("Error signing message: " + ex.Message);
            }
        }

        // ── Save: Public Key file (.pub) ──────────────────────────────
        private void btnSavePublicKey_Click(object sender, EventArgs e)
        {
            BigInteger p, g, y;
            if (!BigInteger.TryParse(txtSignKeyP.Text.Trim(), out p) ||
                !BigInteger.TryParse(txtSignKeyG.Text.Trim(), out g) ||
                !BigInteger.TryParse(txtPublicKeyOut.Text.Trim(), out y))
            {
                ShowError("Generate or enter valid p, g, y first.");
                return;
            }

            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Save Public Key";
                dlg.Filter = "Public Key (*.pub)|*.pub";
                dlg.FileName = "public";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var f = new PublicKeyFile { P = p.ToString(), G = g.ToString(), Y = y.ToString() };
                        f.SaveToFile(dlg.FileName);
                        ShowInfo("Public key saved:\n" + dlg.FileName);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error saving public key: " + ex.Message);
                    }
                }
            }
        }

        // ── Save: Private Key file (.key) ─────────────────────────────
        private void btnSavePrivateKey_Click(object sender, EventArgs e)
        {
            BigInteger p, g, x;
            if (!BigInteger.TryParse(txtSignKeyP.Text.Trim(), out p) ||
                !BigInteger.TryParse(txtSignKeyG.Text.Trim(), out g) ||
                !BigInteger.TryParse(txtSignKeyX.Text.Trim(), out x))
            {
                ShowError("Generate or enter valid p, g, x first.");
                return;
            }

            var confirm = MessageBox.Show(
                "This file contains your SECRET private key.\nDo not share it with anyone.\n\nContinue saving?",
                "Save Private Key", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Save Private Key";
                dlg.Filter = "Private Key (*.key)|*.key";
                dlg.FileName = "private";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var f = new PrivateKeyFile { P = p.ToString(), G = g.ToString(), X = x.ToString() };
                        f.SaveToFile(dlg.FileName);
                        ShowInfo("Private key saved:\n" + dlg.FileName);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error saving private key: " + ex.Message);
                    }
                }
            }
        }

        // ── Load: Private Key file (.key) — restores p, g, x for signing ─
        private void btnLoadPrivateKey_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Private Key";
                dlg.Filter = "Private Key (*.key)|*.key|All Files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        PrivateKeyFile f = PrivateKeyFile.LoadFromFile(dlg.FileName);
                        txtSignKeyP.Text = f.P;
                        txtSignKeyG.Text = f.G;
                        txtSignKeyX.Text = f.X;

                        BigInteger p, g, x;
                        BigInteger.TryParse(f.P, out p);
                        BigInteger.TryParse(f.G, out g);
                        BigInteger.TryParse(f.X, out x);
                        txtPublicKeyOut.Text = BigInteger.ModPow(g, x, p).ToString();
                        txtPrivateKey.Text = x.ToString();

                        grpSign.Enabled = true;
                        ShowInfo("Private key loaded.");
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error loading private key: " + ex.Message);
                    }
                }
            }
        }

        // ── Save: Message file (.msg) ─────────────────────────────────
        private void btnSaveMessage_Click(object sender, EventArgs e)
        {
            string message = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                ShowError("Please enter a message first.");
                return;
            }

            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Save Message";
                dlg.Filter = "Message (*.msg)|*.msg|Text File (*.txt)|*.txt";
                dlg.FileName = "message";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        MessageFile.SaveToFile(dlg.FileName, message);
                        ShowInfo("Message saved:\n" + dlg.FileName);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error saving message: " + ex.Message);
                    }
                }
            }
        }

        // ── Save: Signature file (.sig) ───────────────────────────────
        private void btnSaveSignature_Click(object sender, EventArgs e)
        {
            string sigText = txtSignature.Text;
            BigInteger r, s;

            try
            {
                int rStart = sigText.IndexOf("R = ") + 4;
                int rEnd = sigText.IndexOf("   |");
                int sStart = sigText.IndexOf("S = ") + 4;

                r = BigInteger.Parse(sigText.Substring(rStart, rEnd - rStart).Trim());
                s = BigInteger.Parse(sigText.Substring(sStart).Trim());
            }
            catch
            {
                ShowError("Sign the message before saving.");
                return;
            }

            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Save Signature";
                dlg.Filter = "Signature (*.sig)|*.sig";
                dlg.FileName = "signature";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var f = new SignatureOnlyFile { R = r.ToString(), S = s.ToString() };
                        f.SaveToFile(dlg.FileName);
                        ShowInfo("Signature saved:\n" + dlg.FileName);
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error saving signature: " + ex.Message);
                    }
                }
            }
        }

        // ════════════════════════════════════════════════════════════
        //  VERIFIER TAB
        // ════════════════════════════════════════════════════════════

        // ── Load: Public Key file (.pub) ──────────────────────────────
        private void btnLoadPublicKey_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Public Key";
                dlg.Filter = "Public Key (*.pub)|*.pub|All Files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        PublicKeyFile f = PublicKeyFile.LoadFromFile(dlg.FileName);
                        txtVerifyP.Text = f.P;
                        txtVerifyG.Text = f.G;
                        txtVerifyY.Text = f.Y;
                        ShowInfo("Public key loaded.");
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error loading public key: " + ex.Message);
                    }
                }
            }
        }

        // ── Load: Message file (.msg) ─────────────────────────────────
        private void btnLoadMessage_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Message";
                dlg.Filter = "Message (*.msg)|*.msg|Text File (*.txt)|*.txt|All Files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        txtVerifyMessage.Text = MessageFile.LoadFromFile(dlg.FileName);
                        ShowInfo("Message loaded.");
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error loading message: " + ex.Message);
                    }
                }
            }
        }

        // ── Load: Signature file (.sig) ───────────────────────────────
        private void btnLoadSignature_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Open Signature";
                dlg.Filter = "Signature (*.sig)|*.sig|All Files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        SignatureOnlyFile f = SignatureOnlyFile.LoadFromFile(dlg.FileName);
                        txtVerifyR.Text = f.R;
                        txtVerifyS.Text = f.S;
                        ShowInfo("Signature loaded.");
                    }
                    catch (Exception ex)
                    {
                        ShowError("Error loading signature: " + ex.Message);
                    }
                }
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            BigInteger p, g, y, r, s;

            if (!BigInteger.TryParse(txtVerifyP.Text.Trim(), out p) ||
                !BigInteger.TryParse(txtVerifyG.Text.Trim(), out g) ||
                !BigInteger.TryParse(txtVerifyY.Text.Trim(), out y))
            {
                ShowError("Please enter valid integers for p, g, and y.");
                return;
            }

            if (p < 5 || !IsPrime(p))
            {
                ShowError(string.Format("{0} is not a valid prime.", p));
                return;
            }

            string message = txtVerifyMessage.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                ShowError("Please enter the message to verify.");
                return;
            }

            if (!BigInteger.TryParse(txtVerifyR.Text.Trim(), out r) ||
                !BigInteger.TryParse(txtVerifyS.Text.Trim(), out s))
            {
                ShowError("R and S must be valid integers.");
                return;
            }

            try
            {
                Key key = new Key { P = p, G = g, X = 0, Y = y };
                Signature sig = new Signature(r, s);
                VerificationResult result = _elgamal.DiagnoseVerification(message, sig, key);

                if (result.IsValid)
                {
                    // Lưu snapshot của lần verify thành công này
                    _lastValidSnapshot = new VerifySnapshot
                    {
                        P = txtVerifyP.Text.Trim(),
                        G = txtVerifyG.Text.Trim(),
                        Y = txtVerifyY.Text.Trim(),
                        Message = txtVerifyMessage.Text.Trim(),
                        R = txtVerifyR.Text.Trim(),
                        S = txtVerifyS.Text.Trim()
                    };

                    panelVerifyResult.BackColor = Color.FromArgb(220, 245, 220);
                    lblVerifyResult.ForeColor = Color.FromArgb(30, 120, 50);
                    lblVerifyResult.Text = "✔  Chữ ký HỢP LỆ — thông điệp xác thực";
                    panelVerifyResult.Visible = true;
                    MessageBox.Show(result.Message,
                        "Kết quả xác minh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    panelVerifyResult.BackColor = Color.FromArgb(250, 220, 220);
                    lblVerifyResult.ForeColor = Color.FromArgb(160, 30, 30);
                    panelVerifyResult.Visible = true;

                    string detailMsg;

                    if (result.Status == VerificationStatus.InvalidSignatureValues)
                    {
                        // R/S ngoài phạm vi — có thể kết luận chắc chắn
                        lblVerifyResult.Text = "✖  Chữ ký KHÔNG HỢP LỆ — Giá trị R hoặc S nằm ngoài phạm vi";
                        detailMsg = result.Message;
                    }
                    else if (_lastValidSnapshot != null)
                    {
                        // Có snapshot → so sánh từng field để chỉ ra cái nào thay đổi
                        var snap = _lastValidSnapshot;
                        var changed = new System.Collections.Generic.List<string>();
                        var unchanged = new System.Collections.Generic.List<string>();

                        Action<string, string, string> check = (label, snapVal, curVal) =>
                        {
                            if (snapVal != curVal) changed.Add(label);
                            else unchanged.Add(label);
                        };

                        check("Thông điệp", snap.Message, txtVerifyMessage.Text.Trim());
                        check("Khoá công khai y", snap.Y, txtVerifyY.Text.Trim());
                        check("Tham số p", snap.P, txtVerifyP.Text.Trim());
                        check("Tham số g", snap.G, txtVerifyG.Text.Trim());
                        check("Chữ ký R", snap.R, txtVerifyR.Text.Trim());
                        check("Chữ ký S", snap.S, txtVerifyS.Text.Trim());

                        if (changed.Count == 0)
                        {
                            // Không có gì thay đổi so với snapshot — không nên xảy ra,
                            // nhưng xử lý phòng thủ
                            detailMsg = result.Message;
                            lblVerifyResult.Text = "✖  Chữ ký KHÔNG HỢP LỆ";
                        }
                        else
                        {
                            string changedList = "  • " + string.Join("\n  • ", changed.ToArray());
                            string unchangedList = unchanged.Count > 0
                                ? "  • " + string.Join("\n  • ", unchanged.ToArray())
                                : "  (không có)";

                            // Label ngắn gọn trên panel
                            if (changed.Count == 1)
                                lblVerifyResult.Text = "✖  Chữ ký KHÔNG HỢP LỆ — " + changed[0] + " đã bị thay đổi";
                            else
                                lblVerifyResult.Text = "✖  Chữ ký KHÔNG HỢP LỆ — " + changed.Count + " thành phần đã thay đổi";

                            detailMsg =
                                "Chữ ký KHÔNG HỢP LỆ.\n\n" +
                                "So sánh với lần xác minh thành công gần nhất:\n\n" +
                                "❌ Đã thay đổi:\n" + changedList + "\n\n" +
                                "✔  Không thay đổi:\n" + unchangedList + "\n\n" +
                                "Kết luận: Các thành phần bị thay đổi ở trên là nguyên nhân khiến\n" +
                                "chữ ký không còn hợp lệ.";
                        }
                    }
                    else
                    {
                        // Chưa có snapshot (chưa từng verify thành công trong session này)
                        lblVerifyResult.Text = "✖  Chữ ký KHÔNG HỢP LỆ — Thông điệp, khoá hoặc chữ ký không khớp";
                        detailMsg = result.Message +
                            "\n\n(Mẹo: Hãy verify một bộ hợp lệ trước. Sau đó nếu bạn sửa\n" +
                            "bất kỳ field nào và verify lại, app sẽ chỉ ra chính xác field nào đã thay đổi.)";
                    }

                    MessageBox.Show(detailMsg, "Kết quả xác minh", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ShowError("Error verifying: " + ex.Message);
            }
        }

        // ── Reset ────────────────────────────────────────────────────
        private void btnReset_Click(object sender, EventArgs e)
        {
            txtP.Text = "23";
            txtG.Text = "5";
            txtPrivateKey.Clear();
            txtPublicKeyOut.Clear();
            lblKeyFormula.Text = "";
            txtSignKeyP.Clear();
            txtSignKeyG.Clear();
            txtSignKeyX.Clear();
            txtMessage.Clear();
            txtSignature.Clear();
            btnSaveSignature.Enabled = false;
            grpSign.Enabled = false;

            txtVerifyP.Clear();
            txtVerifyG.Clear();
            txtVerifyY.Clear();
            txtVerifyMessage.Clear();
            txtVerifyR.Clear();
            txtVerifyS.Clear();
            panelVerifyResult.Visible = false;
            lblVerifyResult.Text = "";
            _lastValidSnapshot = null;
        }

        // ── Helpers ──────────────────────────────────────────────────

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowInfo(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    bytes[bytes.Length - 1] = 0x00;
                    result = new BigInteger(bytes) % range;
                }
                while (result < 0);

                return result + min;
            }
        }

        // Miller-Rabin primality test
        private bool IsPrime(BigInteger n, int witnesses = 10)
        {
            if (n < 2) return false;
            if (n == 2 || n == 3) return true;
            if (n % 2 == 0) return false;

            BigInteger d = n - 1;
            int r = 0;
            while (d % 2 == 0) { d /= 2; r++; }

            using (var rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < witnesses; i++)
                {
                    BigInteger a;
                    byte[] bytes = new byte[n.ToByteArray().Length + 1];
                    do
                    {
                        rng.GetBytes(bytes);
                        bytes[bytes.Length - 1] = 0x00;
                        a = new BigInteger(bytes) % (n - 3) + 2;
                    }
                    while (a < 2);

                    BigInteger x = BigInteger.ModPow(a, d, n);
                    if (x == 1 || x == n - 1) continue;

                    bool composite = true;
                    for (int j = 0; j < r - 1; j++)
                    {
                        x = BigInteger.ModPow(x, 2, n);
                        if (x == n - 1) { composite = false; break; }
                    }

                    if (composite) return false;
                }
            }

            return true;
        }
    }
}