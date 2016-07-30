using UnityEngine;
using System.Collections;
using Assets.Scripts.Maps;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    Helper helper;
    public GameObject player;
    public static List<Particle> particles;
    List<GameObject> particleGameObjects;
    float playerlon;
    float playerlat;
    public GameObject locationManager;
    private bool spawn;
    private float lastlon;
    private float lastlat;
    public GameObject dummyParticle;

    	// Use this for initialization
	void Start () {
        helper = new Helper();
        particles = initDemoParticles();
        lastlon = Input.location.lastData.longitude;
        lastlat = Input.location.lastData.latitude;
        particleGameObjects = new List<GameObject>();

       double ratio = Helper.DistanceBetweenPlaces(6.05314,46.23408, 6.05225,46.23263)/Helper.DistanceXYZBetweenPlaces(new Vector3(0,0,0),new Vector3(0.653f,0,1.386f));
        Debug.Log("***************" + ratio);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
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
                if (playerlon != lastlon || playerlat != lastlat)
                {

                    updateParticlePosition();
                }

            }
            lastlat = playerlon;
            lastlon = playerlat;
        }

    }

    void particleSpawn()
    {
        Debug.Log("Spawning");
        foreach(Particle particle in particles)
        {
            double[] xz = Helper.convertXZ(playerlon, playerlat, particle.longitude, particle.latitude, default(Vector3), default(Vector3));
            GameObject temp = Instantiate(dummyParticle, new Vector3((float)xz[0], 0.07f, (float)xz[1]), new Quaternion(0, 0, 0, 0)) as GameObject;


            Debug.Log(temp.transform.position.ToString());
            particleGameObjects.Add(temp);
        }
        spawn = false;
    }
    void updateParticlePosition()
    {
        int i = 0;
        foreach(Particle particle in particles)
        {
            double[] tempxz = Helper.convertXZ(playerlon, playerlat, particle.longitude, particle.latitude,default(Vector3),default(Vector3));
            particleGameObjects[i].transform.position = new Vector3((float)tempxz[0], 0.07f, (float)tempxz[1]);
            Debug.Log(particleGameObjects[i].transform.position.ToString());
            i++;
        }
    }
    List<Particle> initDemoParticles()
    {
        List<Particle> list = new List<Particle>();
        Particle upQuark = new Particle("upQuark", 46.23250, 6.05226);
        list.Add(upQuark);
        Particle downQuark1 = new Particle("downQuark", 46.23165, 6.05193);
        list.Add(downQuark1);
        Particle downQuark2 = new Particle("downQuark2", 46.23066, 6.05129);
        list.Add(downQuark2);
        return list;
    }
}
