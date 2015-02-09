namespace HaRepackerLib.Controls
{
    partial class SoundPlayer
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
            this.components = new System.ComponentModel.Container();
            this.containerPanel = new System.Windows.Forms.Panel();
            this.LoopBox = new System.Windows.Forms.CheckBox();
            this.TimeBar = new System.Windows.Forms.TrackBar();
            this.CurrentPositionLabel = new System.Windows.Forms.Label();
            this.LengthLabel = new System.Windows.Forms.Label();
            this.PauseButton = new System.Windows.Forms.Button();
            this.PlayButton = new System.Windows.Forms.Button();
            this.AudioTimer = new System.Windows.Forms.Timer(this.components);
            this.containerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // containerPanel
            // 
            this.containerPanel.Controls.Add(this.LoopBox);
            this.containerPanel.Controls.Add(this.TimeBar);
            this.containerPanel.Controls.Add(this.CurrentPositionLabel);
            this.containerPanel.Controls.Add(this.LengthLabel);
            this.containerPanel.Controls.Add(this.PauseButton);
            this.containerPanel.Controls.Add(this.PlayButton);
            this.containerPanel.Location = new System.Drawing.Point(3, 3);
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Size = new System.Drawing.Size(304, 79);
            this.containerPanel.TabIndex = 12;
            // 
            // LoopBox
            // 
            this.LoopBox.Location = new System.Drawing.Point(66, 7);
            this.LoopBox.Name = "LoopBox";
            this.LoopBox.Size = new System.Drawing.Size(58, 21);
            this.LoopBox.TabIndex = 38;
            this.LoopBox.Text = "Loop";
            this.LoopBox.CheckedChanged += new System.EventHandler(this.LoopBox_CheckedChanged);
            // 
            // TimeBar
            // 
            this.TimeBar.Location = new System.Drawing.Point(4, 36);
            this.TimeBar.Name = "TimeBar";
            this.TimeBar.Size = new System.Drawing.Size(203, 45);
            this.TimeBar.TabIndex = 37;
            this.TimeBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.TimeBar.Scroll += new System.EventHandler(this.TimeBar_Scroll);
            // 
            // CurrentPositionLabel
            // 
            this.CurrentPositionLabel.AutoSize = true;
            this.CurrentPositionLabel.BackColor = System.Drawing.Color.Transparent;
            this.CurrentPositionLabel.Location = new System.Drawing.Point(213, 36);
            this.CurrentPositionLabel.Name = "CurrentPositionLabel";
            this.CurrentPositionLabel.Size = new System.Drawing.Size(42, 13);
            this.CurrentPositionLabel.TabIndex = 36;
            this.CurrentPositionLabel.Text = "00:00 /";
            // 
            // LengthLabel
            // 
            this.LengthLabel.AutoSize = true;
            this.LengthLabel.BackColor = System.Drawing.Color.Transparent;
            this.LengthLabel.Location = new System.Drawing.Point(256, 36);
            this.LengthLabel.Name = "LengthLabel";
            this.LengthLabel.Size = new System.Drawing.Size(34, 13);
            this.LengthLabel.TabIndex = 35;
            this.LengthLabel.Text = "00:00";
            // 
            // PauseButton
            // 
            this.PauseButton.FlatAppearance.BorderSize = 0;
            this.PauseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PauseButton.Image = global::HaRepackerLib.Properties.Resources.Pause;
            this.PauseButton.Location = new System.Drawing.Point(3, 3);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(57, 27);
            this.PauseButton.TabIndex = 33;
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Visible = false;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // PlayButton
            // 
            this.PlayButton.BackColor = System.Drawing.SystemColors.Control;
            this.PlayButton.FlatAppearance.BorderSize = 0;
            this.PlayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayButton.Image = global::HaRepackerLib.Properties.Resources.Play;
            this.PlayButton.Location = new System.Drawing.Point(3, 3);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(57, 27);
            this.PlayButton.TabIndex = 32;
            this.PlayButton.UseVisualStyleBackColor = false;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // AudioTimer
            // 
            this.AudioTimer.Tick += new System.EventHandler(this.AudioTimer_Tick);
            // 
            // SoundPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.containerPanel);
            this.MaximumSize = new System.Drawing.Size(310, 86);
            this.MinimumSize = new System.Drawing.Size(310, 86);
            this.Name = "SoundPlayer";
            this.Size = new System.Drawing.Size(310, 86);
            this.containerPanel.ResumeLayout(false);
            this.containerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel containerPanel;
        private System.Windows.Forms.CheckBox LoopBox;
        private System.Windows.Forms.TrackBar TimeBar;
        private System.Windows.Forms.Label CurrentPositionLabel;
        private System.Windows.Forms.Label LengthLabel;
        private System.Windows.Forms.Button PauseButton;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.Timer AudioTimer;

    }
}
