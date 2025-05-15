

namespace MemeAlertsScripts.WinForms
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
            groupBoxSettings = new GroupBox();
            groupBoxActions = new GroupBox();
            listBoxTwitch = new ListBox();
            groupBoxActions.SuspendLayout();
            SuspendLayout();
            // 
            // groupBoxSettings
            // 
            groupBoxSettings.Dock = DockStyle.Top;
            groupBoxSettings.Location = new Point(0, 0);
            groupBoxSettings.Name = "groupBoxSettings";
            groupBoxSettings.Size = new Size(692, 124);
            groupBoxSettings.TabIndex = 0;
            groupBoxSettings.TabStop = false;
            groupBoxSettings.Text = "Settings";
            // 
            // groupBoxActions
            // 
            groupBoxActions.Controls.Add(listBoxTwitch);
            groupBoxActions.Dock = DockStyle.Fill;
            groupBoxActions.Location = new Point(0, 124);
            groupBoxActions.Name = "groupBoxActions";
            groupBoxActions.Size = new Size(692, 493);
            groupBoxActions.TabIndex = 1;
            groupBoxActions.TabStop = false;
            groupBoxActions.Text = "Actions";
            // 
            // listBoxTwitch
            // 
            listBoxTwitch.FormattingEnabled = true;
            listBoxTwitch.ItemHeight = 15;
            listBoxTwitch.Location = new Point(12, 22);
            listBoxTwitch.Name = "listBoxTwitch";
            listBoxTwitch.Size = new Size(361, 304);
            listBoxTwitch.TabIndex = 0;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(692, 617);
            Controls.Add(groupBoxActions);
            Controls.Add(groupBoxSettings);
            Name = "MainForm";
            Text = "meme-alerts-scripts";
            FormClosing += this.MainForm_FormClosing;
            Load += MainForm_LoadAsync;
            groupBoxActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxSettings;
        private GroupBox groupBoxActions;
        private ListBox listBoxTwitch;
    }
}
