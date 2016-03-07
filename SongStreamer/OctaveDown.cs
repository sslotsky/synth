using Rationals;

namespace SongStreamer
{
    public class OctaveDown : Note
    {
        public OctaveDown(NoteName noteName, Rational duration) : base(noteName, duration) { }
        public override Pitch Pitch
        {
            get
            {
                return base.Pitch - 12;
            }
        }
    }
}
