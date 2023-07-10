namespace LoopingAudio_net
{
    partial class formLoopingAudio
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTrackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFromDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCurrentTrackToDatabseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.musicBar = new System.Windows.Forms.TrackBar();
            this.lblSongName = new System.Windows.Forms.Label();
            this.btnClearSong = new System.Windows.Forms.Button();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.lblEndTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLoopStartPoint = new System.Windows.Forms.Label();
            this.lblLoopEndPoint = new System.Windows.Forms.Label();
            this.txtLoopStartPoint = new System.Windows.Forms.TextBox();
            this.txtLoopEndPoint = new System.Windows.Forms.TextBox();
            this.btnSetLoopPoints = new System.Windows.Forms.Button();
            this.btnClearLoopPoints = new System.Windows.Forms.Button();
            this.btnTimestamp = new System.Windows.Forms.Button();
            this.txtTimestamps = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.volumeBar = new System.Windows.Forms.TrackBar();
            this.lblVolumeValue = new System.Windows.Forms.Label();
            this.btnPlayOrPause = new System.Windows.Forms.Button();
            this.toolTipMusic = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.musicBar)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(330, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Now Playing";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(779, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTrackToolStripMenuItem,
            this.openFromDatabaseToolStripMenuItem,
            this.saveCurrentTrackToDatabseToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openTrackToolStripMenuItem
            // 
            this.openTrackToolStripMenuItem.Name = "openTrackToolStripMenuItem";
            this.openTrackToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.openTrackToolStripMenuItem.Text = "Open Track...";
            this.openTrackToolStripMenuItem.Click += new System.EventHandler(this.openTrackToolStripMenuItem_Click);
            // 
            // openFromDatabaseToolStripMenuItem
            // 
            this.openFromDatabaseToolStripMenuItem.Name = "openFromDatabaseToolStripMenuItem";
            this.openFromDatabaseToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.openFromDatabaseToolStripMenuItem.Text = "Open From Database...";
            // 
            // saveCurrentTrackToDatabseToolStripMenuItem
            // 
            this.saveCurrentTrackToDatabseToolStripMenuItem.Name = "saveCurrentTrackToDatabseToolStripMenuItem";
            this.saveCurrentTrackToDatabseToolStripMenuItem.Size = new System.Drawing.Size(307, 26);
            this.saveCurrentTrackToDatabseToolStripMenuItem.Text = "Save Current Track to Database...";
            // 
            // musicBar
            // 
            this.musicBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.musicBar.LargeChange = 10;
            this.musicBar.Location = new System.Drawing.Point(6, 21);
            this.musicBar.Maximum = 3600;
            this.musicBar.Name = "musicBar";
            this.musicBar.Size = new System.Drawing.Size(722, 56);
            this.musicBar.TabIndex = 4;
            this.musicBar.TickFrequency = 10;
            this.musicBar.Scroll += new System.EventHandler(this.musicBar_Scroll);
            this.musicBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.musicBar_MouseDown);
            this.musicBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.musicBar_MouseUp);
            // 
            // lblSongName
            // 
            this.lblSongName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongName.Location = new System.Drawing.Point(25, 53);
            this.lblSongName.Name = "lblSongName";
            this.lblSongName.Size = new System.Drawing.Size(730, 82);
            this.lblSongName.TabIndex = 5;
            this.lblSongName.Text = "(mp3 Song Here)";
            this.lblSongName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearSong
            // 
            this.btnClearSong.Location = new System.Drawing.Point(378, 140);
            this.btnClearSong.Name = "btnClearSong";
            this.btnClearSong.Size = new System.Drawing.Size(93, 34);
            this.btnClearSong.TabIndex = 6;
            this.btnClearSong.Text = "Clear Song";
            this.btnClearSong.UseVisualStyleBackColor = true;
            this.btnClearSong.Click += new System.EventHandler(this.btnClearSong_Click);
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartTime.Location = new System.Drawing.Point(6, 78);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(70, 32);
            this.lblStartTime.TabIndex = 7;
            this.lblStartTime.Text = "0:00";
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTime.Location = new System.Drawing.Point(338, 78);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(86, 32);
            this.lblCurrentTime.TabIndex = 8;
            this.lblCurrentTime.Text = "29:59";
            // 
            // lblEndTime
            // 
            this.lblEndTime.AutoSize = true;
            this.lblEndTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndTime.Location = new System.Drawing.Point(657, 78);
            this.lblEndTime.Name = "lblEndTime";
            this.lblEndTime.Size = new System.Drawing.Size(86, 32);
            this.lblEndTime.TabIndex = 9;
            this.lblEndTime.Text = "59:59";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 306);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "Loop Start Point";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(495, 309);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 18);
            this.label3.TabIndex = 11;
            this.label3.Text = "Loop End Point";
            // 
            // lblLoopStartPoint
            // 
            this.lblLoopStartPoint.AutoSize = true;
            this.lblLoopStartPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoopStartPoint.Location = new System.Drawing.Point(142, 147);
            this.lblLoopStartPoint.Name = "lblLoopStartPoint";
            this.lblLoopStartPoint.Size = new System.Drawing.Size(62, 25);
            this.lblLoopStartPoint.TabIndex = 12;
            this.lblLoopStartPoint.Text = "00:00";
            // 
            // lblLoopEndPoint
            // 
            this.lblLoopEndPoint.AutoSize = true;
            this.lblLoopEndPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoopEndPoint.Location = new System.Drawing.Point(583, 147);
            this.lblLoopEndPoint.Name = "lblLoopEndPoint";
            this.lblLoopEndPoint.Size = new System.Drawing.Size(62, 25);
            this.lblLoopEndPoint.TabIndex = 13;
            this.lblLoopEndPoint.Text = "59:59";
            // 
            // txtLoopStartPoint
            // 
            this.txtLoopStartPoint.Location = new System.Drawing.Point(168, 303);
            this.txtLoopStartPoint.Name = "txtLoopStartPoint";
            this.txtLoopStartPoint.Size = new System.Drawing.Size(75, 22);
            this.txtLoopStartPoint.TabIndex = 14;
            // 
            // txtLoopEndPoint
            // 
            this.txtLoopEndPoint.Location = new System.Drawing.Point(617, 306);
            this.txtLoopEndPoint.Name = "txtLoopEndPoint";
            this.txtLoopEndPoint.Size = new System.Drawing.Size(75, 22);
            this.txtLoopEndPoint.TabIndex = 15;
            // 
            // btnSetLoopPoints
            // 
            this.btnSetLoopPoints.Location = new System.Drawing.Point(287, 319);
            this.btnSetLoopPoints.Name = "btnSetLoopPoints";
            this.btnSetLoopPoints.Size = new System.Drawing.Size(82, 37);
            this.btnSetLoopPoints.TabIndex = 16;
            this.btnSetLoopPoints.Text = "Set Loops";
            this.btnSetLoopPoints.UseVisualStyleBackColor = true;
            this.btnSetLoopPoints.Click += new System.EventHandler(this.btnSetLoopPoints_Click);
            // 
            // btnClearLoopPoints
            // 
            this.btnClearLoopPoints.Location = new System.Drawing.Point(378, 314);
            this.btnClearLoopPoints.Name = "btnClearLoopPoints";
            this.btnClearLoopPoints.Size = new System.Drawing.Size(90, 47);
            this.btnClearLoopPoints.TabIndex = 17;
            this.btnClearLoopPoints.Text = "Clear Loop Points";
            this.btnClearLoopPoints.UseVisualStyleBackColor = true;
            this.btnClearLoopPoints.Click += new System.EventHandler(this.btnClearLoopPoints_Click);
            // 
            // btnTimestamp
            // 
            this.btnTimestamp.Location = new System.Drawing.Point(18, 355);
            this.btnTimestamp.Name = "btnTimestamp";
            this.btnTimestamp.Size = new System.Drawing.Size(92, 45);
            this.btnTimestamp.TabIndex = 18;
            this.btnTimestamp.Text = "Go To Timestamp";
            this.btnTimestamp.UseVisualStyleBackColor = true;
            this.btnTimestamp.Click += new System.EventHandler(this.btnTimestamp_Click);
            // 
            // txtTimestamps
            // 
            this.txtTimestamps.Location = new System.Drawing.Point(129, 366);
            this.txtTimestamps.Name = "txtTimestamps";
            this.txtTimestamps.Size = new System.Drawing.Size(75, 22);
            this.txtTimestamps.TabIndex = 19;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.musicBar);
            this.groupBox1.Controls.Add(this.lblStartTime);
            this.groupBox1.Controls.Add(this.lblCurrentTime);
            this.groupBox1.Controls.Add(this.lblEndTime);
            this.groupBox1.Location = new System.Drawing.Point(12, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(749, 122);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // volumeBar
            // 
            this.volumeBar.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.volumeBar.LargeChange = 2;
            this.volumeBar.Location = new System.Drawing.Point(574, 344);
            this.volumeBar.Name = "volumeBar";
            this.volumeBar.Size = new System.Drawing.Size(181, 56);
            this.volumeBar.TabIndex = 21;
            this.volumeBar.TickFrequency = 10;
            this.volumeBar.Value = 5;
            this.volumeBar.Scroll += new System.EventHandler(this.volumeBar_Scroll);
            this.volumeBar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.volumeBar_KeyUp);
            this.volumeBar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.volumeBar_MouseUp);
            // 
            // lblVolumeValue
            // 
            this.lblVolumeValue.AutoSize = true;
            this.lblVolumeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVolumeValue.Location = new System.Drawing.Point(537, 355);
            this.lblVolumeValue.Name = "lblVolumeValue";
            this.lblVolumeValue.Size = new System.Drawing.Size(18, 20);
            this.lblVolumeValue.TabIndex = 22;
            this.lblVolumeValue.Text = "5";
            this.lblVolumeValue.TextChanged += new System.EventHandler(this.lblVolumeValue_TextChanged);
            this.lblVolumeValue.Click += new System.EventHandler(this.lblVolumeValue_Click);
            // 
            // btnPlayOrPause
            // 
            this.btnPlayOrPause.Location = new System.Drawing.Point(294, 140);
            this.btnPlayOrPause.Name = "btnPlayOrPause";
            this.btnPlayOrPause.Size = new System.Drawing.Size(75, 34);
            this.btnPlayOrPause.TabIndex = 23;
            this.btnPlayOrPause.Text = "Play";
            this.btnPlayOrPause.UseVisualStyleBackColor = true;
            this.btnPlayOrPause.Click += new System.EventHandler(this.btnPlayOrPause_Click);
            // 
            // toolTipMusic
            // 
            this.toolTipMusic.AutoPopDelay = 500;
            this.toolTipMusic.InitialDelay = 500;
            this.toolTipMusic.ReshowDelay = 100;
            // 
            // formLoopingAudio
            // 
            this.AcceptButton = this.btnPlayOrPause;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 410);
            this.Controls.Add(this.btnPlayOrPause);
            this.Controls.Add(this.lblVolumeValue);
            this.Controls.Add(this.volumeBar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblLoopStartPoint);
            this.Controls.Add(this.txtTimestamps);
            this.Controls.Add(this.btnTimestamp);
            this.Controls.Add(this.btnClearLoopPoints);
            this.Controls.Add(this.btnSetLoopPoints);
            this.Controls.Add(this.txtLoopEndPoint);
            this.Controls.Add(this.txtLoopStartPoint);
            this.Controls.Add(this.lblLoopEndPoint);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClearSong);
            this.Controls.Add(this.lblSongName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "formLoopingAudio";
            this.Text = "Looping Audio";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.musicBar)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volumeBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTrackToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFromDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCurrentTrackToDatabseToolStripMenuItem;
        private System.Windows.Forms.TrackBar musicBar;
        private System.Windows.Forms.Label lblSongName;
        private System.Windows.Forms.Button btnClearSong;
        private System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Label lblEndTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLoopStartPoint;
        private System.Windows.Forms.Label lblLoopEndPoint;
        private System.Windows.Forms.TextBox txtLoopStartPoint;
        private System.Windows.Forms.TextBox txtLoopEndPoint;
        private System.Windows.Forms.Button btnSetLoopPoints;
        private System.Windows.Forms.Button btnClearLoopPoints;
        private System.Windows.Forms.Button btnTimestamp;
        private System.Windows.Forms.TextBox txtTimestamps;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar volumeBar;
        private System.Windows.Forms.Label lblVolumeValue;
        private System.Windows.Forms.Button btnPlayOrPause;
        private System.Windows.Forms.ToolTip toolTipMusic;
    }
}

