using Rationals;

namespace Synth
{
    public class Flat : Note
    {
        public Flat(NoteName noteName, Rational duration) : base(noteName, duration) { }

        public override Pitch Pitch
        {
            get
            {
                return base.Pitch - 1;
            }
        }
    }
}
