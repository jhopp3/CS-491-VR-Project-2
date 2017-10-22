using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Planet {
//orbit radius (km), radius (km), orbit period (yr), texture, name
//	{ "82578024", "13211",   "0.46", "mercury", "e" }
	public double radiusOfOrbit;
	public double radiusOfPlanet; // Jupiter radii
	public double mass; // Jupiter Mass
	public string name;
	public string discovered;
	public Star star;
	public double timeToOrbit; // Days

	public string texture;

	public bool errorMassRadius;  // Does this have a non-zero mass or radius?

	private const double AU_TO_KM = 149597870.7;
	private const double JUPITER_RADIUS_TO_KM = 69911;
	private const double YEAR_TO_DAYS = 365.2422;

	public Planet (PlanetData pd, Star s) {
		// pd.pl_orbsmax is in AU, convert to KM
		radiusOfOrbit = pd.pl_orbsmax * AU_TO_KM;
		// radiusOfOrbit = pd.pl_orbsmax;
		radiusOfPlanet = pd.pl_radj * JUPITER_RADIUS_TO_KM;
		mass = pd.pl_bmassj;
		name = pd.pl_name;
		discovered = pd.pl_discmethod;
		star = s;
		timeToOrbit = pd.pl_orbper / YEAR_TO_DAYS;
//		timeToOrbit = pd.pl_orbper;

		errorMassRadius = setMassRadius();
		setTexture ();
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
				mass = 0.00672 * Math.Exp(0.0000706 * (radiusOfPlanet));
			} else if (radiusOfPlanet <= 0) {
				// Set radius based on mass
				// Use formula Radius = 72483+(15496 * ln (JupiterMass))
				radiusOfPlanet = 72483+(15496 * Math.Log(mass));
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

	public override string ToString()
	{
		return String.Format("OrbRad {0:e2} : PlanRad {1} : Mass {2} : Name {3} : Disc {4} : Orbit {5} : Tex {6}", radiusOfOrbit, radiusOfPlanet, mass, name, discovered, timeToOrbit, texture);
	}
}
