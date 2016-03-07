using Rationals;
using System.Collections.Generic;

namespace SongStreamer
{
    public class Note
    {
        private static Dictionary<NoteName, Pitch> basePitches = new Dictionary<NoteName, Pitch>
        {
            { NoteName.A, new Pitch(57) },
            { NoteName.B, new Pitch(59) },
            { NoteName.C, new Pitch(60) },
            { NoteName.D, new Pitch(62) },
            { NoteName.E, new Pitch(64) },
            { NoteName.F, new Pitch(65) },
            { NoteName.G, new Pitch(67) }
        };

        public NoteName NoteName { get; private set; }

        public Rational Duration { get; set; }

        public Note(NoteName noteName, Rational duration)
        {
            NoteName = noteName;
            Duration = duration;
        }

        public virtual Pitch Pitch { get { return basePitches[NoteName]; } }

        public Note Sharp { get { return new Sharp(NoteName, Duration); } }

        public Note Flat { get { return new Flat(NoteName, Duration); } }

        public Note OctaveUp { get { return new OctaveUp(NoteName, Duration); } }

        public Note OctaveDown { get { return new OctaveDown(NoteName, Duration); } }

        public Note Natural { get { return new Note(NoteName, Duration); } }
    }
}
