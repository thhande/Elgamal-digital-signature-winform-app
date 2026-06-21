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

            // Signer — Key Generation
            this.grpKey = new System.Windows.Forms.GroupBox();
            this.lblP = new System.Windows.Forms.Label();
            this.txtP = new System.Windows.Forms.TextBox();
            this.lblG = new System.Windows.Forms.Label();
            this.txtG = new System.Windows.Forms.TextBox();
            this.btnGenerateKey = new System.Windows.Forms.Button();
            this.btnGenerateRandom = new System.Windows.Forms.Button();
            this.lblPrivateKey = new System.Windows.Forms.Label();
            this.txtPrivateKey = new System.Windows.Forms.TextBox();
            this.lblPublicKeyOut = new System.Windows.Forms.Label();
            this.txtPublicKeyOut = new System.Windows.Forms.TextBox();
            this.lblKeyFormula = new System.Windows.Forms.Label();
            this.lblShareHint = new System.Windows.Forms.Label();
            this.btnSavePublicKey = new System.Windows.Forms.Button();
            this.btnSavePrivateKey = new System.Windows.Forms.Button();
            this.btnLoadPrivateKey = new System.Windows.Forms.Button();

            // Signer — Sign Message
            this.grpSign = new System.Windows.Forms.GroupBox();
            this.lblSignKeyP = new System.Windows.Forms.Label();
            this.txtSignKeyP = new System.Windows.Forms.TextBox();
            this.lblSignKeyG = new System.Windows.Forms.Label();
            this.txtSignKeyG = new System.Windows.Forms.TextBox();
            this.lblSignKeyX = new System.Windows.Forms.Label();
            this.txtSignKeyX = new System.Windows.Forms.TextBox();
            this.btnRandomX = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSign = new System.Windows.Forms.Button();
            this.lblSignature = new System.Windows.Forms.Label();
            this.txtSignature = new System.Windows.Forms.TextBox();
            this.btnSaveMessage = new System.Windows.Forms.Button();
            this.btnSaveSignature = new System.Windows.Forms.Button();

            // Verifier
            this.grpVerify = new System.Windows.Forms.GroupBox();
            this.btnLoadPublicKey = new System.Windows.Forms.Button();
            this.btnLoadMessage = new System.Windows.Forms.Button();
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

            // ════════════════════════════════════════════════════════
            //  SIGNER TAB — grpKey  (Key Generation)
            // ════════════════════════════════════════════════════════
            grpKey.SuspendLayout();
            grpKey.Text = "Tạo khoá";
            grpKey.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            grpKey.Location = new System.Drawing.Point(12, 12);
            grpKey.Size = new System.Drawing.Size(1056, 290);
            grpKey.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // Row 1: p (left, fixed width) | g (fixed width) | Generate button (right, fixed)
            // None of these stretch — fixed widths/positions to avoid overlap.
            SetLabel(lblP, "Số nguyên tố  p", 16, 32);
            SetFixedTextBox(txtP, "23", 16, 52, 480);

            SetLabel(lblG, "Khởi tạo  g", 540, 32);
            SetFixedTextBox(txtG, "5", 540, 52, 300);

            btnGenerateKey.Text = "Tạo khoá ";
            btnGenerateKey.Location = new System.Drawing.Point(860, 50);
            btnGenerateKey.Size = new System.Drawing.Size(180, 32);
            StyleBtn(btnGenerateKey, System.Drawing.Color.FromArgb(0, 120, 215));
            btnGenerateKey.Click += new System.EventHandler(this.btnGenerateKey_Click);

            // "Random All" button: generates a large prime p, picks a generator g, and random x
            btnGenerateRandom.Text = "🎲 Tạo khoá ngẫu nhiên";
            btnGenerateRandom.Location = new System.Drawing.Point(860, 90);
            btnGenerateRandom.Size = new System.Drawing.Size(180, 32);
            StyleBtn(btnGenerateRandom, System.Drawing.Color.FromArgb(130, 60, 180));
            btnGenerateRandom.Click += new System.EventHandler(this.btnGenerateRandom_Click);

            // Row 2: private key x — fixed-width (NOT stretching) + Random button beside it
            SetLabel(lblPrivateKey, "Khoá bí mật x", 16, 132);
            SetFixedTextBox(txtPrivateKey, "", 16, 152, 780);
            txtPrivateKey.ReadOnly = false;
            txtPrivateKey.BackColor = System.Drawing.Color.White;

            btnRandomX.Text = "x ngẫu nhiên";
            btnRandomX.Location = new System.Drawing.Point(806, 150);
            btnRandomX.Size = new System.Drawing.Size(120, 32);
            btnRandomX.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            StyleBtn(btnRandomX, System.Drawing.Color.FromArgb(90, 90, 90));
            btnRandomX.Click += new System.EventHandler(this.btnRandomX_Click);

            // Row 3: public key y — full width, stretches
            SetLabel(lblPublicKeyOut, "Khoá công khai y ", 16, 192);
            SetStretchTextBox(txtPublicKeyOut, 16, 212, 1024, true);

            // formula + hint — full width labels, stretch
            lblKeyFormula.Location = new System.Drawing.Point(16, 248);
            lblKeyFormula.Size = new System.Drawing.Size(1024, 20);
            lblKeyFormula.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblKeyFormula.Font = new System.Drawing.Font("Consolas", 9f);
            lblKeyFormula.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);

            lblShareHint.Location = new System.Drawing.Point(16, 270);
            lblShareHint.Size = new System.Drawing.Size(1024, 20);
            lblShareHint.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblShareHint.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Italic);
            lblShareHint.ForeColor = System.Drawing.Color.FromArgb(0, 100, 180);
            lblShareHint.Text = "→ chia sẻ p,g,y giữ x bí mật";

            // Row 4: save/load key buttons
            btnSavePublicKey.Text = "Lưu khoá công khai (.pub)";
            btnSavePublicKey.Location = new System.Drawing.Point(16, 296);
            btnSavePublicKey.Size = new System.Drawing.Size(330, 32);
            StyleBtn(btnSavePublicKey, System.Drawing.Color.FromArgb(0, 150, 100));
            btnSavePublicKey.Click += new System.EventHandler(this.btnSavePublicKey_Click);

            btnSavePrivateKey.Text = "Lưu khoá bí mật (.key)";
            btnSavePrivateKey.Location = new System.Drawing.Point(356, 296);
            btnSavePrivateKey.Size = new System.Drawing.Size(330, 32);
            StyleBtn(btnSavePrivateKey, System.Drawing.Color.FromArgb(180, 100, 0));
            btnSavePrivateKey.Click += new System.EventHandler(this.btnSavePrivateKey_Click);

            btnLoadPrivateKey.Text = "Tải khoá bí mật (.key)";
            btnLoadPrivateKey.Location = new System.Drawing.Point(696, 296);
            btnLoadPrivateKey.Size = new System.Drawing.Size(330, 32);
            StyleBtn(btnLoadPrivateKey, System.Drawing.Color.FromArgb(100, 60, 180));
            btnLoadPrivateKey.Click += new System.EventHandler(this.btnLoadPrivateKey_Click);

            grpKey.Size = new System.Drawing.Size(1056, 340);

            grpKey.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblP, txtP, lblG, txtG, btnGenerateKey, btnGenerateRandom,
                lblPrivateKey, txtPrivateKey, btnRandomX,
                lblPublicKeyOut, txtPublicKeyOut,
                lblKeyFormula, lblShareHint,
                btnSavePublicKey, btnSavePrivateKey, btnLoadPrivateKey
            });
            grpKey.ResumeLayout(false);

            // ════════════════════════════════════════════════════════
            //  SIGNER TAB — grpSign  (Sign Message)
            // ════════════════════════════════════════════════════════
            grpSign.SuspendLayout();
            grpSign.Text = "Ký thông điệp";
            grpSign.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            grpSign.Location = new System.Drawing.Point(12, 364);
            grpSign.Size = new System.Drawing.Size(1056, 320);
            grpSign.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            grpSign.Enabled = false;

            // Row 1: p — full width, stretches
            SetLabel(lblSignKeyP, "p", 16, 32);
            SetStretchTextBox(txtSignKeyP, 16, 52, 1024, false);

            // Row 2: g — full width, stretches
            SetLabel(lblSignKeyG, "g", 16, 90);
            SetStretchTextBox(txtSignKeyG, 16, 110, 1024, false);

            // Row 3: x — readonly (set in Key Generation above), full width
            SetLabel(lblSignKeyX, "x  (khoá bí mật)", 16, 148);
            SetStretchTextBox(txtSignKeyX, 16, 168, 1024, false);

            // Row 4: message — full width stretches
            SetLabel(lblMessage, "Thông điệp", 16, 208);
            SetStretchTextBox(txtMessage, 16, 228, 1024, false);
            txtMessage.Font = new System.Drawing.Font("Segoe UI", 10f);

            // Row 5: Sign button — full width, own row so it's always visible
            btnSign.Text = "✍  Ký thông điệp";
            btnSign.Location = new System.Drawing.Point(16, 266);
            btnSign.Size = new System.Drawing.Size(1024, 36);
            btnSign.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            StyleBtn(btnSign, System.Drawing.Color.FromArgb(0, 150, 100));
            btnSign.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            btnSign.Click += new System.EventHandler(this.btnSign_Click);

            // Row 6: signature output — full width, stretches
            SetLabel(lblSignature, "Chữ ký  (R, S)", 16, 308);
            SetStretchTextBox(txtSignature, 16, 328, 1024, true);

            grpSign.Size = new System.Drawing.Size(1056, 368);

            grpSign.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblSignKeyP, txtSignKeyP,
                lblSignKeyG, txtSignKeyG,
                lblSignKeyX, txtSignKeyX,
                lblMessage, txtMessage,
                btnSign,
                lblSignature, txtSignature
            });
            grpSign.ResumeLayout(false);

            // ── Save buttons for message + signature (below grpSign) ──
            // Both fixed-width, anchored top-left only — no stretching,
            // no right-anchoring, so they can never end up off-screen.
            btnSaveMessage.Text = "Lưu thông điệp (.msg)";
            btnSaveMessage.Location = new System.Drawing.Point(12, 742);
            btnSaveMessage.Size = new System.Drawing.Size(516, 40);
            btnSaveMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            StyleBtn(btnSaveMessage, System.Drawing.Color.FromArgb(0, 150, 100));
            btnSaveMessage.Click += new System.EventHandler(this.btnSaveMessage_Click);

            btnSaveSignature.Text = "Lưu chữ ký (.sig)";
            btnSaveSignature.Location = new System.Drawing.Point(540, 742);
            btnSaveSignature.Size = new System.Drawing.Size(516, 40);
            btnSaveSignature.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            StyleBtn(btnSaveSignature, System.Drawing.Color.FromArgb(0, 150, 100));
            btnSaveSignature.Enabled = false;
            btnSaveSignature.Click += new System.EventHandler(this.btnSaveSignature_Click);

            // Signer tab page
            tabSigner.Text = "Ký thông điệp";
            tabSigner.Font = new System.Drawing.Font("Segoe UI", 10f);
            tabSigner.Padding = new System.Windows.Forms.Padding(4);
            tabSigner.AutoScroll = true;
            tabSigner.Controls.AddRange(new System.Windows.Forms.Control[] { grpKey, grpSign, btnSaveMessage, btnSaveSignature });

            // ════════════════════════════════════════════════════════
            //  VERIFIER TAB — grpVerify
            // ════════════════════════════════════════════════════════
            grpVerify.SuspendLayout();
            grpVerify.Text = "Xác thực chữ ký";
            grpVerify.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            grpVerify.Location = new System.Drawing.Point(12, 12);
            grpVerify.Size = new System.Drawing.Size(1056, 580);
            grpVerify.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            // Load buttons row — three fixed-width buttons side by side, anchored top-left
            btnLoadPublicKey.Text = "Tải khoá công khai (.pub)";
            btnLoadPublicKey.Location = new System.Drawing.Point(16, 32);
            btnLoadPublicKey.Size = new System.Drawing.Size(330, 36);
            btnLoadPublicKey.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            StyleBtn(btnLoadPublicKey, System.Drawing.Color.FromArgb(0, 120, 215));
            btnLoadPublicKey.Click += new System.EventHandler(this.btnLoadPublicKey_Click);

            btnLoadMessage.Text = "Tải thông điệp (.msg)";
            btnLoadMessage.Location = new System.Drawing.Point(356, 32);
            btnLoadMessage.Size = new System.Drawing.Size(330, 36);
            btnLoadMessage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            StyleBtn(btnLoadMessage, System.Drawing.Color.FromArgb(0, 120, 215));
            btnLoadMessage.Click += new System.EventHandler(this.btnLoadMessage_Click);

            btnLoadSignature.Text = "Tải chữ ký (.sig)";
            btnLoadSignature.Location = new System.Drawing.Point(696, 32);
            btnLoadSignature.Size = new System.Drawing.Size(330, 36);
            btnLoadSignature.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
            StyleBtn(btnLoadSignature, System.Drawing.Color.FromArgb(0, 120, 215));
            btnLoadSignature.Click += new System.EventHandler(this.btnLoadSignature_Click);

            // divider — full width, stretches
            var lblOr = new System.Windows.Forms.Label();
            lblOr.Text = "";
            lblOr.Location = new System.Drawing.Point(16, 82);
            lblOr.Size = new System.Drawing.Size(1024, 18);
            lblOr.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lblOr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblOr.Font = new System.Drawing.Font("Segoe UI", 8.5f, System.Drawing.FontStyle.Italic);
            lblOr.ForeColor = System.Drawing.Color.Gray;

            // Row: p — full width, stretches
            SetLabel(lblVerifyP, "p", 16, 112);
            SetStretchTextBox(txtVerifyP, 16, 132, 1024, false);

            // Row: g — full width, stretches
            SetLabel(lblVerifyG, "g", 16, 170);
            SetStretchTextBox(txtVerifyG, 16, 190, 1024, false);

            // Row: y — full width, stretches
            SetLabel(lblVerifyY, "y (khoá công khai)", 16, 228);
            SetStretchTextBox(txtVerifyY, 16, 248, 1024, false);

            // Row: message — full width, stretches
            SetLabel(lblVerifyMessage, "Thông điệp", 16, 286);
            SetStretchTextBox(txtVerifyMessage, 16, 306, 1024, false);
            txtVerifyMessage.Font = new System.Drawing.Font("Segoe UI", 10f);

            // Row: R — full width, stretches
            SetLabel(lblVerifyR, "R", 16, 344);
            SetStretchTextBox(txtVerifyR, 16, 364, 1024, false);

            // Row: S — full width, stretches
            SetLabel(lblVerifyS, "S", 16, 402);
            SetStretchTextBox(txtVerifyS, 16, 422, 1024, false);

            // Verify button — full width, stretches
            btnVerify.Text = "Xác minh chữ ký";
            btnVerify.Location = new System.Drawing.Point(16, 462);
            btnVerify.Size = new System.Drawing.Size(1024, 40);
            btnVerify.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            StyleBtn(btnVerify, System.Drawing.Color.FromArgb(100, 60, 180));
            btnVerify.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            btnVerify.Click += new System.EventHandler(this.btnVerify_Click);

            // Result panel — full width, stretches
            panelVerifyResult.Location = new System.Drawing.Point(16, 510);
            panelVerifyResult.Size = new System.Drawing.Size(1024, 44);
            panelVerifyResult.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelVerifyResult.Visible = false;

            lblVerifyResult.Dock = System.Windows.Forms.DockStyle.Fill;
            lblVerifyResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblVerifyResult.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
            panelVerifyResult.Controls.Add(lblVerifyResult);

            grpVerify.Controls.AddRange(new System.Windows.Forms.Control[] {
                btnLoadPublicKey, btnLoadMessage, btnLoadSignature, lblOr,
                lblVerifyP, txtVerifyP,
                lblVerifyG, txtVerifyG,
                lblVerifyY, txtVerifyY,
                lblVerifyMessage, txtVerifyMessage,
                lblVerifyR, txtVerifyR,
                lblVerifyS, txtVerifyS,
                btnVerify, panelVerifyResult
            });
            grpVerify.ResumeLayout(false);

            // Verifier tab page
            tabVerifier.Text = "  Xác minh  ";
            tabVerifier.Font = new System.Drawing.Font("Segoe UI", 10f);
            tabVerifier.Padding = new System.Windows.Forms.Padding(4);
            tabVerifier.AutoScroll = true;
            tabVerifier.Controls.Add(grpVerify);

            // TabControl
            tabControl.Location = new System.Drawing.Point(12, 56);
            tabControl.Size = new System.Drawing.Size(1080, 700);
            tabControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            tabControl.TabPages.Add(tabSigner);
            tabControl.TabPages.Add(tabVerifier);
            tabControl.Font = new System.Drawing.Font("Segoe UI", 10f);

            // Title
            lblTitle.Text = "ElGamal Digital Signature";
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16f, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            lblTitle.Location = new System.Drawing.Point(14, 14);
            lblTitle.Size = new System.Drawing.Size(450, 36);

            // Reset
            btnReset.Text = "Reset";
            btnReset.Location = new System.Drawing.Point(980, 16);
            btnReset.Size = new System.Drawing.Size(112, 30);
            btnReset.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReset.Font = new System.Drawing.Font("Segoe UI", 9f);
            btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            btnReset.Click += new System.EventHandler(this.btnReset_Click);

            // Form
            this.Text = "ElGamal Digital Signature";
            this.ClientSize = new System.Drawing.Size(1104, 768);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblTitle, btnReset, tabControl
            });
        }

        // ── Layout helpers ──────────────────────────────────────────

        private void SetLabel(System.Windows.Forms.Label lbl, string text, int x, int y)
        {
            lbl.Text = text;
            lbl.Location = new System.Drawing.Point(x, y + 4);
            lbl.AutoSize = true;
            lbl.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            lbl.ForeColor = System.Drawing.Color.FromArgb(50, 50, 50);
        }

        // Fixed-size textbox — does NOT stretch. Use when another control
        // sits to its right on the same row (prevents overlap on resize).
        private void SetFixedTextBox(System.Windows.Forms.TextBox tb, string text, int x, int y, int width)
        {
            tb.Text = text;
            tb.Location = new System.Drawing.Point(x, y);
            tb.Size = new System.Drawing.Size(width, 28);
            tb.Font = new System.Drawing.Font("Consolas", 11f);
            tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left;
        }

        // Stretching textbox — anchors Left+Right so it grows with the form.
        // Only use when nothing else shares its row to the right,
        // OR when the sibling control is anchored Top|Right with its own fixed width
        // (the textbox's initial width must leave room for that sibling).
        private void SetStretchTextBox(System.Windows.Forms.TextBox tb, int x, int y, int width, bool readOnly)
        {
            tb.Text = "";
            tb.Location = new System.Drawing.Point(x, y);
            tb.Size = new System.Drawing.Size(width, 28);
            tb.Font = new System.Drawing.Font("Consolas", 11f);
            tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;

            if (readOnly)
            {
                tb.ReadOnly = true;
                tb.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            }
        }

        private void StyleBtn(System.Windows.Forms.Button btn, System.Drawing.Color color)
        {
            btn.BackColor = color;
            btn.ForeColor = System.Drawing.Color.White;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;

            // Flat-style buttons with a custom BackColor can become nearly
            // invisible when Enabled = false (WinForms renders a washed-out
            // gray that blends into the form background). Force a visible
            // disabled appearance instead.
            System.Drawing.Color enabledColor = color;
            btn.EnabledChanged += (s, e) =>
            {
                if (btn.Enabled)
                {
                    btn.BackColor = enabledColor;
                    btn.ForeColor = System.Drawing.Color.White;
                }
                else
                {
                    btn.BackColor = System.Drawing.Color.FromArgb(225, 225, 225);
                    btn.ForeColor = System.Drawing.Color.FromArgb(160, 160, 160);
                }
            };
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
        private System.Windows.Forms.Button btnGenerateRandom;
        private System.Windows.Forms.Button btnSavePublicKey, btnSavePrivateKey, btnLoadPrivateKey;

        private System.Windows.Forms.GroupBox grpSign;
        private System.Windows.Forms.Label lblSignKeyP, lblSignKeyG, lblSignKeyX;
        private System.Windows.Forms.TextBox txtSignKeyP, txtSignKeyG, txtSignKeyX;
        private System.Windows.Forms.Button btnRandomX;
        private System.Windows.Forms.Label lblMessage, lblSignature;
        private System.Windows.Forms.TextBox txtMessage, txtSignature;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Button btnSaveMessage, btnSaveSignature;

        private System.Windows.Forms.GroupBox grpVerify;
        private System.Windows.Forms.Button btnLoadPublicKey, btnLoadMessage, btnLoadSignature;
        private System.Windows.Forms.Label lblVerifyP, lblVerifyG, lblVerifyY;
        private System.Windows.Forms.TextBox txtVerifyP, txtVerifyG, txtVerifyY;
        private System.Windows.Forms.Label lblVerifyMessage, lblVerifyR, lblVerifyS;
        private System.Windows.Forms.TextBox txtVerifyMessage, txtVerifyR, txtVerifyS;
        private System.Windows.Forms.Button btnVerify;
        private System.Windows.Forms.Panel panelVerifyResult;
        private System.Windows.Forms.Label lblVerifyResult;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnReset;

        #endregion
    }
}