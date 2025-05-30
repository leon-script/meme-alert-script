using System.ComponentModel;

namespace TwitchLeonScript.WinForms.Forms
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            pnlAuth = new Panel();
            grpMemeAuth = new GroupBox();
            btnMemeLogout = new Button();
            btnMemeLogin = new Button();
            txtMemeLogin = new TextBox();
            grpTwitchAuth = new GroupBox();
            btnTwitchLogout = new Button();
            btnTwitchLogin = new Button();
            txtTwitchLogin = new TextBox();
            grpRedemptions = new GroupBox();
            pnlRedemptionsControl = new Panel();
            pnlRedemptionsClear = new Panel();
            btnRedemptionsClear = new Button();
            dgvMemeRedemptions = new DataGridView();
            MemeRedemptionIdColumn = new DataGridViewTextBoxColumn();
            MemeRedemptionRewardIdColumn = new DataGridViewTextBoxColumn();
            MemeRedemptionStatusColumn = new DataGridViewTextBoxColumn();
            MemeRedemptionTimeColumn = new DataGridViewTextBoxColumn();
            MemeRedemptionTwitchUsernameColumn = new DataGridViewTextBoxColumn();
            MemeRedemptionMemeUsernameColumn = new DataGridViewTextBoxColumn();
            MemeRedemptionMemeBonusColumn = new DataGridViewTextBoxColumn();
            MemeRedemptionRewardTitleColumn = new DataGridViewTextBoxColumn();
            pnlRedemptionRewardsControl = new Panel();
            btnTwitchRewardsEdit = new Button();
            lblRedemptionsStatus = new Label();
            btnMemeRewardCreate = new Button();
            pnlAuth.SuspendLayout();
            grpMemeAuth.SuspendLayout();
            grpTwitchAuth.SuspendLayout();
            grpRedemptions.SuspendLayout();
            pnlRedemptionsControl.SuspendLayout();
            pnlRedemptionsClear.SuspendLayout();
            ((ISupportInitialize)dgvMemeRedemptions).BeginInit();
            pnlRedemptionRewardsControl.SuspendLayout();
            SuspendLayout();
            // 
            // pnlAuth
            // 
            pnlAuth.Controls.Add(grpMemeAuth);
            pnlAuth.Controls.Add(grpTwitchAuth);
            pnlAuth.Dock = DockStyle.Top;
            pnlAuth.Location = new Point(0, 0);
            pnlAuth.Name = "pnlAuth";
            pnlAuth.Size = new Size(769, 62);
            pnlAuth.TabIndex = 9;
            // 
            // grpMemeAuth
            // 
            grpMemeAuth.Controls.Add(btnMemeLogout);
            grpMemeAuth.Controls.Add(btnMemeLogin);
            grpMemeAuth.Controls.Add(txtMemeLogin);
            grpMemeAuth.Dock = DockStyle.Left;
            grpMemeAuth.Location = new Point(310, 0);
            grpMemeAuth.Name = "grpMemeAuth";
            grpMemeAuth.Size = new Size(310, 62);
            grpMemeAuth.TabIndex = 2;
            grpMemeAuth.TabStop = false;
            grpMemeAuth.Text = "Meme Auth";
            // 
            // btnMemeLogout
            // 
            btnMemeLogout.Enabled = false;
            btnMemeLogout.Location = new Point(117, 22);
            btnMemeLogout.Name = "btnMemeLogout";
            btnMemeLogout.Size = new Size(75, 23);
            btnMemeLogout.TabIndex = 0;
            btnMemeLogout.Text = "Logout";
            btnMemeLogout.UseVisualStyleBackColor = true;
            btnMemeLogout.Click += btnMemeLogout_Click;
            // 
            // btnMemeLogin
            // 
            btnMemeLogin.Location = new Point(12, 22);
            btnMemeLogin.Name = "btnMemeLogin";
            btnMemeLogin.Size = new Size(103, 23);
            btnMemeLogin.TabIndex = 0;
            btnMemeLogin.Text = "2. Login Meme";
            btnMemeLogin.UseVisualStyleBackColor = true;
            btnMemeLogin.Click += btnMemeLogin_Click;
            // 
            // txtMemeLogin
            // 
            txtMemeLogin.Location = new Point(196, 22);
            txtMemeLogin.Name = "txtMemeLogin";
            txtMemeLogin.ReadOnly = true;
            txtMemeLogin.Size = new Size(103, 23);
            txtMemeLogin.TabIndex = 3;
            // 
            // grpTwitchAuth
            // 
            grpTwitchAuth.Controls.Add(btnTwitchLogout);
            grpTwitchAuth.Controls.Add(btnTwitchLogin);
            grpTwitchAuth.Controls.Add(txtTwitchLogin);
            grpTwitchAuth.Dock = DockStyle.Left;
            grpTwitchAuth.Location = new Point(0, 0);
            grpTwitchAuth.Name = "grpTwitchAuth";
            grpTwitchAuth.Size = new Size(310, 62);
            grpTwitchAuth.TabIndex = 1;
            grpTwitchAuth.TabStop = false;
            grpTwitchAuth.Text = "Twitch Auth";
            // 
            // btnTwitchLogout
            // 
            btnTwitchLogout.Enabled = false;
            btnTwitchLogout.Location = new Point(117, 22);
            btnTwitchLogout.Name = "btnTwitchLogout";
            btnTwitchLogout.Size = new Size(75, 23);
            btnTwitchLogout.TabIndex = 0;
            btnTwitchLogout.Text = "Logout";
            btnTwitchLogout.UseVisualStyleBackColor = true;
            btnTwitchLogout.Click += btnTwitchLogout_Click;
            // 
            // btnTwitchLogin
            // 
            btnTwitchLogin.Location = new Point(12, 22);
            btnTwitchLogin.Name = "btnTwitchLogin";
            btnTwitchLogin.Size = new Size(103, 23);
            btnTwitchLogin.TabIndex = 0;
            btnTwitchLogin.Text = "1. Login Twitch";
            btnTwitchLogin.UseVisualStyleBackColor = true;
            btnTwitchLogin.Click += btnTwitchLogin_Click;
            // 
            // txtTwitchLogin
            // 
            txtTwitchLogin.Location = new Point(196, 22);
            txtTwitchLogin.Name = "txtTwitchLogin";
            txtTwitchLogin.ReadOnly = true;
            txtTwitchLogin.Size = new Size(103, 23);
            txtTwitchLogin.TabIndex = 3;
            // 
            // grpRedemptions
            // 
            grpRedemptions.Controls.Add(pnlRedemptionsControl);
            grpRedemptions.Controls.Add(dgvMemeRedemptions);
            grpRedemptions.Controls.Add(pnlRedemptionRewardsControl);
            grpRedemptions.Dock = DockStyle.Fill;
            grpRedemptions.Location = new Point(0, 62);
            grpRedemptions.Name = "grpRedemptions";
            grpRedemptions.Size = new Size(769, 678);
            grpRedemptions.TabIndex = 10;
            grpRedemptions.TabStop = false;
            grpRedemptions.Text = "Meme redemptions";
            // 
            // pnlRedemptionsControl
            // 
            pnlRedemptionsControl.Controls.Add(pnlRedemptionsClear);
            pnlRedemptionsControl.Dock = DockStyle.Bottom;
            pnlRedemptionsControl.Location = new Point(3, 638);
            pnlRedemptionsControl.Name = "pnlRedemptionsControl";
            pnlRedemptionsControl.Size = new Size(763, 37);
            pnlRedemptionsControl.TabIndex = 2;
            // 
            // pnlRedemptionsClear
            // 
            pnlRedemptionsClear.Controls.Add(btnRedemptionsClear);
            pnlRedemptionsClear.Dock = DockStyle.Right;
            pnlRedemptionsClear.Location = new Point(678, 0);
            pnlRedemptionsClear.Name = "pnlRedemptionsClear";
            pnlRedemptionsClear.Size = new Size(85, 37);
            pnlRedemptionsClear.TabIndex = 5;
            // 
            // btnRedemptionsClear
            // 
            btnRedemptionsClear.Location = new Point(5, 7);
            btnRedemptionsClear.Name = "btnRedemptionsClear";
            btnRedemptionsClear.Size = new Size(75, 23);
            btnRedemptionsClear.TabIndex = 5;
            btnRedemptionsClear.Text = "Clear list";
            btnRedemptionsClear.UseVisualStyleBackColor = true;
            btnRedemptionsClear.Click += btnRedemptionsClear_Click;
            // 
            // dgvMemeRedemptions
            // 
            dgvMemeRedemptions.AllowUserToAddRows = false;
            dgvMemeRedemptions.AllowUserToDeleteRows = false;
            dgvMemeRedemptions.AllowUserToResizeRows = false;
            dgvMemeRedemptions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMemeRedemptions.Columns.AddRange(new DataGridViewColumn[] { MemeRedemptionIdColumn, MemeRedemptionRewardIdColumn, MemeRedemptionStatusColumn, MemeRedemptionTimeColumn, MemeRedemptionTwitchUsernameColumn, MemeRedemptionMemeUsernameColumn, MemeRedemptionMemeBonusColumn, MemeRedemptionRewardTitleColumn });
            dgvMemeRedemptions.Dock = DockStyle.Fill;
            dgvMemeRedemptions.Location = new Point(3, 53);
            dgvMemeRedemptions.MultiSelect = false;
            dgvMemeRedemptions.Name = "dgvMemeRedemptions";
            dgvMemeRedemptions.ReadOnly = true;
            dgvMemeRedemptions.RowHeadersVisible = false;
            dgvMemeRedemptions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMemeRedemptions.ShowEditingIcon = false;
            dgvMemeRedemptions.Size = new Size(763, 622);
            dgvMemeRedemptions.TabIndex = 1;
            // 
            // MemeRedemptionIdColumn
            // 
            MemeRedemptionIdColumn.HeaderText = "Hidden Redemption Id";
            MemeRedemptionIdColumn.Name = "MemeRedemptionIdColumn";
            MemeRedemptionIdColumn.ReadOnly = true;
            MemeRedemptionIdColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            MemeRedemptionIdColumn.Visible = false;
            // 
            // MemeRedemptionRewardIdColumn
            // 
            MemeRedemptionRewardIdColumn.HeaderText = "Hidden Reward Id";
            MemeRedemptionRewardIdColumn.Name = "MemeRedemptionRewardIdColumn";
            MemeRedemptionRewardIdColumn.ReadOnly = true;
            MemeRedemptionRewardIdColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            MemeRedemptionRewardIdColumn.Visible = false;
            // 
            // MemeRedemptionStatusColumn
            // 
            MemeRedemptionStatusColumn.HeaderText = "Status";
            MemeRedemptionStatusColumn.Name = "MemeRedemptionStatusColumn";
            MemeRedemptionStatusColumn.ReadOnly = true;
            MemeRedemptionStatusColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            MemeRedemptionStatusColumn.Width = 150;
            // 
            // MemeRedemptionTimeColumn
            // 
            MemeRedemptionTimeColumn.HeaderText = "Time";
            MemeRedemptionTimeColumn.Name = "MemeRedemptionTimeColumn";
            MemeRedemptionTimeColumn.ReadOnly = true;
            MemeRedemptionTimeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // MemeRedemptionTwitchUsernameColumn
            // 
            MemeRedemptionTwitchUsernameColumn.HeaderText = "Twitch Username";
            MemeRedemptionTwitchUsernameColumn.Name = "MemeRedemptionTwitchUsernameColumn";
            MemeRedemptionTwitchUsernameColumn.ReadOnly = true;
            MemeRedemptionTwitchUsernameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            MemeRedemptionTwitchUsernameColumn.Width = 140;
            // 
            // MemeRedemptionMemeUsernameColumn
            // 
            MemeRedemptionMemeUsernameColumn.HeaderText = "Meme Username";
            MemeRedemptionMemeUsernameColumn.Name = "MemeRedemptionMemeUsernameColumn";
            MemeRedemptionMemeUsernameColumn.ReadOnly = true;
            MemeRedemptionMemeUsernameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            MemeRedemptionMemeUsernameColumn.Width = 140;
            // 
            // MemeRedemptionMemeBonusColumn
            // 
            MemeRedemptionMemeBonusColumn.HeaderText = "Bonus";
            MemeRedemptionMemeBonusColumn.Name = "MemeRedemptionMemeBonusColumn";
            MemeRedemptionMemeBonusColumn.ReadOnly = true;
            MemeRedemptionMemeBonusColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            MemeRedemptionMemeBonusColumn.Width = 50;
            // 
            // MemeRedemptionRewardTitleColumn
            // 
            MemeRedemptionRewardTitleColumn.HeaderText = "Title";
            MemeRedemptionRewardTitleColumn.Name = "MemeRedemptionRewardTitleColumn";
            MemeRedemptionRewardTitleColumn.ReadOnly = true;
            MemeRedemptionRewardTitleColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            MemeRedemptionRewardTitleColumn.Width = 180;
            // 
            // pnlRedemptionRewardsControl
            // 
            pnlRedemptionRewardsControl.Controls.Add(btnTwitchRewardsEdit);
            pnlRedemptionRewardsControl.Controls.Add(lblRedemptionsStatus);
            pnlRedemptionRewardsControl.Controls.Add(btnMemeRewardCreate);
            pnlRedemptionRewardsControl.Dock = DockStyle.Top;
            pnlRedemptionRewardsControl.Location = new Point(3, 19);
            pnlRedemptionRewardsControl.Name = "pnlRedemptionRewardsControl";
            pnlRedemptionRewardsControl.Size = new Size(763, 34);
            pnlRedemptionRewardsControl.TabIndex = 0;
            // 
            // btnTwitchRewardsEdit
            // 
            btnTwitchRewardsEdit.Enabled = false;
            btnTwitchRewardsEdit.Location = new Point(179, 1);
            btnTwitchRewardsEdit.Name = "btnTwitchRewardsEdit";
            btnTwitchRewardsEdit.Size = new Size(202, 23);
            btnTwitchRewardsEdit.TabIndex = 3;
            btnTwitchRewardsEdit.Text = "4. Open twitch rewards settings";
            btnTwitchRewardsEdit.UseVisualStyleBackColor = true;
            btnTwitchRewardsEdit.Click += btnTwitchRewardsEdit_Click;
            // 
            // lblRedemptionsStatus
            // 
            lblRedemptionsStatus.AutoSize = true;
            lblRedemptionsStatus.Dock = DockStyle.Right;
            lblRedemptionsStatus.Location = new Point(600, 0);
            lblRedemptionsStatus.Name = "lblRedemptionsStatus";
            lblRedemptionsStatus.Padding = new Padding(0, 4, 0, 0);
            lblRedemptionsStatus.Size = new Size(163, 19);
            lblRedemptionsStatus.TabIndex = 2;
            lblRedemptionsStatus.Text = "Status: awaiting authorization";
            // 
            // btnMemeRewardCreate
            // 
            btnMemeRewardCreate.Enabled = false;
            btnMemeRewardCreate.Location = new Point(9, 1);
            btnMemeRewardCreate.Name = "btnMemeRewardCreate";
            btnMemeRewardCreate.Size = new Size(164, 23);
            btnMemeRewardCreate.TabIndex = 1;
            btnMemeRewardCreate.Text = "3. Create meme reward";
            btnMemeRewardCreate.UseVisualStyleBackColor = true;
            btnMemeRewardCreate.Click += btnMemeRewardCreate_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(769, 740);
            Controls.Add(grpRedemptions);
            Controls.Add(pnlAuth);
            MinimumSize = new Size(640, 0);
            Name = "MainForm";
            Text = "twitch-leon-script";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            pnlAuth.ResumeLayout(false);
            grpMemeAuth.ResumeLayout(false);
            grpMemeAuth.PerformLayout();
            grpTwitchAuth.ResumeLayout(false);
            grpTwitchAuth.PerformLayout();
            grpRedemptions.ResumeLayout(false);
            pnlRedemptionsControl.ResumeLayout(false);
            pnlRedemptionsClear.ResumeLayout(false);
            ((ISupportInitialize)dgvMemeRedemptions).EndInit();
            pnlRedemptionRewardsControl.ResumeLayout(false);
            pnlRedemptionRewardsControl.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlAuth;
        private GroupBox grpTwitchAuth;
        private Button btnTwitchLogout;
        private Button btnTwitchLogin;
        private TextBox txtTwitchLogin;
        private GroupBox grpMemeAuth;
        private Button btnMemeLogout;
        private Button btnMemeLogin;
        private TextBox txtMemeLogin;
        private GroupBox grpRedemptions;
        private DataGridView dgvMemeRedemptions;
        private Panel pnlRedemptionRewardsControl;
        private Label lblRedemptionsStatus;
        private Button btnMemeRewardCreate;
        private DataGridViewTextBoxColumn MemeRedemptionIdColumn;
        private DataGridViewTextBoxColumn MemeRedemptionRewardIdColumn;
        private DataGridViewTextBoxColumn MemeRedemptionStatusColumn;
        private DataGridViewTextBoxColumn MemeRedemptionTimeColumn;
        private DataGridViewTextBoxColumn MemeRedemptionTwitchUsernameColumn;
        private DataGridViewTextBoxColumn MemeRedemptionMemeUsernameColumn;
        private DataGridViewTextBoxColumn MemeRedemptionMemeBonusColumn;
        private DataGridViewTextBoxColumn MemeRedemptionRewardTitleColumn;
        private Panel pnlRedemptionsControl;
        private Panel pnlRedemptionsClear;
        private Button btnRedemptionsClear;
        private Button btnTwitchRewardsEdit;
    }
}
