using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.DataModel
{
    class ParticleGameObjectScript : MonoBehaviour 
    {
        void OnMouseDown()
        {
            Destroy(gameObject);
        }
    }
}
