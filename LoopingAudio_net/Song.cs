using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LoopingAudio_net
{
    internal class Song
    {
        internal MediaPlayer mediaPlayer = null;
        internal Tuple<string, string> loop;   

        internal Song(MediaPlayer newSong, string startLoop, string endLoop)
        {
            loop = new Tuple<string, string>(startLoop, endLoop);
            
        }
    }
}
