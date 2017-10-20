using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Star {
	public double luminosity;
	public string name;
	public double distanceFromUs; // in parsecs
	public string type; // Classification of the star based on their spectral characteristics following the Morgan-Keenan system.
	public double radius;
//	public string spectralClassification; // Same as type?
	public int numberOfPlanets;

	public Star (PlanetData pd) {
		luminosity = pd.st_lum;
		name = pd.pl_hostname;
		distanceFromUs = pd.st_dist;
		type = pd.st_spstr;
		radius = pd.st_rad;
		numberOfPlanets = pd.pl_pnum;
	}

	public override string ToString()
	{
		return String.Format("Lum {0} : Name {1} : Dist {2} : Type {3} : Radius {4} : Planets {5}", luminosity, name, distanceFromUs, type, radius, numberOfPlanets);
	}
}
