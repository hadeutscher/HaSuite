namespace HaRepacker.GUI.Interaction
{
    partial class VectorInputBox
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
            this.label1 = new System.Windows.Forms.Label();
            this.resultBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.xBox = new HaRepackerLib.Controls.IntegerInput();
            this.yBox = new HaRepackerLib.Controls.IntegerInput();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 999;
            this.label1.Text = "Name:";
            // 
            // resultBox
            // 
            this.resultBox.Location = new System.Drawing.Point(56, 12);
            this.resultBox.Name = "resultBox";
            this.resultBox.Size = new System.Drawing.Size(132, 20);
            this.resultBox.TabIndex = 0;
            this.resultBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameBox_KeyPress);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(40, 64);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(54, 25);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(100, 64);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(54, 25);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 999;
            this.label2.Text = "Value:";
            // 
            // xBox
            // 
            this.xBox.Location = new System.Drawing.Point(56, 38);
            this.xBox.Name = "xBox";
            this.xBox.Size = new System.Drawing.Size(55, 20);
            this.xBox.TabIndex = 2;
            this.xBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameBox_KeyPress);
            // 
            // yBox
            // 
            this.yBox.Location = new System.Drawing.Point(133, 38);
            this.yBox.Name = "yBox";
            this.yBox.Size = new System.Drawing.Size(55, 20);
            this.yBox.TabIndex = 3;
            this.yBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 13);
            this.label3.TabIndex = 999;
            this.label3.Text = ",";
            // 
            // VectorInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 98);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.yBox);
            this.Controls.Add(this.xBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.resultBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "VectorInputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameBox_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox resultBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
        private HaRepackerLib.Controls.IntegerInput xBox;
        private HaRepackerLib.Controls.IntegerInput yBox;
        private System.Windows.Forms.Label label3;
    }
}