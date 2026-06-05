using System;
using System.Drawing;
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

        // ── Generate Keys ────────────────────────────────────────────
        private void btnGenerateKey_Click(object sender, EventArgs e)
        {
            int p, g;

            if (!int.TryParse(txtP.Text, out p) || !int.TryParse(txtG.Text, out g))
            {
                ShowStatus("Please enter valid integers for p and g.", false);
                return;
            }

            if (p < 5 || !IsPrime(p))
            {
                ShowStatus(string.Format("{0} is not a valid prime. Try: 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47.", p), false);
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

                lblKeyFormula.Text = string.Format(
                    "y = g^x mod p  →  {0}^{1} mod {2} = {3}",
                    g, key.X, p, key.Y
                );

                // store full key for signing
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

        // ── Sign ─────────────────────────────────────────────────────
        private void btnSign_Click(object sender, EventArgs e)
        {
            int p, g, x;

            if (!int.TryParse(txtSignKeyP.Text, out p) ||
                !int.TryParse(txtSignKeyG.Text, out g) ||
                !int.TryParse(txtSignKeyX.Text, out x))
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
                ShowStatus(string.Format("Signed. Send the verifier: message, R={0}, S={1}, p={2}, g={3}, y={4}",
                    sig.R, sig.S, p, g, txtPublicKeyOut.Text), true);
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
            int p, g, y, r, s;

            if (!int.TryParse(txtVerifyP.Text, out p) ||
                !int.TryParse(txtVerifyG.Text, out g) ||
                !int.TryParse(txtVerifyY.Text, out y))
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

            if (!int.TryParse(txtVerifyR.Text, out r) || !int.TryParse(txtVerifyS.Text, out s))
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
            // signer tab
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

            // verifier tab
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

        private bool IsPrime(int n)
        {
            if (n < 2) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;
            for (int i = 3; i * i <= n; i += 2)
                if (n % i == 0) return false;
            return true;
        }
    }
}