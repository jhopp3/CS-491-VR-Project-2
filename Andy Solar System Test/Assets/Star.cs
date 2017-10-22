﻿using System.Collections;
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

	public const int EARTH_RADIUS = 695500;

	public Star (PlanetData pd) {
		luminosity = Math.Exp(pd.st_lum);
		name = pd.pl_hostname;
		distanceFromUs = pd.st_dist;
		type = pd.st_spstr;
		radius = pd.st_rad * EARTH_RADIUS;
		numberOfPlanets = pd.pl_pnum;
<<<<<<< HEAD
=======

		setTexture();
	}

	public Star (string[] sunArray) {
		//radius (km), name, type, spectral classification, luminosity
		//new string[5] { "695500", "Our Sun", "sol", "G2V", "1.0" };
		radius = double.Parse(sunArray[0]);
		name = sunArray[1];
		type = sunArray[3];
		luminosity = double.Parse(sunArray[4]);
		distanceFromUs = 0;
		numberOfPlanets = 8;
		setTexture();

		Debug.Log(this.ToString());
	}

	private void setTexture() {
		if (!string.IsNullOrEmpty(type)) {
			// Debug.LogError("Type is " + type);
			spectralType = type[0];
			spectralType = Char.ToLower(spectralType);

			switch (spectralType)
			{
				case 'a':
					texture = "astar";
					break;
				case 'b':
					texture = "bstar";
					break;
				case 'f':
					texture = "fstar";
					break;
				case 'g':
					texture = "gstar";
					break;
				case 'k':
					texture = "kstar";
					break;
				case 'm':
					texture = "mstar";
					break;
				case 'l':
					// Binary system of two brown dwarfs
					// https://en.wikipedia.org/wiki/DENIS-P_J082303.1-491201_b
					texture = "mstar";
					break;
				case 't':
					// Binary system of two brown dwarfs
					// https://en.wikipedia.org/wiki/WISE_1217%2B1626
					texture = "mstar";
					break;
				case 'o':
					texture = "ostar";
					break;
				case 'w':
					// WD https://en.wikipedia.org/wiki/NN_Serpentis
					// Handle 2 stars here.
					texture = "bstar";
					break;
				case 's':
					// sdBV https://en.wikipedia.org/wiki/Subdwarf_B_star
					texture = "bstar";
					break;
				default:
					Debug.LogError("Invalid Spectral Type: " + spectralType);
					break;
			}

		} else {
			// Debug.LogError("Spectral Type is null");
		}
>>>>>>> master
	}

	public override string ToString()
	{
		return String.Format("{0} : {1} : {2} : {3} : {4} : {5}", luminosity, name, distanceFromUs, type, radius, numberOfPlanets);
	}
}