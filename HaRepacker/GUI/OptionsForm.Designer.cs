namespace HaRepacker.GUI
{
    partial class OptionsForm
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
            this.sortBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.indentBox = new HaRepackerLib.Controls.IntegerInput();
            this.label1 = new System.Windows.Forms.Label();
            this.lineBreakBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.apngIncompEnable = new System.Windows.Forms.CheckBox();
            this.defXmlFolderEnable = new System.Windows.Forms.CheckBox();
            this.defXmlFolderBox = new System.Windows.Forms.TextBox();
            this.browse = new System.Windows.Forms.Button();
            this.autoAssociateBox = new System.Windows.Forms.CheckBox();
            this.autoUpdate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // sortBox
            // 
            this.sortBox.AutoSize = true;
            this.sortBox.Location = new System.Drawing.Point(12, 11);
            this.sortBox.Name = "sortBox";
            this.sortBox.Size = new System.Drawing.Size(84, 16);
            this.sortBox.TabIndex = 0;
            this.sortBox.Text = "排序樹視圖";
            this.sortBox.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(64, 221);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(93, 30);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "確認";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(163, 221);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(93, 30);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "取消";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // indentBox
            // 
            this.indentBox.Location = new System.Drawing.Point(78, 169);
            this.indentBox.Name = "indentBox";
            this.indentBox.Size = new System.Drawing.Size(68, 20);
            this.indentBox.TabIndex = 4;
            this.indentBox.Value = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "縮進";
            // 
            // lineBreakBox
            // 
            this.lineBreakBox.FormattingEnabled = true;
            this.lineBreakBox.Items.AddRange(new object[] {
            "不換行",
            "Windows",
            "Unix"});
            this.lineBreakBox.Location = new System.Drawing.Point(78, 195);
            this.lineBreakBox.Name = "lineBreakBox";
            this.lineBreakBox.Size = new System.Drawing.Size(68, 21);
            this.lineBreakBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "換行格式";
            // 
            // apngIncompEnable
            // 
            this.apngIncompEnable.AutoSize = true;
            this.apngIncompEnable.Location = new System.Drawing.Point(12, 32);
            this.apngIncompEnable.Name = "apngIncompEnable";
            this.apngIncompEnable.Size = new System.Drawing.Size(210, 16);
            this.apngIncompEnable.TabIndex = 8;
            this.apngIncompEnable.Text = "使用APNG動畫不兼容顯示楨";
            this.apngIncompEnable.UseVisualStyleBackColor = true;
            // 
            // defXmlFolderEnable
            // 
            this.defXmlFolderEnable.AutoSize = true;
            this.defXmlFolderEnable.Location = new System.Drawing.Point(12, 111);
            this.defXmlFolderEnable.Name = "defXmlFolderEnable";
            this.defXmlFolderEnable.Size = new System.Drawing.Size(108, 16);
            this.defXmlFolderEnable.TabIndex = 9;
            this.defXmlFolderEnable.Text = "默認XML資料夾:";
            this.defXmlFolderEnable.UseVisualStyleBackColor = true;
            this.defXmlFolderEnable.CheckedChanged += new System.EventHandler(this.defXmlFolderEnable_CheckedChanged);
            // 
            // defXmlFolderBox
            // 
            this.defXmlFolderBox.Enabled = false;
            this.defXmlFolderBox.Location = new System.Drawing.Point(12, 143);
            this.defXmlFolderBox.Name = "defXmlFolderBox";
            this.defXmlFolderBox.Size = new System.Drawing.Size(170, 20);
            this.defXmlFolderBox.TabIndex = 10;
            // 
            // browse
            // 
            this.browse.Enabled = false;
            this.browse.Location = new System.Drawing.Point(188, 143);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(31, 19);
            this.browse.TabIndex = 11;
            this.browse.Text = "...";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // autoAssociateBox
            // 
            this.autoAssociateBox.AutoSize = true;
            this.autoAssociateBox.Location = new System.Drawing.Point(12, 54);
            this.autoAssociateBox.Name = "autoAssociateBox";
            this.autoAssociateBox.Size = new System.Drawing.Size(204, 16);
            this.autoAssociateBox.TabIndex = 12;
            this.autoAssociateBox.Text = "自動關聯WZ檔案以HaRepacker打開";
            this.autoAssociateBox.UseVisualStyleBackColor = true;
            // 
            // autoUpdate
            // 
            this.autoUpdate.AutoSize = true;
            this.autoUpdate.Location = new System.Drawing.Point(12, 75);
            this.autoUpdate.Name = "autoUpdate";
            this.autoUpdate.Size = new System.Drawing.Size(180, 16);
            this.autoUpdate.TabIndex = 13;
            this.autoUpdate.Text = "定期檢測更新(需要連結網路)";
            this.autoUpdate.UseVisualStyleBackColor = true;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 253);
            this.Controls.Add(this.autoUpdate);
            this.Controls.Add(this.autoAssociateBox);
            this.Controls.Add(this.browse);
            this.Controls.Add(this.defXmlFolderBox);
            this.Controls.Add(this.defXmlFolderEnable);
            this.Controls.Add(this.apngIncompEnable);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lineBreakBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.indentBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.sortBox);
            this.Name = "OptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "選項";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox sortBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private HaRepackerLib.Controls.IntegerInput indentBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox lineBreakBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox apngIncompEnable;
        private System.Windows.Forms.CheckBox defXmlFolderEnable;
        private System.Windows.Forms.TextBox defXmlFolderBox;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.CheckBox autoAssociateBox;
        private System.Windows.Forms.CheckBox autoUpdate;
    }
}