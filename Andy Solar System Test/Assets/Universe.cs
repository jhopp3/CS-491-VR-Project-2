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

			// Debug Star prints
//			if (pd.pl_pnum > 5) {
//				count++;
//				Debug.Log(count.ToString() + ": " + starName + ": " + pd.pl_pnum);
//				Debug.Log(starSystem.ToString());
//			}
		}
		starSystem.addPlanetData (pd);

		// Debug Planet prints
		// if ((pd.pl_pnum > 5) && (starSystem.planets.Count == 5)) {
		// 	Debug.Log(count.ToString() + ": " + starName + ": " + pd.pl_pnum);
		// 	Debug.Log(starSystem.printFirstPlanet());
		// }

		if (pd.pl_hostname == "tau Cet") {
			Debug.Log(count.ToString() + ": " + starName + ": " + pd.pl_pnum);
			Debug.Log(starSystem.ToString());
			starSystem.printAllPlanets();
		}
	}
}
