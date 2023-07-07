using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;
using Microsoft.Win32;

namespace LoopingAudio_net
{
    //https://wpf-tutorial.com/audio-video/playing-audio/
    //https://stackoverflow.com/questions/8109358/passing-variable-with-routedeventargs


    //NEXT STEP IS TO BIND TRACKBAR TO DURATION OF MEDIA
    //ALSO INCLUDE A SEPERATE TRACKBAR TO REPRESENT THE VOLUME
    //https://stackoverflow.com/questions/24289297/how-to-make-trackbar-works-while-media-is-playing
    public partial class formLoopingAudio : Form
    {
        private MediaPlayer mediaPlayer = null;
        private DispatcherTimer timer = null;
        private const int MaxSongLength = 3600;
        private bool allowedSong;

        public formLoopingAudio()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            mediaPlayer = new MediaPlayer();
            timer.Interval = TimeSpan.FromSeconds(1);
            allowedSong = true;
            //musicBar.Enabled = false;
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (mediaPlayer.Source != null)
            {
                mediaPlayer.Play();
            }           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //for audio loop, may have to use a delegate or an involke...
            lblCurrentTime.Text =
                String.Format(mediaPlayer.Position.ToString(@"mm\:ss"));
            musicBar.Value = mediaPlayer.Position.Seconds;
        }

        private void ResetStuffandChangeNameofmethodlater()
        {
            txtLoopStartPoint.Text = txtLoopEndPoint.Text = "";
            lblLoopStartPoint.Text = "00:00"; //TODO: assign to start of media song
            lblLoopEndPoint.Text = "59:59"; //TODO: assign to end of media song
        }

        private void btnClearLoopPoints_Click(object sender, EventArgs e)
        {
            //txtLoopStartPoint.Text = txtLoopEndPoint.Text = "";
            //lblLoopStartPoint.Text = "00:00"; //TODO: assign to start of media song
            //lblLoopEndPoint.Text = "59:59"; //TODO: assign to end of media song

            ResetStuffandChangeNameofmethodlater();
        }

        //private void media_MediaOpened(object sender, RoutedEventArgs e)
        //{
        //    if (mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds > MaxSongLength)
        //    {
        //        lblEndTime.Text = mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
        //    }
        //}

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

                    musicBar.Enabled = true;
                    timer.Tick += timer1_Tick;                
                    timer.Start();
                    mediaPlayer.Play();
                    lblSongName.Text = openFileDialog.SafeFileName;
                }
            }
        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            //if(mediaPlayer.NaturalDuration.HasTimeSpan)

            if (mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds <= MaxSongLength)
            {
                lblEndTime.Text = mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss");
                musicBar.Maximum = (int)mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                //musicBar.Maximum = mediaPlayer.NaturalDuration.TimeSpan.Seconds +
                //    (60 * mediaPlayer.NaturalDuration.TimeSpan.Minutes);

                //TODO: trackbar goes to a certain point, then resets to beginning and continues...
                //find a way to fix this

                //Identified a problem where the trackbar's value would go up to 59, then reset back to 0

                //I believe it has something to do with the MediaPlayer.Position property, because when
                //I tested the MouseUp event on the musicBar, the label updated the value properly
                //then when I dragged the bar across the tool, the label updated but the bar
                //itself reseted to 0.
            }
            else
                allowedSong = false;
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if(mediaPlayer.Source != null)
                mediaPlayer.Pause();
        }

        private void btnClearSong_Click(object sender, EventArgs e)
        {
            if(mediaPlayer.Source != null)
            {
                mediaPlayer.Close();

                musicBar.Value = 0;
                musicBar.Enabled = false;
                
                lblSongName.Text = "";
                lblEndTime.Text = "59:59";
                musicBar.Maximum = MaxSongLength;
                ResetStuffandChangeNameofmethodlater();
            }
        }

        private void musicBar_MouseUp(object sender, MouseEventArgs e)
        {
            mediaPlayer.Position = TimeSpan.FromSeconds(musicBar.Value); //this line of code is key for music loops
            //lblStartTime.Text = musicBar.Value.ToString(); //debugging
        }

        private void btnTimestamp_Click(object sender, EventArgs e)
        {
            //TODO: time value goes to the trackbar's maximum value instead of user inputted time.
            //fix this later
            //mediaPlayer.Position = TimeSpan.Parse(txtTimestamps.Text);
        }

        private void volumeBar_MouseUp(object sender, MouseEventArgs e)
        {
            //Dividing by 10.0 for converting to double
            //"Convert.ToDouble()" wasn't getting the desired output
            mediaPlayer.Volume = volumeBar.Value / 10.0; //on a scale between 0 and 1, default is 0.5
            lblVolumeValue.Text = volumeBar.Value.ToString();
        }
    }
}
