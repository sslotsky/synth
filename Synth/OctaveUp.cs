using Rationals;

namespace Synth
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
