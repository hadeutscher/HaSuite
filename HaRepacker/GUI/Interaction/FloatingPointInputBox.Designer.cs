namespace HaRepacker.GUI.Interaction
{
    partial class FloatingPointInputBox
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
            this.nameBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.valueBox = new HaRepackerLib.Controls.DoubleInput();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // nameBox
            // 
            this.nameBox.Location = new System.Drawing.Point(56, 12);
            this.nameBox.Name = "nameBox";
            this.nameBox.Size = new System.Drawing.Size(132, 20);
            this.nameBox.TabIndex = 1;
            this.nameBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameBox_KeyPress);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(45, 64);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(54, 25);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(105, 64);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(54, 25);
            this.cancelButton.TabIndex = 3;
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
            this.label2.TabIndex = 4;
            this.label2.Text = "Value:";
            // 
            // valueBox
            // 
            this.valueBox.Location = new System.Drawing.Point(56, 38);
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(132, 20);
            this.valueBox.TabIndex = 6;
            this.valueBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameBox_KeyPress);
            // 
            // FloatingPointInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 101);
            this.Controls.Add(this.valueBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.nameBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FloatingPointInputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameBox_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox nameBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
        private HaRepackerLib.Controls.DoubleInput valueBox;
    }
}