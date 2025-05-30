using TwitchLeonScript.WinForms.Properties;

namespace TwitchLeonScript.WinForms.Forms
{
    partial class RewardCreateForm
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
            this.txtTitle = new TextBox();
            this.lblTitle = new Label();
            this.lblPrompt = new Label();
            this.rtbPrompt = new RichTextBox();
            this.lblTwitchPoints = new Label();
            this.nudTwitchPoints = new NumericUpDown();
            this.btnCreateMemeReward = new Button();
            this.lblMemeBonus = new Label();
            this.nudBonus = new NumericUpDown();
            this.picReward112 = new PictureBox();
            this.picReward56 = new PictureBox();
            this.picReward28 = new PictureBox();
            this.lblEditInfo = new Label();
            this.lbTitleUniqueInfo = new Label();
            ((System.ComponentModel.ISupportInitialize)this.nudTwitchPoints).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.nudBonus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.picReward112).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.picReward56).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.picReward28).BeginInit();
            SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new Point(12, 170);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new Size(432, 23);
            this.txtTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new Point(12, 150);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(30, 15);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Title";
            // 
            // lblPrompt
            // 
            this.lblPrompt.AutoSize = true;
            this.lblPrompt.Location = new Point(13, 201);
            this.lblPrompt.Name = "lblPrompt";
            this.lblPrompt.Size = new Size(47, 15);
            this.lblPrompt.TabIndex = 2;
            this.lblPrompt.Text = "Prompt";
            // 
            // rtbPrompt
            // 
            this.rtbPrompt.Location = new Point(13, 220);
            this.rtbPrompt.Name = "rtbPrompt";
            this.rtbPrompt.Size = new Size(431, 141);
            this.rtbPrompt.TabIndex = 3;
            this.rtbPrompt.Text = "";
            // 
            // lblTwitchPoints
            // 
            this.lblTwitchPoints.AutoSize = true;
            this.lblTwitchPoints.Location = new Point(13, 369);
            this.lblTwitchPoints.Name = "lblTwitchPoints";
            this.lblTwitchPoints.Size = new Size(78, 15);
            this.lblTwitchPoints.TabIndex = 4;
            this.lblTwitchPoints.Text = "Twitch points";
            // 
            // nudTwitchPoints
            // 
            this.nudTwitchPoints.Increment = new decimal(new int[] { 50, 0, 0, 0 });
            this.nudTwitchPoints.Location = new Point(13, 388);
            this.nudTwitchPoints.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudTwitchPoints.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudTwitchPoints.Name = "nudTwitchPoints";
            this.nudTwitchPoints.Size = new Size(210, 23);
            this.nudTwitchPoints.TabIndex = 5;
            this.nudTwitchPoints.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // btnCreateMemeReward
            // 
            this.btnCreateMemeReward.Location = new Point(12, 445);
            this.btnCreateMemeReward.Name = "btnCreateMemeReward";
            this.btnCreateMemeReward.Size = new Size(432, 37);
            this.btnCreateMemeReward.TabIndex = 6;
            this.btnCreateMemeReward.Text = "Create meme reward";
            this.btnCreateMemeReward.UseVisualStyleBackColor = true;
            this.btnCreateMemeReward.Click += btnCreateMemeReward_Click;
            // 
            // lblMemeBonus
            // 
            this.lblMemeBonus.AutoSize = true;
            this.lblMemeBonus.Location = new Point(233, 369);
            this.lblMemeBonus.Name = "lblMemeBonus";
            this.lblMemeBonus.Size = new Size(77, 15);
            this.lblMemeBonus.TabIndex = 7;
            this.lblMemeBonus.Text = "Meme bonus";
            // 
            // nudBonus
            // 
            this.nudBonus.Location = new Point(233, 387);
            this.nudBonus.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            this.nudBonus.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudBonus.Name = "nudBonus";
            this.nudBonus.Size = new Size(210, 23);
            this.nudBonus.TabIndex = 8;
            this.nudBonus.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // picReward112
            // 
            this.picReward112.Image = Resources.meme_112;
            this.picReward112.Location = new Point(125, 22);
            this.picReward112.Name = "picReward112";
            this.picReward112.Size = new Size(112, 112);
            this.picReward112.SizeMode = PictureBoxSizeMode.AutoSize;
            this.picReward112.TabIndex = 9;
            this.picReward112.TabStop = false;
            // 
            // picReward56
            // 
            this.picReward56.Image = Resources.meme_56;
            this.picReward56.Location = new Point(243, 22);
            this.picReward56.Name = "picReward56";
            this.picReward56.Size = new Size(56, 56);
            this.picReward56.SizeMode = PictureBoxSizeMode.AutoSize;
            this.picReward56.TabIndex = 10;
            this.picReward56.TabStop = false;
            // 
            // picReward28
            // 
            this.picReward28.Image = Resources.meme_28;
            this.picReward28.Location = new Point(305, 22);
            this.picReward28.Name = "picReward28";
            this.picReward28.Size = new Size(28, 28);
            this.picReward28.SizeMode = PictureBoxSizeMode.AutoSize;
            this.picReward28.TabIndex = 11;
            this.picReward28.TabStop = false;
            // 
            // lblEditInfo
            // 
            this.lblEditInfo.AutoSize = true;
            this.lblEditInfo.ForeColor = Color.Navy;
            this.lblEditInfo.Location = new Point(23, 427);
            this.lblEditInfo.Name = "lblEditInfo";
            this.lblEditInfo.Size = new Size(410, 15);
            this.lblEditInfo.TabIndex = 12;
            this.lblEditInfo.Text = "You can change the image or other settings on Twitch website after creation.";
            this.lblEditInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbTitleUniqueInfo
            // 
            this.lbTitleUniqueInfo.AutoSize = true;
            this.lbTitleUniqueInfo.ForeColor = Color.Red;
            this.lbTitleUniqueInfo.Location = new Point(39, 150);
            this.lbTitleUniqueInfo.Name = "lbTitleUniqueInfo";
            this.lbTitleUniqueInfo.Size = new Size(98, 15);
            this.lbTitleUniqueInfo.TabIndex = 13;
            this.lbTitleUniqueInfo.Text = "(must be unique)";
            // 
            // RewardCreateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(456, 495);
            Controls.Add(this.lbTitleUniqueInfo);
            Controls.Add(this.lblEditInfo);
            Controls.Add(this.picReward28);
            Controls.Add(this.picReward56);
            Controls.Add(this.picReward112);
            Controls.Add(this.nudBonus);
            Controls.Add(this.lblMemeBonus);
            Controls.Add(this.btnCreateMemeReward);
            Controls.Add(this.nudTwitchPoints);
            Controls.Add(this.lblTwitchPoints);
            Controls.Add(this.rtbPrompt);
            Controls.Add(this.lblPrompt);
            Controls.Add(this.lblTitle);
            Controls.Add(this.txtTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "RewardCreateForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Create meme reward";
            Load += RewardCreateForm_Load;
            ((System.ComponentModel.ISupportInitialize)this.nudTwitchPoints).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.nudBonus).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.picReward112).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.picReward56).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.picReward28).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtTitle;
        private Label lblTitle;
        private Label lblPrompt;
        private RichTextBox rtbPrompt;
        private Label lblTwitchPoints;
        private NumericUpDown nudTwitchPoints;
        private Button btnCreateMemeReward;
        private Label lblMemeBonus;
        private NumericUpDown nudBonus;
        private PictureBox picReward112;
        private PictureBox picReward56;
        private PictureBox picReward28;
        private Label lblEditInfo;
        private Label lbTitleUniqueInfo;
    }
}