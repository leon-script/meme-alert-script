namespace TwitchLeonScript.UI.WinForms.Forms
{
    partial class TwitchAuthForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wv2TwitchAuth = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)this.wv2TwitchAuth).BeginInit();
            SuspendLayout();
            // 
            // wv2TwitchAuth
            // 
            this.wv2TwitchAuth.AllowExternalDrop = true;
            this.wv2TwitchAuth.CreationProperties = null;
            this.wv2TwitchAuth.DefaultBackgroundColor = Color.White;
            this.wv2TwitchAuth.Dock = DockStyle.Fill;
            this.wv2TwitchAuth.Location = new Point(0, 0);
            this.wv2TwitchAuth.Name = "wv2TwitchAuth";
            this.wv2TwitchAuth.Size = new Size(1099, 661);
            this.wv2TwitchAuth.TabIndex = 0;
            this.wv2TwitchAuth.ZoomFactor = 1D;
            // 
            // TwitchAuthForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1099, 661);
            Controls.Add(this.wv2TwitchAuth);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "TwitchAuthForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Twitch Auth";
            FormClosed += OAuthForm_FormClosed;
            Load += OAuthForm_Load;
            ((System.ComponentModel.ISupportInitialize)this.wv2TwitchAuth).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 wv2TwitchAuth;
    }
}