using Rationals;
using System.Collections.Generic;

namespace Synth
{
    public class BeatFiller
    {
        private List<Beat> beats;

        public IEnumerable<Beat> Beats { get { return beats; } }

        public BeatFiller()
        {
            beats = new List<Beat>();
        }

        public void Fill(List<Note> notes)
        {
            Fill(new Queue<Note>(notes), 0);
        }

        private void Fill(Queue<Note> notes, Rational offset)
        {
            var beat = new Beat(offset);
            beats.Add(beat);
            if (offset >= 1)
            {
                Fill(notes, offset - 1);
                return;
            }

            while (notes.Count > 0)
            {
                var note = notes.Peek();
                if (!beat.AddNote(note))
                {
                    var newoffset = -beat.TimeRemaining;

                    Fill(notes, newoffset);
                    return;
                }

                notes.Dequeue();
            }
        }
    }
}
