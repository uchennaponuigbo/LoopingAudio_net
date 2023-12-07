using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Windows.Media;

namespace LoopingAudio_net
{
    public partial class formLoopingAudio
    {
        private MediaPlayer mediaPlayer = null;
        private DispatcherTimer timer = null;

        formDatabaseSongs databaseSongForm = new formDatabaseSongs();
        internal static formLoopingAudio loopingAudioForm = null;

        private const int MaxSongLength = 3600;
        private const int SmallestLoopInterval = 3;
        private int loopEndPoint = MaxSongLength;

        private const string MinSecsFormat = @"m\:ss"; //.f
        private const string DefaultStartTime = "0:00";
        private const string DefaultEndTime = "60:00";
        private const string DefaultMidTime = "30:00";

        private const string Play = "▶";
        private const string Pause = "❚❚";

        private readonly string BinDebug = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private string databaseSongAbsPath = "";

        private bool listenForLoop;
        private bool allowedSong;
        private bool isSongPlaying;
        private bool isSongFromDatabase;

        private void PlayMusic()
        {
            btnPlayOrPause.Text = Pause;
            timer.Start();
            mediaPlayer.Play();
            isSongPlaying = true;
        }

        private void PauseMusic()
        {
            btnPlayOrPause.Text = Play;
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
            btnClearSong.Enabled =
            btnSaveToDatabase.Enabled =
            enable;
        }

        private void ResetAfterClearingSong()
        {
            txtLoopStartPoint.Text = txtLoopEndPoint.Text = txtTimestamps.Text = "";
            lblLoopStartPoint.Text = DefaultStartTime;
            lblLoopEndPoint.Text = DefaultEndTime;

            lblLoopStartPoint.ForeColor = lblLoopEndPoint.ForeColor = System.Drawing.Color.Black;
            //CheckForSongLooping();

            lblSongName.Text = "(mp3 Song Here)";
            btnPlayOrPause.Text = Play;

            lblCurrentTime.Text = DefaultMidTime;
            lblEndTime.Text = DefaultEndTime;

            loopEndPoint = MaxSongLength;
            isSongPlaying = false;


            if (listenForLoop)
            {
                lblCurrentTime.TextChanged -= lblCurrentTime_TextChanged;
                listenForLoop = false;
            }

            DeleteExternalDatabaseSong();
        }

        private void AdjustMusicPosition()
        {
            mediaPlayer.Position = TimeSpan.FromSeconds(musicBar.Value);
            if (isSongPlaying) //  mediaPlayer.IsSongPlaying
                PlayMusic();

            //There is no property that indicates if a media is currently playing,
            //so I had to add my own flag
        }

        private void AdjustVolume()
        {
            //Dividing by 10.0 for converting to double
            //"Convert.ToDouble()" wasn't getting the desired output. It was always zero
            mediaPlayer.Volume = volumeBar.Value / 10.0;
            //mediaPlayer.Volume is on a scale between 0 and 1, default is 0.5
        }

        private int ParseMinSecs(string time)
        {
            string[] split = time.Split(':');
            int value = (Convert.ToInt32(split[0]) * 60) + Convert.ToInt32(split[1]);
            return value;
        }

        private void SetLoopPoints()
        {
            if (Validator.IsCorrectFormat(txtLoopStartPoint, MinSecsFormat) &&
                   Validator.IsCorrectFormat(txtLoopEndPoint, MinSecsFormat))
            {
                int min = ParseMinSecs(txtLoopStartPoint.Text);
                int max = ParseMinSecs(txtLoopEndPoint.Text);

                if (Validator.IsMinLessThanMax(min, max))
                {
                    if (!Validator.IsGreaterThanInterval(min, max, SmallestLoopInterval))
                    {
                        MessageBox.Show($"The loop interval must be at least {SmallestLoopInterval} seconds apart.");
                        return;
                    }

                    lblLoopStartPoint.Text = txtLoopStartPoint.Text;
                    lblLoopEndPoint.Text = txtLoopEndPoint.Text;
                    loopEndPoint = max;

                    lblLoopStartPoint.ForeColor = lblLoopEndPoint.ForeColor = System.Drawing.Color.Green;

                    if (!listenForLoop)
                    {
                        listenForLoop = true;
                        lblCurrentTime.TextChanged += lblCurrentTime_TextChanged;
                    }
                }
            }
        }

        private void DeleteExternalDatabaseSong()
        {
            if (isSongFromDatabase)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    File.Delete(databaseSongAbsPath);
                    Cursor.Current = Cursors.Default;
                }
                catch (IOException ex)
                {
                    MessageBox.Show
                        ($"The file could not be deleted./r/nPath: {databaseSongAbsPath}/r/n {ex}");
                }

                isSongFromDatabase = false;
                databaseSongAbsPath = "";
            }
        }

        internal void PlaySongFromDatabase(Music music)
        {
            if (databaseSongForm != null)
            {
                DeleteExternalDatabaseSong(); //if there is another song playing from DB...

                //create the file and store it and path to it in bin/debug folder
                //the file will be deleted once the user is done with the song

                using (MemoryStream m = new MemoryStream(music.Song))
                {
                    string filename = $"{music.Name}.mp3";
                    try
                    {
                        using (FileStream file = new FileStream(filename, FileMode.Create, FileAccess.Write))
                        {
                            m.CopyTo(file);
                            databaseSongAbsPath = Path.Combine(BinDebug, filename);
                            mediaPlayer.Open(new Uri(databaseSongAbsPath));
                        }
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
                mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
                if (!allowedSong)
                {
                    allowedSong = true;
                    mediaPlayer.MediaOpened -= MediaPlayer_MediaOpened;
                    mediaPlayer.MediaEnded -= MediaPlayer_MediaEnded;
                    MessageBox.Show($"The song duration cannot be longer than {MaxSongLength / 60} minutes" +
                        $"or shorter than {SmallestLoopInterval} seconds!");
                    DeleteExternalDatabaseSong();
                    return;
                }

                CheckForSongLooping();

                isSongFromDatabase = true;
                EnableOrDisableButtons(true);
                timer.Tick += timer1_Tick;
                PlayMusic();

                lblSongName.Text = music.Name;
                txtLoopStartPoint.Text = music.StartPoint;
                txtLoopEndPoint.Text = music.EndPoint;

                SetLoopPoints();
                //btnSetLoopPoints.PerformClick();
            }
        }

        private void CheckForSongLooping()
        {
            if (listenForLoop)
            {
                lblLoopStartPoint.Text = DefaultStartTime;
                lblLoopEndPoint.Text = DefaultEndTime;

                txtLoopStartPoint.Text = txtLoopEndPoint.Text = "";

                lblLoopStartPoint.ForeColor = lblLoopEndPoint.ForeColor = System.Drawing.Color.Black;
                lblCurrentTime.TextChanged -= lblCurrentTime_TextChanged;
                listenForLoop = false;
            }
        }
    }
}
