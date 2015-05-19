namespace HaRepacker.GUI
{
    partial class FirstRunForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.autoUpdate = new System.Windows.Forms.CheckBox();
            this.autoAssociateBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(112, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "確認";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // autoUpdate
            // 
            this.autoUpdate.AutoSize = true;
            this.autoUpdate.Location = new System.Drawing.Point(12, 32);
            this.autoUpdate.Name = "autoUpdate";
            this.autoUpdate.Size = new System.Drawing.Size(180, 16);
            this.autoUpdate.TabIndex = 15;
            this.autoUpdate.Text = "定期檢測更新(需要連結網路)";
            this.autoUpdate.UseVisualStyleBackColor = true;
            // 
            // autoAssociateBox
            // 
            this.autoAssociateBox.AutoSize = true;
            this.autoAssociateBox.Location = new System.Drawing.Point(12, 11);
            this.autoAssociateBox.Name = "autoAssociateBox";
            this.autoAssociateBox.Size = new System.Drawing.Size(204, 16);
            this.autoAssociateBox.TabIndex = 14;
            this.autoAssociateBox.Text = "自動關聯WZ檔案以HaRepacker打開";
            this.autoAssociateBox.UseVisualStyleBackColor = true;
            // 
            // FirstRunForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 89);
            this.Controls.Add(this.autoUpdate);
            this.Controls.Add(this.autoAssociateBox);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FirstRunForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "首次運行";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FirstRunForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox autoUpdate;
        private System.Windows.Forms.CheckBox autoAssociateBox;
    }
}