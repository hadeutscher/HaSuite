namespace HaRepackerLib
{
    partial class ReplaceBox
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
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnYestoall = new System.Windows.Forms.Button();
            this.btnNotoall = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(337, 61);
            this.label1.TabIndex = 0;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(12, 83);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(80, 29);
            this.btnYes.TabIndex = 1;
            this.btnYes.Text = "確認";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(98, 83);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(80, 29);
            this.btnNo.TabIndex = 2;
            this.btnNo.Text = "取消";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnYestoall
            // 
            this.btnYestoall.Location = new System.Drawing.Point(184, 83);
            this.btnYestoall.Name = "btnYestoall";
            this.btnYestoall.Size = new System.Drawing.Size(80, 29);
            this.btnYestoall.TabIndex = 3;
            this.btnYestoall.Text = "確認全部";
            this.btnYestoall.UseVisualStyleBackColor = true;
            this.btnYestoall.Click += new System.EventHandler(this.btnYestoall_Click);
            // 
            // btnNotoall
            // 
            this.btnNotoall.Location = new System.Drawing.Point(270, 83);
            this.btnNotoall.Name = "btnNotoall";
            this.btnNotoall.Size = new System.Drawing.Size(80, 29);
            this.btnNotoall.TabIndex = 4;
            this.btnNotoall.Text = "取消全部";
            this.btnNotoall.UseVisualStyleBackColor = true;
            this.btnNotoall.Click += new System.EventHandler(this.btnNotoall_Click);
            // 
            // ReplaceBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 124);
            this.Controls.Add(this.btnNotoall);
            this.Controls.Add(this.btnYestoall);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ReplaceBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "取代";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReplaceBox_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnYestoall;
        private System.Windows.Forms.Button btnNotoall;
    }
}