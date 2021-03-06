﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystem {
	public Star star;
	public List<Planet> planets = new List<Planet>();

	public StarSystem(PlanetData pd) {
		star = new Star (pd);
	}

	public void addPlanetData (PlanetData pd) {
		Planet newPlanet = new Planet (pd, star);
		if (!newPlanet.errorMassRadius) {
			planets.Add (newPlanet);
		} else {
			// Debug.Log("Not adding planet!");
		}
	}

	public double getMostOptimalPlanetDistance() {
		// We want closest to:
		// 1.175 * (Luminosity of this star / Luminosity of our sun) in astronomical units
		double minOptimalPlanetDistance = double.MaxValue;

		foreach (Planet planet in planets)
		{
			if (planet.distanceFromOptimal < minOptimalPlanetDistance) {
				minOptimalPlanetDistance = planet.distanceFromOptimal;
			}
		}

		return minOptimalPlanetDistance;
	}

	public override string ToString()
	{
		return star.ToString();
	}

	public string printFirstPlanet()
	{
		if (planets.Count > 0) {
			return planets [0].ToString ();
		}

		return "";
	}
	public void printAllPlanets()
	{
		foreach (Planet p in planets) {
			Debug.Log(p.ToString());
		}
	}
}
