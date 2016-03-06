using Akka.Actor;
using NAudio.Midi;
using System;

namespace Synth
{
    public class AudioHub : IDisposable
    {
        public static int KeyboardChannel = 1;
        public static int SaxChannel = 2;

        private MidiOut output;
        public ActorSystem Speakers { get; private set; }

        public AudioHub()
        {
            output = new MidiOut(0);
            output.Send(MidiMessage.ChangePatch(66, 2).RawData);
            Speakers = ActorSystem.Create("speakers");
        }

        public void Play(int pitch, int channel)
        {
            output.Send(MidiMessage.StartNote(pitch, 127, channel).RawData);
        }

        public void Stop(int pitch, int channel)
        {
            output.Send(MidiMessage.StopNote(pitch, 0, channel).RawData);
        }

        public IActorRef NewSpeaker()
        {
            return Speakers.ActorOf(Props.Create(() => new Speaker(this)));
        }

        public void Dispose()
        {
            output.Close();
            output.Dispose();
        }
    }
}
