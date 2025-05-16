

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
            groupBoxRewards = new GroupBox();
            dataGridViewRewards = new DataGridView();
            panelRewards = new Panel();
            buttonRefreshRewards = new Button();
            buttonCreateReward = new Button();
            groupBoxActions = new GroupBox();
            logsRichTextBox = new RichTextBox();
            ColumnDelete = new DataGridViewButtonColumn();
            ColumnName = new DataGridViewTextBoxColumn();
            ColumnCost = new DataGridViewTextBoxColumn();
            ColumnPrompt = new DataGridViewTextBoxColumn();
            ColumnId = new DataGridViewTextBoxColumn();
            groupBoxLogin.SuspendLayout();
            groupBoxRewards.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewRewards).BeginInit();
            panelRewards.SuspendLayout();
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
            // groupBoxRewards
            // 
            groupBoxRewards.Controls.Add(dataGridViewRewards);
            groupBoxRewards.Controls.Add(panelRewards);
            groupBoxRewards.Dock = DockStyle.Top;
            groupBoxRewards.Location = new Point(0, 59);
            groupBoxRewards.Name = "groupBoxRewards";
            groupBoxRewards.Size = new Size(692, 127);
            groupBoxRewards.TabIndex = 2;
            groupBoxRewards.TabStop = false;
            groupBoxRewards.Text = "Rewards";
            // 
            // dataGridViewRewards
            // 
            dataGridViewRewards.AllowUserToAddRows = false;
            dataGridViewRewards.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewRewards.Columns.AddRange(new DataGridViewColumn[] { ColumnDelete, ColumnName, ColumnCost, ColumnPrompt, ColumnId });
            dataGridViewRewards.Dock = DockStyle.Fill;
            dataGridViewRewards.Location = new Point(168, 19);
            dataGridViewRewards.Name = "dataGridViewRewards";
            dataGridViewRewards.ReadOnly = true;
            dataGridViewRewards.Size = new Size(521, 105);
            dataGridViewRewards.TabIndex = 1;
            dataGridViewRewards.CellContentClick += dataGridViewRewards_CellContentClick;
            // 
            // panelRewards
            // 
            panelRewards.Controls.Add(buttonRefreshRewards);
            panelRewards.Controls.Add(buttonCreateReward);
            panelRewards.Dock = DockStyle.Left;
            panelRewards.Location = new Point(3, 19);
            panelRewards.Name = "panelRewards";
            panelRewards.Size = new Size(165, 105);
            panelRewards.TabIndex = 0;
            // 
            // buttonRefreshRewards
            // 
            buttonRefreshRewards.Location = new Point(7, 55);
            buttonRefreshRewards.Name = "buttonRefreshRewards";
            buttonRefreshRewards.Size = new Size(150, 34);
            buttonRefreshRewards.TabIndex = 1;
            buttonRefreshRewards.Text = "Refresh list";
            buttonRefreshRewards.UseVisualStyleBackColor = true;
            buttonRefreshRewards.Click += buttonRefreshRewards_Click;
            // 
            // buttonCreateReward
            // 
            buttonCreateReward.Location = new Point(7, 14);
            buttonCreateReward.Name = "buttonCreateReward";
            buttonCreateReward.Size = new Size(150, 34);
            buttonCreateReward.TabIndex = 0;
            buttonCreateReward.Text = "Create meme reward";
            buttonCreateReward.UseVisualStyleBackColor = true;
            buttonCreateReward.Click += buttonCreateReward_Click;
            // 
            // groupBoxActions
            // 
            groupBoxActions.Controls.Add(logsRichTextBox);
            groupBoxActions.Dock = DockStyle.Fill;
            groupBoxActions.Location = new Point(0, 186);
            groupBoxActions.Name = "groupBoxActions";
            groupBoxActions.Size = new Size(692, 554);
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
            // ColumnDelete
            // 
            ColumnDelete.HeaderText = "Delete";
            ColumnDelete.Name = "ColumnDelete";
            ColumnDelete.ReadOnly = true;
            // 
            // ColumnName
            // 
            ColumnName.HeaderText = "Name";
            ColumnName.Name = "ColumnName";
            ColumnName.ReadOnly = true;
            // 
            // ColumnCost
            // 
            ColumnCost.HeaderText = "Cost";
            ColumnCost.Name = "ColumnCost";
            ColumnCost.ReadOnly = true;
            // 
            // ColumnPrompt
            // 
            ColumnPrompt.HeaderText = "Prompt";
            ColumnPrompt.Name = "ColumnPrompt";
            ColumnPrompt.ReadOnly = true;
            // 
            // ColumnId
            // 
            ColumnId.HeaderText = "Id";
            ColumnId.Name = "ColumnId";
            ColumnId.ReadOnly = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(692, 740);
            Controls.Add(groupBoxActions);
            Controls.Add(groupBoxRewards);
            Controls.Add(groupBoxLogin);
            Name = "MainForm";
            Text = "meme-alerts-scripts poc-1";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_LoadAsync;
            groupBoxLogin.ResumeLayout(false);
            groupBoxLogin.PerformLayout();
            groupBoxRewards.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewRewards).EndInit();
            panelRewards.ResumeLayout(false);
            groupBoxActions.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBoxLogin;
        private GroupBox groupBoxRewards;
        private GroupBox groupBoxActions;
        private Button buttonTwitchLogout;
        private Button buttonTwitchLogin;
        private TextBox textBoxTwitchLogin;
        private RichTextBox logsRichTextBox;
        private DataGridView dataGridViewRewards;
        private Panel panelRewards;
        private Button buttonCreateReward;
        private Button buttonRefreshRewards;
        private DataGridViewButtonColumn ColumnDelete;
        private DataGridViewTextBoxColumn ColumnName;
        private DataGridViewTextBoxColumn ColumnCost;
        private DataGridViewTextBoxColumn ColumnPrompt;
        private DataGridViewTextBoxColumn ColumnId;
    }
}
