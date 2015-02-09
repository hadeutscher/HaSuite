namespace Footholds
{
    partial class Edit
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
            this.label2 = new System.Windows.Forms.Label();
            this.PrevLbl = new System.Windows.Forms.Label();
            this.NextLbl = new System.Windows.Forms.Label();
            this.PrevTBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.NextTBox = new System.Windows.Forms.TextBox();
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.ForceTBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ForceLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current prev value:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Current next value:";
            // 
            // PrevLbl
            // 
            this.PrevLbl.AutoSize = true;
            this.PrevLbl.Location = new System.Drawing.Point(115, 9);
            this.PrevLbl.Name = "PrevLbl";
            this.PrevLbl.Size = new System.Drawing.Size(0, 13);
            this.PrevLbl.TabIndex = 2;
            // 
            // NextLbl
            // 
            this.NextLbl.AutoSize = true;
            this.NextLbl.Location = new System.Drawing.Point(115, 27);
            this.NextLbl.Name = "NextLbl";
            this.NextLbl.Size = new System.Drawing.Size(0, 13);
            this.NextLbl.TabIndex = 3;
            // 
            // PrevTBox
            // 
            this.PrevTBox.Location = new System.Drawing.Point(102, 76);
            this.PrevTBox.Name = "PrevTBox";
            this.PrevTBox.Size = new System.Drawing.Size(46, 20);
            this.PrevTBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "New prev value:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "New next value:";
            // 
            // NextTBox
            // 
            this.NextTBox.Location = new System.Drawing.Point(102, 102);
            this.NextTBox.Name = "NextTBox";
            this.NextTBox.Size = new System.Drawing.Size(46, 20);
            this.NextTBox.TabIndex = 7;
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.Location = new System.Drawing.Point(15, 162);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(59, 23);
            this.ConfirmBtn.TabIndex = 8;
            this.ConfirmBtn.Text = "Confirm";
            this.ConfirmBtn.UseVisualStyleBackColor = true;
            this.ConfirmBtn.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(89, 162);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(59, 23);
            this.CancelBtn.TabIndex = 9;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "New force value:";
            // 
            // ForceTBox
            // 
            this.ForceTBox.Location = new System.Drawing.Point(102, 128);
            this.ForceTBox.Name = "ForceTBox";
            this.ForceTBox.Size = new System.Drawing.Size(46, 20);
            this.ForceTBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Current force value:";
            // 
            // ForceLbl
            // 
            this.ForceLbl.AutoSize = true;
            this.ForceLbl.Location = new System.Drawing.Point(115, 49);
            this.ForceLbl.Name = "ForceLbl";
            this.ForceLbl.Size = new System.Drawing.Size(0, 13);
            this.ForceLbl.TabIndex = 13;
            // 
            // Edit
            // 
            this.AcceptButton = this.ConfirmBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(162, 197);
            this.Controls.Add(this.ForceLbl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ForceTBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.ConfirmBtn);
            this.Controls.Add(this.NextTBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PrevTBox);
            this.Controls.Add(this.NextLbl);
            this.Controls.Add(this.PrevLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Edit";
            this.ShowIcon = false;
            this.Text = "Edit";
            this.Load += new System.EventHandler(this.Edit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PrevLbl;
        private System.Windows.Forms.Label NextLbl;
        private System.Windows.Forms.TextBox PrevTBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox NextTBox;
        private System.Windows.Forms.Button ConfirmBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ForceTBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label ForceLbl;
    }
}