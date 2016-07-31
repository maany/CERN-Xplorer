using UnityEngine;
using System.Collections;
using Assets.Scripts.Maps;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
    Helper helper;
    public GameObject player;
    public List<Particle> particles;
    List<GameObject> particleGameObjects;
    float playerlon;
    float playerlat;
    public GameObject locationManager;
    private bool spawn;
    private float lastlon;
    private float lastlat;
    public GameObject dummyParticle;

   public Spawner()
    {

    }
    //   // Use this for initialization
    void Start()
    {
        helper = new Helper();
        particles = initDemoParticles();
        lastlon = Input.location.lastData.longitude;
        lastlat = Input.location.lastData.latitude;
        particleGameObjects = new List<GameObject>();
        spawn = true;
        double ratio = Helper.DistanceBetweenPlaces(6.05314, 46.23408, 6.05225, 46.23263) / Helper.DistanceXYZBetweenPlaces(new Vector3(0, 0, 0), new Vector3(0.653f, 0, 1.386f));
        Debug.Log("***************" + ratio);
        particles = initDemoParticles();
        particleGameObjects = new List<GameObject>();
        SpawnBegin();

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

    void particleSpawn()
    {
        Debug.Log("Spawning");
        playerlat = (float)46.23408;
        playerlon = (float)6.05314;
        Particle referenceParticle = new Particle("refParticle", playerlat, playerlon);
        particles.Add(referenceParticle);
        foreach(Particle particle in particles)
        {
            double[] xz = Helper.convertXZ(playerlon, playerlat, particle.longitude, particle.latitude, default(Vector3), default(Vector3));
            GameObject temp = Instantiate(dummyParticle, new Vector3((float)xz[0]+4548.703f, 0.07f, (float)xz[1]+45035.17f), new Quaternion(0, 0, 0, 0)) as GameObject;
            //GameObject temp = Instantiate(dummyParticle, player.transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;

            Debug.Log(temp.transform.position.ToString());
            particleGameObjects.Add(temp);
        }
        spawn = false;
        //balanceParticles();
    }
    void updateParticlePosition()
    {
        int i = 0;
        foreach(Particle particle in particles)
        {
            double[] tempxz = Helper.convertXZ(playerlon, playerlat, particle.longitude, particle.latitude,default(Vector3),default(Vector3));
            particleGameObjects[i].transform.position = new Vector3((float)tempxz[0]+4548.703f, 0.00f, (float)tempxz[1]+45035.17f);
            Debug.Log(particleGameObjects[i].transform.position.ToString());
            i++;
        }
        //balanceParticles();
    }
    void balanceParticles()
    {
        GameObject playerObject = particleGameObjects[particleGameObjects.Count - 1];
        Vector3 diff = player.transform.position - playerObject.transform.position;
        foreach (GameObject go in particleGameObjects)
        {
            go.transform.position = go.transform.position + new Vector3(4548.703f,0,45035.17f);            
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
