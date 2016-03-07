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
            beatTime = song.BeatTime;
            this.instrument = instrument;
            this.speaker = speaker;
            Receive<Beat>(beat => beat.Notes.Count > 0, beat => Play(beat.Notes));
        }

        public void Play(List<Note> notes)
        {
            foreach (var note in notes)
            {
                if (note.NoteName.Equals(NoteName.Rest))
                {
                    Thread.Sleep(note.RelativeDuration(beatTime));
                }
                else
                {
                    speaker.Play(note.Pitch, instrument);
                    Thread.Sleep(note.RelativeDuration(beatTime));
                    speaker.Stop(note.Pitch, instrument);
                }
            }
        }
    }
}
