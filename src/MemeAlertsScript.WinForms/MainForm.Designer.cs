

namespace MemeAlertsScript.WinForms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBoxLogin = new GroupBox();
            buttonTwitchLogout = new Button();
            buttonTwitchLogin = new Button();
            textBoxTwitchLogin = new TextBox();
            groupBoxSettings = new GroupBox();
            groupBoxActions = new GroupBox();
            logsRichTextBox = new RichTextBox();
            groupBoxLogin.SuspendLayout();
            groupBoxActions.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxLogin
            // 
            groupBoxLogin.Controls.Add(buttonTwitchLogout);
            groupBoxLogin.Controls.Add(buttonTwitchLogin);
            groupBoxLogin.Controls.Add(textBoxTwitchLogin);
            groupBoxLogin.Dock = DockStyle.Top;
            groupBoxLogin.Location = new Point(0, 0);
            groupBoxLogin.Name = "groupBoxLogin";
            groupBoxLogin.Size = new Size(692, 59);
            groupBoxLogin.TabIndex = 0;
            groupBoxLogin.TabStop = false;
            groupBoxLogin.Text = "OAuth";
            // 
            // buttonTwitchLogout
            // 
            buttonTwitchLogout.Enabled = false;
            buttonTwitchLogout.Location = new Point(121, 22);
            buttonTwitchLogout.Name = "buttonTwitchLogout";
            buttonTwitchLogout.Size = new Size(75, 23);
            buttonTwitchLogout.TabIndex = 0;
            buttonTwitchLogout.Text = "Logout";
            buttonTwitchLogout.UseVisualStyleBackColor = true;
            buttonTwitchLogout.Click += buttonTwitchLogout_Click;
            // 
            // buttonTwitchLogin
            // 
            buttonTwitchLogin.Location = new Point(12, 22);
            buttonTwitchLogin.Name = "buttonTwitchLogin";
            buttonTwitchLogin.Size = new Size(103, 23);
            buttonTwitchLogin.TabIndex = 0;
            buttonTwitchLogin.Text = "Login Twitch";
            buttonTwitchLogin.UseVisualStyleBackColor = true;
            buttonTwitchLogin.Click += buttonTwitchLogin_Click;
            // 
            // textBoxTwitchLogin
            // 
            textBoxTwitchLogin.Location = new Point(204, 22);
            textBoxTwitchLogin.Name = "textBoxTwitchLogin";
            textBoxTwitchLogin.ReadOnly = true;
            textBoxTwitchLogin.Size = new Size(254, 23);
            textBoxTwitchLogin.TabIndex = 3;
            // 
            // groupBoxSettings
            // 
            groupBoxSettings.Dock = DockStyle.Top;
            groupBoxSettings.Location = new Point(0, 59);
            groupBoxSettings.Name = "groupBoxSettings";
            groupBoxSettings.Size = new Size(692, 177);
            groupBoxSettings.TabIndex = 2;
            groupBoxSettings.TabStop = false;
            groupBoxSettings.Text = "Settings";
            // 
            // groupBoxActions
            // 
            groupBoxActions.Controls.Add(logsRichTextBox);
            groupBoxActions.Dock = DockStyle.Fill;
            groupBoxActions.Location = new Point(0, 236);
            groupBoxActions.Name = "groupBoxActions";
            groupBoxActions.Size = new Size(692, 504);
            groupBoxActions.TabIndex = 3;
            groupBoxActions.TabStop = false;
            groupBoxActions.Text = "Actions";
            // 
            // logsRichTextBox
            // 
            logsRichTextBox.Location = new Point(12, 22);
            logsRichTextBox.Name = "logsRichTextBox";
            logsRichTextBox.ReadOnly = true;
            logsRichTextBox.Size = new Size(674, 358);
            logsRichTextBox.TabIndex = 0;
            logsRichTextBox.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(692, 740);
            Controls.Add(groupBoxActions);
            Controls.Add(groupBoxSettings);
            Controls.Add(groupBoxLogin);
            Name = "MainForm";
            Text = "meme-alerts-scripts";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_LoadAsync;
            groupBoxLogin.ResumeLayout(false);
            groupBoxLogin.PerformLayout();
            groupBoxActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxLogin;
        private GroupBox groupBoxSettings;
        private GroupBox groupBoxActions;
        private Button buttonTwitchLogout;
        private Button buttonTwitchLogin;
        private TextBox textBoxTwitchLogin;
        private RichTextBox logsRichTextBox;
    }
}
