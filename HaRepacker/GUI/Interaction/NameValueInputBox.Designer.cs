namespace HaRepacker.GUI.Interaction
{
    partial class NameValueInputBox
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
            this.valueBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
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
            this.okButton.Location = new System.Drawing.Point(41, 64);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(54, 25);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(101, 64);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(54, 25);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // valueBox
            // 
            this.valueBox.Location = new System.Drawing.Point(56, 38);
            this.valueBox.Name = "valueBox";
            this.valueBox.Size = new System.Drawing.Size(132, 20);
            this.valueBox.TabIndex = 1;
            this.valueBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameBox_KeyPress);
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
            // NameValueInputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(200, 101);
            this.Controls.Add(this.valueBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.resultBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NameValueInputBox";
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
        private System.Windows.Forms.TextBox valueBox;
        private System.Windows.Forms.Label label2;
    }
}