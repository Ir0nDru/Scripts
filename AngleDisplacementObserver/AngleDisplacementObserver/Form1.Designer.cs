namespace AngleDisplacementObserver
{
    partial class MainForm
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
            this.portLabel = new System.Windows.Forms.Label();
            this.portComboBox = new System.Windows.Forms.ComboBox();
            this.connButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.angleLabel = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.TextBox();
            this.testButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(12, 16);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(29, 13);
            this.portLabel.TabIndex = 0;
            this.portLabel.Text = "Port:";
            // 
            // portComboBox
            // 
            this.portComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.portComboBox.FormattingEnabled = true;
            this.portComboBox.Location = new System.Drawing.Point(47, 12);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Size = new System.Drawing.Size(121, 21);
            this.portComboBox.TabIndex = 1;
            // 
            // connButton
            // 
            this.connButton.Location = new System.Drawing.Point(174, 11);
            this.connButton.Name = "connButton";
            this.connButton.Size = new System.Drawing.Size(98, 23);
            this.connButton.TabIndex = 2;
            this.connButton.Text = "Open";
            this.connButton.UseVisualStyleBackColor = true;
            this.connButton.Click += new System.EventHandler(this.ConnButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Angle:";
            // 
            // angleLabel
            // 
            this.angleLabel.AutoSize = true;
            this.angleLabel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.angleLabel.Font = new System.Drawing.Font("Consolas", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.angleLabel.ForeColor = System.Drawing.Color.Lime;
            this.angleLabel.Location = new System.Drawing.Point(59, 67);
            this.angleLabel.Name = "angleLabel";
            this.angleLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.angleLabel.Size = new System.Drawing.Size(154, 56);
            this.angleLabel.TabIndex = 4;
            this.angleLabel.Text = "00.00";
            this.angleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(13, 137);
            this.logBox.Multiline = true;
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logBox.Size = new System.Drawing.Size(259, 112);
            this.logBox.TabIndex = 5;
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(174, 35);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(98, 23);
            this.testButton.TabIndex = 6;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.angleLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.connButton);
            this.Controls.Add(this.portComboBox);
            this.Controls.Add(this.portLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(300, 300);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Angle Displacement";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.ComboBox portComboBox;
        private System.Windows.Forms.Button connButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label angleLabel;
        private System.Windows.Forms.TextBox logBox;
        private System.Windows.Forms.Button testButton;
    }
}

