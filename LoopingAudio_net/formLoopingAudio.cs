using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;

namespace LoopingAudio_net
{
    public partial class formLoopingAudio : Form
    {
        private MediaPlayer mediaPlayer = null;
        private DispatcherTimer timer = null;

        private const int MaxSongLength = 3600;
        private const int SmallestLoopInterval = 3;

        private const string MinSecsFormat = @"m\:ss"; //.f
        private const string DefaultStartTime = "00:00";
        private const string DefaultEndTime = "59:59";
        private const string DefaultMidTime = "29:59";

        private bool listenForLoop;
        private bool allowedSong;

        public formLoopingAudio()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            mediaPlayer = new MediaPlayer();
            timer.Interval = TimeSpan.FromSeconds(1);

            allowedSong = true;
            listenForLoop = false;

            EnableOrDisableButtons(false);
        }

        private void PlayMusic()
        {
            btnPlayOrPause.Text = "Pause";
            timer.Start();
            mediaPlayer.Play();
        }

        private void PauseMusic()
        {
            btnPlayOrPause.Text = "Play";
            mediaPlayer.Pause();
            timer.Stop();
        }

        private void EnableOrDisableButtons(bool enable)
        {
            musicBar.Enabled = 
            btnTimestamp.Enabled =
            btnSetLoopPoints.Enabled =
            btnClearLoopPoints.Enabled =
            btnPlayOrPause.Enabled =
            btnClearSong.Enabled = enable;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text =
                String.Format(mediaPlayer.Position.ToString(MinSecsFormat));
            musicBar.Value = (int)mediaPlayer.Position.TotalSeconds;

            //FIXED: trackbar goes to a certain point, then resets to beginning and continues...
            //find a way to fix this

            //Identified a problem where the trackbar's value would go up to 59, then reset back to 0

            //I believe it has something to do with the MediaPlayer.Position property, because when
            //I tested the MouseUp event on the musicBar, the label updated the value properly
            //then when I dragged the bar across the tool, the label updated but the bar
            //itself reseted to 0.

            //The solution was to change .Seconds to (int)...TotalSeconds. The cast is needed
            //because TotalSeconds returns a double
        }

        private void ResetAfterClearingSong()
        {
            txtLoopStartPoint.Text = txtLoopEndPoint.Text = "";
            lblLoopStartPoint.Text = DefaultStartTime; 
            lblLoopEndPoint.Text = DefaultEndTime;
            
            lblSongName.Text = "(mp3 Song Here)";
            btnPlayOrPause.Text = "Play";

            lblCurrentTime.Text = DefaultMidTime;
            lblEndTime.Text = DefaultEndTime;

            if(listenForLoop)
            {
                lblCurrentTime.TextChanged -= lblCurrentTime_TextChanged;
                listenForLoop = false;
            }          
        }

        private void btnClearLoopPoints_Click(object sender, EventArgs e)
        {
            txtLoopStartPoint.Text = txtLoopEndPoint.Text = "";
            lblLoopStartPoint.Text = DefaultStartTime;
            lblLoopEndPoint.Text = DefaultEndTime;

            if(listenForLoop)
            {
                listenForLoop = false;
                lblCurrentTime.TextChanged -= lblCurrentTime_TextChanged;
            }           
        }

        private void openTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
                openFileDialog.FileName = "";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {            
                    mediaPlayer.Open(new Uri(openFileDialog.FileName));

                    //It seems that the MediaOpened event only fires during the timer1_tick method and
                    //nowhere else. Unless I overcome this hurdle, I can't do my checks beneath

                    //I learned the basics of eventhandlers and created my own event handler that will fire 
                    //when this method is called
                    
                    mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
                    if(!allowedSong)
                    {
                        allowedSong = true;
                        MessageBox.Show("The song duration is too long!");
                        return;
                    }

                    EnableOrDisableButtons(true);

                    timer.Tick += timer1_Tick;                
                    timer.Start();
                    //mediaPlayer.Play();
                    
                    lblSongName.Text = openFileDialog.SafeFileName.Replace(".mp3", "");
                }
            }
        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds <= MaxSongLength)
            {
                lblLoopEndPoint.Text = 
                    lblEndTime.Text = mediaPlayer.NaturalDuration.TimeSpan.ToString(MinSecsFormat);
                musicBar.Maximum = (int)mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;          
            }
            else
                allowedSong = false;
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            PauseMusic();
        }

        private void btnClearSong_Click(object sender, EventArgs e)
        {
            //if(mediaPlayer.Source != null)
            mediaPlayer.Close();
            timer.Stop();
            timer.Tick -= timer1_Tick;

            musicBar.Value = 0;          
            musicBar.Maximum = MaxSongLength;
            
            ResetAfterClearingSong();

            EnableOrDisableButtons(false);
        }

        private void musicBar_MouseUp(object sender, MouseEventArgs e)
        {
            mediaPlayer.Position = TimeSpan.FromSeconds(musicBar.Value);
        }

        private void btnTimestamp_Click(object sender, EventArgs e)
        {
            //FIXED: time value goes to the trackbar's maximum value instead of user inputted time.

            //the problem comes from how TimeSpan parses the numbers. It over/underflows and
            //makes the track go to the end or beginning. This was found by writing my own 
            //basic logic on getting the total seconds of the mm:ss format

            //I found the issue. I realized that .Parse by default uses h:mm, which would explain
            //the very large numbers. My solution is to use .ParseExact and add my own specific format
            //which in my case is mm:ss (or m:ss)
            if (Validator.IsCorrectFormat(txtTimestamps, MinSecsFormat))
                mediaPlayer.Position = TimeSpan.ParseExact
                    (txtTimestamps.Text, MinSecsFormat, CultureInfo.InvariantCulture);

            //if (!Validator.IsEmpty(txtTimestamps))
            //{
                
            //}          
        }

        private void volumeBar_MouseUp(object sender, MouseEventArgs e)
        {
            //Dividing by 10.0 for converting to double
            //"Convert.ToDouble()" wasn't getting the desired output. It was always zero
            mediaPlayer.Volume = volumeBar.Value / 10.0; //mediaPlayer.Volume is on a scale 
            lblVolumeValue.Text = volumeBar.Value.ToString(); //between 0 and 1, default is 0.5
        }

        private int ParseMinSecs(string time)
        {
            string[] split = time.Split(':');
            int value = (Convert.ToInt32(split[0]) * 60) + Convert.ToInt32(split[1]);
            return value;

            //int min = (int)TimeSpan.Parse(txtLoopStartPoint.Text).TotalSeconds; //computes as hours...
            //int max = (int)TimeSpan.Parse(txtLoopEndPoint.Text).TotalSeconds;
        }

        private void btnSetLoopPoints_Click(object sender, EventArgs e)
        {
            if (Validator.IsCorrectFormat(txtLoopStartPoint, MinSecsFormat) &&
                    Validator.IsCorrectFormat(txtLoopEndPoint, MinSecsFormat))
            {
                int min = ParseMinSecs(txtLoopStartPoint.Text);
                int max = ParseMinSecs(txtLoopEndPoint.Text);

                if (Validator.IsMinLessThanMax(min, max))
                {
                    if(!Validator.IsGreaterThanInterval(min, max, SmallestLoopInterval))
                    {
                        MessageBox.Show($"The loop interval must be at least {SmallestLoopInterval} seconds apart.");
                        return;
                    }

                    lblLoopStartPoint.Text = txtLoopStartPoint.Text;
                    lblLoopEndPoint.Text = txtLoopEndPoint.Text;

                    if (!listenForLoop)
                    {
                        listenForLoop = true;
                        lblCurrentTime.TextChanged += lblCurrentTime_TextChanged;
                    }
                }
            }
            //if (!Validator.IsEmpty(txtLoopStartPoint) && !Validator.IsEmpty(txtLoopEndPoint))
            //{
                    
            //}           
        }

        private void lblCurrentTime_TextChanged(object sender, EventArgs e)
        {
            if (lblCurrentTime.Text == lblLoopEndPoint.Text)
            {
                mediaPlayer.Position = TimeSpan.ParseExact
                        (lblLoopStartPoint.Text, MinSecsFormat, CultureInfo.InvariantCulture);
            }
        }

        private void lblVolumeValue_TextChanged(object sender, EventArgs e)
        {
            if (lblVolumeValue.Text == "0")
                lblVolumeValue.ForeColor = System.Drawing.Color.Red;
            else if(lblVolumeValue.Text == "10")
                lblVolumeValue.ForeColor = System.Drawing.Color.Green;
            else
                lblVolumeValue.ForeColor = System.Drawing.Color.Black;
        }

        private void volumeBar_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void btnPlayOrPause_Click(object sender, EventArgs e)
        {
            if(btnPlayOrPause.Text == "Play")
            {    
                PlayMusic();
            }
            else if(btnPlayOrPause.Text == "Pause")
            {        
                PauseMusic();
            }
        }

        private void lblVolumeValue_Click(object sender, EventArgs e)
        {

        }

        private void volumeBar_Scroll(object sender, EventArgs e)
        {

        }

        private void musicBar_Scroll(object sender, EventArgs e)
        {
            toolTipMusic.SetToolTip(musicBar, 
                TimeSpan.FromSeconds(musicBar.Value).ToString(MinSecsFormat));
        }

        private void musicBar_MouseDown(object sender, MouseEventArgs e)
        {
            //TODO: when I click and drag the music bar, the music and timer should pause
            //until I release the mouse button. This will prevent the user from
            //having their click and drag motions be taken away by the moving scroll bar
        }
    }
}
