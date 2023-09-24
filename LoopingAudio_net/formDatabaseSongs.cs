using System;
using System.Windows.Forms;

namespace LoopingAudio_net
{
    public partial class formDatabaseSongs : Form
    {
        private AudioDB audioDB = null;
        private static formDatabaseSongs formSongs = null;
        public formDatabaseSongs()
        {
            InitializeComponent();
        }

        internal static formDatabaseSongs GetInstance()
        {
            if (formSongs == null)
            {
                formSongs = new formDatabaseSongs();
                formSongs.FormClosed += delegate { formSongs = null; };
            }
            return formSongs;
        }

        private void listBoxSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxSongs.Enabled = false;
            listBoxSongs.ForeColor = System.Drawing.Color.Red;
            if (listBoxSongs.SelectedIndex != -1)
            {   
                Cursor.Current = Cursors.WaitCursor;               
                Music music = audioDB.GetSongData(listBoxSongs.SelectedItem.ToString());
                formLoopingAudio.loopingAudioForm.PlaySongFromDatabase(music);
                Cursor.Current = Cursors.Default;               
            }
            listBoxSongs.ForeColor = ListBox.DefaultForeColor;
            listBoxSongs.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            listBoxSongs.SelectedIndexChanged -= listBoxSongs_SelectedIndexChanged;
            listBoxSongs.DataSource = audioDB.GetSongList();
            listBoxSongs.SelectedIndexChanged += listBoxSongs_SelectedIndexChanged;
        }

        private void formDatabaseSongs_Load(object sender, EventArgs e)
        {
            //call database function to load names of songs and store into listbox
            //user selects a listbox index and either

            //a database call to that song

            //or store all songs into List<T> and get index

            //will do former because I don't think there's an need to store the songs in memory
            //while the form is opened.  
            listBoxSongs.SelectedIndexChanged -= listBoxSongs_SelectedIndexChanged;
            audioDB = new AudioDB();
            audioDB.SetUp();
            listBoxSongs.DataSource = audioDB.GetSongList();
            listBoxSongs.SelectedIndex = -1;
            listBoxSongs.SelectedIndexChanged += listBoxSongs_SelectedIndexChanged;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBoxSongs.SelectedIndex == -1)
                return;

            DialogResult result = MessageBox.Show("Do you want to delete this song from the database?", "Confirmation", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                listBoxSongs.SelectedIndexChanged -= listBoxSongs_SelectedIndexChanged;

                audioDB.DeleteSong(listBoxSongs.SelectedItem.ToString());
                listBoxSongs.DataSource = audioDB.GetSongList();

                listBoxSongs.SelectedIndexChanged += listBoxSongs_SelectedIndexChanged;
            }         
        } 
    }
}
