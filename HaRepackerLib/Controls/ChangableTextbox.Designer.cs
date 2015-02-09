namespace HaRepackerLib
{
    partial class ChangableTextbox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.applyButton = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.Enabled = false;
            this.applyButton.Location = new System.Drawing.Point(280, 0);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(44, 20);
            this.applyButton.TabIndex = 7;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(274, 20);
            this.textBox.TabIndex = 6;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // ChangableTextbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.textBox);
            this.Name = "ChangableTextbox";
            this.Size = new System.Drawing.Size(330, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.TextBox textBox;
    }
}
