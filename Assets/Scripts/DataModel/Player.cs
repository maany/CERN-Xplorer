using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.DataModel
{
    class Player : MonoBehaviour 
    {
        public List<Particle> collectedParticles;
        public int energy;
        public Player()
        {
            collectedParticles = new List<Particle>();
        }
    }
}
