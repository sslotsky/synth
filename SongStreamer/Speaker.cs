using Akka.Actor;
using System;
using System.Collections.Generic;

namespace SongStreamer
{
    public class Speaker : ReceiveActor
    {
        private AudioHub hub;

        private int volume;

        public ActorSystem System { get { return hub.System; } }

        private Action advance
        {
            get
            {
                return () => volume = Math.Min(volume + 1, 127);
            }
        }

        private Action recoil
        {
            get
            {
                return () => volume = Math.Max(volume - 1, 0);
            }
        }

        private Dictionary<Event, Action> handlers;

        public Speaker(AudioHub hub)
        {
            this.hub = hub;
            this.volume = 127;
            handlers = new Dictionary<Event, Action>
            {
                { Event.Advance, advance },
                { Event.Recoil, recoil }
            };

            Receive<Song>(song => PlaySong(song));
            Receive<Event>(e => handlers[e]());
        }

        public void PlaySong(Song song)
        {
            song.Play(this);
        }

        public void Play(Pitch pitch, Instrument instrument)
        {
            hub.Play(pitch, instrument, volume);
        }

        public void Stop(Pitch pitch, Instrument instrument)
        {
            hub.Stop(pitch, instrument);
        }

        public IActorRef NewMusician(Song song, Instrument instrument)
        {
            return System.ActorOf(Props.Create(() => new Musician(song, this, instrument)));
        }
    }
}
