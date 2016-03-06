using Akka.Actor;
using NAudio.Midi;
using System;
using System.Collections.Generic;

namespace Synth
{
    public class AudioHub : IDisposable
    {
        private static Dictionary<Instrument, int> channels = new Dictionary<Instrument, int>
        {
            { Instrument.Choir, 1 },
            { Instrument.Flute, 2 },
            { Instrument.JazzGuitar, 3 },
            { Instrument.Organ, 4 },
            { Instrument.Piano, 5 },
            { Instrument.SlapBass, 6 },
            { Instrument.TenorSax, 7 },
            { Instrument.Trumpet, 8 },
            { Instrument.Vibes, 9 }
        };

        private Synthesizer synth;

        public ActorSystem Speakers { get; private set; }

        public AudioHub()
        {
            Speakers = ActorSystem.Create("speakers");
            synth = new Synthesizer();
            foreach (var channel in channels)
                synth.SetVoice(channel.Value, (int)channel.Key);
        }

        public void Play(Pitch pitch, Instrument instrument)
        {
            synth.Play(pitch, channels[instrument]);
        }

        public void Stop(Pitch pitch, Instrument instrument)
        {
            synth.Stop(pitch, channels[instrument]);
        }

        public IActorRef NewSpeaker()
        {
            return Speakers.ActorOf(Props.Create(() => new Speaker(this)));
        }

        public void Dispose()
        {
            synth.Dispose();
        }
    }
}
