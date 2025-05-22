using System.ComponentModel;

namespace MemeAlertsScript.WinForms.Forms
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
            this.pnlAuth = new Panel();
            this.grpMemeAuth = new GroupBox();
            this.btnMemeLogout = new Button();
            this.btnMemeLogin = new Button();
            this.txtMemeLogin = new TextBox();
            this.grpTwitchAuth = new GroupBox();
            this.btnTwitchLogout = new Button();
            this.btnTwitchLogin = new Button();
            this.txtTwitchLogin = new TextBox();
            this.grpRedemptions = new GroupBox();
            this.pnlRedemptionsControl = new Panel();
            this.pnlRedemptionsClear = new Panel();
            this.btnRedemptionsClear = new Button();
            this.dgvMemeRedemptions = new DataGridView();
            this.MemeRedemptionIdColumn = new DataGridViewTextBoxColumn();
            this.MemeRedemptionRewardIdColumn = new DataGridViewTextBoxColumn();
            this.MemeRedemptionStatusColumn = new DataGridViewTextBoxColumn();
            this.MemeRedemptionTimeColumn = new DataGridViewTextBoxColumn();
            this.MemeRedemptionTwitchUsernameColumn = new DataGridViewTextBoxColumn();
            this.MemeRedemptionMemeUsernameColumn = new DataGridViewTextBoxColumn();
            this.MemeRedemptionMemeBonusColumn = new DataGridViewTextBoxColumn();
            this.MemeRedemptionRewardTitleColumn = new DataGridViewTextBoxColumn();
            this.pnlRedemptionRewardsControl = new Panel();
            this.btnTwitchRewardsEdit = new Button();
            this.lblRedemptionsStatus = new Label();
            this.btnMemeRewardCreate = new Button();
            this.pnlAuth.SuspendLayout();
            this.grpMemeAuth.SuspendLayout();
            this.grpTwitchAuth.SuspendLayout();
            this.grpRedemptions.SuspendLayout();
            this.pnlRedemptionsControl.SuspendLayout();
            this.pnlRedemptionsClear.SuspendLayout();
            ((ISupportInitialize)this.dgvMemeRedemptions).BeginInit();
            this.pnlRedemptionRewardsControl.SuspendLayout();
            SuspendLayout();
            // 
            // pnlAuth
            // 
            this.pnlAuth.Controls.Add(this.grpMemeAuth);
            this.pnlAuth.Controls.Add(this.grpTwitchAuth);
            this.pnlAuth.Dock = DockStyle.Top;
            this.pnlAuth.Location = new Point(0, 0);
            this.pnlAuth.Name = "pnlAuth";
            this.pnlAuth.Size = new Size(769, 62);
            this.pnlAuth.TabIndex = 9;
            // 
            // grpMemeAuth
            // 
            this.grpMemeAuth.Controls.Add(this.btnMemeLogout);
            this.grpMemeAuth.Controls.Add(this.btnMemeLogin);
            this.grpMemeAuth.Controls.Add(this.txtMemeLogin);
            this.grpMemeAuth.Dock = DockStyle.Left;
            this.grpMemeAuth.Location = new Point(310, 0);
            this.grpMemeAuth.Name = "grpMemeAuth";
            this.grpMemeAuth.Size = new Size(310, 62);
            this.grpMemeAuth.TabIndex = 2;
            this.grpMemeAuth.TabStop = false;
            this.grpMemeAuth.Text = "Meme Auth";
            // 
            // btnMemeLogout
            // 
            this.btnMemeLogout.Enabled = false;
            this.btnMemeLogout.Location = new Point(117, 22);
            this.btnMemeLogout.Name = "btnMemeLogout";
            this.btnMemeLogout.Size = new Size(75, 23);
            this.btnMemeLogout.TabIndex = 0;
            this.btnMemeLogout.Text = "Logout";
            this.btnMemeLogout.UseVisualStyleBackColor = true;
            this.btnMemeLogout.Click += btnMemeLogout_Click;
            // 
            // btnMemeLogin
            // 
            this.btnMemeLogin.Location = new Point(12, 22);
            this.btnMemeLogin.Name = "btnMemeLogin";
            this.btnMemeLogin.Size = new Size(103, 23);
            this.btnMemeLogin.TabIndex = 0;
            this.btnMemeLogin.Text = "2. Login Meme";
            this.btnMemeLogin.UseVisualStyleBackColor = true;
            this.btnMemeLogin.Click += btnMemeLogin_Click;
            // 
            // txtMemeLogin
            // 
            this.txtMemeLogin.Location = new Point(196, 22);
            this.txtMemeLogin.Name = "txtMemeLogin";
            this.txtMemeLogin.ReadOnly = true;
            this.txtMemeLogin.Size = new Size(103, 23);
            this.txtMemeLogin.TabIndex = 3;
            // 
            // grpTwitchAuth
            // 
            this.grpTwitchAuth.Controls.Add(this.btnTwitchLogout);
            this.grpTwitchAuth.Controls.Add(this.btnTwitchLogin);
            this.grpTwitchAuth.Controls.Add(this.txtTwitchLogin);
            this.grpTwitchAuth.Dock = DockStyle.Left;
            this.grpTwitchAuth.Location = new Point(0, 0);
            this.grpTwitchAuth.Name = "grpTwitchAuth";
            this.grpTwitchAuth.Size = new Size(310, 62);
            this.grpTwitchAuth.TabIndex = 1;
            this.grpTwitchAuth.TabStop = false;
            this.grpTwitchAuth.Text = "Twitch Auth";
            // 
            // btnTwitchLogout
            // 
            this.btnTwitchLogout.Enabled = false;
            this.btnTwitchLogout.Location = new Point(117, 22);
            this.btnTwitchLogout.Name = "btnTwitchLogout";
            this.btnTwitchLogout.Size = new Size(75, 23);
            this.btnTwitchLogout.TabIndex = 0;
            this.btnTwitchLogout.Text = "Logout";
            this.btnTwitchLogout.UseVisualStyleBackColor = true;
            this.btnTwitchLogout.Click += btnTwitchLogout_Click;
            // 
            // btnTwitchLogin
            // 
            this.btnTwitchLogin.Location = new Point(12, 22);
            this.btnTwitchLogin.Name = "btnTwitchLogin";
            this.btnTwitchLogin.Size = new Size(103, 23);
            this.btnTwitchLogin.TabIndex = 0;
            this.btnTwitchLogin.Text = "1. Login Twitch";
            this.btnTwitchLogin.UseVisualStyleBackColor = true;
            this.btnTwitchLogin.Click += btnTwitchLogin_Click;
            // 
            // txtTwitchLogin
            // 
            this.txtTwitchLogin.Location = new Point(196, 22);
            this.txtTwitchLogin.Name = "txtTwitchLogin";
            this.txtTwitchLogin.ReadOnly = true;
            this.txtTwitchLogin.Size = new Size(103, 23);
            this.txtTwitchLogin.TabIndex = 3;
            // 
            // grpRedemptions
            // 
            this.grpRedemptions.Controls.Add(this.pnlRedemptionsControl);
            this.grpRedemptions.Controls.Add(this.dgvMemeRedemptions);
            this.grpRedemptions.Controls.Add(this.pnlRedemptionRewardsControl);
            this.grpRedemptions.Dock = DockStyle.Fill;
            this.grpRedemptions.Location = new Point(0, 62);
            this.grpRedemptions.Name = "grpRedemptions";
            this.grpRedemptions.Size = new Size(769, 678);
            this.grpRedemptions.TabIndex = 10;
            this.grpRedemptions.TabStop = false;
            this.grpRedemptions.Text = "Meme redemptions";
            // 
            // pnlRedemptionsControl
            // 
            this.pnlRedemptionsControl.Controls.Add(this.pnlRedemptionsClear);
            this.pnlRedemptionsControl.Dock = DockStyle.Bottom;
            this.pnlRedemptionsControl.Location = new Point(3, 638);
            this.pnlRedemptionsControl.Name = "pnlRedemptionsControl";
            this.pnlRedemptionsControl.Size = new Size(763, 37);
            this.pnlRedemptionsControl.TabIndex = 2;
            // 
            // pnlRedemptionsClear
            // 
            this.pnlRedemptionsClear.Controls.Add(this.btnRedemptionsClear);
            this.pnlRedemptionsClear.Dock = DockStyle.Right;
            this.pnlRedemptionsClear.Location = new Point(678, 0);
            this.pnlRedemptionsClear.Name = "pnlRedemptionsClear";
            this.pnlRedemptionsClear.Size = new Size(85, 37);
            this.pnlRedemptionsClear.TabIndex = 5;
            // 
            // btnRedemptionsClear
            // 
            this.btnRedemptionsClear.Location = new Point(5, 7);
            this.btnRedemptionsClear.Name = "btnRedemptionsClear";
            this.btnRedemptionsClear.Size = new Size(75, 23);
            this.btnRedemptionsClear.TabIndex = 5;
            this.btnRedemptionsClear.Text = "Clear list";
            this.btnRedemptionsClear.UseVisualStyleBackColor = true;
            this.btnRedemptionsClear.Click += btnRedemptionsClear_Click;
            // 
            // dgvMemeRedemptions
            // 
            this.dgvMemeRedemptions.AllowUserToAddRows = false;
            this.dgvMemeRedemptions.AllowUserToDeleteRows = false;
            this.dgvMemeRedemptions.AllowUserToResizeRows = false;
            this.dgvMemeRedemptions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMemeRedemptions.Columns.AddRange(new DataGridViewColumn[] { this.MemeRedemptionIdColumn, this.MemeRedemptionRewardIdColumn, this.MemeRedemptionStatusColumn, this.MemeRedemptionTimeColumn, this.MemeRedemptionTwitchUsernameColumn, this.MemeRedemptionMemeUsernameColumn, this.MemeRedemptionMemeBonusColumn, this.MemeRedemptionRewardTitleColumn });
            this.dgvMemeRedemptions.Dock = DockStyle.Fill;
            this.dgvMemeRedemptions.Location = new Point(3, 53);
            this.dgvMemeRedemptions.MultiSelect = false;
            this.dgvMemeRedemptions.Name = "dgvMemeRedemptions";
            this.dgvMemeRedemptions.ReadOnly = true;
            this.dgvMemeRedemptions.RowHeadersVisible = false;
            this.dgvMemeRedemptions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMemeRedemptions.ShowEditingIcon = false;
            this.dgvMemeRedemptions.Size = new Size(763, 622);
            this.dgvMemeRedemptions.TabIndex = 1;
            // 
            // MemeRedemptionIdColumn
            // 
            this.MemeRedemptionIdColumn.HeaderText = "Hidden Redemption Id";
            this.MemeRedemptionIdColumn.Name = "MemeRedemptionIdColumn";
            this.MemeRedemptionIdColumn.ReadOnly = true;
            this.MemeRedemptionIdColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.MemeRedemptionIdColumn.Visible = false;
            // 
            // MemeRedemptionRewardIdColumn
            // 
            this.MemeRedemptionRewardIdColumn.HeaderText = "Hidden Reward Id";
            this.MemeRedemptionRewardIdColumn.Name = "MemeRedemptionRewardIdColumn";
            this.MemeRedemptionRewardIdColumn.ReadOnly = true;
            this.MemeRedemptionRewardIdColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.MemeRedemptionRewardIdColumn.Visible = false;
            // 
            // MemeRedemptionStatusColumn
            // 
            this.MemeRedemptionStatusColumn.HeaderText = "Status";
            this.MemeRedemptionStatusColumn.Name = "MemeRedemptionStatusColumn";
            this.MemeRedemptionStatusColumn.ReadOnly = true;
            this.MemeRedemptionStatusColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.MemeRedemptionStatusColumn.Width = 150;
            // 
            // MemeRedemptionTimeColumn
            // 
            this.MemeRedemptionTimeColumn.HeaderText = "Time";
            this.MemeRedemptionTimeColumn.Name = "MemeRedemptionTimeColumn";
            this.MemeRedemptionTimeColumn.ReadOnly = true;
            this.MemeRedemptionTimeColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            // 
            // MemeRedemptionTwitchUsernameColumn
            // 
            this.MemeRedemptionTwitchUsernameColumn.HeaderText = "Twitch Username";
            this.MemeRedemptionTwitchUsernameColumn.Name = "MemeRedemptionTwitchUsernameColumn";
            this.MemeRedemptionTwitchUsernameColumn.ReadOnly = true;
            this.MemeRedemptionTwitchUsernameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.MemeRedemptionTwitchUsernameColumn.Width = 140;
            // 
            // MemeRedemptionMemeUsernameColumn
            // 
            this.MemeRedemptionMemeUsernameColumn.HeaderText = "Meme Username";
            this.MemeRedemptionMemeUsernameColumn.Name = "MemeRedemptionMemeUsernameColumn";
            this.MemeRedemptionMemeUsernameColumn.ReadOnly = true;
            this.MemeRedemptionMemeUsernameColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.MemeRedemptionMemeUsernameColumn.Width = 140;
            // 
            // MemeRedemptionMemeBonusColumn
            // 
            this.MemeRedemptionMemeBonusColumn.HeaderText = "Bonus";
            this.MemeRedemptionMemeBonusColumn.Name = "MemeRedemptionMemeBonusColumn";
            this.MemeRedemptionMemeBonusColumn.ReadOnly = true;
            this.MemeRedemptionMemeBonusColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.MemeRedemptionMemeBonusColumn.Width = 50;
            // 
            // MemeRedemptionRewardTitleColumn
            // 
            this.MemeRedemptionRewardTitleColumn.HeaderText = "Title";
            this.MemeRedemptionRewardTitleColumn.Name = "MemeRedemptionRewardTitleColumn";
            this.MemeRedemptionRewardTitleColumn.ReadOnly = true;
            this.MemeRedemptionRewardTitleColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            this.MemeRedemptionRewardTitleColumn.Width = 180;
            // 
            // pnlRedemptionRewardsControl
            // 
            this.pnlRedemptionRewardsControl.Controls.Add(this.btnTwitchRewardsEdit);
            this.pnlRedemptionRewardsControl.Controls.Add(this.lblRedemptionsStatus);
            this.pnlRedemptionRewardsControl.Controls.Add(this.btnMemeRewardCreate);
            this.pnlRedemptionRewardsControl.Dock = DockStyle.Top;
            this.pnlRedemptionRewardsControl.Location = new Point(3, 19);
            this.pnlRedemptionRewardsControl.Name = "pnlRedemptionRewardsControl";
            this.pnlRedemptionRewardsControl.Size = new Size(763, 34);
            this.pnlRedemptionRewardsControl.TabIndex = 0;
            // 
            // btnTwitchRewardsEdit
            // 
            this.btnTwitchRewardsEdit.Enabled = false;
            this.btnTwitchRewardsEdit.Location = new Point(179, 1);
            this.btnTwitchRewardsEdit.Name = "btnTwitchRewardsEdit";
            this.btnTwitchRewardsEdit.Size = new Size(202, 23);
            this.btnTwitchRewardsEdit.TabIndex = 3;
            this.btnTwitchRewardsEdit.Text = "4. Open twitch rewards settings";
            this.btnTwitchRewardsEdit.UseVisualStyleBackColor = true;
            this.btnTwitchRewardsEdit.Click += btnTwitchRewardsEdit_Click;
            // 
            // lblRedemptionsStatus
            // 
            this.lblRedemptionsStatus.AutoSize = true;
            this.lblRedemptionsStatus.Dock = DockStyle.Right;
            this.lblRedemptionsStatus.Location = new Point(600, 0);
            this.lblRedemptionsStatus.Name = "lblRedemptionsStatus";
            this.lblRedemptionsStatus.Padding = new Padding(0, 4, 0, 0);
            this.lblRedemptionsStatus.Size = new Size(163, 19);
            this.lblRedemptionsStatus.TabIndex = 2;
            this.lblRedemptionsStatus.Text = "Status: awaiting authorization";
            // 
            // btnMemeRewardCreate
            // 
            this.btnMemeRewardCreate.Enabled = false;
            this.btnMemeRewardCreate.Location = new Point(9, 1);
            this.btnMemeRewardCreate.Name = "btnMemeRewardCreate";
            this.btnMemeRewardCreate.Size = new Size(164, 23);
            this.btnMemeRewardCreate.TabIndex = 1;
            this.btnMemeRewardCreate.Text = "3. Create meme reward";
            this.btnMemeRewardCreate.UseVisualStyleBackColor = true;
            this.btnMemeRewardCreate.Click += btnMemeRewardCreate_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(769, 740);
            Controls.Add(this.grpRedemptions);
            Controls.Add(this.pnlAuth);
            MinimumSize = new Size(640, 0);
            Name = "MainForm";
            Text = "twitch-leon-script";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_LoadAsync;
            this.pnlAuth.ResumeLayout(false);
            this.grpMemeAuth.ResumeLayout(false);
            this.grpMemeAuth.PerformLayout();
            this.grpTwitchAuth.ResumeLayout(false);
            this.grpTwitchAuth.PerformLayout();
            this.grpRedemptions.ResumeLayout(false);
            this.pnlRedemptionsControl.ResumeLayout(false);
            this.pnlRedemptionsClear.ResumeLayout(false);
            ((ISupportInitialize)this.dgvMemeRedemptions).EndInit();
            this.pnlRedemptionRewardsControl.ResumeLayout(false);
            this.pnlRedemptionRewardsControl.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpMemeLogin;
        private TextBox loginMemeTextBox;
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
