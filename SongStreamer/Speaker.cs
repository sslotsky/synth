using Akka.Actor;

namespace SongStreamer
{
    public class Speaker : ReceiveActor
    {
        private AudioHub hub;
        public Speaker(AudioHub hub)
        {
            this.hub = hub;
            Receive<Song>(song => PlaySong(song));
        }

        public void PlaySong(Song song)
        {
            song.Play(hub);
        }
    }
}
