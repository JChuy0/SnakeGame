namespace SnakeGame
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            grpStats = new GroupBox();
            lblScore = new Label();
            tmrUpdate = new System.Windows.Forms.Timer(components);
            tmrWallUpdate = new System.Windows.Forms.Timer(components);
            grpStats.SuspendLayout();
            SuspendLayout();
            // 
            // grpStats
            // 
            grpStats.Controls.Add(lblScore);
            grpStats.Font = new Font("Segoe UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            grpStats.Location = new Point(1, 374);
            grpStats.Name = "grpStats";
            grpStats.Size = new Size(680, 80);
            grpStats.TabIndex = 0;
            grpStats.TabStop = false;
            grpStats.Text = "Stats:";
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Location = new Point(263, 32);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(115, 38);
            lblScore.TabIndex = 0;
            lblScore.Text = "Score: 0";
            // 
            // tmrUpdate
            // 
            tmrUpdate.Tick += TmrUpdate_Tick;
            // 
            // tmrWallUpdate
            // 
            tmrWallUpdate.Interval = 5000;
            tmrWallUpdate.Tick += TmrWallUpdate_Tick;
            // 
            // frmMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(682, 453);
            Controls.Add(grpStats);
            Name = "frmMain";
            Text = "Snake Game";
            grpStats.ResumeLayout(false);
            grpStats.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpStats;
        private Label lblScore;
        private System.Windows.Forms.Timer tmrUpdate;
        private System.Windows.Forms.Timer tmrWallUpdate;
    }
}
