namespace HaCreator.GUI
{
    partial class Load
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Load));
            this.loadButton = new System.Windows.Forms.Button();
            this.WZSelect = new System.Windows.Forms.RadioButton();
            this.XMLSelect = new System.Windows.Forms.RadioButton();
            this.XMLBox = new System.Windows.Forms.TextBox();
            this.NewSelect = new System.Windows.Forms.RadioButton();
            this.newWidth = new System.Windows.Forms.NumericUpDown();
            this.newHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.browseXML = new System.Windows.Forms.Button();
            this.newTab = new System.Windows.Forms.CheckBox();
            this.mapBrowser = new HaCreator.CustomControls.MapBrowser();
            ((System.ComponentModel.ISupportInitialize)(this.newWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // loadButton
            // 
            this.loadButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.loadButton.Enabled = false;
            this.loadButton.Location = new System.Drawing.Point(169, 300);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(200, 30);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Load";
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // WZSelect
            // 
            this.WZSelect.AutoSize = true;
            this.WZSelect.Checked = true;
            this.WZSelect.Location = new System.Drawing.Point(11, 60);
            this.WZSelect.Name = "WZSelect";
            this.WZSelect.Size = new System.Drawing.Size(43, 17);
            this.WZSelect.TabIndex = 2;
            this.WZSelect.TabStop = true;
            this.WZSelect.Text = "WZ";
            this.WZSelect.UseVisualStyleBackColor = true;
            this.WZSelect.CheckedChanged += new System.EventHandler(this.selectionChanged);
            // 
            // XMLSelect
            // 
            this.XMLSelect.AutoSize = true;
            this.XMLSelect.Location = new System.Drawing.Point(11, 36);
            this.XMLSelect.Name = "XMLSelect";
            this.XMLSelect.Size = new System.Drawing.Size(47, 17);
            this.XMLSelect.TabIndex = 3;
            this.XMLSelect.Text = "XML";
            this.XMLSelect.UseVisualStyleBackColor = true;
            this.XMLSelect.CheckedChanged += new System.EventHandler(this.selectionChanged);
            // 
            // XMLBox
            // 
            // 
            // 
            // 
            this.XMLBox.Enabled = false;
            this.XMLBox.Location = new System.Drawing.Point(64, 35);
            this.XMLBox.Name = "XMLBox";
            this.XMLBox.Size = new System.Drawing.Size(158, 20);
            this.XMLBox.TabIndex = 5;
            this.XMLBox.Click += new System.EventHandler(this.browseXML_Click);
            // 
            // NewSelect
            // 
            this.NewSelect.AutoSize = true;
            this.NewSelect.Location = new System.Drawing.Point(11, 13);
            this.NewSelect.Name = "NewSelect";
            this.NewSelect.Size = new System.Drawing.Size(47, 17);
            this.NewSelect.TabIndex = 8;
            this.NewSelect.TabStop = true;
            this.NewSelect.Text = "New";
            this.NewSelect.UseVisualStyleBackColor = true;
            this.NewSelect.CheckedChanged += new System.EventHandler(this.selectionChanged);
            // 
            // newWidth
            // 
            this.newWidth.Enabled = false;
            this.newWidth.Location = new System.Drawing.Point(64, 12);
            this.newWidth.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.newWidth.Name = "newWidth";
            this.newWidth.Size = new System.Drawing.Size(45, 20);
            this.newWidth.TabIndex = 9;
            this.newWidth.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            // 
            // newHeight
            // 
            this.newHeight.Enabled = false;
            this.newHeight.Location = new System.Drawing.Point(169, 12);
            this.newHeight.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.newHeight.Name = "newHeight";
            this.newHeight.Size = new System.Drawing.Size(41, 20);
            this.newHeight.TabIndex = 11;
            this.newHeight.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(109, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Width    X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(210, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Height";
            // 
            // searchBox
            // 
            // 
            // 
            // 
            this.searchBox.Location = new System.Drawing.Point(64, 59);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(158, 20);
            this.searchBox.TabIndex = 15;
            // 
            // browseXML
            // 
            this.browseXML.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.browseXML.Location = new System.Drawing.Point(228, 36);
            this.browseXML.Name = "browseXML";
            this.browseXML.Size = new System.Drawing.Size(36, 19);
            this.browseXML.TabIndex = 16;
            this.browseXML.Text = "...";
            this.browseXML.Click += new System.EventHandler(this.browseXML_Click);
            // 
            // newTab
            // 
            this.newTab.AutoSize = true;
            this.newTab.Location = new System.Drawing.Point(284, 62);
            this.newTab.Name = "newTab";
            this.newTab.Size = new System.Drawing.Size(111, 17);
            this.newTab.TabIndex = 18;
            this.newTab.Text = "Load into new tab";
            this.newTab.Visible = false;
            // 
            // mapBrowser
            // 
            this.mapBrowser.Location = new System.Drawing.Point(11, 83);
            this.mapBrowser.Name = "mapBrowser";
            this.mapBrowser.Size = new System.Drawing.Size(533, 211);
            this.mapBrowser.TabIndex = 19;
            this.mapBrowser.SelectionChanged += new HaCreator.CustomControls.MapBrowser.MapSelectChangedDelegate(this.mapBrowser_SelectionChanged);
            // 
            // Load
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 342);
            this.Controls.Add(this.mapBrowser);
            this.Controls.Add(this.newTab);
            this.Controls.Add(this.browseXML);
            this.Controls.Add(this.searchBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newHeight);
            this.Controls.Add(this.newWidth);
            this.Controls.Add(this.NewSelect);
            this.Controls.Add(this.XMLBox);
            this.Controls.Add(this.XMLSelect);
            this.Controls.Add(this.WZSelect);
            this.Controls.Add(this.loadButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Load";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Load";
            this.Load += new System.EventHandler(this.Load_Load);
            ((System.ComponentModel.ISupportInitialize)(this.newWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.RadioButton WZSelect;
        private System.Windows.Forms.RadioButton XMLSelect;
        private System.Windows.Forms.RadioButton NewSelect;
        private System.Windows.Forms.NumericUpDown newWidth;
        private System.Windows.Forms.NumericUpDown newHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button browseXML;
        private System.Windows.Forms.CheckBox newTab;
        private System.Windows.Forms.TextBox XMLBox;
        private System.Windows.Forms.TextBox searchBox;
        private CustomControls.MapBrowser mapBrowser;
    }
}