
namespace Synth
{
    public class Pitch
    {
        private static int min = 0;
        private static int max = 127;

        public int IntegralPitch { get; private set; }

        public Pitch(int pitch)
        {
            while (pitch > max)
                pitch -= 12;

            while (pitch < min)
                pitch += 12;

            this.IntegralPitch = pitch;
        }

        public static implicit operator int(Pitch pitch)
        {
            return pitch.IntegralPitch;
        }

        public static Pitch operator +(Pitch pitch, int interval)
        {
            return new Pitch(pitch.IntegralPitch + interval);
        }

        public static Pitch operator -(Pitch pitch, int interval)
        {
            return pitch + -interval;
        }
    }
}
