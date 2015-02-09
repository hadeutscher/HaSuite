namespace HaRepacker
{
    partial class Settings
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
            this.SettingsTab = new System.Windows.Forms.TabControl();
            this.FootholdTPage = new System.Windows.Forms.TabPage();
            this.ForceCBox = new System.Windows.Forms.CheckBox();
            this.ForceTBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NextCBox = new System.Windows.Forms.CheckBox();
            this.PrevCBox = new System.Windows.Forms.CheckBox();
            this.NextTBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PrevTBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PortalTPage = new System.Windows.Forms.TabPage();
            this.TypeCBox = new System.Windows.Forms.CheckBox();
            this.YCBox = new System.Windows.Forms.CheckBox();
            this.XCBox = new System.Windows.Forms.CheckBox();
            this.TypeTBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.YTBox = new System.Windows.Forms.TextBox();
            this.XTBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GeneralTPage = new System.Windows.Forms.TabPage();
            this.SizeCBox = new System.Windows.Forms.CheckBox();
            this.SizeTBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.FilepathCBox = new System.Windows.Forms.CheckBox();
            this.OpenFileBtn = new System.Windows.Forms.Button();
            this.FilepathTBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ConfirmBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SettingsTab.SuspendLayout();
            this.FootholdTPage.SuspendLayout();
            this.PortalTPage.SuspendLayout();
            this.GeneralTPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingsTab
            // 
            this.SettingsTab.Controls.Add(this.FootholdTPage);
            this.SettingsTab.Controls.Add(this.PortalTPage);
            this.SettingsTab.Controls.Add(this.GeneralTPage);
            this.SettingsTab.Location = new System.Drawing.Point(0, 0);
            this.SettingsTab.Name = "SettingsTab";
            this.SettingsTab.SelectedIndex = 0;
            this.SettingsTab.Size = new System.Drawing.Size(177, 114);
            this.SettingsTab.TabIndex = 0;
            // 
            // FootholdTPage
            // 
            this.FootholdTPage.Controls.Add(this.ForceCBox);
            this.FootholdTPage.Controls.Add(this.ForceTBox);
            this.FootholdTPage.Controls.Add(this.label3);
            this.FootholdTPage.Controls.Add(this.NextCBox);
            this.FootholdTPage.Controls.Add(this.PrevCBox);
            this.FootholdTPage.Controls.Add(this.NextTBox);
            this.FootholdTPage.Controls.Add(this.label2);
            this.FootholdTPage.Controls.Add(this.PrevTBox);
            this.FootholdTPage.Controls.Add(this.label1);
            this.FootholdTPage.Location = new System.Drawing.Point(4, 22);
            this.FootholdTPage.Name = "FootholdTPage";
            this.FootholdTPage.Padding = new System.Windows.Forms.Padding(3);
            this.FootholdTPage.Size = new System.Drawing.Size(169, 88);
            this.FootholdTPage.TabIndex = 0;
            this.FootholdTPage.Text = "Footholds";
            this.FootholdTPage.UseVisualStyleBackColor = true;
            // 
            // ForceCBox
            // 
            this.ForceCBox.AutoSize = true;
            this.ForceCBox.Location = new System.Drawing.Point(150, 58);
            this.ForceCBox.Name = "ForceCBox";
            this.ForceCBox.Size = new System.Drawing.Size(15, 14);
            this.ForceCBox.TabIndex = 8;
            this.ForceCBox.UseVisualStyleBackColor = true;
            // 
            // ForceTBox
            // 
            this.ForceTBox.Location = new System.Drawing.Point(106, 55);
            this.ForceTBox.Name = "ForceTBox";
            this.ForceTBox.Size = new System.Drawing.Size(38, 20);
            this.ForceTBox.TabIndex = 7;
            this.ForceTBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ForceTBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Default force value:";
            // 
            // NextCBox
            // 
            this.NextCBox.AutoSize = true;
            this.NextCBox.Location = new System.Drawing.Point(150, 35);
            this.NextCBox.Name = "NextCBox";
            this.NextCBox.Size = new System.Drawing.Size(15, 14);
            this.NextCBox.TabIndex = 5;
            this.NextCBox.UseVisualStyleBackColor = true;
            // 
            // PrevCBox
            // 
            this.PrevCBox.AutoSize = true;
            this.PrevCBox.Location = new System.Drawing.Point(150, 12);
            this.PrevCBox.Name = "PrevCBox";
            this.PrevCBox.Size = new System.Drawing.Size(15, 14);
            this.PrevCBox.TabIndex = 4;
            this.PrevCBox.UseVisualStyleBackColor = true;
            // 
            // NextTBox
            // 
            this.NextTBox.Location = new System.Drawing.Point(106, 33);
            this.NextTBox.Name = "NextTBox";
            this.NextTBox.Size = new System.Drawing.Size(38, 20);
            this.NextTBox.TabIndex = 3;
            this.NextTBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NextTBox_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Default next value:";
            // 
            // PrevTBox
            // 
            this.PrevTBox.Location = new System.Drawing.Point(106, 9);
            this.PrevTBox.Name = "PrevTBox";
            this.PrevTBox.Size = new System.Drawing.Size(38, 20);
            this.PrevTBox.TabIndex = 1;
            this.PrevTBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PrevTBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Default prev value:";
            // 
            // PortalTPage
            // 
            this.PortalTPage.Controls.Add(this.TypeCBox);
            this.PortalTPage.Controls.Add(this.YCBox);
            this.PortalTPage.Controls.Add(this.XCBox);
            this.PortalTPage.Controls.Add(this.TypeTBox);
            this.PortalTPage.Controls.Add(this.label6);
            this.PortalTPage.Controls.Add(this.YTBox);
            this.PortalTPage.Controls.Add(this.XTBox);
            this.PortalTPage.Controls.Add(this.label5);
            this.PortalTPage.Controls.Add(this.label4);
            this.PortalTPage.Location = new System.Drawing.Point(4, 22);
            this.PortalTPage.Name = "PortalTPage";
            this.PortalTPage.Padding = new System.Windows.Forms.Padding(3);
            this.PortalTPage.Size = new System.Drawing.Size(169, 88);
            this.PortalTPage.TabIndex = 1;
            this.PortalTPage.Text = "Portals";
            this.PortalTPage.UseVisualStyleBackColor = true;
            // 
            // TypeCBox
            // 
            this.TypeCBox.AutoSize = true;
            this.TypeCBox.Location = new System.Drawing.Point(141, 57);
            this.TypeCBox.Name = "TypeCBox";
            this.TypeCBox.Size = new System.Drawing.Size(15, 14);
            this.TypeCBox.TabIndex = 8;
            this.TypeCBox.UseVisualStyleBackColor = true;
            // 
            // YCBox
            // 
            this.YCBox.AutoSize = true;
            this.YCBox.Location = new System.Drawing.Point(141, 35);
            this.YCBox.Name = "YCBox";
            this.YCBox.Size = new System.Drawing.Size(15, 14);
            this.YCBox.TabIndex = 7;
            this.YCBox.UseVisualStyleBackColor = true;
            // 
            // XCBox
            // 
            this.XCBox.AutoSize = true;
            this.XCBox.Location = new System.Drawing.Point(141, 12);
            this.XCBox.Name = "XCBox";
            this.XCBox.Size = new System.Drawing.Size(15, 14);
            this.XCBox.TabIndex = 6;
            this.XCBox.UseVisualStyleBackColor = true;
            // 
            // TypeTBox
            // 
            this.TypeTBox.Location = new System.Drawing.Point(99, 55);
            this.TypeTBox.Name = "TypeTBox";
            this.TypeTBox.Size = new System.Drawing.Size(35, 20);
            this.TypeTBox.TabIndex = 5;
            this.TypeTBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TypeTBox_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Default type:";
            // 
            // YTBox
            // 
            this.YTBox.Location = new System.Drawing.Point(99, 32);
            this.YTBox.Name = "YTBox";
            this.YTBox.Size = new System.Drawing.Size(35, 20);
            this.YTBox.TabIndex = 3;
            this.YTBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.YTBox_KeyPress);
            // 
            // XTBox
            // 
            this.XTBox.Location = new System.Drawing.Point(99, 9);
            this.XTBox.Name = "XTBox";
            this.XTBox.Size = new System.Drawing.Size(35, 20);
            this.XTBox.TabIndex = 2;
            this.XTBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.XTBox_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Default Y coord:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Default X coord:";
            // 
            // GeneralTPage
            // 
            this.GeneralTPage.Controls.Add(this.SizeCBox);
            this.GeneralTPage.Controls.Add(this.SizeTBox);
            this.GeneralTPage.Controls.Add(this.label8);
            this.GeneralTPage.Controls.Add(this.FilepathCBox);
            this.GeneralTPage.Controls.Add(this.OpenFileBtn);
            this.GeneralTPage.Controls.Add(this.FilepathTBox);
            this.GeneralTPage.Controls.Add(this.label7);
            this.GeneralTPage.Location = new System.Drawing.Point(4, 22);
            this.GeneralTPage.Name = "GeneralTPage";
            this.GeneralTPage.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTPage.Size = new System.Drawing.Size(169, 88);
            this.GeneralTPage.TabIndex = 2;
            this.GeneralTPage.Text = "Other";
            this.GeneralTPage.UseVisualStyleBackColor = true;
            // 
            // SizeCBox
            // 
            this.SizeCBox.AutoSize = true;
            this.SizeCBox.Location = new System.Drawing.Point(143, 46);
            this.SizeCBox.Name = "SizeCBox";
            this.SizeCBox.Size = new System.Drawing.Size(15, 14);
            this.SizeCBox.TabIndex = 6;
            this.SizeCBox.UseVisualStyleBackColor = true;
            // 
            // SizeTBox
            // 
            this.SizeTBox.Location = new System.Drawing.Point(113, 43);
            this.SizeTBox.Name = "SizeTBox";
            this.SizeTBox.Size = new System.Drawing.Size(24, 20);
            this.SizeTBox.TabIndex = 5;
            this.SizeTBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SizeTBox_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Default render size:";
            // 
            // FilepathCBox
            // 
            this.FilepathCBox.AutoSize = true;
            this.FilepathCBox.Location = new System.Drawing.Point(143, 22);
            this.FilepathCBox.Name = "FilepathCBox";
            this.FilepathCBox.Size = new System.Drawing.Size(15, 14);
            this.FilepathCBox.TabIndex = 3;
            this.FilepathCBox.UseVisualStyleBackColor = true;
            // 
            // OpenFileBtn
            // 
            this.OpenFileBtn.Location = new System.Drawing.Point(113, 17);
            this.OpenFileBtn.Name = "OpenFileBtn";
            this.OpenFileBtn.Size = new System.Drawing.Size(24, 23);
            this.OpenFileBtn.TabIndex = 2;
            this.OpenFileBtn.Text = "...";
            this.OpenFileBtn.UseVisualStyleBackColor = true;
            this.OpenFileBtn.Click += new System.EventHandler(this.OpenFileBtn_Click);
            // 
            // FilepathTBox
            // 
            this.FilepathTBox.Enabled = false;
            this.FilepathTBox.Location = new System.Drawing.Point(6, 19);
            this.FilepathTBox.Name = "FilepathTBox";
            this.FilepathTBox.Size = new System.Drawing.Size(101, 20);
            this.FilepathTBox.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Default filepath:";
            // 
            // ConfirmBtn
            // 
            this.ConfirmBtn.Location = new System.Drawing.Point(12, 116);
            this.ConfirmBtn.Name = "ConfirmBtn";
            this.ConfirmBtn.Size = new System.Drawing.Size(69, 23);
            this.ConfirmBtn.TabIndex = 1;
            this.ConfirmBtn.Text = "Confirm";
            this.ConfirmBtn.UseVisualStyleBackColor = true;
            this.ConfirmBtn.Click += new System.EventHandler(this.ConfirmBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(94, 116);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(69, 23);
            this.CancelBtn.TabIndex = 2;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 144);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.ConfirmBtn);
            this.Controls.Add(this.SettingsTab);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.SettingsTab.ResumeLayout(false);
            this.FootholdTPage.ResumeLayout(false);
            this.FootholdTPage.PerformLayout();
            this.PortalTPage.ResumeLayout(false);
            this.PortalTPage.PerformLayout();
            this.GeneralTPage.ResumeLayout(false);
            this.GeneralTPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl SettingsTab;
        private System.Windows.Forms.TabPage FootholdTPage;
        private System.Windows.Forms.CheckBox NextCBox;
        private System.Windows.Forms.CheckBox PrevCBox;
        private System.Windows.Forms.TextBox NextTBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PrevTBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage PortalTPage;
        private System.Windows.Forms.TextBox ForceTBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ForceCBox;
        private System.Windows.Forms.Button ConfirmBtn;
        private System.Windows.Forms.CheckBox XCBox;
        private System.Windows.Forms.TextBox TypeTBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox YTBox;
        private System.Windows.Forms.TextBox XTBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.CheckBox TypeCBox;
        private System.Windows.Forms.CheckBox YCBox;
        private System.Windows.Forms.TabPage GeneralTPage;
        private System.Windows.Forms.CheckBox FilepathCBox;
        private System.Windows.Forms.Button OpenFileBtn;
        private System.Windows.Forms.TextBox FilepathTBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox SizeCBox;
        private System.Windows.Forms.TextBox SizeTBox;
        private System.Windows.Forms.Label label8;
    }
}