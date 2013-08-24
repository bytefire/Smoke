namespace Smoke.Test
{
    partial class Form1
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
            this.betfairServiceTest = new System.Windows.Forms.Button();
            this.priceWatcherTestButton = new System.Windows.Forms.Button();
            this.placeBetButton = new System.Windows.Forms.Button();
            this.wcfTestButton = new System.Windows.Forms.Button();
            this.scoreProTestButton = new System.Windows.Forms.Button();
            this.manageRunnerAliasesButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // betfairServiceTest
            // 
            this.betfairServiceTest.Location = new System.Drawing.Point(124, 109);
            this.betfairServiceTest.Name = "betfairServiceTest";
            this.betfairServiceTest.Size = new System.Drawing.Size(75, 23);
            this.betfairServiceTest.TabIndex = 0;
            this.betfairServiceTest.Text = "BF Svc Test";
            this.betfairServiceTest.UseVisualStyleBackColor = true;
            this.betfairServiceTest.Click += new System.EventHandler(this.betfairServiceTest_Click);
            // 
            // priceWatcherTestButton
            // 
            this.priceWatcherTestButton.Location = new System.Drawing.Point(109, 12);
            this.priceWatcherTestButton.Name = "priceWatcherTestButton";
            this.priceWatcherTestButton.Size = new System.Drawing.Size(106, 23);
            this.priceWatcherTestButton.TabIndex = 1;
            this.priceWatcherTestButton.Text = "PriceWatcher Test";
            this.priceWatcherTestButton.UseVisualStyleBackColor = true;
            this.priceWatcherTestButton.Click += new System.EventHandler(this.priceWatcherTestButton_Click);
            // 
            // placeBetButton
            // 
            this.placeBetButton.Location = new System.Drawing.Point(12, 67);
            this.placeBetButton.Name = "placeBetButton";
            this.placeBetButton.Size = new System.Drawing.Size(94, 23);
            this.placeBetButton.TabIndex = 2;
            this.placeBetButton.Text = "Place Bet Test";
            this.placeBetButton.UseVisualStyleBackColor = true;
            this.placeBetButton.Click += new System.EventHandler(this.placeBetButton_Click);
            // 
            // wcfTestButton
            // 
            this.wcfTestButton.Location = new System.Drawing.Point(124, 67);
            this.wcfTestButton.Name = "wcfTestButton";
            this.wcfTestButton.Size = new System.Drawing.Size(91, 23);
            this.wcfTestButton.TabIndex = 3;
            this.wcfTestButton.Text = "WCF Test";
            this.wcfTestButton.UseVisualStyleBackColor = true;
            this.wcfTestButton.Click += new System.EventHandler(this.wcfTestButton_Click);
            // 
            // scoreProTestButton
            // 
            this.scoreProTestButton.Location = new System.Drawing.Point(12, 109);
            this.scoreProTestButton.Name = "scoreProTestButton";
            this.scoreProTestButton.Size = new System.Drawing.Size(94, 23);
            this.scoreProTestButton.TabIndex = 4;
            this.scoreProTestButton.Text = "ScoresPro Test";
            this.scoreProTestButton.UseVisualStyleBackColor = true;
            this.scoreProTestButton.Click += new System.EventHandler(this.scoreProTestButton_Click);
            // 
            // manageRunnerAliasesButton
            // 
            this.manageRunnerAliasesButton.Location = new System.Drawing.Point(9, 12);
            this.manageRunnerAliasesButton.Name = "manageRunnerAliasesButton";
            this.manageRunnerAliasesButton.Size = new System.Drawing.Size(94, 36);
            this.manageRunnerAliasesButton.TabIndex = 5;
            this.manageRunnerAliasesButton.Text = "Manage Runner Aliases";
            this.manageRunnerAliasesButton.UseVisualStyleBackColor = true;
            this.manageRunnerAliasesButton.Click += new System.EventHandler(this.manageRunnerAliasesButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.manageRunnerAliasesButton);
            this.Controls.Add(this.scoreProTestButton);
            this.Controls.Add(this.wcfTestButton);
            this.Controls.Add(this.placeBetButton);
            this.Controls.Add(this.priceWatcherTestButton);
            this.Controls.Add(this.betfairServiceTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button betfairServiceTest;
        private System.Windows.Forms.Button priceWatcherTestButton;
        private System.Windows.Forms.Button placeBetButton;
        private System.Windows.Forms.Button wcfTestButton;
        private System.Windows.Forms.Button scoreProTestButton;
        private System.Windows.Forms.Button manageRunnerAliasesButton;
    }
}

