using Rationals;
using System.Collections.Generic;

namespace SongStreamer
{
    public class Beat
    {
        public List<Note> Notes { get; set; }

        public Rational Offset { get; set; }

        public Beat() 
        {
            Notes = new List<Note>();
        }

        public Beat(Rational offset) : this()
        { 
            Offset = offset; 
        }

        public Rational ActualDuration
        {
            get
            {
                return Offset + Notes.Sum(n => n.Duration);
            }
        }

        public Rational PortionTaken
        {
            get
            {
                return ActualDuration < 1 ? ActualDuration : 1;
            }
        }

        public Rational TimeRemaining
        {
            get
            {
                return 1 - ActualDuration;
            }
        }

        public bool AddNote(Note note)
        {
            var canAdd = TimeRemaining > 0;
            if (canAdd)
                Notes.Add(note);

            return canAdd;
        }
    }
}
