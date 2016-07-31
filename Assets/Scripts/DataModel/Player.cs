using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataModel
{
    class Player
    {
        List<Particle> collectedParticles;
        int energy;
        public Player()
        {
            collectedParticles = new List<Particle>();
        }
    }
}
