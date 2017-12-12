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
            this.RoomOrCorridor = new System.Windows.Forms.ComboBox();
            this.Resume = new System.Windows.Forms.Button();
            this.FinishOverride = new System.Windows.Forms.Button();
            this.OFowardBtn = new System.Windows.Forms.Button();
            this.OLeftBtn = new System.Windows.Forms.Button();
            this.ORiightBtn = new System.Windows.Forms.Button();
            this.OBackBtn = new System.Windows.Forms.Button();
            this.OStopBtn = new System.Windows.Forms.Button();
            this.BackBtn = new System.Windows.Forms.Button();
            this.FowardBtn = new System.Windows.Forms.Button();
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
            this.OverrideBtn.Location = new System.Drawing.Point(28, 282);
            this.OverrideBtn.Name = "OverrideBtn";
            this.OverrideBtn.Size = new System.Drawing.Size(75, 23);
            this.OverrideBtn.TabIndex = 4;
            this.OverrideBtn.Text = "Override";
            this.OverrideBtn.UseVisualStyleBackColor = true;
            this.OverrideBtn.Click += new System.EventHandler(this.OverrideBtn_Click);
            // 
            // RoomOrCorridor
            // 
            this.RoomOrCorridor.FormattingEnabled = true;
            this.RoomOrCorridor.Items.AddRange(new object[] {
            "Corridor",
            "Room"});
            this.RoomOrCorridor.Location = new System.Drawing.Point(28, 60);
            this.RoomOrCorridor.Name = "RoomOrCorridor";
            this.RoomOrCorridor.Size = new System.Drawing.Size(121, 21);
            this.RoomOrCorridor.TabIndex = 6;
            this.RoomOrCorridor.Visible = false;
            // 
            // Resume
            // 
            this.Resume.Location = new System.Drawing.Point(140, 162);
            this.Resume.Name = "Resume";
            this.Resume.Size = new System.Drawing.Size(75, 23);
            this.Resume.TabIndex = 7;
            this.Resume.Text = "Resume";
            this.Resume.UseVisualStyleBackColor = true;
            this.Resume.Visible = false;
            this.Resume.Click += new System.EventHandler(this.Resume_Click);
            // 
            // FinishOverride
            // 
            this.FinishOverride.Location = new System.Drawing.Point(28, 311);
            this.FinishOverride.Name = "FinishOverride";
            this.FinishOverride.Size = new System.Drawing.Size(75, 23);
            this.FinishOverride.TabIndex = 8;
            this.FinishOverride.Text = "Finish";
            this.FinishOverride.UseVisualStyleBackColor = true;
            this.FinishOverride.Click += new System.EventHandler(this.button1_Click);
            // 
            // OFowardBtn
            // 
            this.OFowardBtn.Location = new System.Drawing.Point(270, 262);
            this.OFowardBtn.Name = "OFowardBtn";
            this.OFowardBtn.Size = new System.Drawing.Size(75, 23);
            this.OFowardBtn.TabIndex = 9;
            this.OFowardBtn.Text = "Foward";
            this.OFowardBtn.UseVisualStyleBackColor = true;
            this.OFowardBtn.Click += new System.EventHandler(this.OFowardBtn_Click);
            // 
            // OLeftBtn
            // 
            this.OLeftBtn.Location = new System.Drawing.Point(177, 311);
            this.OLeftBtn.Name = "OLeftBtn";
            this.OLeftBtn.Size = new System.Drawing.Size(75, 23);
            this.OLeftBtn.TabIndex = 10;
            this.OLeftBtn.Text = "Left";
            this.OLeftBtn.UseVisualStyleBackColor = true;
            this.OLeftBtn.Click += new System.EventHandler(this.OLeftBtn_Click);
            // 
            // ORiightBtn
            // 
            this.ORiightBtn.Location = new System.Drawing.Point(348, 311);
            this.ORiightBtn.Name = "ORiightBtn";
            this.ORiightBtn.Size = new System.Drawing.Size(75, 23);
            this.ORiightBtn.TabIndex = 11;
            this.ORiightBtn.Text = "Right";
            this.ORiightBtn.UseVisualStyleBackColor = true;
            this.ORiightBtn.Click += new System.EventHandler(this.ORiightBtn_Click);
            // 
            // OBackBtn
            // 
            this.OBackBtn.Location = new System.Drawing.Point(267, 311);
            this.OBackBtn.Name = "OBackBtn";
            this.OBackBtn.Size = new System.Drawing.Size(75, 23);
            this.OBackBtn.TabIndex = 12;
            this.OBackBtn.Text = "Back";
            this.OBackBtn.UseVisualStyleBackColor = true;
            this.OBackBtn.Click += new System.EventHandler(this.OBackBtn_Click);
            // 
            // OStopBtn
            // 
            this.OStopBtn.Location = new System.Drawing.Point(270, 336);
            this.OStopBtn.Name = "OStopBtn";
            this.OStopBtn.Size = new System.Drawing.Size(75, 23);
            this.OStopBtn.TabIndex = 13;
            this.OStopBtn.Text = "Stop";
            this.OStopBtn.UseVisualStyleBackColor = true;
            this.OStopBtn.Click += new System.EventHandler(this.OStopBtn_Click);
            // 
            // BackBtn
            // 
            this.BackBtn.Location = new System.Drawing.Point(140, 129);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(75, 23);
            this.BackBtn.TabIndex = 15;
            this.BackBtn.Text = "Return";
            this.BackBtn.UseVisualStyleBackColor = true;
            this.BackBtn.Visible = false;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // FowardBtn
            // 
            this.FowardBtn.Location = new System.Drawing.Point(140, 100);
            this.FowardBtn.Name = "FowardBtn";
            this.FowardBtn.Size = new System.Drawing.Size(75, 23);
            this.FowardBtn.TabIndex = 16;
            this.FowardBtn.Text = "Foward";
            this.FowardBtn.UseVisualStyleBackColor = true;
            this.FowardBtn.Visible = false;
            this.FowardBtn.Click += new System.EventHandler(this.FowardBtn_Click);
            // 
            // ArduinoControllerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 371);
            this.Controls.Add(this.FowardBtn);
            this.Controls.Add(this.BackBtn);
            this.Controls.Add(this.OStopBtn);
            this.Controls.Add(this.OBackBtn);
            this.Controls.Add(this.ORiightBtn);
            this.Controls.Add(this.OLeftBtn);
            this.Controls.Add(this.OFowardBtn);
            this.Controls.Add(this.FinishOverride);
            this.Controls.Add(this.Resume);
            this.Controls.Add(this.RoomOrCorridor);
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
        private System.Windows.Forms.ComboBox RoomOrCorridor;
        private System.Windows.Forms.Button Resume;
        private System.Windows.Forms.Button FinishOverride;
        private System.Windows.Forms.Button OFowardBtn;
        private System.Windows.Forms.Button OLeftBtn;
        private System.Windows.Forms.Button ORiightBtn;
        private System.Windows.Forms.Button OBackBtn;
        private System.Windows.Forms.Button OStopBtn;
        private System.Windows.Forms.Button BackBtn;
        private System.Windows.Forms.Button FowardBtn;
    }
}