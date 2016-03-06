using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Synth
{
    public class Musician : ReceiveActor
    {
        private int beatTime;
        private AudioHub hub;
        private int channel;
        public Musician(Song song, AudioHub hub, int channel)
        {
            beatTime = (int)Math.Round(1000 / (song.Tempo / 60.0));
            this.channel = channel;
            this.hub = hub;
            Receive<Beat>(beat => Play(beat.Notes));
        }

        public void Play(List<Note> notes)
        {
            Func<Note, int> getDuration = n =>
                (int)Math.Floor(beatTime * (decimal)n.Duration.Numerator / (decimal)n.Duration.Denominator);

            foreach (var note in notes)
            {
                hub.Play(note.Pitch, channel);
                Thread.Sleep(getDuration(note));
                hub.Stop(note.Pitch, channel);
            }
        }
    }
}
