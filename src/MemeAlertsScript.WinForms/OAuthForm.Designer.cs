namespace MemeAlertsScript.WinForms
{
    partial class OAuthForm
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
            twitchWebView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)twitchWebView2).BeginInit();
            SuspendLayout();
            // 
            // twitchWebView2
            // 
            twitchWebView2.AllowExternalDrop = true;
            twitchWebView2.CreationProperties = null;
            twitchWebView2.DefaultBackgroundColor = Color.White;
            twitchWebView2.Dock = DockStyle.Fill;
            twitchWebView2.Location = new Point(0, 0);
            twitchWebView2.Name = "twitchWebView2";
            twitchWebView2.Size = new Size(737, 474);
            twitchWebView2.TabIndex = 0;
            twitchWebView2.ZoomFactor = 1D;
            // 
            // OAuthForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(737, 474);
            Controls.Add(twitchWebView2);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "OAuthForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Twitch Login";
            FormClosed += OAuthForm_FormClosed;
            Load += OAuthForm_Load;
            ((System.ComponentModel.ISupportInitialize)twitchWebView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 twitchWebView2;
    }
}