using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SongStreamer
{
    public class Musician : ReceiveActor
    {
        private int beatTime;
        private Speaker speaker;
        private Instrument instrument;

        public Musician(Song song, Speaker speaker, Instrument instrument)
        {
            beatTime = (int)Math.Round(1000 / (song.Tempo / 60.0));
            this.instrument = instrument;
            this.speaker = speaker;
            Receive<Beat>(beat => beat.Notes.Count > 0, beat => Play(beat.Notes));
        }

        public void Play(List<Note> notes)
        {
            Func<Note, int> getDuration = n =>
                (int)Math.Floor(beatTime * (decimal)n.Duration.Numerator / (decimal)n.Duration.Denominator);

            foreach (var note in notes)
            {
                if (note.NoteName.Equals(NoteName.Rest))
                {
                    Thread.Sleep(getDuration(note));
                }
                else
                {
                    speaker.Play(note.Pitch, instrument);
                    Thread.Sleep(getDuration(note));
                    speaker.Stop(note.Pitch, instrument);
                }
            }
        }
    }
}
