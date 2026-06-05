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
            this.grpKey = new System.Windows.Forms.GroupBox();
            this.lblKeyFormula = new System.Windows.Forms.Label();
            this.txtPublicKey = new System.Windows.Forms.TextBox();
            this.txtPrivateKey = new System.Windows.Forms.TextBox();
            this.lblPublicKey = new System.Windows.Forms.Label();
            this.lblPrivateKey = new System.Windows.Forms.Label();
            this.btnGenerateKey = new System.Windows.Forms.Button();
            this.txtG = new System.Windows.Forms.TextBox();
            this.txtP = new System.Windows.Forms.TextBox();
            this.lblG = new System.Windows.Forms.Label();
            this.lblP = new System.Windows.Forms.Label();

            this.grpSign = new System.Windows.Forms.GroupBox();
            this.txtSignature = new System.Windows.Forms.TextBox();
            this.lblSignature = new System.Windows.Forms.Label();
            this.btnSign = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();

            this.grpVerify = new System.Windows.Forms.GroupBox();
            this.panelVerifyResult = new System.Windows.Forms.Panel();
            this.lblVerifyResult = new System.Windows.Forms.Label();
            this.btnVerify = new System.Windows.Forms.Button();
            this.txtVerifyS = new System.Windows.Forms.TextBox();
            this.txtVerifyR = new System.Windows.Forms.TextBox();
            this.txtVerifyMessage = new System.Windows.Forms.TextBox();
            this.lblVerifyS = new System.Windows.Forms.Label();
            this.lblVerifyR = new System.Windows.Forms.Label();
            this.lblVerifyMessage = new System.Windows.Forms.Label();

            this.panelStatus = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();

            // ── grpKey ───────────────────────────────────────────────────
            this.grpKey.SuspendLayout();
            this.grpKey.Text = "Step 1 — Key Generation";
            this.grpKey.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.grpKey.Location = new System.Drawing.Point(16, 60);
            this.grpKey.Size = new System.Drawing.Size(560, 180);
            this.grpKey.TabIndex = 0;

            SetLabel(this.lblP, "Prime  p", 16, 30);
            SetTextBox(this.txtP, "23", 110, 27, 60);

            SetLabel(this.lblG, "Generator  g", 200, 30);
            SetTextBox(this.txtG, "5", 310, 27, 60);

            this.btnGenerateKey.Text = "Generate Keys";
            this.btnGenerateKey.Location = new System.Drawing.Point(400, 24);
            this.btnGenerateKey.Size = new System.Drawing.Size(140, 32);
            this.btnGenerateKey.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnGenerateKey.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnGenerateKey.ForeColor = System.Drawing.Color.White;
            this.btnGenerateKey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerateKey.FlatAppearance.BorderSize = 0;
            this.btnGenerateKey.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerateKey.Click += new System.EventHandler(this.btnGenerateKey_Click);

            SetLabel(this.lblPrivateKey, "Private key  x", 16, 78);
            SetReadonlyBox(this.txtPrivateKey, 130, 75, 180);

            SetLabel(this.lblPublicKey, "Public key  y", 16, 118);
            SetReadonlyBox(this.txtPublicKey, 130, 115, 180);

            this.lblKeyFormula.Location = new System.Drawing.Point(16, 148);
            this.lblKeyFormula.Size = new System.Drawing.Size(530, 20);
            this.lblKeyFormula.Font = new System.Drawing.Font("Consolas", 8.5f);
            this.lblKeyFormula.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);

            this.grpKey.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblP, this.txtP, this.lblG, this.txtG, this.btnGenerateKey,
                this.lblPrivateKey, this.txtPrivateKey,
                this.lblPublicKey, this.txtPublicKey,
                this.lblKeyFormula
            });
            this.grpKey.ResumeLayout(false);

            // ── grpSign ──────────────────────────────────────────────────
            this.grpSign.SuspendLayout();
            this.grpSign.Text = "Step 2 — Sign Message";
            this.grpSign.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.grpSign.Location = new System.Drawing.Point(16, 258);
            this.grpSign.Size = new System.Drawing.Size(560, 130);
            this.grpSign.TabIndex = 1;
            this.grpSign.Enabled = false;

            SetLabel(this.lblMessage, "Message", 16, 30);
            SetTextBox(this.txtMessage, "", 100, 27, 320);
            this.txtMessage.Font = new System.Drawing.Font("Segoe UI", 9.5f);

            this.btnSign.Text = "Sign";
            this.btnSign.Location = new System.Drawing.Point(440, 24);
            this.btnSign.Size = new System.Drawing.Size(100, 32);
            this.btnSign.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnSign.BackColor = System.Drawing.Color.FromArgb(0, 150, 100);
            this.btnSign.ForeColor = System.Drawing.Color.White;
            this.btnSign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSign.FlatAppearance.BorderSize = 0;
            this.btnSign.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);

            SetLabel(this.lblSignature, "Signature", 16, 78);
            SetReadonlyBox(this.txtSignature, 100, 75, 440);

            this.grpSign.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblMessage, this.txtMessage, this.btnSign,
                this.lblSignature, this.txtSignature
            });
            this.grpSign.ResumeLayout(false);

            // ── grpVerify ────────────────────────────────────────────────
            this.grpVerify.SuspendLayout();
            this.grpVerify.Text = "Step 3 — Verify Signature";
            this.grpVerify.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.grpVerify.Location = new System.Drawing.Point(16, 406);
            this.grpVerify.Size = new System.Drawing.Size(560, 180);
            this.grpVerify.TabIndex = 2;
            this.grpVerify.Enabled = false;

            SetLabel(this.lblVerifyMessage, "Message", 16, 30);
            SetTextBox(this.txtVerifyMessage, "", 100, 27, 440);

            SetLabel(this.lblVerifyR, "R", 16, 72);
            SetTextBox(this.txtVerifyR, "", 100, 69, 180);

            SetLabel(this.lblVerifyS, "S", 310, 72);
            SetTextBox(this.txtVerifyS, "", 340, 69, 100);

            this.btnVerify.Text = "Verify";
            this.btnVerify.Location = new System.Drawing.Point(455, 65);
            this.btnVerify.Size = new System.Drawing.Size(85, 32);
            this.btnVerify.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnVerify.BackColor = System.Drawing.Color.FromArgb(100, 60, 180);
            this.btnVerify.ForeColor = System.Drawing.Color.White;
            this.btnVerify.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVerify.FlatAppearance.BorderSize = 0;
            this.btnVerify.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerify.Click += new System.EventHandler(this.btnVerify_Click);

            this.panelVerifyResult.Location = new System.Drawing.Point(16, 112);
            this.panelVerifyResult.Size = new System.Drawing.Size(524, 40);
            this.panelVerifyResult.BackColor = System.Drawing.Color.FromArgb(220, 245, 220);
            this.panelVerifyResult.Visible = false;

            this.lblVerifyResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVerifyResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblVerifyResult.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.panelVerifyResult.Controls.Add(this.lblVerifyResult);

            this.grpVerify.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblVerifyMessage, this.txtVerifyMessage,
                this.lblVerifyR, this.txtVerifyR,
                this.lblVerifyS, this.txtVerifyS,
                this.btnVerify, this.panelVerifyResult
            });
            this.grpVerify.ResumeLayout(false);

            // ── Status bar ───────────────────────────────────────────────
            this.panelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatus.Height = 30;
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);

            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblStatus.Text = "Ready.";
            this.panelStatus.Controls.Add(this.lblStatus);

            // ── Title ────────────────────────────────────────────────────
            this.lblTitle.Text = "ElGamal Digital Signature";
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblTitle.Location = new System.Drawing.Point(16, 12);
            this.lblTitle.Size = new System.Drawing.Size(400, 34);

            // ── Reset button ─────────────────────────────────────────────
            this.btnReset.Text = "Reset";
            this.btnReset.Location = new System.Drawing.Point(470, 16);
            this.btnReset.Size = new System.Drawing.Size(100, 28);
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 8.5f);
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);

            // ── Form ─────────────────────────────────────────────────────
            this.Text = "ElGamal Digital Signature";
            this.ClientSize = new System.Drawing.Size(592, 640);
            this.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblTitle,
                this.btnReset,
                this.grpKey,
                this.grpSign,
                this.grpVerify,
                this.panelStatus
            });
        }

        // ── Layout helpers ───────────────────────────────────────────────
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
            tb.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
        }

        // ── Controls ─────────────────────────────────────────────────────
        private System.Windows.Forms.GroupBox grpKey;
        private System.Windows.Forms.Label lblP, lblG, lblPrivateKey, lblPublicKey, lblKeyFormula;
        private System.Windows.Forms.TextBox txtP, txtG, txtPrivateKey, txtPublicKey;
        private System.Windows.Forms.Button btnGenerateKey;

        private System.Windows.Forms.GroupBox grpSign;
        private System.Windows.Forms.Label lblMessage, lblSignature;
        private System.Windows.Forms.TextBox txtMessage, txtSignature;
        private System.Windows.Forms.Button btnSign;

        private System.Windows.Forms.GroupBox grpVerify;
        private System.Windows.Forms.Label lblVerifyMessage, lblVerifyR, lblVerifyS;
        private System.Windows.Forms.TextBox txtVerifyMessage, txtVerifyR, txtVerifyS;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Panel panelVerifyResult;
        private System.Windows.Forms.Label lblVerifyResult;

        private System.Windows.Forms.Panel panelStatus;
        private System.Windows.Forms.Label lblStatus, lblTitle;
        private System.Windows.Forms.Button btnReset;

        #endregion
    }
}