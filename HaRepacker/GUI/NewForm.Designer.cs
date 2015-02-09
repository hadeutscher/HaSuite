namespace HaRepacker.GUI
{
    partial class NewForm
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
            this.nameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.regBox = new System.Windows.Forms.RadioButton();
            this.listBox = new System.Windows.Forms.RadioButton();
            this.copyrightBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.encryptionBox = new System.Windows.Forms.ComboBox();
            this.versionBox = new HaRepackerLib.Controls.IntegerInput();
            this.SuspendLayout();
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(75, 12);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(101, 20);
            this.nameBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = ".wz";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Type";
            // 
            // regBox
            // 
            this.regBox.AutoSize = true;
            this.regBox.Checked = true;
            this.regBox.Location = new System.Drawing.Point(76, 38);
            this.regBox.Name = "regBox";
            this.regBox.Size = new System.Drawing.Size(62, 17);
            this.regBox.TabIndex = 4;
            this.regBox.TabStop = true;
            this.regBox.Text = "Regular";
            this.regBox.UseVisualStyleBackColor = true;
            this.regBox.CheckedChanged += new System.EventHandler(this.regBox_CheckedChanged);
            // 
            // listBox
            // 
            this.listBox.AutoSize = true;
            this.listBox.Location = new System.Drawing.Point(141, 38);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(41, 17);
            this.listBox.TabIndex = 5;
            this.listBox.Text = "List";
            this.listBox.UseVisualStyleBackColor = true;
            // 
            // copyrightBox
            // 
            this.copyrightBox.Location = new System.Drawing.Point(75, 61);
            this.copyrightBox.Name = "copyrightBox";
            this.copyrightBox.Size = new System.Drawing.Size(205, 20);
            this.copyrightBox.TabIndex = 6;
            this.copyrightBox.Text = "Package file v1.0 Copyright 2002 Wizet, ZMS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Copyright";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(35, 140);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(103, 38);
            this.okButton.TabIndex = 8;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(144, 140);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(112, 38);
            this.cancelButton.TabIndex = 9;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Encryption";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 117);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Version";
            // 
            // encryptionBox
            // 
            this.encryptionBox.FormattingEnabled = true;
            this.encryptionBox.Items.AddRange(new object[] {
            "GMS (old)",
            "MSEA (old)",
            "BMS\\GMS\\MSEA"});
            this.encryptionBox.Location = new System.Drawing.Point(75, 87);
            this.encryptionBox.Name = "encryptionBox";
            this.encryptionBox.Size = new System.Drawing.Size(205, 21);
            this.encryptionBox.TabIndex = 12;
            // 
            // versionBox
            // 
            this.versionBox.Location = new System.Drawing.Point(75, 114);
            this.versionBox.Name = "versionBox";
            this.versionBox.Size = new System.Drawing.Size(51, 20);
            this.versionBox.TabIndex = 13;
            this.versionBox.Text = "1";
            this.versionBox.Value = 1;
            // 
            // NewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 187);
            this.Controls.Add(this.versionBox);
            this.Controls.Add(this.encryptionBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.copyrightBox);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.regBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nameBox);
            this.Name = "NewForm";
            this.Text = "New...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton regBox;
        private System.Windows.Forms.RadioButton listBox;
        private System.Windows.Forms.TextBox copyrightBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox encryptionBox;
        private HaRepackerLib.Controls.IntegerInput versionBox;
    }
}