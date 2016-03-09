using Akka.Actor;
using System;
using System.Collections.Generic;

namespace SongStreamer
{
    public class Song
    {
        public int Tempo { get; set; }

        public int BeatTime { get { return (int)Math.Round(1000 / (Tempo / 60.0)); } }

        private AudioHub hub;

        private List<IActorRef> tracks;

        public bool Stopped { get; private set; }

        public Song(AudioHub hub)
        {
            this.hub = hub;
            tracks = new List<IActorRef>();
        }

        public void AddTrack(Instrument instrument, List<Note> notes)
        {
            tracks.Add(hub.System.ActorOf(Props.Create(() => new Track(this, instrument, notes))));
        }

        public void Play(Speaker speaker)
        {
            foreach (var track in tracks)
                track.Tell(speaker);
        }

        public void Stop()
        {
            Stopped = true;
        }
    }
}
