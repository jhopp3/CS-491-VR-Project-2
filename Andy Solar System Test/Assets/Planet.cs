using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Planet {
	public double radiusOfOrbit;
	public double radiusOfPlanet; // Jupiter radii
	public double mass; // Jupiter Mass
	public string name;
	public string discovered;
	public Star star;
	public double timeToOrbit; // Days

	public string texture;

	public Planet (PlanetData pd, Star s) {
		radiusOfOrbit = pd.pl_orbsmax;
		radiusOfPlanet = pd.pl_radj;
		mass = pd.pl_bmassj;
		name = pd.pl_name;
		discovered = pd.pl_discmethod;
		star = s;
		timeToOrbit = pd.pl_orbper;

		setTexture ();
	}

	private void setTexture() {
		// Set the texture based on the radius/mass
		texture = "";
	}

	public override string ToString()
	{
		return String.Format("{0} : {1} : {2} : {3} : {4} : {5} : {6}", radiusOfOrbit, radiusOfPlanet, mass, name, discovered, timeToOrbit, texture);
	}
}
