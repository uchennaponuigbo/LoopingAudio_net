namespace LoopingAudio_net
{
    internal class Music
    {
        internal string Name { get; set; }
        internal byte[] Song { get; set; }
        internal string StartPoint { get; set; }  
        internal string EndPoint { get; set; }

        internal Music() { }

        internal Music(string name, string startLoop, string endLoop, byte[] song)
        {
            StartPoint = startLoop;
            EndPoint = endLoop;
            Name = name;
            Song = song;
        }

        internal Music(string name, string startLoop, string endLoop, string absolutePath)
        {
            StartPoint = startLoop;
            EndPoint = endLoop;
            Name = name;
            Song = System.IO.File.ReadAllBytes(absolutePath);
        }
    }
}
