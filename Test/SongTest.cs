using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Synth;
using System.Collections.Generic;
using System.Threading;
using Rationals;
using Akka.Actor;

namespace Test
{
    [TestClass]
    public class SongTest
    {
        [TestMethod]
        public void MaryHadALittleLamb()
        {
            using (var hub = new AudioHub())
            {
                IActorRef leftSpeaker = hub.NewSpeaker();
                var song = new Song();
                song.Tempo = 120;
                var notes = new List<Note>
                {
                    new Note(NoteName.E, 1),
                    new Note(NoteName.D, 1),
                    new Note(NoteName.C, (Rational)1/3),
                    new Note(NoteName.B, (Rational)1/3),
                    new Note(NoteName.C, (Rational)1/3),
                    new Note(NoteName.D, 1),
                    new Note(NoteName.E, 1),
                    new Note(NoteName.E, 1),
                    new Note(NoteName.E, 2)
                };
                song.AddTrack(0, notes);

                var otherNotes = new List<Note>
                {
                    new Note(NoteName.G, 2),
                    new Note(NoteName.A, 2).OctaveUp,
                    new Note(NoteName.G, 4)
                };
                song.AddTrack(1, otherNotes);

                var numBeats = 8;
                var beatTime = (int)Math.Round(1000 / (song.Tempo / 60.0));
                leftSpeaker.Tell(song);
                Thread.Sleep(numBeats * beatTime);
            }
        }

        [TestMethod]
        public void TrackWithRests()
        {
            using (var hub = new AudioHub())
            {
                IActorRef leftSpeaker = hub.NewSpeaker();
                var song = new Song();
                song.Tempo = 120;
                var notes = new List<Note>
                {
                    new Note(NoteName.E, 1),
                    new Note(NoteName.Rest, 1),
                    new Note(NoteName.D, 1),
                    new Note(NoteName.Rest, 1),
                    new Note(NoteName.C, 4),
                };
                song.AddTrack(0, notes);

                var numBeats = 8;
                var beatTime = (int)Math.Round(1000 / (song.Tempo / 60.0));
                leftSpeaker.Tell(song);
                Thread.Sleep(numBeats * beatTime);
            }
        }
    }
}
