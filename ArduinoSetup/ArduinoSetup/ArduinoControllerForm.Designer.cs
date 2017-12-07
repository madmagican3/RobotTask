namespace ArduinoSetup
{
    partial class ArduinoControllerForm
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
            this.SerialReturnsList = new System.Windows.Forms.ListBox();
            this.LeftBtn = new System.Windows.Forms.Button();
            this.RightBtn = new System.Windows.Forms.Button();
            this.PauseBtn = new System.Windows.Forms.Button();
            this.OverrideBtn = new System.Windows.Forms.Button();
            this.CancelOverrideBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SerialReturnsList
            // 
            this.SerialReturnsList.FormattingEnabled = true;
            this.SerialReturnsList.Location = new System.Drawing.Point(429, 0);
            this.SerialReturnsList.Name = "SerialReturnsList";
            this.SerialReturnsList.Size = new System.Drawing.Size(364, 368);
            this.SerialReturnsList.TabIndex = 0;
            // 
            // LeftBtn
            // 
            this.LeftBtn.Location = new System.Drawing.Point(28, 129);
            this.LeftBtn.Name = "LeftBtn";
            this.LeftBtn.Size = new System.Drawing.Size(75, 23);
            this.LeftBtn.TabIndex = 1;
            this.LeftBtn.Text = "Left";
            this.LeftBtn.UseVisualStyleBackColor = true;
            this.LeftBtn.Visible = false;
            this.LeftBtn.Click += new System.EventHandler(this.LeftBtn_Click);
            // 
            // RightBtn
            // 
            this.RightBtn.Location = new System.Drawing.Point(241, 129);
            this.RightBtn.Name = "RightBtn";
            this.RightBtn.Size = new System.Drawing.Size(75, 23);
            this.RightBtn.TabIndex = 2;
            this.RightBtn.Text = "Right";
            this.RightBtn.UseVisualStyleBackColor = true;
            this.RightBtn.Visible = false;
            this.RightBtn.Click += new System.EventHandler(this.RightBtn_Click);
            // 
            // PauseBtn
            // 
            this.PauseBtn.Location = new System.Drawing.Point(140, 213);
            this.PauseBtn.Name = "PauseBtn";
            this.PauseBtn.Size = new System.Drawing.Size(75, 23);
            this.PauseBtn.TabIndex = 3;
            this.PauseBtn.Text = "Pause";
            this.PauseBtn.UseVisualStyleBackColor = true;
            this.PauseBtn.Visible = false;
            this.PauseBtn.Click += new System.EventHandler(this.PauseBtn_Click);
            // 
            // OverrideBtn
            // 
            this.OverrideBtn.Location = new System.Drawing.Point(152, 288);
            this.OverrideBtn.Name = "OverrideBtn";
            this.OverrideBtn.Size = new System.Drawing.Size(75, 23);
            this.OverrideBtn.TabIndex = 4;
            this.OverrideBtn.Text = "Override";
            this.OverrideBtn.UseVisualStyleBackColor = true;
            this.OverrideBtn.Click += new System.EventHandler(this.OverrideBtn_Click);
            // 
            // CancelOverrideBtn
            // 
            this.CancelOverrideBtn.Location = new System.Drawing.Point(152, 318);
            this.CancelOverrideBtn.Name = "CancelOverrideBtn";
            this.CancelOverrideBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelOverrideBtn.TabIndex = 5;
            this.CancelOverrideBtn.Text = "Resume Run";
            this.CancelOverrideBtn.UseVisualStyleBackColor = true;
            this.CancelOverrideBtn.Visible = false;
            this.CancelOverrideBtn.Click += new System.EventHandler(this.CancelOverrideBtn_Click);
            // 
            // ArduinoControllerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 371);
            this.Controls.Add(this.CancelOverrideBtn);
            this.Controls.Add(this.OverrideBtn);
            this.Controls.Add(this.PauseBtn);
            this.Controls.Add(this.RightBtn);
            this.Controls.Add(this.LeftBtn);
            this.Controls.Add(this.SerialReturnsList);
            this.Name = "ArduinoControllerForm";
            this.Text = "ArduinoControllerForm";
            this.Load += new System.EventHandler(this.ArduinoControllerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox SerialReturnsList;
        private System.Windows.Forms.Button LeftBtn;
        private System.Windows.Forms.Button RightBtn;
        private System.Windows.Forms.Button PauseBtn;
        private System.Windows.Forms.Button OverrideBtn;
        private System.Windows.Forms.Button CancelOverrideBtn;
    }
}