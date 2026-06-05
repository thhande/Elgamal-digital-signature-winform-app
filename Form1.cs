using System;
using System.Drawing;
using System.Windows.Forms;

namespace ElGamalApp
{
    public partial class Form1 : Form
    {
        private ElGamal _elgamal = new ElGamal();
        private Key _currentKey = null;
        private Signature _currentSignature = null;

        public Form1()
        {
            InitializeComponent();
        }

        // ── Step 1: Generate Keys ────────────────────────────────────────
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
                ShowStatus("g must be between 2 and p-1.", false);
                return;
            }

            try
            {
                _currentKey = _elgamal.GenerateKey(p, g);
                _currentSignature = null;

                txtPrivateKey.Text = _currentKey.X.ToString();
                txtPublicKey.Text = _currentKey.Y.ToString();

                lblKeyFormula.Text = string.Format(
                    "y = g^x mod p  →  {0}^{1} mod {2} = {3}",
                    g, _currentKey.X, p, _currentKey.Y
                );

                // reset downstream panels
                txtSignature.Clear();
                txtVerifyMessage.Clear();
                txtVerifyR.Clear();
                txtVerifyS.Clear();
                ClearVerifyResult();

                ShowStatus("Keys generated successfully.", true);
                grpSign.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowStatus("Error generating keys: " + ex.Message, false);
            }
        }

        // ── Step 2: Sign ────────────────────────────────────────────────
        private void btnSign_Click(object sender, EventArgs e)
        {
            if (_currentKey == null)
            {
                ShowStatus("Generate keys first.", false);
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
                _currentSignature = _elgamal.Sign(message, _currentKey);

                txtSignature.Text = string.Format("R = {0}   |   S = {1}",
                    _currentSignature.R, _currentSignature.S);

                // auto-fill verify panel
                txtVerifyMessage.Text = message;
                txtVerifyR.Text = _currentSignature.R.ToString();
                txtVerifyS.Text = _currentSignature.S.ToString();

                ClearVerifyResult();
                ShowStatus("Message signed successfully.", true);
                grpVerify.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowStatus("Error signing message: " + ex.Message, false);
            }
        }

        // ── Step 3: Verify ──────────────────────────────────────────────
        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (_currentKey == null)
            {
                ShowStatus("Generate keys first.", false);
                return;
            }

            string message = txtVerifyMessage.Text.Trim();
            int r, s;

            if (string.IsNullOrEmpty(message))
            {
                ShowStatus("Please enter a message to verify.", false);
                return;
            }

            if (!int.TryParse(txtVerifyR.Text, out r) || !int.TryParse(txtVerifyS.Text, out s))
            {
                ShowStatus("R and S must be valid integers.", false);
                return;
            }

            try
            {
                Signature sig = new Signature(r, s);
                bool valid = _elgamal.Verify(message, sig, _currentKey);

                if (valid)
                {
                    panelVerifyResult.BackColor = Color.FromArgb(220, 245, 220);
                    lblVerifyResult.ForeColor = Color.FromArgb(30, 120, 50);
                    lblVerifyResult.Text = "✔  Signature VALID — message is authentic";
                    ShowStatus("Verification complete: valid.", true);
                }
                else
                {
                    panelVerifyResult.BackColor = Color.FromArgb(250, 220, 220);
                    lblVerifyResult.ForeColor = Color.FromArgb(160, 30, 30);
                    lblVerifyResult.Text = "✖  Signature INVALID — message may be tampered";
                    ShowStatus("Verification complete: invalid.", false);
                }

                panelVerifyResult.Visible = true;
            }
            catch (Exception ex)
            {
                ShowStatus("Error verifying: " + ex.Message, false);
            }
        }

        // ── Reset ────────────────────────────────────────────────────────
        private void btnReset_Click(object sender, EventArgs e)
        {
            _currentKey = null;
            _currentSignature = null;

            txtP.Text = "23";
            txtG.Text = "5";
            txtPrivateKey.Clear();
            txtPublicKey.Clear();
            lblKeyFormula.Text = "";
            txtMessage.Clear();
            txtSignature.Clear();
            txtVerifyMessage.Clear();
            txtVerifyR.Clear();
            txtVerifyS.Clear();
            ClearVerifyResult();

            grpSign.Enabled = false;
            grpVerify.Enabled = false;
            ShowStatus("Reset complete.", true);
        }

        // ── Helpers ──────────────────────────────────────────────────────
        private void ShowStatus(string message, bool success)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = success
                ? Color.FromArgb(30, 120, 50)
                : Color.FromArgb(160, 30, 30);
        }

        // Trial-division primality check — sufficient for small int values
        private bool IsPrime(int n)
        {
            if (n < 2) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;
            for (int i = 3; i * i <= n; i += 2)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        private void ClearVerifyResult()
        {
            panelVerifyResult.Visible = false;
            lblVerifyResult.Text = "";
        }
    }
}