namespace HaRepacker.GUI
{
    partial class SaveForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveForm));
            this.encryptionBox = new System.Windows.Forms.ComboBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.versionBox = new HaRepackerLib.Controls.IntegerInput();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // encryptionBox
            // 
            this.encryptionBox.FormattingEnabled = true;
            this.encryptionBox.Items.AddRange(new object[] {
            "GMS (old)",
            "MSEA (old)",
            "BMS\\GMS\\MSEA"});
            this.encryptionBox.Location = new System.Drawing.Point(75, 12);
            this.encryptionBox.Name = "encryptionBox";
            this.encryptionBox.Size = new System.Drawing.Size(178, 21);
            this.encryptionBox.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(75, 65);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(178, 27);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // versionBox
            // 
            this.versionBox.Location = new System.Drawing.Point(75, 39);
            this.versionBox.Name = "versionBox";
            this.versionBox.Size = new System.Drawing.Size(178, 20);
            this.versionBox.TabIndex = 3;
            this.versionBox.Text = "0";
            this.versionBox.Value = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Version";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Encryption";
            // 
            // SaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 104);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.versionBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.encryptionBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SaveForm";
            this.Text = "Save";
            this.Load += new System.EventHandler(this.SaveForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        public System.Windows.Forms.ComboBox encryptionBox;
        private HaRepackerLib.Controls.IntegerInput versionBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}