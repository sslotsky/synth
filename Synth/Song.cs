using Akka.Actor;
using System;
using System.Collections.Generic;

namespace Synth
{
    public class Song
    {
        public int Tempo { get; set; }

        private ActorSystem song;

        private List<IActorRef> tracks;

        public Song()
        {
            song = ActorSystem.Create("song-" + Guid.NewGuid().ToString());
            tracks = new List<IActorRef>();
        }

        public void AddTrack(Instrument instrument, List<Note> notes)
        {
            tracks.Add(song.ActorOf(Props.Create(() => new Track(this, instrument, notes))));
        }

        public IActorRef NewMusician(AudioHub hub, Instrument instrument)
        {
            return song.ActorOf(Props.Create(() => new Musician(this, hub, instrument)));
        }

        public void Play(AudioHub hub)
        {
            foreach (var track in tracks)
                track.Tell(hub);
        }
    }
}
