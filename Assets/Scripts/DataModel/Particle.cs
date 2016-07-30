using UnityEngine;
using System.Collections;

public class Particle  {
    public string name;
    public double latitude;
    public double longitude;
    public bool spawn;
    public Particle()
    {

    }
    public Particle(string name, double latitude, double longitude)
    {
        this.name = name;
        this.latitude = latitude;
        this.longitude = longitude;
        spawn = true;
    }

}
