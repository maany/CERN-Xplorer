using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.DataModel
{
    class ParticleGameObjectScript : MonoBehaviour
    {
        public Particle particle;
        bool enable;
        void OnMouseDown()
        {
            string message = "particle captured : ";
            enable = true;
            message += particle.name;
            //SceneManager.LoadScene("SimpleScene");
            
            //Destroy(gameObject);
        }
    
        void OnGUI()
        {
            if (enable)
            {
                GUIStyle style = new GUIStyle();
                style.fontSize = 100;
                GUIStyle desStyle = new GUIStyle();
                desStyle.fontSize = 30;
                StartCoroutine(disable());
                if (GUI.Button(new Rect(0, 600, Screen.width, 200), particle.name + " : mass : " + particle.mass , style))
                {

                }
                if (GUI.Button(new Rect(0, 800, Screen.width, 200), "description " + particle.description , desStyle))
                {
                    
                }

                
            }        
        }

        public IEnumerator disable()
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }
    }
}
