using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;

namespace LoopingAudio_net
{
    public partial class formLoopingAudio : Form
    {
        public formLoopingAudio()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            mediaPlayer = new MediaPlayer();
            timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Interval = TimeSpan.FromMilliseconds(1);

            allowedSong = true;
            listenForLoop = false;
            isSongPlaying = false;
            isSongFromDatabase = false;

            EnableOrDisableButtons(false);

            loopingAudioForm = this;

            //MessageBox.Show(BinDebug);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = mediaPlayer.Position.ToString(MinSecsFormat);
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

        private void btnClearLoopPoints_Click(object sender, EventArgs e)
        {
            lblLoopStartPoint.Text = DefaultStartTime;
            lblLoopEndPoint.Text = mediaPlayer.NaturalDuration.TimeSpan.ToString(MinSecsFormat);
            lblLoopStartPoint.ForeColor = lblLoopEndPoint.ForeColor = System.Drawing.Color.Black;

            if (listenForLoop)
            {
                listenForLoop = false;
                lblCurrentTime.TextChanged -= lblCurrentTime_TextChanged;
            }           
        }

        private void openTrackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
                openFileDialog.FileName = "";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    mediaPlayer.Open(new Uri(openFileDialog.FileName));
                    name = openFileDialog.SafeFileName.Replace(".mp3", "");
                }
                else
                    return; 
            }

            //It seems that the MediaOpened event only fires during the timer1_tick method and
            //nowhere else. Unless I overcome this hurdle, I can't do my checks beneath

            //I learned the basics of eventhandlers and created my own event handler that will fire 
            //when this method is called
            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            if (!allowedSong)
            {
                allowedSong = true;
                mediaPlayer.MediaOpened -= MediaPlayer_MediaOpened;
                MessageBox.Show($"The song duration cannot be longer than {MaxSongLength / 60} minutes" +
                    $"or shorter than {SmallestLoopInterval} seconds!");
                return;
            }

            CheckForSongLooping();
            DeleteExternalDatabaseSong();
            EnableOrDisableButtons(true);

            timer.Tick += timer1_Tick;
            PlayMusic();
            lblSongName.Text = name;
        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds <= MaxSongLength &&
                mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds >= SmallestLoopInterval)
            {
                lblEndTime.Text = mediaPlayer.NaturalDuration.TimeSpan.ToString(MinSecsFormat);

                if (!isSongFromDatabase)
                    lblLoopEndPoint.Text = mediaPlayer.NaturalDuration.TimeSpan.ToString(MinSecsFormat);
                else
                    lblLoopEndPoint.Text = txtLoopEndPoint.Text;

                musicBar.Maximum = (int)mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;          
            }
            else
                allowedSong = false;
        }

        private void btnClearSong_Click(object sender, EventArgs e)
        {
            //if(mediaPlayer.Source != null)
            mediaPlayer.MediaOpened -= MediaPlayer_MediaOpened;
            mediaPlayer.Close();
            timer.Stop();
            timer.Tick -= timer1_Tick;

            musicBar.Value = 0;          
            musicBar.Maximum = MaxSongLength;
            
            ResetAfterClearingSong();
            DeleteExternalDatabaseSong();

            EnableOrDisableButtons(false);
        }  

        private void musicBar_MouseUp(object sender, MouseEventArgs e)
        {
            btnPlayOrPause.Enabled = true;
            AdjustMusicPosition();
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
            {
                mediaPlayer.Position = TimeSpan.ParseExact
                    (txtTimestamps.Text, MinSecsFormat, CultureInfo.InvariantCulture);
            }                      
        }

        private void volumeBar_MouseUp(object sender, MouseEventArgs e)
        {
            AdjustVolume();
        }

        private void btnSetLoopPoints_Click(object sender, EventArgs e)
        {
            SetLoopPoints();
        }
        
        private void lblCurrentTime_TextChanged(object sender, EventArgs e)
        {
            //Changing "==" to "<=" fixes the issues of position going beyond loop point,
            //but prevents user manually going beyond the same pont. A fair trade off.
            if (loopEndPoint <= (int)mediaPlayer.Position.TotalSeconds)
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

        private void btnPlayOrPause_Click(object sender, EventArgs e)
        {
            if(btnPlayOrPause.Text == Play)
            {
                PlayMusic(); 
            }                                                 
            else if(btnPlayOrPause.Text == Pause)
            {               
                PauseMusic();
                isSongPlaying = false;
            }                 
        }

        private void volumeBar_Scroll(object sender, EventArgs e)
        {
            lblVolumeValue.Text = volumeBar.Value.ToString();
        }

        private void musicBar_Scroll(object sender, EventArgs e)
        {
            lblCurrentTime.Text = TimeSpan.FromSeconds(musicBar.Value).ToString(MinSecsFormat);
        }

        private void musicBar_MouseDown(object sender, MouseEventArgs e)
        {
            //FIXED: when I click and drag the music bar, the music and timer should pause
            //until I release the mouse button. This will prevent the user from
            //having their click and drag motions be taken away by the moving scroll bar

            //solved by using the trackbar mousedown and mouseup events
            PauseMusic();
            btnPlayOrPause.Enabled = false;
        }

        //Finished: Right and Left Arrow Key compatability with trackbar for music and volume respectively
        //TODO: ONLY left and right arrow keys should work, and nothing else
        //current problem: any key would briefly pause the player before it continues
        //up and down keys move the trackbar, but since there is no event tied to it, no data changes
        private void musicBar_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Left:
                    PauseMusic();
                    btnPlayOrPause.Enabled = false;
                    break;
            }
        }

        private void musicBar_KeyUp(object sender, KeyEventArgs e)
        {
            btnPlayOrPause.Enabled = true;
            AdjustMusicPosition();          
        }

        private void volumeBar_KeyDown(object sender, KeyEventArgs e)
        {
            //lblVolumeValue.Text = volumeBar.Value.ToString();
            
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Left:
                    AdjustVolume();
                    break;
            }
        }

        private void musicBar_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Left:
                    e.IsInputKey = true;
                    break;
                default:
                    e.IsInputKey = false;
                    break;

            }
        }

        private void volumeBar_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Left:
                    e.IsInputKey = true;
                    break;
                default: e.IsInputKey = false;
                    break;
            }
        }

        private void volumeBar_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void openFromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            databaseSongForm = formDatabaseSongs.GetInstance();
            databaseSongForm.Show();
        }

        private void btnSaveToDatabase_Click(object sender, EventArgs e)
        {
            AudioDB audio = new AudioDB();
            Music newMusic = null;
            try
            {
                newMusic = new Music(lblSongName.Text, lblLoopStartPoint.Text, lblLoopEndPoint.Text, 
                                        File.ReadAllBytes(mediaPlayer.Source.OriginalString));
            }
            catch(FileNotFoundException ex)
            {
                MessageBox.Show("This file doesn't exist at path:/r/n" + ex.Message);
                return;
            }
            audio.InsertOrUpdateSong(newMusic);
            MessageBox.Show("Song added/updated in database.");
        }

        private void formLoopingAudio_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeleteExternalDatabaseSong();
        }
    }
}
