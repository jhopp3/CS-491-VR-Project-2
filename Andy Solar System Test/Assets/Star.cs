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
	public string texture;
	public char spectralType;

	public Star (PlanetData pd) {
		luminosity = pd.st_lum;
		name = pd.pl_hostname;
		distanceFromUs = pd.st_dist;
		type = pd.st_spstr;
		radius = pd.st_rad;
		numberOfPlanets = pd.pl_pnum;

		setTexture();
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
	}

	public override string ToString()
	{
		return String.Format("Lum {0} : Name {1} : Dist {2} : Type {3} : Radius {4} : Planets {5}", luminosity, name, distanceFromUs, type, radius, numberOfPlanets);
	}
}
