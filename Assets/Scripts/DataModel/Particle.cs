﻿using UnityEngine;
using System.Collections;

public class Particle  {
	public string name;
	public double latitude;
	public double longitude;
	public string charge;
	public string mass;
	public string description;
	public bool spawn;
	public Particle()
	{

	}
	public Particle(string name, double latitude, double longitude, string charge, string mass, string description)
	{
		this.name = name;
		this.latitude = latitude;
		this.longitude = longitude;
		this.charge = charge;
		this.mass = mass;
		this.description = description;
		spawn = true;
	}

}
