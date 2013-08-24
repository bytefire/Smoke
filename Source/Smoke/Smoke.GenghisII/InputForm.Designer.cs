namespace Smoke.GenghisII
{
    partial class InputForm
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
            this.startingDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.budgetTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chosenAmountTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.getMarketsButton = new System.Windows.Forms.Button();
            this.matchIdTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chosenOddsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chosenRunnerIdTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startConsoleButton = new System.Windows.Forms.Button();
            this.manageRunnerAliasesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startingDateTimePicker
            // 
            this.startingDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.startingDateTimePicker.Location = new System.Drawing.Point(111, 136);
            this.startingDateTimePicker.Name = "startingDateTimePicker";
            this.startingDateTimePicker.Size = new System.Drawing.Size(164, 20);
            this.startingDateTimePicker.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Starting Time";
            // 
            // budgetTextBox
            // 
            this.budgetTextBox.Location = new System.Drawing.Point(111, 110);
            this.budgetTextBox.Name = "budgetTextBox";
            this.budgetTextBox.Size = new System.Drawing.Size(164, 20);
            this.budgetTextBox.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Budget";
            // 
            // chosenAmountTextBox
            // 
            this.chosenAmountTextBox.Location = new System.Drawing.Point(111, 84);
            this.chosenAmountTextBox.Name = "chosenAmountTextBox";
            this.chosenAmountTextBox.Size = new System.Drawing.Size(164, 20);
            this.chosenAmountTextBox.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Chosen Amount";
            // 
            // getMarketsButton
            // 
            this.getMarketsButton.Location = new System.Drawing.Point(17, 173);
            this.getMarketsButton.Name = "getMarketsButton";
            this.getMarketsButton.Size = new System.Drawing.Size(81, 35);
            this.getMarketsButton.TabIndex = 22;
            this.getMarketsButton.Text = "Get Markets";
            this.getMarketsButton.UseVisualStyleBackColor = true;
            this.getMarketsButton.Click += new System.EventHandler(this.getMarketsButton_Click);
            // 
            // matchIdTextBox
            // 
            this.matchIdTextBox.Location = new System.Drawing.Point(111, 6);
            this.matchIdTextBox.Name = "matchIdTextBox";
            this.matchIdTextBox.Size = new System.Drawing.Size(164, 20);
            this.matchIdTextBox.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Match Id";
            // 
            // chosenOddsTextBox
            // 
            this.chosenOddsTextBox.Location = new System.Drawing.Point(111, 58);
            this.chosenOddsTextBox.Name = "chosenOddsTextBox";
            this.chosenOddsTextBox.Size = new System.Drawing.Size(164, 20);
            this.chosenOddsTextBox.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Chosen Odds";
            // 
            // chosenRunnerIdTextBox
            // 
            this.chosenRunnerIdTextBox.Location = new System.Drawing.Point(111, 32);
            this.chosenRunnerIdTextBox.Name = "chosenRunnerIdTextBox";
            this.chosenRunnerIdTextBox.Size = new System.Drawing.Size(164, 20);
            this.chosenRunnerIdTextBox.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Chosen Runner Id";
            // 
            // startConsoleButton
            // 
            this.startConsoleButton.Location = new System.Drawing.Point(200, 173);
            this.startConsoleButton.Name = "startConsoleButton";
            this.startConsoleButton.Size = new System.Drawing.Size(75, 35);
            this.startConsoleButton.TabIndex = 29;
            this.startConsoleButton.Text = "Start Console";
            this.startConsoleButton.UseVisualStyleBackColor = true;
            this.startConsoleButton.Click += new System.EventHandler(this.startConsoleButton_Click);
            // 
            // manageRunnerAliasesButton
            // 
            this.manageRunnerAliasesButton.Location = new System.Drawing.Point(15, 258);
            this.manageRunnerAliasesButton.Name = "manageRunnerAliasesButton";
            this.manageRunnerAliasesButton.Size = new System.Drawing.Size(133, 23);
            this.manageRunnerAliasesButton.TabIndex = 30;
            this.manageRunnerAliasesButton.Text = "Manage Runner Aliases";
            this.manageRunnerAliasesButton.UseVisualStyleBackColor = true;
            this.manageRunnerAliasesButton.Click += new System.EventHandler(this.manageRunnerAliasesButton_Click);
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 293);
            this.Controls.Add(this.manageRunnerAliasesButton);
            this.Controls.Add(this.startConsoleButton);
            this.Controls.Add(this.startingDateTimePicker);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.budgetTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chosenAmountTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.getMarketsButton);
            this.Controls.Add(this.matchIdTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chosenOddsTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chosenRunnerIdTextBox);
            this.Controls.Add(this.label1);
            this.Name = "InputForm";
            this.Text = "Genghis II";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker startingDateTimePicker;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox budgetTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox chosenAmountTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button getMarketsButton;
        private System.Windows.Forms.TextBox matchIdTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox chosenOddsTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox chosenRunnerIdTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button startConsoleButton;
        private System.Windows.Forms.Button manageRunnerAliasesButton;
    }
}

