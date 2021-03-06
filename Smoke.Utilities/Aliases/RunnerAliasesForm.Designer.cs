﻿namespace Smoke.Utilities.Aliases
{
    partial class RunnerAliasesForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.newRunnerNameTextBox = new System.Windows.Forms.TextBox();
            this.existingRunnersListBox = new System.Windows.Forms.ListBox();
            this.addToExistingRunnersButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.selectedRunnerAliasesListBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.addAliasButton = new System.Windows.Forms.Button();
            this.newAliasTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "New Runner";
            // 
            // newRunnerNameTextBox
            // 
            this.newRunnerNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newRunnerNameTextBox.Location = new System.Drawing.Point(91, 11);
            this.newRunnerNameTextBox.Name = "newRunnerNameTextBox";
            this.newRunnerNameTextBox.Size = new System.Drawing.Size(257, 20);
            this.newRunnerNameTextBox.TabIndex = 1;
            // 
            // existingRunnersListBox
            // 
            this.existingRunnersListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.existingRunnersListBox.FormattingEnabled = true;
            this.existingRunnersListBox.Location = new System.Drawing.Point(15, 50);
            this.existingRunnersListBox.Name = "existingRunnersListBox";
            this.existingRunnersListBox.Size = new System.Drawing.Size(469, 121);
            this.existingRunnersListBox.TabIndex = 2;
            this.existingRunnersListBox.SelectedIndexChanged += new System.EventHandler(this.existingRunnersListBox_SelectedIndexChanged);
            // 
            // addToExistingRunnersButton
            // 
            this.addToExistingRunnersButton.Location = new System.Drawing.Point(354, 8);
            this.addToExistingRunnersButton.Name = "addToExistingRunnersButton";
            this.addToExistingRunnersButton.Size = new System.Drawing.Size(137, 25);
            this.addToExistingRunnersButton.TabIndex = 3;
            this.addToExistingRunnersButton.Text = "Add to Existing Runners";
            this.addToExistingRunnersButton.UseVisualStyleBackColor = true;
            this.addToExistingRunnersButton.Click += new System.EventHandler(this.addToExistingRunnersButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Existing Runners";
            // 
            // selectedRunnerAliasesListBox
            // 
            this.selectedRunnerAliasesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedRunnerAliasesListBox.FormattingEnabled = true;
            this.selectedRunnerAliasesListBox.Location = new System.Drawing.Point(12, 227);
            this.selectedRunnerAliasesListBox.Name = "selectedRunnerAliasesListBox";
            this.selectedRunnerAliasesListBox.Size = new System.Drawing.Size(472, 121);
            this.selectedRunnerAliasesListBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 211);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Selected Runner Aliases";
            // 
            // addAliasButton
            // 
            this.addAliasButton.Location = new System.Drawing.Point(329, 184);
            this.addAliasButton.Name = "addAliasButton";
            this.addAliasButton.Size = new System.Drawing.Size(155, 23);
            this.addAliasButton.TabIndex = 7;
            this.addAliasButton.Text = "Add Alias to Selected Runner";
            this.addAliasButton.UseVisualStyleBackColor = true;
            this.addAliasButton.Click += new System.EventHandler(this.addAliasButton_Click);
            // 
            // newAliasTextBox
            // 
            this.newAliasTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newAliasTextBox.Location = new System.Drawing.Point(91, 186);
            this.newAliasTextBox.Name = "newAliasTextBox";
            this.newAliasTextBox.Size = new System.Drawing.Size(232, 20);
            this.newAliasTextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "New Alias";
            // 
            // RunnerAliasesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 369);
            this.Controls.Add(this.newAliasTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.addAliasButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.selectedRunnerAliasesListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.addToExistingRunnersButton);
            this.Controls.Add(this.existingRunnersListBox);
            this.Controls.Add(this.newRunnerNameTextBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "RunnerAliasesForm";
            this.Text = "Runner Aliases";
            this.Load += new System.EventHandler(this.RunnerAliasesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newRunnerNameTextBox;
        private System.Windows.Forms.ListBox existingRunnersListBox;
        private System.Windows.Forms.Button addToExistingRunnersButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox selectedRunnerAliasesListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button addAliasButton;
        private System.Windows.Forms.TextBox newAliasTextBox;
        private System.Windows.Forms.Label label4;
    }
}