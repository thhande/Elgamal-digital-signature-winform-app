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

        public Form1()
        {
            InitializeComponent();
        }

        // ════════════════════════════════════════════════════════════
        //  SIGNER TAB
        // ════════════════════════════════════════════════════════════

        private void btnGenerateKey_Click(object sender, EventArgs e)
        {
            BigInteger p, g;

            if (!BigInteger.TryParse(txtP.Text.Trim(), out p) ||
                !BigInteger.TryParse(txtG.Text.Trim(), out g))
            {
                ShowStatus("Please enter valid integers for p and g.", false);
                return;
            }

            if (p < 5 || !IsPrime(p))
            {
                ShowStatus(string.Format("{0} is not a valid prime.", p), false);
                return;
            }

            if (g < 2 || g >= p)
            {
                ShowStatus(string.Format("g must be between 2 and {0}.", p - 1), false);
                return;
            }

            try
            {
                Key key = _elgamal.GenerateKey(p, g);

                txtPrivateKey.Text = key.X.ToString();
                txtPublicKeyOut.Text = key.Y.ToString();

                // show shortened formula if numbers are large
                string px = key.X.ToString();
                string shortX = px.Length > 20 ? px.Substring(0, 20) + "..." : px;
                lblKeyFormula.Text = string.Format("y = g^x mod p  →  computed ({0}-digit result)", key.Y.ToString().Length);

                txtSignKeyP.Text = p.ToString();
                txtSignKeyG.Text = g.ToString();
                txtSignKeyX.Text = key.X.ToString();

                txtSignature.Clear();
                grpSign.Enabled = true;
                ShowStatus("Keys generated. Share p, g, y with the verifier — keep x secret.", true);
            }
            catch (Exception ex)
            {
                ShowStatus("Error: " + ex.Message, false);
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            BigInteger p, g, x;

            if (!BigInteger.TryParse(txtSignKeyP.Text.Trim(), out p) ||
                !BigInteger.TryParse(txtSignKeyG.Text.Trim(), out g) ||
                !BigInteger.TryParse(txtSignKeyX.Text.Trim(), out x))
            {
                ShowStatus("Please enter valid integers for p, g, and x.", false);
                return;
            }

            if (p < 5 || !IsPrime(p))
            {
                ShowStatus(string.Format("{0} is not a valid prime.", p), false);
                return;
            }

            string message = txtMessage.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                ShowStatus("Please enter a message to sign.", false);
                return;
            }

            try
            {
                Key key = new Key { P = p, G = g, X = x, Y = 0 };
                Signature sig = _elgamal.Sign(message, key);

                txtSignature.Text = string.Format("R = {0}   |   S = {1}", sig.R, sig.S);
                ShowStatus("Message signed. Send the verifier: message, R, S, p, g, y.", true);
            }
            catch (Exception ex)
            {
                ShowStatus("Error signing: " + ex.Message, false);
            }
        }

        // ════════════════════════════════════════════════════════════
        //  VERIFIER TAB
        // ════════════════════════════════════════════════════════════

        private void btnVerify_Click(object sender, EventArgs e)
        {
            BigInteger p, g, y, r, s;

            if (!BigInteger.TryParse(txtVerifyP.Text.Trim(), out p) ||
                !BigInteger.TryParse(txtVerifyG.Text.Trim(), out g) ||
                !BigInteger.TryParse(txtVerifyY.Text.Trim(), out y))
            {
                ShowStatus("Please enter valid integers for p, g, and y.", false);
                return;
            }

            if (p < 5 || !IsPrime(p))
            {
                ShowStatus(string.Format("{0} is not a valid prime.", p), false);
                return;
            }

            string message = txtVerifyMessage.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                ShowStatus("Please enter the message to verify.", false);
                return;
            }

            if (!BigInteger.TryParse(txtVerifyR.Text.Trim(), out r) ||
                !BigInteger.TryParse(txtVerifyS.Text.Trim(), out s))
            {
                ShowStatus("R and S must be valid integers.", false);
                return;
            }

            try
            {
                Key key = new Key { P = p, G = g, X = 0, Y = y };
                Signature sig = new Signature(r, s);
                bool valid = _elgamal.Verify(message, sig, key);

                if (valid)
                {
                    panelVerifyResult.BackColor = Color.FromArgb(220, 245, 220);
                    lblVerifyResult.ForeColor = Color.FromArgb(30, 120, 50);
                    lblVerifyResult.Text = "✔  Signature VALID — message is authentic";
                    ShowStatus("Verification passed.", true);
                }
                else
                {
                    panelVerifyResult.BackColor = Color.FromArgb(250, 220, 220);
                    lblVerifyResult.ForeColor = Color.FromArgb(160, 30, 30);
                    lblVerifyResult.Text = "✖  Signature INVALID — message may be tampered";
                    ShowStatus("Verification failed.", false);
                }

                panelVerifyResult.Visible = true;
            }
            catch (Exception ex)
            {
                ShowStatus("Error verifying: " + ex.Message, false);
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
            grpSign.Enabled = false;

            txtVerifyP.Clear();
            txtVerifyG.Clear();
            txtVerifyY.Clear();
            txtVerifyMessage.Clear();
            txtVerifyR.Clear();
            txtVerifyS.Clear();
            panelVerifyResult.Visible = false;
            lblVerifyResult.Text = "";

            ShowStatus("Reset complete.", true);
        }

        // ── Helpers ──────────────────────────────────────────────────

        private void ShowStatus(string message, bool success)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = success
                ? Color.FromArgb(30, 120, 50)
                : Color.FromArgb(160, 30, 30);
        }

        // Miller-Rabin primality test — works correctly for large BigInteger primes
        private bool IsPrime(BigInteger n, int witnesses = 10)
        {
            if (n < 2) return false;
            if (n == 2 || n == 3) return true;
            if (n % 2 == 0) return false;

            // write n-1 as 2^r * d
            BigInteger d = n - 1;
            int r = 0;
            while (d % 2 == 0) { d /= 2; r++; }

            using (var rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < witnesses; i++)
                {
                    // pick random a in [2, n-2]
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