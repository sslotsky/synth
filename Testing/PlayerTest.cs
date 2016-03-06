using NUnit.Framework;
using Synth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing
{
    public class PlayerTest
    {
        [Test]
        public void TestPlayer()
        {
            var player = new Player();
            player.Start();
        }
    }
}
