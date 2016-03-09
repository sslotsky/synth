using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SongStreamer;
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
                song.AddTrack(Instrument.Vibes, notes);

                var otherNotes = new List<Note>
                {
                    new Note(NoteName.G, 2),
                    new Note(NoteName.A, 2).OctaveUp,
                    new Note(NoteName.G, 4)
                };
                song.AddTrack(Instrument.TenorSax, otherNotes);

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
                song.AddTrack(Instrument.Flute, notes);

                var numBeats = 8;
                var beatTime = (int)Math.Round(1000 / (song.Tempo / 60.0));
                speaker.Tell(song);
                Thread.Sleep(numBeats * beatTime);
            });
        }

        [TestMethod]
        public void StopTest()
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
                song.AddTrack(Instrument.Flute, notes);

                var numBeats = 8;
                var beatTime = (int)Math.Round(1000 / (song.Tempo / 60.0));
                speaker.Tell(song);
                Thread.Sleep(numBeats * beatTime / 8);
                song.Stop();
                Thread.Sleep(numBeats * beatTime);
            });
        }

        [TestMethod]
        public void RepeatTest()
        {
            InitializeSong(220, (song, speaker) =>
            {
                var notes = new List<Note>
                {
                    new Note(NoteName.A, 1),
                    new Note(NoteName.C, (Rational)1/2),
                    new Note(NoteName.Rest, (Rational)1/2),
                    new Note(NoteName.C, 1)
                };
                var repeat = new Repeat(notes);

                var moreNotes = new List<Note>
                {
                    new Note(NoteName.G, 1),
                    new Note(NoteName.C, (Rational)1/2),
                    new Note(NoteName.Rest, (Rational)1/2),
                    new Note(NoteName.C, 1),
                    new Note(NoteName.G, 1).Sharp,
                    new Note(NoteName.C, (Rational)1/2),
                    new Note(NoteName.Rest, (Rational)1/2),
                    new Note(NoteName.C, 1)
                };

                var phrase = repeat.NoteList.Concat(moreNotes).ToList();
                var repeatedPhrase = new Repeat(phrase);
                song.AddTrack(Instrument.Organ, repeatedPhrase.NoteList);

                var harmony = new List<Note>
                {
                    new Note(NoteName.Rest, 1),
                    new Note(NoteName.E, (Rational)1/2),
                    new Note(NoteName.Rest, (Rational)1/2),
                    new Note(NoteName.E, 1)
                };

                var harmonyRepeat = new Repeat(harmony, 7);
                song.AddTrack(Instrument.Organ, harmonyRepeat.NoteList);

                var numBeats = 25;
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
                song.AddTrack(Instrument.Trumpet, notes);

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
                var song = new Song(hub);
                song.Tempo = tempo;
                block(song, speaker);
            }
        }
    }
}
