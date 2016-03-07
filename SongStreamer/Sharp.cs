using Rationals;

namespace SongStreamer
{
    public class Sharp : Note
    {
        public Sharp(NoteName noteName, Rational duration) : base(noteName, duration) { }

        public override Pitch Pitch
        {
            get
            {
                return base.Pitch + 1;
            }
        }
    }
}
