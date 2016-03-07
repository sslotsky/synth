using Akka.Actor;
using System;
using System.Collections.Generic;

namespace SongStreamer
{
    public class Song
    {
        public int Tempo { get; set; }

        private AudioHub hub;

        private List<IActorRef> tracks;

        public Song(AudioHub hub)
        {
            this.hub = hub;
            tracks = new List<IActorRef>();
        }

        public void AddTrack(Instrument instrument, List<Note> notes)
        {
            tracks.Add(hub.System.ActorOf(Props.Create(() => new Track(this, instrument, notes))));
        }

        public void Play(AudioHub hub)
        {
            foreach (var track in tracks)
                track.Tell(hub);
        }
    }
}
