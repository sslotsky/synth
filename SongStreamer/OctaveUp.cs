using Rationals;

namespace SongStreamer
{
    public class OctaveUp : Note
    {
        public OctaveUp(NoteName noteName, Rational duration) : base(noteName, duration) { }
        public override Pitch Pitch
        {
            get
            {
                return base.Pitch + 12;
            }
        }
    }
}
