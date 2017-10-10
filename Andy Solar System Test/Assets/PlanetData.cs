using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetData  {
	public string pl_hostname; //Name of the planet
	public string pl_discmethod; //How the planets were discovered
	public string pl_orbsmax; //Radius of orbit
	public string pl_bmassj; //Mass
	public string pl_radj; //Radius of planet


	public static PlanetData CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<PlanetData>(jsonString);
	}

	// Given JSON input:
	// {"name":"Dr Charles","lives":3,"health":0.8}
	// this example will return a PlayerInfo object with
	// name == "Dr Charles", lives == 3, and health == 0.8f.
}
