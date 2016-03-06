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

        public void AddTrack(int instrument, List<Note> notes)
        {
            tracks.Add(song.ActorOf(Props.Create(() => new Track(this, instrument, notes))));
        }

        public IActorRef NewMusician(AudioHub hub, int channel)
        {
            return song.ActorOf(Props.Create(() => new Musician(this, hub, channel)));
        }

        public void Play(AudioHub hub)
        {
            foreach (var track in tracks)
                track.Tell(hub);
        }
    }
}
