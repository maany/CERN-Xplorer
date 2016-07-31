using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
[ExecuteInEditMode]
public class Text : MonoBehaviour {
    public string nextScene;
    // Use this for initialization
    int count =0;
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnMouseDown()
    {
        SceneManager.LoadScene(nextScene);

        //Destroy(gameObject);
    }
    void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,Screen.width,80), "Proceed"))
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
