namespace HaRepackerLib
{
    partial class SearchOptionsForm
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
            this.button2 = new System.Windows.Forms.Button();
            this.parseImages = new System.Windows.Forms.CheckBox();
            this.searchValues = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(73, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(93, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(172, 58);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(93, 35);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // parseImages
            // 
            this.parseImages.AutoSize = true;
            this.parseImages.Location = new System.Drawing.Point(12, 12);
            this.parseImages.Name = "parseImages";
            this.parseImages.Size = new System.Drawing.Size(319, 17);
            this.parseImages.TabIndex = 2;
            this.parseImages.Text = "Parse images while searching (warning - can take a lot of time)";
            this.parseImages.UseVisualStyleBackColor = true;
            // 
            // searchValues
            // 
            this.searchValues.AutoSize = true;
            this.searchValues.Location = new System.Drawing.Point(12, 35);
            this.searchValues.Name = "searchValues";
            this.searchValues.Size = new System.Drawing.Size(122, 17);
            this.searchValues.TabIndex = 3;
            this.searchValues.Text = "Search string values";
            this.searchValues.UseVisualStyleBackColor = true;
            // 
            // SearchOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 105);
            this.Controls.Add(this.searchValues);
            this.Controls.Add(this.parseImages);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SearchOptionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox parseImages;
        private System.Windows.Forms.CheckBox searchValues;
    }
}