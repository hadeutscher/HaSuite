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
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "現在的prev值:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "現在的next值:";
            // 
            // PrevLbl
            // 
            this.PrevLbl.AutoSize = true;
            this.PrevLbl.Location = new System.Drawing.Point(115, 8);
            this.PrevLbl.Name = "PrevLbl";
            this.PrevLbl.Size = new System.Drawing.Size(0, 12);
            this.PrevLbl.TabIndex = 2;
            // 
            // NextLbl
            // 
            this.NextLbl.AutoSize = true;
            this.NextLbl.Location = new System.Drawing.Point(115, 25);
            this.NextLbl.Name = "NextLbl";
            this.NextLbl.Size = new System.Drawing.Size(0, 12);
            this.NextLbl.TabIndex = 3;
            // 
            // PrevTBox
            // 
            this.PrevTBox.Location = new System.Drawing.Point(102, 70);
            this.PrevTBox.Name = "PrevTBox";
            this.PrevTBox.Size = new System.Drawing.Size(46, 21);
            this.PrevTBox.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "新的prev值:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "新的next值:";
            // 
            // NextTBox
            // 
            this.NextTBox.Location = new System.Drawing.Point(102, 94);
            this.NextTBox.Name = "NextTBox";
            this.NextTBox.Size = new System.Drawing.Size(46, 21);
            this.NextTBox.TabIndex = 7;
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.Location = new System.Drawing.Point(15, 150);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(59, 21);
            this.ConfirmBtn.TabIndex = 8;
            this.ConfirmBtn.Text = "確認";
            this.ConfirmBtn.UseVisualStyleBackColor = true;
            this.ConfirmBtn.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.Location = new System.Drawing.Point(89, 150);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(59, 21);
            this.CancelBtn.TabIndex = 9;
            this.CancelBtn.Text = "取消";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "新的force值:";
            // 
            // ForceTBox
            // 
            this.ForceTBox.Location = new System.Drawing.Point(102, 118);
            this.ForceTBox.Name = "ForceTBox";
            this.ForceTBox.Size = new System.Drawing.Size(46, 21);
            this.ForceTBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "現在的force值:";
            // 
            // ForceLbl
            // 
            this.ForceLbl.AutoSize = true;
            this.ForceLbl.Location = new System.Drawing.Point(115, 45);
            this.ForceLbl.Name = "ForceLbl";
            this.ForceLbl.Size = new System.Drawing.Size(0, 12);
            this.ForceLbl.TabIndex = 13;
            // 
            // Edit
            // 
            this.AcceptButton = this.ConfirmBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelBtn;
            this.ClientSize = new System.Drawing.Size(162, 182);
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
            this.Text = "編輯";
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