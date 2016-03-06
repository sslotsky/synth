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
            InitializeSong(120, (song, speaker) =>
            {
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
                speaker.Tell(song);
                Thread.Sleep(numBeats * beatTime);
            });
        }

        [TestMethod]
        public void TrackWithRests()
        {
            InitializeSong(120, (song, speaker) =>
            {
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
                speaker.Tell(song);
                Thread.Sleep(numBeats * beatTime);
            });
        }

        [TestMethod]
        public void SyncopationTest()
        {
            InitializeSong(120, (song, speaker) =>
            {
                var notes = new List<Note>
                {
                    new Note(NoteName.E, (Rational)3/2),
                    new Note(NoteName.E, 1),
                    new Note(NoteName.E, 1),
                    new Note(NoteName.E, (Rational)1/4),
                    new Note(NoteName.E, (Rational)1/4),
                    new Note(NoteName.E, (Rational)1/2)
                };
                song.AddTrack(0, notes);

                var numBeats = 5;
                var beatTime = (int)Math.Round(1000 / (song.Tempo / 60.0));
                speaker.Tell(song);
                Thread.Sleep(numBeats * beatTime);
            });
        }

        private void InitializeSong(int tempo, Action<Song, IActorRef> block)
        {
            using (var hub = new AudioHub())
            {
                IActorRef speaker = hub.NewSpeaker();
                var song = new Song();
                song.Tempo = 120;
                block(song, speaker);
            }
        }
    }
}
