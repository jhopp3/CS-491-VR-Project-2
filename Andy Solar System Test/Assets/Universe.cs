using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe {
	public Dictionary<string, StarSystem> StarSystems = new Dictionary<string, StarSystem>();
	int count = 0;
<<<<<<< Updated upstream
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

		// if (pd.pl_hostname == "tau Cet") {
		// 	Debug.Log(count.ToString() + ": " + starName + ": " + pd.pl_pnum);
		// 	Debug.Log(starSystem.ToString());
		// 	Debug.Log(starSystem.printFirstPlanet());
		// }
	}
=======
    /*private Dictionary<string, StarSystem> AllStarSystems = new Dictionary<string, StarSystem>();
    private Dictionary<string, StarSystem> StarSystems = new Dictionary<string, StarSystem>();
    int count = 0;
    public void addPlanetData(PlanetData pd)
    {
        string starName = pd.pl_hostname;

        StarSystem starSystem;
        if (AllStarSystems.TryGetValue(starName, out starSystem))
        {
            // Already have this star
        }
        else
        {
            // First time seeing this star
            starSystem = new StarSystem(pd);
            AllStarSystems.Add(starName, starSystem);
        }
        starSystem.addPlanetData(pd);
    }

    public Dictionary<string, StarSystem> getStarSystems()
    {
        return StarSystems;
    }

    public void resetStarSystems()
    {
        // Shallow copy should be fine since the data doesn't change.
        StarSystems = AllStarSystems.ToDictionary(entry => entry.Key,
                                               entry => entry.Value);
    }

    // nearest to the Earth, most planets, most planets likely to be habitable,
    // stars most like the sun
    public void nearestEarth()
    {
        UniverseSorter uSorter = new UniverseSorter();
        foreach (KeyValuePair<string, StarSystem> entry in AllStarSystems)
        {
            StarSystem ss = entry.Value;
            double luminosityDifference;
            if (ss.star.distanceFromUs <= 0)
            {
                luminosityDifference = double.MaxValue;
            }
            else
            {
                luminosityDifference = ss.star.distanceFromUs;
            }
            uSorter.add(luminosityDifference, ss);
        }
        StarSystems = uSorter.get();
    }

    public void mostPlanets()
    {
        UniverseSorter uSorter = new UniverseSorter();
        foreach (KeyValuePair<string, StarSystem> entry in AllStarSystems)
        {
            StarSystem ss = entry.Value;
            double planetsInSystem;
            // Assume int.MaxValue > planets in a star system.
            planetsInSystem = int.MaxValue - ss.star.numberOfPlanets;
            uSorter.add(planetsInSystem, ss);
        }
        StarSystems = uSorter.get();
    }

    public void mostLikelyHabitable()
    {
        // We want closest to:
        // 1.175 * (Luminosity of this star / Luminosity of our sun) in astronomical units
        UniverseSorter uSorter = new UniverseSorter();
        double distanceFromOptimal;

        foreach (KeyValuePair<string, StarSystem> entry in AllStarSystems)
        {
            StarSystem ss = entry.Value;
            if (ss.star.luminosity <= 0)
            {
                distanceFromOptimal = double.MaxValue;
            }
            else
            {
                distanceFromOptimal = ss.getMostOptimalPlanetDistance();
            }
            uSorter.add(distanceFromOptimal, ss);
        }
        StarSystems = uSorter.get();
    }

    public void mostLikeSun()
    {
        UniverseSorter uSorter = new UniverseSorter();
        foreach (KeyValuePair<string, StarSystem> entry in AllStarSystems)
        {
            StarSystem ss = entry.Value;
            double luminosityDifference;
            if (ss.star.luminosityLog == 0)
            {
                luminosityDifference = double.MaxValue;
            }
            else
            {
                luminosityDifference = Math.Abs(ss.star.luminosityLog);
            }
            uSorter.add(luminosityDifference, ss);
        }
        StarSystems = uSorter.get();
    }

    public void sortedAlphabetical()
    {
        SortedDictionary<string, StarSystem> sortedDict = new SortedDictionary<string, StarSystem>();
        foreach (KeyValuePair<string, StarSystem> entry in AllStarSystems)
        {
            string name = entry.Key;
            StarSystem ss = entry.Value;
            sortedDict.Add(name, ss);
        }

        Dictionary<string, StarSystem> outDict = new Dictionary<string, StarSystem>();

        foreach (KeyValuePair<string, StarSystem> entry in sortedDict)
        {
            string name = entry.Key;
            StarSystem ss = entry.Value;
            outDict.Add(name, ss);
        }

        StarSystems = outDict;
    }

    public class UniverseSorter
    {
        // List<StarSystem> li = new List<StarSystem>();
        public SortedDictionary<double, List<StarSystem>> sortedDict;

        public UniverseSorter()
        {
            sortedDict = new SortedDictionary<double, List<StarSystem>>();
        }

        public void add(double comparer, StarSystem ss)
        {
            List<StarSystem> ssList;
            if (sortedDict.TryGetValue(comparer, out ssList))
            {
                ssList.Add(ss);
            }
            else
            {
                ssList = new List<StarSystem>();
                ssList.Add(ss);
                sortedDict.Add(comparer, ssList);
            }
        }

        public Dictionary<string, StarSystem> get()
        {
            Dictionary<string, StarSystem> outDict = new Dictionary<string, StarSystem>();
            foreach (KeyValuePair<double, List<StarSystem>> entry in sortedDict)
            {
                List<StarSystem> ssList = entry.Value;
                foreach (StarSystem ss in ssList)
                {
                    outDict.Add(ss.star.name, ss);
                }
            }
            return outDict;
        }
    }*/
>>>>>>> Stashed changes
}
