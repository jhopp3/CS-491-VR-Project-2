using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSystem {
	public Star star;
	public List<Planet> planets = new List<Planet>();

	public StarSystem(PlanetData pd) {
		star = new Star (pd);
	}

	public void addPlanetData (PlanetData pd) {
		Planet newPlanet = new Planet (pd);
		planets.Add (newPlanet);
	}
}
