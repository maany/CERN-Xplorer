using UnityEngine;
using System.Collections;
using Assets.Scripts.Maps;
using System.Collections.Generic;
using Assets.Scripts.DataModel;

public class Spawner : MonoBehaviour {
    Helper helper;
    public GameObject player;
    public List<Particle> particles;
    List<GameObject> particleGameObjects;
    float playerlon;
    float playerlat;
    public GameObject locationManager;
    private bool spawn;
    private bool spawned;
    private float lastlon;
    private float lastlat;
    public GameObject dummyParticle;
    private float xdiff=0;
    private float ydiff=0;
    int count = 0;
    bool isinit=false;
    bool updatingParticlePosition;
    Vector3 diff;
   public Spawner()
    {

    }
    //   // Use this for initialization
    void Start()
    {
        helper = new Helper();
        particles = initDemoParticles();
        //lastlon = Input.location.lastData.longitude;
        //lastlat = Input.location.lastData.latitude;
        particleGameObjects = new List<GameObject>();
        spawn = true;
        double ratio = new Helper().DistanceBetweenPlaces(6.05314, 46.23408, 6.05225, 46.23263) / new Helper().DistanceXYZBetweenPlaces(new Vector3(0, 0, 0), new Vector3(0.653f, 0, 1.386f));
        Debug.Log("***************" + ratio);
        isinit = true;
        //SpawnBegin();
        //GameObject temp = Instantiate(Resources.Load<GameObject>("ParticleDUmmy"), new Vector3(0,0,0), new Quaternion(0, 0, 0, 0)) as GameObject;

    }

    // Update is called once per frame
    void Update () {
        //if (Input.location.isEnabledByUser)
        //{
        //    Input.location.Start();
            
        //}

    }
    public void SpawnBegin()
    {
        
        if (isinit)
        {
            particles = initDemoParticles();
            particleGameObjects = new List<GameObject>();
            playerlon = Input.location.lastData.longitude;
            playerlat = Input.location.lastData.latitude;
            //Debug.Log(locationManager.GetComponent<LocationManager>().getLon().ToString());
            if (spawn == true)
            {
                particleSpawn();
                spawn = false;
            }
            if (particles.Count != 0)
            {
                //if (playerlon != lastlon || playerlat != lastlat)
                //{

                    updatingParticlePosition = true;
                    updateParticlePosition();
                    //updatingParticlePosition = false;
                //}

            }
            lastlat = playerlon;
            lastlon = playerlat;
        }
    }
    void OnGUI()
    {
        //if (GUI.Button(new Rect(100, 150, Screen.width / 5, Screen.height), "Click!!" + "isinit spawner:" + isinit + " update?" + updatingParticlePosition))
        //{
        //    Debug.Log("Clicked Button");
        //    //SpawnBegin();
        //}
        //if (spawned)
        //{
        //    GUIStyle style = new GUIStyle();
        //    style.fontSize = 50;
        //    GUI.Label(new Rect(350, 290, Screen.width / 5, Screen.height), "Spawned " + xdiff + " : " + ydiff);
            
        //}
        ////Particle particle = new Particle("downQuark2", 46.22974, 6.04962);
        ////double[] xz = new Helper().convertXZ(playerlon, playerlat, particle.longitude, particle.latitude, default(Vector3), default(Vector3));
        //GUIStyle style1 = new GUIStyle();
        //style1.fontSize = 50;
        //GUI.Label(new Rect(350, 250, Screen.width / 5, Screen.height), "Particles instantiated : " + count);
    }
    void particleSpawn()
    {
        Debug.Log("Spawning");
        //playerlat = (float)46.23408;
        //playerlon = (float)6.05314;
        Particle referenceParticle = new Particle("refParticle", playerlat, playerlon);
        particles.Add(referenceParticle);
        foreach(Particle particle in particles)
        {
            double[] xz = new Helper().convertXZ(playerlon, playerlat, particle.longitude, particle.latitude, default(Vector3), default(Vector3));
            //GameObject temp = Instantiate(Resources.Load<GameObject>("ParticleDUmmy"), new Vector3((float)xz[0]+4548.703f, 0.07f, (float)xz[1]+45035.17f), new Quaternion(0, 0, 0, 0)) as GameObject;
            GameObject temp = Instantiate(Resources.Load<GameObject>("ParticleDUmmy"), new Vector3((float)xz[0] , 0.07f, (float)xz[1] ), new Quaternion(0, 0, 0, 0)) as GameObject;
            //BoxCollider colldier = temp.AddComponent<BoxCollider>();
            temp.name = particle.name;
            temp.AddComponent<ParticleGameObjectScript>();
            count++;
           // Material mat = temp.GetComponent<Renderer>().material;
           // mat.color = Color.red;
            //GameObject temp = Instantiate(dummyParticle, player.transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;

            Debug.Log(temp.transform.position.ToString());
            particleGameObjects.Add(temp);
        }
        spawn = false;
        spawned = true;
        //balanceParticles();
    }
    
    void updateParticlePosition()
    {
        int i = 0;
        foreach(Particle particle in particles)
        {
            //double[] tempxz = new Helper().convertXZ(playerlon, playerlat, particle.longitude, particle.latitude,default(Vector3),default(Vector3));
            //particleGameObjects[i].transform.position = new Vector3((float)tempxz[0]+4548.703f, 0.00f, (float)tempxz[1]+45035.17f);
           // particleGameObjects[i].transform.position = new Vector3((float)tempxz[0] , 0.00f, (float)tempxz[1] );
           // Debug.Log(particleGameObjects[i].transform.position.ToString());
           // i++;
        }
        balanceParticles();
    }
    void balanceParticles()
    {
        GameObject playerObject = particleGameObjects[particleGameObjects.Count - 1];
        if (diff == default(Vector3))
        {
            diff = player.transform.position - playerObject.transform.position;
        }
        foreach (GameObject go in particleGameObjects)
        {
            go.transform.position = go.transform.position -diff;            
        }
    }
    List<Particle> initDemoParticles()
    {
        List<Particle> list = new List<Particle>();
        Particle upQuark = new Particle("upQuark", 46.23250, 6.05226);
        list.Add(upQuark);
        Particle downQuark1 = new Particle("downQuark", 46.23165, 6.05193);
        list.Add(downQuark1);
        Particle downQuark2 = new Particle("downQuark2", 46.22974, 6.04962);
        list.Add(downQuark2);
        return list;
    }
}
