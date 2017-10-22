using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Universe {
	private Dictionary<string, StarSystem> AllStarSystems = new Dictionary<string, StarSystem>();
	private Dictionary<string, StarSystem> StarSystems = new Dictionary<string, StarSystem>();
	int count = 0;
	public void addPlanetData(PlanetData pd) {
		string starName = pd.pl_hostname;

		StarSystem starSystem;
		if (AllStarSystems.TryGetValue(starName, out starSystem)) {
			// Already have this star
		} else {
			// First time seeing this star
			starSystem = new StarSystem (pd);
			AllStarSystems.Add(starName, starSystem);
		}
		starSystem.addPlanetData (pd);
	}

	public Dictionary<string, StarSystem> getStarSystems(){
		return StarSystems;
	}

	public void resetStarSystems() {
		// Shallow copy should be fine since the data doesn't change.
		StarSystems = AllStarSystems.ToDictionary(entry => entry.Key,
                                               entry => entry.Value);
	}

	// nearest to the Earth, most planets, most planets likely to be habitable, 
	// stars most like the sun
	public void nearestEarth() {
		foreach(KeyValuePair<string, StarSystem> starSystem in AllStarSystems)
		{
			// do something with entry.Value or entry.Key
		}
	}

	public void mostPlanets() {

	}

	public void mostLikelyHabitable() {

	}

	public void mostLikeSun() {

	}
}
