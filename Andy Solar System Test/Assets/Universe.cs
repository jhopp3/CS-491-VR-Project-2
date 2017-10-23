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
		UniverseSorter uSorter = new UniverseSorter();
		// int count = 0;
		foreach(KeyValuePair<string, StarSystem> entry in AllStarSystems)
		{
			StarSystem ss = entry.Value;
			// count++;
			double distanceToStar;
			if (ss.star.distanceFromUs <= 0) {
				distanceToStar = double.MaxValue;
			} else {
				distanceToStar = ss.star.distanceFromUs;
			}
			uSorter.add(distanceToStar, ss);
		}
		// Debug.Log("Nearest * " + count.ToString());
		StarSystems = uSorter.get();
		// Debug.Log("SortedDict: " + uSorter.sortedDict.Count + " StarSystems.");
		// Debug.Log("Sorted with: " + StarSystems.Count + " StarSystems.");
	}

	public void mostPlanets() {

	}

	public void mostLikelyHabitable() {

	}

	public void mostLikeSun() {

	}

	public class UniverseSorter {
		// List<StarSystem> li = new List<StarSystem>();
		public SortedDictionary<double, List<StarSystem>> sortedDict;

		public UniverseSorter() {
			sortedDict = new SortedDictionary<double, List<StarSystem>>();
		}

		public void add(double comparer, StarSystem ss) {
			List<StarSystem> ssList;
			if(sortedDict.TryGetValue(comparer, out ssList)){
				ssList.Add(ss);
			} else {
				ssList = new List<StarSystem>();
				ssList.Add(ss);
				sortedDict.Add(comparer, ssList);
			}
		}

		public Dictionary<string, StarSystem> get() {
			Dictionary<string, StarSystem> outDict = new Dictionary<string, StarSystem>();
			foreach (KeyValuePair<double, List<StarSystem>> entry in sortedDict)
			{
				List<StarSystem> ssList = entry.Value;
				foreach (StarSystem ss in ssList) {
					outDict.Add(ss.star.name, ss);
				}
			}
			return outDict;
		}
	}
}
