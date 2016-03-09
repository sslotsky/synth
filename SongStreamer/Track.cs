using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SongStreamer
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
            Receive<Speaker>(speaker => Stream(speaker));
        }

        public void Stream(Speaker speaker)
        {
            var musician = speaker.NewMusician(song, instrument);

            foreach (var beat in beats)
            {
                if (!song.Stopped)
                    musician.Tell(beat);
                Thread.Sleep(song.BeatTime);
            }
        }

        private void FillBeats(List<Note> notes)
        {
            var filler = new BeatFiller();
            filler.Fill(notes);
            beats = filler.Beats.ToList();
        }
    }
}
