namespace Smoke.Genghis
{
    partial class inputForm
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
            this.chosenRunnerIdTextBox = new System.Windows.Forms.TextBox();
            this.chosenOddsTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.matchIdTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.getMarketsButton = new System.Windows.Forms.Button();
            this.chosenAmountTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.budgetTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.expectedEndingDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chosen Runner Id";
            // 
            // chosenRunnerIdTextBox
            // 
            this.chosenRunnerIdTextBox.Location = new System.Drawing.Point(106, 38);
            this.chosenRunnerIdTextBox.Name = "chosenRunnerIdTextBox";
            this.chosenRunnerIdTextBox.Size = new System.Drawing.Size(164, 20);
            this.chosenRunnerIdTextBox.TabIndex = 2;
            // 
            // chosenOddsTextBox
            // 
            this.chosenOddsTextBox.Location = new System.Drawing.Point(106, 64);
            this.chosenOddsTextBox.Name = "chosenOddsTextBox";
            this.chosenOddsTextBox.Size = new System.Drawing.Size(164, 20);
            this.chosenOddsTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Chosen Odds";
            // 
            // matchIdTextBox
            // 
            this.matchIdTextBox.Location = new System.Drawing.Point(106, 12);
            this.matchIdTextBox.Name = "matchIdTextBox";
            this.matchIdTextBox.Size = new System.Drawing.Size(164, 20);
            this.matchIdTextBox.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Match Id";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(189, 179);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(81, 35);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // getMarketsButton
            // 
            this.getMarketsButton.Location = new System.Drawing.Point(12, 179);
            this.getMarketsButton.Name = "getMarketsButton";
            this.getMarketsButton.Size = new System.Drawing.Size(81, 35);
            this.getMarketsButton.TabIndex = 7;
            this.getMarketsButton.Text = "Get Markets";
            this.getMarketsButton.UseVisualStyleBackColor = true;
            this.getMarketsButton.Click += new System.EventHandler(this.getMarketsButton_Click);
            // 
            // chosenAmountTextBox
            // 
            this.chosenAmountTextBox.Location = new System.Drawing.Point(106, 90);
            this.chosenAmountTextBox.Name = "chosenAmountTextBox";
            this.chosenAmountTextBox.Size = new System.Drawing.Size(164, 20);
            this.chosenAmountTextBox.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Chosen Amount";
            // 
            // budgetTextBox
            // 
            this.budgetTextBox.Location = new System.Drawing.Point(106, 116);
            this.budgetTextBox.Name = "budgetTextBox";
            this.budgetTextBox.Size = new System.Drawing.Size(164, 20);
            this.budgetTextBox.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Budget";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Exp. End Time";
            // 
            // expectedEndingDateTimePicker
            // 
            this.expectedEndingDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.expectedEndingDateTimePicker.Location = new System.Drawing.Point(106, 142);
            this.expectedEndingDateTimePicker.Name = "expectedEndingDateTimePicker";
            this.expectedEndingDateTimePicker.Size = new System.Drawing.Size(164, 20);
            this.expectedEndingDateTimePicker.TabIndex = 14;
            // 
            // inputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 220);
            this.Controls.Add(this.expectedEndingDateTimePicker);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.budgetTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chosenAmountTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.getMarketsButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.matchIdTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chosenOddsTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chosenRunnerIdTextBox);
            this.Controls.Add(this.label1);
            this.Name = "inputForm";
            this.Text = "Genghis";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox chosenRunnerIdTextBox;
        private System.Windows.Forms.TextBox chosenOddsTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox matchIdTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button getMarketsButton;
        private System.Windows.Forms.TextBox chosenAmountTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox budgetTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker expectedEndingDateTimePicker;
    }
}

