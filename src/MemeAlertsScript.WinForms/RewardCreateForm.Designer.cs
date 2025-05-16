namespace MemeAlertsScript.WinForms
{
    partial class CreateRewardForm
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
            nameTextBox = new TextBox();
            nameLabel = new Label();
            promptLabel = new Label();
            promptRichTextBox = new RichTextBox();
            twitchCostLabel = new Label();
            twitchCostNumericUpDown = new NumericUpDown();
            createRewardButton = new Button();
            memeCostLabel = new Label();
            memeCostNumericUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)twitchCostNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)memeCostNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(12, 31);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(432, 23);
            nameTextBox.TabIndex = 0;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(12, 11);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(39, 15);
            nameLabel.TabIndex = 1;
            nameLabel.Text = "Name";
            // 
            // promptLabel
            // 
            promptLabel.AutoSize = true;
            promptLabel.Location = new Point(13, 62);
            promptLabel.Name = "promptLabel";
            promptLabel.Size = new Size(47, 15);
            promptLabel.TabIndex = 2;
            promptLabel.Text = "Prompt";
            // 
            // promptRichTextBox
            // 
            promptRichTextBox.Location = new Point(13, 81);
            promptRichTextBox.Name = "promptRichTextBox";
            promptRichTextBox.Size = new Size(431, 141);
            promptRichTextBox.TabIndex = 3;
            promptRichTextBox.Text = "";
            // 
            // twitchCostLabel
            // 
            twitchCostLabel.AutoSize = true;
            twitchCostLabel.Location = new Point(13, 230);
            twitchCostLabel.Name = "twitchCostLabel";
            twitchCostLabel.Size = new Size(67, 15);
            twitchCostLabel.TabIndex = 4;
            twitchCostLabel.Text = "Twitch cost";
            // 
            // twitchCostNumericUpDown
            // 
            twitchCostNumericUpDown.Increment = new decimal(new int[] { 100, 0, 0, 0 });
            twitchCostNumericUpDown.Location = new Point(13, 249);
            twitchCostNumericUpDown.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            twitchCostNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            twitchCostNumericUpDown.Name = "twitchCostNumericUpDown";
            twitchCostNumericUpDown.Size = new Size(210, 23);
            twitchCostNumericUpDown.TabIndex = 5;
            twitchCostNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // createRewardButton
            // 
            createRewardButton.Location = new Point(12, 287);
            createRewardButton.Name = "createRewardButton";
            createRewardButton.Size = new Size(432, 37);
            createRewardButton.TabIndex = 6;
            createRewardButton.Text = "Create reward";
            createRewardButton.UseVisualStyleBackColor = true;
            createRewardButton.Click += createRewardButton_Click;
            // 
            // memeCostLabel
            // 
            memeCostLabel.AutoSize = true;
            memeCostLabel.Location = new Point(233, 230);
            memeCostLabel.Name = "memeCostLabel";
            memeCostLabel.Size = new Size(66, 15);
            memeCostLabel.TabIndex = 7;
            memeCostLabel.Text = "Meme cost";
            // 
            // memeCostNumericUpDown
            // 
            memeCostNumericUpDown.Location = new Point(233, 248);
            memeCostNumericUpDown.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            memeCostNumericUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            memeCostNumericUpDown.Name = "memeCostNumericUpDown";
            memeCostNumericUpDown.Size = new Size(210, 23);
            memeCostNumericUpDown.TabIndex = 8;
            memeCostNumericUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // CreateRewardForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(456, 341);
            Controls.Add(memeCostNumericUpDown);
            Controls.Add(memeCostLabel);
            Controls.Add(createRewardButton);
            Controls.Add(twitchCostNumericUpDown);
            Controls.Add(twitchCostLabel);
            Controls.Add(promptRichTextBox);
            Controls.Add(promptLabel);
            Controls.Add(nameLabel);
            Controls.Add(nameTextBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "CreateRewardForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Create meme reward";
            Load += CreateRewardForm_Load;
            ((System.ComponentModel.ISupportInitialize)twitchCostNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)memeCostNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox nameTextBox;
        private Label nameLabel;
        private Label promptLabel;
        private RichTextBox promptRichTextBox;
        private Label twitchCostLabel;
        private NumericUpDown twitchCostNumericUpDown;
        private Button createRewardButton;
        private Label memeCostLabel;
        private NumericUpDown memeCostNumericUpDown;
    }
}