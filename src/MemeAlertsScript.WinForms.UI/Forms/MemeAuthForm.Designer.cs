namespace MemeAlertsScript.WinForms
{
    partial class MemeAuthForm
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
            this.wv2MemeAuth = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)this.wv2MemeAuth).BeginInit();
            SuspendLayout();
            // 
            // wv2MemeAuth
            // 
            this.wv2MemeAuth.AllowExternalDrop = true;
            this.wv2MemeAuth.CreationProperties = null;
            this.wv2MemeAuth.DefaultBackgroundColor = Color.White;
            this.wv2MemeAuth.Dock = DockStyle.Fill;
            this.wv2MemeAuth.Location = new Point(0, 0);
            this.wv2MemeAuth.Name = "wv2MemeAuth";
            this.wv2MemeAuth.Size = new Size(1099, 661);
            this.wv2MemeAuth.TabIndex = 1;
            this.wv2MemeAuth.ZoomFactor = 1D;
            // 
            // MemeAuthForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1099, 661);
            Controls.Add(this.wv2MemeAuth);
            Name = "MemeAuthForm";
            Text = "Meme Auth";
            FormClosing += LoginMemeForm_FormClosing;
            Load += LoginMemeForm_Load;
            ((System.ComponentModel.ISupportInitialize)this.wv2MemeAuth).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 wv2MemeAuth;
    }
}