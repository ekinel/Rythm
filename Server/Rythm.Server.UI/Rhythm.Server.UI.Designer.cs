namespace Rythm.Server.UI
{
    partial class RhythmServerUI
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
            this.ButtonStop = new System.Windows.Forms.Button();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.TextBoxPort = new System.Windows.Forms.TextBox();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.LabelPort = new System.Windows.Forms.Label();
            this.LabelAddress = new System.Windows.Forms.Label();
            this.TextBoxAddress = new System.Windows.Forms.TextBox();
            this.LabelTimeOut = new System.Windows.Forms.Label();
            this.TextBoxTimeOut = new System.Windows.Forms.TextBox();
            this.LabelDataBase = new System.Windows.Forms.Label();
            this.TextBoxDataBase = new System.Windows.Forms.TextBox();
            this.LabelServerStatus = new System.Windows.Forms.Label();
            this.GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonStop
            // 
            this.ButtonStop.Enabled = false;
            this.ButtonStop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ButtonStop.Location = new System.Drawing.Point(264, 66);
            this.ButtonStop.Name = "ButtonStop";
            this.ButtonStop.Size = new System.Drawing.Size(120, 27);
            this.ButtonStop.TabIndex = 7;
            this.ButtonStop.Text = "Stop";
            this.ButtonStop.UseVisualStyleBackColor = true;
            this.ButtonStop.Click += new System.EventHandler(this.ButtonStopClick);
            // 
            // ButtonStart
            // 
            this.ButtonStart.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ButtonStart.Location = new System.Drawing.Point(264, 28);
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.Size = new System.Drawing.Size(120, 27);
            this.ButtonStart.TabIndex = 6;
            this.ButtonStart.Text = "Start";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.ButtonStartClick);
            // 
            // TextBoxPort
            // 
            this.TextBoxPort.Location = new System.Drawing.Point(21, 61);
            this.TextBoxPort.Name = "TextBoxPort";
            this.TextBoxPort.Size = new System.Drawing.Size(120, 20);
            this.TextBoxPort.TabIndex = 5;
            this.TextBoxPort.Text = "65000";
            // 
            // GroupBox
            // 
            this.GroupBox.Controls.Add(this.LabelDataBase);
            this.GroupBox.Controls.Add(this.TextBoxDataBase);
            this.GroupBox.Controls.Add(this.LabelTimeOut);
            this.GroupBox.Controls.Add(this.TextBoxTimeOut);
            this.GroupBox.Controls.Add(this.LabelAddress);
            this.GroupBox.Controls.Add(this.TextBoxAddress);
            this.GroupBox.Controls.Add(this.LabelPort);
            this.GroupBox.Controls.Add(this.TextBoxPort);
            this.GroupBox.Location = new System.Drawing.Point(27, 14);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Size = new System.Drawing.Size(200, 172);
            this.GroupBox.TabIndex = 8;
            this.GroupBox.TabStop = false;
            // 
            // LabelPort
            // 
            this.LabelPort.AutoSize = true;
            this.LabelPort.Location = new System.Drawing.Point(18, 45);
            this.LabelPort.Name = "LabelPort";
            this.LabelPort.Size = new System.Drawing.Size(26, 13);
            this.LabelPort.TabIndex = 9;
            this.LabelPort.Text = "Port";
            // 
            // LabelAddress
            // 
            this.LabelAddress.AutoSize = true;
            this.LabelAddress.Location = new System.Drawing.Point(18, 7);
            this.LabelAddress.Name = "LabelAddress";
            this.LabelAddress.Size = new System.Drawing.Size(45, 13);
            this.LabelAddress.TabIndex = 11;
            this.LabelAddress.Text = "Address";
            // 
            // TextBoxAddress
            // 
            this.TextBoxAddress.Location = new System.Drawing.Point(21, 23);
            this.TextBoxAddress.Name = "TextBoxAddress";
            this.TextBoxAddress.Size = new System.Drawing.Size(120, 20);
            this.TextBoxAddress.TabIndex = 10;
            // 
            // LabelTimeOut
            // 
            this.LabelTimeOut.AutoSize = true;
            this.LabelTimeOut.Location = new System.Drawing.Point(18, 83);
            this.LabelTimeOut.Name = "LabelTimeOut";
            this.LabelTimeOut.Size = new System.Drawing.Size(47, 13);
            this.LabelTimeOut.TabIndex = 13;
            this.LabelTimeOut.Text = "TimeOut";
            // 
            // TextBoxTimeOut
            // 
            this.TextBoxTimeOut.Location = new System.Drawing.Point(21, 99);
            this.TextBoxTimeOut.Name = "TextBoxTimeOut";
            this.TextBoxTimeOut.Size = new System.Drawing.Size(120, 20);
            this.TextBoxTimeOut.TabIndex = 12;
            // 
            // LabelDataBase
            // 
            this.LabelDataBase.AutoSize = true;
            this.LabelDataBase.Location = new System.Drawing.Point(18, 120);
            this.LabelDataBase.Name = "LabelDataBase";
            this.LabelDataBase.Size = new System.Drawing.Size(54, 13);
            this.LabelDataBase.TabIndex = 15;
            this.LabelDataBase.Text = "DataBase";
            // 
            // TextBoxDataBase
            // 
            this.TextBoxDataBase.Location = new System.Drawing.Point(21, 136);
            this.TextBoxDataBase.Name = "TextBoxDataBase";
            this.TextBoxDataBase.Size = new System.Drawing.Size(120, 20);
            this.TextBoxDataBase.TabIndex = 14;
            // 
            // LabelServerStatus
            // 
            this.LabelServerStatus.AutoSize = true;
            this.LabelServerStatus.Location = new System.Drawing.Point(261, 157);
            this.LabelServerStatus.Name = "LabelServerStatus";
            this.LabelServerStatus.Size = new System.Drawing.Size(0, 13);
            this.LabelServerStatus.TabIndex = 9;
            // 
            // RhythmServerUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 211);
            this.Controls.Add(this.LabelServerStatus);
            this.Controls.Add(this.GroupBox);
            this.Controls.Add(this.ButtonStop);
            this.Controls.Add(this.ButtonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 250);
            this.Name = "RhythmServerUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rhythm";
            this.GroupBox.ResumeLayout(false);
            this.GroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonStop;
        private System.Windows.Forms.Button ButtonStart;
        private System.Windows.Forms.TextBox TextBoxPort;
        private System.Windows.Forms.GroupBox GroupBox;
        private System.Windows.Forms.Label LabelDataBase;
        private System.Windows.Forms.TextBox TextBoxDataBase;
        private System.Windows.Forms.Label LabelTimeOut;
        private System.Windows.Forms.TextBox TextBoxTimeOut;
        private System.Windows.Forms.Label LabelAddress;
        private System.Windows.Forms.TextBox TextBoxAddress;
        private System.Windows.Forms.Label LabelPort;
        private System.Windows.Forms.Label LabelServerStatus;
    }
}