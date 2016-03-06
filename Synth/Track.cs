using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Synth
{
    public class Track : ReceiveActor
    {
        private Instrument instrument;
        private Song song;

        private List<Beat> beats;

        public Track(Song song, Instrument instrument, List<Note> notes)
        {
            this.song = song;
            this.instrument = instrument;
            FillBeats(notes);
            Receive<AudioHub>(hub => Stream(hub));
        }

        public void Stream(AudioHub hub)
        {
            var beatTime = (int)Math.Round(1000 / (song.Tempo / 60.0));
            var musician = song.NewMusician(hub, instrument);

            foreach (var beat in beats)
            {
                musician.Tell(beat);
                Thread.Sleep(beatTime);
            }
        }

        public void FillBeats(List<Note> notes)
        {
            var filler = new BeatFiller();
            filler.Fill(notes);
            beats = filler.Beats.ToList();
        }
    }
}
