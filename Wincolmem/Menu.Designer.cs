﻿namespace Wincolmem
{
    partial class Menu
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
            this.components = new System.ComponentModel.Container();
            this.MemoryLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.HiScoresButton = new System.Windows.Forms.Button();
            this.QuitButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.mainMenuTimer = new System.Windows.Forms.Timer(this.components);
            this.onGameTimer = new System.Windows.Forms.Timer(this.components);
            this.timeLabel = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pointsText = new System.Windows.Forms.TextBox();
            this.nameText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MemoryLabel
            // 
            this.MemoryLabel.BackColor = System.Drawing.Color.Transparent;
            this.MemoryLabel.Location = new System.Drawing.Point(38, 88);
            this.MemoryLabel.Margin = new System.Windows.Forms.Padding(0);
            this.MemoryLabel.Name = "MemoryLabel";
            this.MemoryLabel.Size = new System.Drawing.Size(242, 65);
            this.MemoryLabel.TabIndex = 0;
            this.MemoryLabel.Text = "Memory";
            this.MemoryLabel.UseCompatibleTextRendering = true;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(97, 242);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(122, 40);
            this.StartButton.TabIndex = 1;
            this.StartButton.Text = "Start";
            this.StartButton.UseCompatibleTextRendering = true;
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // HiScoresButton
            // 
            this.HiScoresButton.Location = new System.Drawing.Point(97, 298);
            this.HiScoresButton.Name = "HiScoresButton";
            this.HiScoresButton.Size = new System.Drawing.Size(122, 40);
            this.HiScoresButton.TabIndex = 2;
            this.HiScoresButton.Text = "Hi-Score";
            this.HiScoresButton.UseCompatibleTextRendering = true;
            this.HiScoresButton.UseVisualStyleBackColor = true;
            this.HiScoresButton.Click += new System.EventHandler(this.HiScoresButton_Click);
            // 
            // QuitButton
            // 
            this.QuitButton.Location = new System.Drawing.Point(97, 357);
            this.QuitButton.Name = "QuitButton";
            this.QuitButton.Size = new System.Drawing.Size(122, 40);
            this.QuitButton.TabIndex = 3;
            this.QuitButton.Text = "Quit";
            this.QuitButton.UseCompatibleTextRendering = true;
            this.QuitButton.UseVisualStyleBackColor = true;
            this.QuitButton.Click += new System.EventHandler(this.QuitButton_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(61, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 56);
            this.label1.TabIndex = 4;
            this.label1.Text = "C";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label1.UseCompatibleTextRendering = true;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(131, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 56);
            this.label3.TabIndex = 6;
            this.label3.Text = "l";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label3.UseCompatibleTextRendering = true;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(96, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 56);
            this.label4.TabIndex = 7;
            this.label4.Text = "o";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label4.UseCompatibleTextRendering = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.Purple;
            this.label5.Location = new System.Drawing.Point(189, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 56);
            this.label5.TabIndex = 8;
            this.label5.Text = "u";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label5.UseCompatibleTextRendering = true;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Cyan;
            this.label6.Location = new System.Drawing.Point(224, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 56);
            this.label6.TabIndex = 9;
            this.label6.Text = "r";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label6.UseCompatibleTextRendering = true;
            // 
            // mainMenuTimer
            // 
            this.mainMenuTimer.Enabled = true;
            this.mainMenuTimer.Tick += new System.EventHandler(this.mainMenuTimer_Tick);
            // 
            // onGameTimer
            // 
            this.onGameTimer.Tick += new System.EventHandler(this.onGameTimer_Tick);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(125, 1);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(33, 17);
            this.timeLabel.TabIndex = 11;
            this.timeLabel.Text = "00:00";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.timeLabel.UseCompatibleTextRendering = true;
            this.timeLabel.Visible = false;
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Location = new System.Drawing.Point(12, 1);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(31, 17);
            this.levelLabel.TabIndex = 12;
            this.levelLabel.Text = "Level";
            this.levelLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.levelLabel.UseCompatibleTextRendering = true;
            this.levelLabel.Visible = false;
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Location = new System.Drawing.Point(236, 1);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(34, 17);
            this.scoreLabel.TabIndex = 13;
            this.scoreLabel.Text = "Score";
            this.scoreLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.scoreLabel.UseCompatibleTextRendering = true;
            this.scoreLabel.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(150, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 56);
            this.label2.TabIndex = 14;
            this.label2.Text = "o";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.label2.UseCompatibleTextRendering = true;
            // 
            // pointsText
            // 
            this.pointsText.BackColor = System.Drawing.SystemColors.Control;
            this.pointsText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pointsText.Enabled = false;
            this.pointsText.Location = new System.Drawing.Point(49, 162);
            this.pointsText.Multiline = true;
            this.pointsText.Name = "pointsText";
            this.pointsText.Size = new System.Drawing.Size(95, 116);
            this.pointsText.TabIndex = 16;
            this.pointsText.Text = "pointsText";
            this.pointsText.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.pointsText.Visible = false;
            // 
            // nameText
            // 
            this.nameText.BackColor = System.Drawing.SystemColors.Control;
            this.nameText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nameText.Enabled = false;
            this.nameText.Location = new System.Drawing.Point(172, 162);
            this.nameText.Multiline = true;
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(95, 116);
            this.nameText.TabIndex = 17;
            this.nameText.Text = "nameText";
            this.nameText.Visible = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(317, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.QuitButton);
            this.Controls.Add(this.HiScoresButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.MemoryLabel);
            this.Controls.Add(this.pointsText);
            this.Controls.Add(this.nameText);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Menu";
            this.Text = "Colour Memory for Windows";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MemoryLabel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button HiScoresButton;
        private System.Windows.Forms.Button QuitButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pointsText;
        private System.Windows.Forms.TextBox nameText;
    }
}

