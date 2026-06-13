namespace ElGamalApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSigner = new System.Windows.Forms.TabPage();
            this.tabVerifier = new System.Windows.Forms.TabPage();

            // Signer tab controls
            this.grpKey = new System.Windows.Forms.GroupBox();
            this.lblP = new System.Windows.Forms.Label();
            this.txtP = new System.Windows.Forms.TextBox();
            this.lblG = new System.Windows.Forms.Label();
            this.txtG = new System.Windows.Forms.TextBox();
            this.btnGenerateKey = new System.Windows.Forms.Button();
            this.lblPrivateKey = new System.Windows.Forms.Label();
            this.txtPrivateKey = new System.Windows.Forms.TextBox();
            this.lblPublicKeyOut = new System.Windows.Forms.Label();
            this.txtPublicKeyOut = new System.Windows.Forms.TextBox();
            this.lblKeyFormula = new System.Windows.Forms.Label();
            this.lblShareHint = new System.Windows.Forms.Label();

            this.grpSign = new System.Windows.Forms.GroupBox();
            this.lblSignKeyP = new System.Windows.Forms.Label();
            this.txtSignKeyP = new System.Windows.Forms.TextBox();
            this.lblSignKeyG = new System.Windows.Forms.Label();
            this.txtSignKeyG = new System.Windows.Forms.TextBox();
            this.lblSignKeyX = new System.Windows.Forms.Label();
            this.txtSignKeyX = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSign = new System.Windows.Forms.Button();
            this.lblSignature = new System.Windows.Forms.Label();
            this.txtSignature = new System.Windows.Forms.TextBox();
            this.btnSaveSignature = new System.Windows.Forms.Button();

            // Verifier tab controls
            this.grpVerify = new System.Windows.Forms.GroupBox();
            this.btnLoadSignature = new System.Windows.Forms.Button();
            this.lblVerifyP = new System.Windows.Forms.Label();
            this.txtVerifyP = new System.Windows.Forms.TextBox();
            this.lblVerifyG = new System.Windows.Forms.Label();
            this.txtVerifyG = new System.Windows.Forms.TextBox();
            this.lblVerifyY = new System.Windows.Forms.Label();
            this.txtVerifyY = new System.Windows.Forms.TextBox();
            this.lblVerifyMessage = new System.Windows.Forms.Label();
            this.txtVerifyMessage = new System.Windows.Forms.TextBox();
            this.lblVerifyR = new System.Windows.Forms.Label();
            this.txtVerifyR = new System.Windows.Forms.TextBox();
            this.lblVerifyS = new System.Windows.Forms.Label();
            this.txtVerifyS = new System.Windows.Forms.TextBox();
            this.btnVerify = new System.Windows.Forms.Button();
            this.panelVerifyResult = new System.Windows.Forms.Panel();
            this.lblVerifyResult = new System.Windows.Forms.Label();

            // Shared
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();

            // ════════════════════════════════════════════════════════
            //  SIGNER TAB — grpKey
            // ════════════════════════════════════════════════════════
            grpKey.SuspendLayout();
            grpKey.Text = "Key Generation";
            grpKey.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            grpKey.Location = new System.Drawing.Point(10, 10);
            grpKey.Size = new System.Drawing.Size(580, 185);

            SetLabel(lblP, "Prime  p", 14, 28);
            SetTextBox(txtP, "23", 108, 25, 100);

            SetLabel(lblG, "Generator  g", 230, 28);
            SetTextBox(txtG, "5", 340, 25, 100);

            btnGenerateKey.Text = "Generate Keys";
            btnGenerateKey.Location = new System.Drawing.Point(458, 22);
            btnGenerateKey.Size = new System.Drawing.Size(108, 32);
            StyleBtn(btnGenerateKey, System.Drawing.Color.FromArgb(0, 120, 215));
            btnGenerateKey.Click += new System.EventHandler(this.btnGenerateKey_Click);

            SetLabel(lblPrivateKey, "Private key  x  (secret)", 14, 72);
            SetReadonlyBox(txtPrivateKey, 185, 69, 175);

            SetLabel(lblPublicKeyOut, "Public key  y  (share)", 378, 72);
            SetReadonlyBox(txtPublicKeyOut, 510, 69, 56);

            lblKeyFormula.Location = new System.Drawing.Point(14, 112);
            lblKeyFormula.Size = new System.Drawing.Size(550, 20);
            lblKeyFormula.Font = new System.Drawing.Font("Consolas", 8.5f);
            lblKeyFormula.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);

            lblShareHint.Location = new System.Drawing.Point(14, 140);
            lblShareHint.Size = new System.Drawing.Size(550, 20);
            lblShareHint.Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Italic);
            lblShareHint.ForeColor = System.Drawing.Color.FromArgb(0, 100, 180);
            lblShareHint.Text = "→ Share  p, g, y  with the verifier.  Keep  x  secret.";

            grpKey.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblP, txtP, lblG, txtG, btnGenerateKey,
                lblPrivateKey, txtPrivateKey,
                lblPublicKeyOut, txtPublicKeyOut,
                lblKeyFormula, lblShareHint
            });
            grpKey.ResumeLayout(false);

            // ════════════════════════════════════════════════════════
            //  SIGNER TAB — grpSign
            // ════════════════════════════════════════════════════════
            grpSign.SuspendLayout();
            grpSign.Text = "Sign Message";
            grpSign.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            grpSign.Location = new System.Drawing.Point(10, 208);
            grpSign.Size = new System.Drawing.Size(580, 210);
            grpSign.Enabled = false;

            SetLabel(lblSignKeyP, "p", 14, 28);
            SetTextBox(txtSignKeyP, "", 40, 25, 100);

            SetLabel(lblSignKeyG, "g", 160, 28);
            SetTextBox(txtSignKeyG, "", 185, 25, 100);

            SetLabel(lblSignKeyX, "x  (private key)", 305, 28);
            SetTextBox(txtSignKeyX, "", 430, 25, 136);

            SetLabel(lblMessage, "Message", 14, 72);
            SetTextBox(txtMessage, "", 100, 69, 360);
            txtMessage.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            btnSign.Text = "Sign";
            btnSign.Location = new System.Drawing.Point(476, 65);
            btnSign.Size = new System.Drawing.Size(90, 32);
            StyleBtn(btnSign, System.Drawing.Color.FromArgb(0, 150, 100));
            btnSign.Click += new System.EventHandler(this.btnSign_Click);

            SetLabel(lblSignature, "Signature", 14, 118);
            SetReadonlyBox(txtSignature, 100, 115, 466);

            // Save signature file button
            btnSaveSignature.Text = "💾  Save Signature File";
            btnSaveSignature.Location = new System.Drawing.Point(14, 155);
            btnSaveSignature.Size = new System.Drawing.Size(552, 36);
            btnSaveSignature.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            btnSaveSignature.BackColor = System.Drawing.Color.FromArgb(230, 230, 230);
            btnSaveSignature.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
            btnSaveSignature.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSaveSignature.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            btnSaveSignature.Cursor = System.Windows.Forms.Cursors.Hand;
            btnSaveSignature.Enabled = false;
            btnSaveSignature.Click += new System.EventHandler(this.btnSaveSignature_Click);

            grpSign.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblSignKeyP, txtSignKeyP, lblSignKeyG, txtSignKeyG,
                lblSignKeyX, txtSignKeyX,
                lblMessage, txtMessage, btnSign,
                lblSignature, txtSignature,
                btnSaveSignature
            });
            grpSign.ResumeLayout(false);

            // Signer tab page
            tabSigner.Text = "  Signer  ";
            tabSigner.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            tabSigner.Padding = new System.Windows.Forms.Padding(4);
            tabSigner.Controls.AddRange(new System.Windows.Forms.Control[] { grpKey, grpSign });

            // ════════════════════════════════════════════════════════
            //  VERIFIER TAB — grpVerify
            // ════════════════════════════════════════════════════════
            grpVerify.SuspendLayout();
            grpVerify.Text = "Verify Signature";
            grpVerify.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            grpVerify.Location = new System.Drawing.Point(10, 10);
            grpVerify.Size = new System.Drawing.Size(580, 340);

            // Load file button — top of verifier
            btnLoadSignature.Text = "📂  Load Signature File (.elg)";
            btnLoadSignature.Location = new System.Drawing.Point(14, 28);
            btnLoadSignature.Size = new System.Drawing.Size(552, 36);
            btnLoadSignature.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            btnLoadSignature.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            btnLoadSignature.ForeColor = System.Drawing.Color.White;
            btnLoadSignature.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLoadSignature.FlatAppearance.BorderSize = 0;
            btnLoadSignature.Cursor = System.Windows.Forms.Cursors.Hand;
            btnLoadSignature.Click += new System.EventHandler(this.btnLoadSignature_Click);

            // divider label
            var lblOr = new System.Windows.Forms.Label();
            lblOr.Text = "— or enter manually —";
            lblOr.Location = new System.Drawing.Point(14, 76);
            lblOr.Size = new System.Drawing.Size(550, 18);
            lblOr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblOr.Font = new System.Drawing.Font("Segoe UI", 8f, System.Drawing.FontStyle.Italic);
            lblOr.ForeColor = System.Drawing.Color.Gray;

            SetLabel(lblVerifyP, "p", 14, 106);
            SetTextBox(txtVerifyP, "", 40, 103, 100);

            SetLabel(lblVerifyG, "g", 160, 106);
            SetTextBox(txtVerifyG, "", 185, 103, 100);

            SetLabel(lblVerifyY, "y  (public key)", 305, 106);
            SetTextBox(txtVerifyY, "", 430, 103, 136);

            SetLabel(lblVerifyMessage, "Message", 14, 150);
            SetTextBox(txtVerifyMessage, "", 100, 147, 466);
            txtVerifyMessage.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            SetLabel(lblVerifyR, "R", 14, 194);
            SetTextBox(txtVerifyR, "", 40, 191, 230);

            SetLabel(lblVerifyS, "S", 290, 194);
            SetTextBox(txtVerifyS, "", 315, 191, 155);

            btnVerify.Text = "Verify";
            btnVerify.Location = new System.Drawing.Point(484, 187);
            btnVerify.Size = new System.Drawing.Size(82, 32);
            StyleBtn(btnVerify, System.Drawing.Color.FromArgb(100, 60, 180));
            btnVerify.Click += new System.EventHandler(this.btnVerify_Click);

            panelVerifyResult.Location = new System.Drawing.Point(14, 238);
            panelVerifyResult.Size = new System.Drawing.Size(552, 42);
            panelVerifyResult.Visible = false;

            lblVerifyResult.Dock = System.Windows.Forms.DockStyle.Fill;
            lblVerifyResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblVerifyResult.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            panelVerifyResult.Controls.Add(lblVerifyResult);

            grpVerify.Controls.AddRange(new System.Windows.Forms.Control[] {
                btnLoadSignature, lblOr,
                lblVerifyP, txtVerifyP, lblVerifyG, txtVerifyG,
                lblVerifyY, txtVerifyY,
                lblVerifyMessage, txtVerifyMessage,
                lblVerifyR, txtVerifyR, lblVerifyS, txtVerifyS,
                btnVerify, panelVerifyResult
            });
            grpVerify.ResumeLayout(false);

            // Verifier tab page
            tabVerifier.Text = "  Verifier  ";
            tabVerifier.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            tabVerifier.Padding = new System.Windows.Forms.Padding(4);
            tabVerifier.Controls.Add(grpVerify);

            // TabControl
            tabControl.Location = new System.Drawing.Point(10, 55);
            tabControl.Size = new System.Drawing.Size(610, 530);
            tabControl.TabPages.Add(tabSigner);
            tabControl.TabPages.Add(tabVerifier);
            tabControl.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            // Title
            lblTitle.Text = "ElGamal Digital Signature";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            lblTitle.Location = new System.Drawing.Point(12, 14);
            lblTitle.Size = new System.Drawing.Size(400, 32);

            // Reset
            btnReset.Text = "Reset";
            btnReset.Location = new System.Drawing.Point(520, 18);
            btnReset.Size = new System.Drawing.Size(90, 26);
            btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReset.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            btnReset.Click += new System.EventHandler(this.btnReset_Click);

            // Status bar
            panelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelStatus.Height = 28;
            panelStatus.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lblStatus.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            lblStatus.ForeColor = System.Drawing.Color.Gray;
            lblStatus.Text = "Ready.";
            panelStatus.Controls.Add(lblStatus);

            // Form
            this.Text = "ElGamal Digital Signature";
            this.ClientSize = new System.Drawing.Size(632, 630);
            this.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitle, btnReset, tabControl, panelStatus
            });
        }

        // Layout helpers
        private void SetLabel(System.Windows.Forms.Label lbl, string text, int x, int y)
        {
            lbl.Text = text;
            lbl.Location = new System.Drawing.Point(x, y + 4);
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 9f);
            lbl.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
        }

        private void SetTextBox(System.Windows.Forms.TextBox tb, string text, int x, int y, int width)
        {
            tb.Text = text;
            tb.Location = new System.Drawing.Point(x, y);
            tb.Size = new System.Drawing.Size(width, 24);
            tb.Font = new System.Drawing.Font("Consolas", 9.5f);
            tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }

        private void SetReadonlyBox(System.Windows.Forms.TextBox tb, int x, int y, int width)
        {
            SetTextBox(tb, "", x, y, width);
            tb.ReadOnly = true;
            tb.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
        }

        private void StyleBtn(System.Windows.Forms.Button btn, System.Drawing.Color color)
        {
            btn.BackColor = color;
            btn.ForeColor = System.Drawing.Color.White;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        // Controls
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSigner;
        private System.Windows.Forms.TabPage tabVerifier;

        private System.Windows.Forms.GroupBox grpKey;
        private System.Windows.Forms.Label lblP, lblG, lblPrivateKey, lblPublicKeyOut;
        private System.Windows.Forms.Label lblKeyFormula, lblShareHint;
        private System.Windows.Forms.TextBox txtP, txtG, txtPrivateKey, txtPublicKeyOut;
        private System.Windows.Forms.Button btnGenerateKey;

        private System.Windows.Forms.GroupBox grpSign;
        private System.Windows.Forms.Label lblSignKeyP, lblSignKeyG, lblSignKeyX;
        private System.Windows.Forms.TextBox txtSignKeyP, txtSignKeyG, txtSignKeyX;
        private System.Windows.Forms.Label lblMessage, lblSignature;
        private System.Windows.Forms.TextBox txtMessage, txtSignature;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Button btnSaveSignature;

        private System.Windows.Forms.GroupBox grpVerify;
        private System.Windows.Forms.Button btnLoadSignature;
        private System.Windows.Forms.Label lblVerifyP, lblVerifyG, lblVerifyY;
        private System.Windows.Forms.TextBox txtVerifyP, txtVerifyG, txtVerifyY;
        private System.Windows.Forms.Label lblVerifyMessage, lblVerifyR, lblVerifyS;
        private System.Windows.Forms.TextBox txtVerifyMessage, txtVerifyR, txtVerifyS;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Panel panelVerifyResult;
        private System.Windows.Forms.Label lblVerifyResult;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblStatus;

        #endregion
    }
}