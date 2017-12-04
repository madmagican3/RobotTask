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
            // ArduinoControllerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 371);
            this.Controls.Add(this.SerialReturnsList);
            this.Name = "ArduinoControllerForm";
            this.Text = "ArduinoControllerForm";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox SerialReturnsList;
    }
}