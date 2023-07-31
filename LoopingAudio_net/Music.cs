namespace LoopingAudio_net
{
    internal class Music
    {
        internal string Name { get; }
        internal byte[] Song { get; }
        internal string StartPoint { get; }  
        internal string EndPoint { get; }

        internal Music() { }

        internal Music(string name, string startLoop, string endLoop, byte[] song)
        {
            StartPoint = startLoop;
            EndPoint = endLoop;
            Name = name;
            Song = song;
        }

        //internal Music(string name, string startLoop, string endLoop, string absolutePath)
        //{
        //    StartPoint = startLoop;
        //    EndPoint = endLoop;
        //    Name = name;
        //    Song = System.IO.File.ReadAllBytes(absolutePath);
        //}
    }
}
