

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
            loginGroupBox = new GroupBox();
            twitchLogoutButton = new Button();
            twitchLoginButton = new Button();
            twitchLoginTextBox = new TextBox();
            rewardsGroupBox = new GroupBox();
            rewardsDataGridView = new DataGridView();
            idColumn = new DataGridViewTextBoxColumn();
            deleteColumn = new DataGridViewButtonColumn();
            titleColumn = new DataGridViewTextBoxColumn();
            twitchCostColumn = new DataGridViewTextBoxColumn();
            memeCostColumn = new DataGridViewTextBoxColumn();
            promptColumn = new DataGridViewTextBoxColumn();
            rewardsPanel = new Panel();
            refreshRewardsButton = new Button();
            createRewardButton = new Button();
            logsGroupBox = new GroupBox();
            logsRichTextBox = new RichTextBox();
            loginGroupBox.SuspendLayout();
            rewardsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rewardsDataGridView).BeginInit();
            rewardsPanel.SuspendLayout();
            logsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // loginGroupBox
            // 
            loginGroupBox.Controls.Add(twitchLogoutButton);
            loginGroupBox.Controls.Add(twitchLoginButton);
            loginGroupBox.Controls.Add(twitchLoginTextBox);
            loginGroupBox.Dock = DockStyle.Top;
            loginGroupBox.Location = new Point(0, 0);
            loginGroupBox.Name = "loginGroupBox";
            loginGroupBox.Size = new Size(694, 59);
            loginGroupBox.TabIndex = 0;
            loginGroupBox.TabStop = false;
            loginGroupBox.Text = "OAuth";
            // 
            // twitchLogoutButton
            // 
            twitchLogoutButton.Enabled = false;
            twitchLogoutButton.Location = new Point(121, 22);
            twitchLogoutButton.Name = "twitchLogoutButton";
            twitchLogoutButton.Size = new Size(75, 23);
            twitchLogoutButton.TabIndex = 0;
            twitchLogoutButton.Text = "Logout";
            twitchLogoutButton.UseVisualStyleBackColor = true;
            twitchLogoutButton.Click += buttonTwitchLogout_Click;
            // 
            // twitchLoginButton
            // 
            twitchLoginButton.Location = new Point(12, 22);
            twitchLoginButton.Name = "twitchLoginButton";
            twitchLoginButton.Size = new Size(103, 23);
            twitchLoginButton.TabIndex = 0;
            twitchLoginButton.Text = "Login Twitch";
            twitchLoginButton.UseVisualStyleBackColor = true;
            twitchLoginButton.Click += buttonTwitchLogin_Click;
            // 
            // twitchLoginTextBox
            // 
            twitchLoginTextBox.Location = new Point(204, 22);
            twitchLoginTextBox.Name = "twitchLoginTextBox";
            twitchLoginTextBox.ReadOnly = true;
            twitchLoginTextBox.Size = new Size(254, 23);
            twitchLoginTextBox.TabIndex = 3;
            // 
            // rewardsGroupBox
            // 
            rewardsGroupBox.Controls.Add(rewardsDataGridView);
            rewardsGroupBox.Controls.Add(rewardsPanel);
            rewardsGroupBox.Dock = DockStyle.Top;
            rewardsGroupBox.Location = new Point(0, 59);
            rewardsGroupBox.Name = "rewardsGroupBox";
            rewardsGroupBox.Size = new Size(694, 127);
            rewardsGroupBox.TabIndex = 2;
            rewardsGroupBox.TabStop = false;
            rewardsGroupBox.Text = "Rewards";
            // 
            // rewardsDataGridView
            // 
            rewardsDataGridView.AllowUserToAddRows = false;
            rewardsDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            rewardsDataGridView.Columns.AddRange(new DataGridViewColumn[] { idColumn, deleteColumn, titleColumn, twitchCostColumn, memeCostColumn, promptColumn });
            rewardsDataGridView.Dock = DockStyle.Fill;
            rewardsDataGridView.Location = new Point(168, 19);
            rewardsDataGridView.Name = "rewardsDataGridView";
            rewardsDataGridView.ReadOnly = true;
            rewardsDataGridView.Size = new Size(523, 105);
            rewardsDataGridView.TabIndex = 1;
            rewardsDataGridView.CellContentClick += dataGridViewRewards_CellContentClick;
            // 
            // idColumn
            // 
            idColumn.HeaderText = "Id";
            idColumn.Name = "idColumn";
            idColumn.ReadOnly = true;
            idColumn.Visible = false;
            // 
            // deleteColumn
            // 
            deleteColumn.HeaderText = "";
            deleteColumn.Name = "deleteColumn";
            deleteColumn.ReadOnly = true;
            deleteColumn.Width = 60;
            // 
            // titleColumn
            // 
            titleColumn.HeaderText = "Title";
            titleColumn.Name = "titleColumn";
            titleColumn.ReadOnly = true;
            titleColumn.Width = 150;
            // 
            // twitchCostColumn
            // 
            twitchCostColumn.HeaderText = "Twitch Cost";
            twitchCostColumn.Name = "twitchCostColumn";
            twitchCostColumn.ReadOnly = true;
            // 
            // memeCostColumn
            // 
            memeCostColumn.HeaderText = "Meme cost";
            memeCostColumn.Name = "memeCostColumn";
            memeCostColumn.ReadOnly = true;
            // 
            // promptColumn
            // 
            promptColumn.HeaderText = "Prompt";
            promptColumn.Name = "promptColumn";
            promptColumn.ReadOnly = true;
            promptColumn.Width = 250;
            // 
            // rewardsPanel
            // 
            rewardsPanel.Controls.Add(refreshRewardsButton);
            rewardsPanel.Controls.Add(createRewardButton);
            rewardsPanel.Dock = DockStyle.Left;
            rewardsPanel.Location = new Point(3, 19);
            rewardsPanel.Name = "rewardsPanel";
            rewardsPanel.Size = new Size(165, 105);
            rewardsPanel.TabIndex = 0;
            // 
            // refreshRewardsButton
            // 
            refreshRewardsButton.Location = new Point(7, 55);
            refreshRewardsButton.Name = "refreshRewardsButton";
            refreshRewardsButton.Size = new Size(150, 34);
            refreshRewardsButton.TabIndex = 1;
            refreshRewardsButton.Text = "Refresh list";
            refreshRewardsButton.UseVisualStyleBackColor = true;
            refreshRewardsButton.Click += buttonRefreshRewards_Click;
            // 
            // createRewardButton
            // 
            createRewardButton.Location = new Point(7, 14);
            createRewardButton.Name = "createRewardButton";
            createRewardButton.Size = new Size(150, 34);
            createRewardButton.TabIndex = 0;
            createRewardButton.Text = "Create meme reward";
            createRewardButton.UseVisualStyleBackColor = true;
            createRewardButton.Click += buttonCreateReward_Click;
            // 
            // logsGroupBox
            // 
            logsGroupBox.Controls.Add(logsRichTextBox);
            logsGroupBox.Dock = DockStyle.Fill;
            logsGroupBox.Location = new Point(0, 186);
            logsGroupBox.Name = "logsGroupBox";
            logsGroupBox.Size = new Size(694, 554);
            logsGroupBox.TabIndex = 3;
            logsGroupBox.TabStop = false;
            logsGroupBox.Text = "Logs";
            // 
            // logsRichTextBox
            // 
            logsRichTextBox.Dock = DockStyle.Fill;
            logsRichTextBox.Location = new Point(3, 19);
            logsRichTextBox.Name = "logsRichTextBox";
            logsRichTextBox.ReadOnly = true;
            logsRichTextBox.Size = new Size(688, 532);
            logsRichTextBox.TabIndex = 0;
            logsRichTextBox.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(694, 740);
            Controls.Add(logsGroupBox);
            Controls.Add(rewardsGroupBox);
            Controls.Add(loginGroupBox);
            Name = "MainForm";
            Text = "meme-alerts-scripts poc-1";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_LoadAsync;
            loginGroupBox.ResumeLayout(false);
            loginGroupBox.PerformLayout();
            rewardsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)rewardsDataGridView).EndInit();
            rewardsPanel.ResumeLayout(false);
            logsGroupBox.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox loginGroupBox;
        private GroupBox rewardsGroupBox;
        private GroupBox logsGroupBox;
        private Button twitchLogoutButton;
        private Button twitchLoginButton;
        private TextBox twitchLoginTextBox;
        private RichTextBox logsRichTextBox;
        private DataGridView rewardsDataGridView;
        private Panel rewardsPanel;
        private Button createRewardButton;
        private Button refreshRewardsButton;
        private DataGridViewTextBoxColumn idColumn;
        private DataGridViewButtonColumn deleteColumn;
        private DataGridViewTextBoxColumn titleColumn;
        private DataGridViewTextBoxColumn twitchCostColumn;
        private DataGridViewTextBoxColumn memeCostColumn;
        private DataGridViewTextBoxColumn promptColumn;
    }
}
