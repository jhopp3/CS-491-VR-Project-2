using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe {
	public Dictionary<string, StarSystem> StarSystems = new Dictionary<string, StarSystem>();
	int count = 0;
	public void addPlanetData(PlanetData pd) {
		string starName = pd.pl_hostname;

		StarSystem starSystem;
		if (StarSystems.TryGetValue(starName, out starSystem)) {
			// Already have this star
		} else {
			// First time seeing this star
			starSystem = new StarSystem (pd);
			StarSystems.Add(starName, starSystem);

			if (pd.pl_pnum > 1) {
				count++;
				Debug.Log(count.ToString() + ": " + starName + ": " + pd.pl_pnum);
			}
		}
		starSystem.addPlanetData (pd);
	}
}
