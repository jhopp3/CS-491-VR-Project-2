using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Planet {
//orbit radius (km), radius (km), orbit period (yr), texture, name
//	{ "82578024", "13211",   "0.46", "mercury", "e" }
	public double radiusOfOrbit;
	public float radiusOfPlanet; // Jupiter radii
	public float mass; // Jupiter Mass
	public string name;
	public string discovered;
	public Star star;
	public double timeToOrbit; // Days

	public string texture;

	public bool errorMassRadius;  // Does this have a non-zero mass or radius?

	public double distanceFromOptimal;

	private const double AU_TO_KM = 149597870.7;
	private const int JUPITER_RADIUS_TO_KM = 69911;
	private const double YEAR_TO_DAYS = 365.2422;
	private const double EARTH_MASS = 0.00315; // Jupiter Mass Units

	private const int EARTH_RADIUS = 6371;

	public Planet (PlanetData pd, Star s) {
		// pd.pl_orbsmax is in AU, convert to KM
		radiusOfOrbit = pd.pl_orbsmax * AU_TO_KM;
		// radiusOfOrbit = pd.pl_orbsmax;
		radiusOfPlanet = (float)pd.pl_radj * JUPITER_RADIUS_TO_KM;
		mass = (float)pd.pl_bmassj;
		name = pd.pl_name;
		discovered = pd.pl_discmethod;
		star = s;
		timeToOrbit = pd.pl_orbper / YEAR_TO_DAYS;
//		timeToOrbit = pd.pl_orbper;

		errorMassRadius = setMassRadius();
		setTexture ();
		setDistanceFromOptimal();
	}

	public Planet (string[] planetArray, Star s) {
		//orbit radius (km), radius (km), orbit period (yr), texture, name
		// {   "57910000",  "2440",    "0.24", "mercury", "mercury" }
		radiusOfOrbit = double.Parse(planetArray[0]);
		radiusOfPlanet = float.Parse(planetArray[1]);
		timeToOrbit = double.Parse(planetArray[2]);
		texture = planetArray[3];
		name = planetArray[4];
		star = s;
	}

	private bool setMassRadius() {
		// If either the Mass or Radius is null from the import, guess it's value based on the other.

		if ((mass <= 0) && (radiusOfPlanet <= 0)) {
			// Debug.LogError("Mass and Radius of 0.");
			return true;
		} else {
			if (mass <= 0) {
				// Set mass based on radius
				// Use formula JupiterMass = 0.00672 * EXP(0.0000706*(Radius))
				mass = (float)0.00672 * (float)Math.Exp(0.0000706 * (radiusOfPlanet));
			} else if (radiusOfPlanet <= 0) {
				// Set radius based on mass
				// http://phl.upr.edu/library/notes/standardmass-radiusrelationforexoplanets

				if (mass < EARTH_MASS) {
					radiusOfPlanet = (float)Math.Pow(mass/EARTH_MASS, 0.3) * EARTH_RADIUS;
				} else if (mass < EARTH_MASS * 200) {
					radiusOfPlanet = (float)Math.Pow(mass/EARTH_MASS, 0.5) * EARTH_RADIUS;
				} else {
					radiusOfPlanet = (float)Math.Pow(mass/EARTH_MASS, -0.0886) * (float)22.6 * EARTH_RADIUS;
				}
			}
		}
		return false;
	}

	private void setTexture() {
		// Set the texture based on the radius/mass

		if (mass <= 0) {
			// Debug.LogError("Can't set the texture.");
		} else {
			if (mass < 0.05) {
				texture = "uranus";
			} else if (mass < 0.1) {
				texture = "neptune";
			} else if (mass < 0.65) {
				texture = "saturn";
			} else {
				texture = "jupiter";
			}
		}
	}

	private void setDistanceFromOptimal() {
		// We want closest to:
		// 1.175 * (Luminosity of this star / Luminosity of our sun) in astronomical units
		if (star.luminosity <= 0) {
			distanceFromOptimal = Double.MaxValue;
		} else {
			double optimalRadius = 1.175 * star.luminosity * AU_TO_KM;
			distanceFromOptimal = Math.Abs(radiusOfOrbit - optimalRadius);
		}
		// if (distanceFromOptimal <  AU_TO_KM * 0.1) {
		// 	Debug.Log("Very Habitable: " + distanceFromOptimal);
		// }
	}

	public override string ToString()
	{
		return String.Format("OrbRad {0:e2} : PlanRad {1} : Mass {2} : Name {3} : Disc {4} : Orbit {5} : Tex {6}", radiusOfOrbit, radiusOfPlanet, mass, name, discovered, timeToOrbit, texture);
	}
}
