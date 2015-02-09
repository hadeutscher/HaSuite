namespace HaRepackerLib.Controls
{
    partial class DockableSearchResult
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.searchResultsBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // searchResultsBox
            // 
            this.searchResultsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchResultsBox.FormattingEnabled = true;
            this.searchResultsBox.Location = new System.Drawing.Point(0, 0);
            this.searchResultsBox.Name = "searchResultsBox";
            this.searchResultsBox.Size = new System.Drawing.Size(284, 262);
            this.searchResultsBox.TabIndex = 0;
            this.searchResultsBox.SelectedIndexChanged += new System.EventHandler(this.searchResultsBox_SelectedIndexChanged);
            // 
            // DockableSearchResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.searchResultsBox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Name = "DockableSearchResult";
            this.Text = "Search Results";
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListBox searchResultsBox;


    }
}
