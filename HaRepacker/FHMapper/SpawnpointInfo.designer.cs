namespace Footholds
{
    partial class SpawnpointInfo
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
            this.MobIDLbl = new System.Windows.Forms.Label();
            this.FHIDLbl = new System.Windows.Forms.Label();
            this.OKBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.XLbl = new System.Windows.Forms.Label();
            this.YLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "����ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "����̎ID:";
            // 
            // MobIDLbl
            // 
            this.MobIDLbl.AutoSize = true;
            this.MobIDLbl.Location = new System.Drawing.Point(90, 8);
            this.MobIDLbl.Name = "MobIDLbl";
            this.MobIDLbl.Size = new System.Drawing.Size(11, 12);
            this.MobIDLbl.TabIndex = 2;
            this.MobIDLbl.Text = "*";
            // 
            // FHIDLbl
            // 
            this.FHIDLbl.AutoSize = true;
            this.FHIDLbl.Location = new System.Drawing.Point(90, 29);
            this.FHIDLbl.Name = "FHIDLbl";
            this.FHIDLbl.Size = new System.Drawing.Size(11, 12);
            this.FHIDLbl.TabIndex = 3;
            this.FHIDLbl.Text = "*";
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(66, 88);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(48, 21);
            this.OKBtn.TabIndex = 4;
            this.OKBtn.Text = "�_�J";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "X����:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Y����:";
            // 
            // XLbl
            // 
            this.XLbl.AutoSize = true;
            this.XLbl.Location = new System.Drawing.Point(90, 49);
            this.XLbl.Name = "XLbl";
            this.XLbl.Size = new System.Drawing.Size(11, 12);
            this.XLbl.TabIndex = 7;
            this.XLbl.Text = "*";
            // 
            // YLbl
            // 
            this.YLbl.AutoSize = true;
            this.YLbl.Location = new System.Drawing.Point(90, 69);
            this.YLbl.Name = "YLbl";
            this.YLbl.Size = new System.Drawing.Size(11, 12);
            this.YLbl.TabIndex = 8;
            this.YLbl.Text = "*";
            // 
            // SpawnpointInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(176, 115);
            this.Controls.Add(this.YLbl);
            this.Controls.Add(this.XLbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.FHIDLbl);
            this.Controls.Add(this.MobIDLbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpawnpointInfo";
            this.ShowIcon = false;
            this.Text = "�ن��c";
            this.Load += new System.EventHandler(this.SpawnpointInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label MobIDLbl;
        private System.Windows.Forms.Label FHIDLbl;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label XLbl;
        private System.Windows.Forms.Label YLbl;
    }
}