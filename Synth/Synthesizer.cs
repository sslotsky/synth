using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synth
{
    public class Synthesizer : IDisposable
    {
        private MidiOut output;

        public Synthesizer()
        {
            output = new MidiOut(0);
        }

        public void SetVoice(int channel, int voice)
        {
            output.Send(MidiMessage.ChangePatch(voice, channel).RawData);
        }

        public void Play(int pitch, int channel, int volume = 127)
        {
            output.Send(MidiMessage.StartNote(pitch, volume, channel).RawData);
        }

        public void Stop(int pitch, int channel)
        {
            output.Send(MidiMessage.StopNote(pitch, 0, channel).RawData);
        }

        public void Dispose()
        {
            output.Close();
            output.Dispose();
        }
    }
}
