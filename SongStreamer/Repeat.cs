using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SongStreamer
{
    public class Repeat
    {
        public List<Note> NoteList { get; private set; }
        public Repeat(List<Note> noteList, int times = 1)
        {
            NoteList = noteList;
            times = Math.Max(times, 1);
            foreach (var n in Enumerable.Range(1, times))
                NoteList.AddRange(noteList);
        }
    }
}
