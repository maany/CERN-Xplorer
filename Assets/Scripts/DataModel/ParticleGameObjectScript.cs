using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.DataModel
{
    class ParticleGameObjectScript : MonoBehaviour 
    {
        
        void OnMouseDown()
        {
            SceneManager.LoadScene("SimpleScene");
            
            //Destroy(gameObject);
        }
    }
}
