/// Sample Code for CS 491 Virtual And Augmented Reality Course - Fall 2017
/// written by Andy Johnson
///
/// makes use of various textures from the celestia motherlode - http://www.celestiamotherlode.net/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CreateSystems : MonoBehaviour {
	public enum SceneTypes { TwoD, ThreeDSystems, ThreeDStars };
	public SceneTypes sceneType;
	float panelHeight = 0.1F;
	float panelWidth = 30.0F;
	float panelDepth = 0.1F;

    //Changable
    [SerializeField] public static float planetSizeChange = 1.0f;
    [SerializeField] public static float orbitSizeChange = 1.0f;
    [SerializeField] public static float yearMultiplier = 1.0f;
    [SerializeField] public static float StarSizeChange = 1.0f;
    private float planetSizeChange_old = 1.0f;
    private float orbitSizeChange_old = 1.0f;
    private float yearMultiplier_old = 1.0f;
    private float starSizeChange_old = 1.0f;
    public bool loadNextPage = false;
    public bool loadpreviousPage = false;


    public GameObject universeCenter;
    public Vector3 systemOffset;

    List<Star> stars = new List<Star>();
    
    public Dictionary<string, int> StarSystems = new Dictionary<string, int>();
    private int pageNumber = 1;
    public static int goToPage  =1;
    
    List<int> loaddedSystemsIndex = new List<int>();

    [HideInInspector] public  PlanetData[] planetData;

    // [SerializeField] private List<GameObject> allPlanetsCenters = new List<GameObject>() ;

    float orbitWidth = 0.001F;
	float habWidth = 0.003F;

	float panelXScale = 2.0F;
	float orbitXScale = 2.0F ;

    private string JSONFile = "MPS585.json"; // 585 Multi Planet Systems

	public static Universe THE_UNIVERSE = new Universe();

    

    void SolarSystemGenerator(string[] starInfo, string[,] planetInfo, Vector3 offset, GameObject UniverseCenter)
    {
        print("SolarSystemGenerator");
        // Remnant of dealing with our solar system values in string array form
        Star s = new Star(starInfo);
        List<PlanetData> pdata = new List<PlanetData>();
        
        for (int i = 0; i < planetInfo.GetLength(0); i++)
        {
            pdata.Add(new PlanetData());
            string[] planetArray = new string[] { planetInfo[i, 0], planetInfo[i, 1], planetInfo[i, 2], planetInfo[i, 3], planetInfo[i, 4] };
            Planet p = new Planet(planetArray);
            pdata[i].pl_hostname = "sol";
            pdata[i].pl_name = planetInfo[i,4];
            pdata[i].pl_orbsmax = double.Parse(planetInfo[i, 0]);
            pdata[i].pl_radj = float.Parse(planetInfo[i, 1]);
            pdata[i].pl_orbper = float.Parse(planetInfo[i, 2]);
            s.planets.Add(p);
        }
        StarSystems.Add(s.name, stars.Count);
        stars.Add(s);
        
    }

    void randomStars()
    {
        print("randomStars");
        int i = 0;
        foreach (Star star in stars)
        {
            if (i == 500)
                return;
            star.dealWithStar(star.dealWithStarInSpace(star));
            star.star.transform.parent = universeCenter.transform;
            ++i;
        }
    }

    void dealWithSystem(Star star,  Vector3 offset)
    {
        print("dealWithSystem");
        GameObject SolarCenter;
        if (sceneType == SceneTypes.ThreeDSystems)
        {
            
            star.dealWithStarAndPlanets();
            // need to do this last

        }
        if (sceneType == SceneTypes.TwoD)
        {
            GameObject SolarSide;
            SolarSide = new GameObject();
            SolarSide.name = "Side View of" + star.name;
            SolarSide.tag = "Panel";
            BoxCollider bCollider = SolarSide.AddComponent(typeof(BoxCollider)) as BoxCollider;
            bCollider.center = new Vector3(100, 0, 0);
            bCollider.size = new Vector3(300, 4, 1);
            Debug.Log("1 " + panelWidth+ " 2 " + panelDepth+ " 3 " + panelHeight + " 4 " + panelXScale);
            star.sideDealWithStar(panelWidth,panelDepth,panelHeight,panelXScale);
            SolarSide.transform.position = new Vector3(0, 8, 10.0F);
            SolarSide.transform.position += (offset * 0.15F);
        }
        SolarCenter = star.star;

        SolarCenter.transform.position = offset;
        SolarCenter.transform.parent = universeCenter.transform;
    }

    string fixJson(string value)
	{
		value = "{\"Items\":" + value + "}";
		return value;
	}


    /// <summary>
    /// loads planet's data from the json file and 
    /// sends it to addPlanetData (no 3D objects generated here)
    /// </summary>
	private void LoadPlanetData()
	{
        print("LoadPlanetData");
        // Path.Combine combines strings into a file path
        // Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
        string filePath = Path.Combine(Application.streamingAssetsPath, JSONFile);

		if(File.Exists(filePath))
		{
			string dataAsJson = fixJson(File.ReadAllText(filePath));

			planetData = JsonHelper.FromJson<PlanetData>(dataAsJson);/*change to planetsData*/
            foreach (PlanetData planet in planetData)
            {
                addPlanetData(planet);
            }

			Debug.Log("Planet Data loaded.");
		}
		else
		{
			Debug.LogError("Cannot load planet data!");
		}
	}

    /// <summary>
    /// inserts the data of a planet to the Planet object, and the star data if not already existing
    /// (no 3D objects generated here)
    /// </summary>
    /// <param name="pd">the data for the planet to be added</param>
    public void addPlanetData(PlanetData pd)
    {
        print("addPlanetData");
        string starName = pd.pl_hostname;
        int starIndex = -1;
        if (StarSystems.TryGetValue(starName, out starIndex)) // star already exists
        {
            if (stars[starIndex].PlanetSystems.ContainsKey(pd.pl_letter)) // planet already exists
            {
                return;
            }
            if (sceneType == SceneTypes.TwoD)
            {
                stars[starIndex].AddPlanet2D(pd);
            }
            else if (sceneType == SceneTypes.ThreeDSystems)
            {
                stars[starIndex].AddPlanet3D(pd);
            }
        }
        else // star not found (create new star & add planet)
        {
            Star s = new Star(pd);
           
            if (sceneType == SceneTypes.TwoD)
            {
                s.AddPlanet2D(pd);
            }
            else if (sceneType == SceneTypes.ThreeDSystems)
            {
                s.AddPlanet3D(pd);
            }
            StarSystems.Add(starName, stars.Count);
            stars.Add(s);
        }


    }

    /// <summary>
    /// loads a list of systems as 3D game objects in the scene
    /// </summary>
    /// <param name="page">the page number to load</param>
    public void LoadPage(int page)
    {
        
        StopAllCoroutines();
        print("Star list size = "+stars.Count);

        if (loaddedSystemsIndex.Count != 0)
        {
            for (int i = 0;i< loaddedSystemsIndex.Count; ++i){
                stars[loaddedSystemsIndex[i]].DestroyStar();
            }
        }

        loaddedSystemsIndex = new List<int>();
        if (page < 1)
        {
            pageNumber = 1;
            goToPage = pageNumber;
        }
        else
        {
            pageNumber = page;
            goToPage = pageNumber;
        }
        print("Loadding page " + pageNumber);
        var systemOffset = new Vector3(0, 0, 0);
        var oneOffset = new Vector3(0, -15, 0);
        if(sceneType == SceneTypes.ThreeDSystems)
        {
            oneOffset = new Vector3(0, -50, 0);
        }

        systemOffset += oneOffset;
        int lastToLoad;
        if (stars.Count < pageNumber * 10)
            lastToLoad = stars.Count;
        else
            lastToLoad = pageNumber * 10;
        for (int i = (pageNumber * 10) - 10; i < lastToLoad; ++i)
        {
             Star s = stars[i];


            dealWithSystem(s, systemOffset);
            systemOffset += oneOffset;
            loaddedSystemsIndex.Add(i);
        }
        StartCoroutine(LateStart(0.5f));
    }
    /// <summary>
    /// loads page+1
    /// </summary>
    public void LoadNextPage()
    {
        
        LoadPage(++pageNumber);
    }
    /// <summary>
    /// loads page-1
    /// </summary>
    public void LoadpreviousPage()
    {
        if (pageNumber <= 1)
            return;
        LoadPage(--pageNumber);
    }

    void Start () {
        print("Starting...");
        print(sceneType);
		planetSizeChange = 1.0F;//To change planet size
		orbitSizeChange = 1.0F;//To change orbit size
		yearMultiplier= 1.0F;//To change orbit Speed
        StarSizeChange = 1.0F;
        universeCenter = new GameObject
        {
            name = "all systems"
        };
        systemOffset = new Vector3(0, -30, 0);
       
        // creating our system's data (sample)
        string[] sol = new string[5] { "695500", "Our Sun", "sol", "G2V", "1.0" };

		string[,] solPlanets = new string[8, 5] {
			{   "57910000",  "2440",    "0.24", "mercury", "mercury" },
			{  "108200000",  "6052",    "0.62", "venus",   "venus" },
			{  "149600000",  "6371",    "1.00", "earthmap", "earth" },
			{  "227900000",  "3400",    "1.88", "mars",     "mars" },
			{  "778500000", "69911",   "11.86", "jupiter", "jupiter" },
			{ "1433000000", "58232",   "29.46", "saturn",   "saturn" },
			{ "2877000000", "25362",   "84.01", "neptune", "uranus" },
			{ "4503000000", "24622",  "164.80", "uranus", "neptune" }
		};
        SolarSystemGenerator(sol, solPlanets, systemOffset, universeCenter);
        // complete your system
        LoadPlanetData();//loads data from json file, will be moved to json class
        universeCenter.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);//TODO scale down the universe to be viewable
        if (sceneType == SceneTypes.ThreeDStars)
        {
            randomStars();
            return;
        }
        LoadPage(1);
        CreateSystems.planetSizeChange = 0.3f;
        //print(stars[0].planets[2].orbit.orbit.GetComponent<LineRenderer>().positionCount);
        //stars[0].planets[2].thisPlanet.transform.localPosition = stars[0].planets[2].orbit.orbit.GetComponent<LineRenderer>().GetPosition(5);
        StartCoroutine(LateStart(1));
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //Your Function You Want to Call
        print("LateStart");
        if (SaveManager.movingToScene)
        {

        }
        foreach (int index in loaddedSystemsIndex)
        {
            foreach (Planet p in stars[index].planets)
            {
                p.UpdateRadius(planetSizeChange);
                p.thisPlanet.transform.localPosition = p.orbit.orbit.GetComponent<LineRenderer>().GetPosition(0);

            }
        }
        if (sceneType == SceneTypes.ThreeDStars)
        {
            starSizeChange_old = StarSizeChange;
            foreach (int index in loaddedSystemsIndex)
            {
                foreach (Star s in stars)
                {
                    s.UpdateRadius(StarSizeChange);
                }
            }
        }
    }
    void Update()
	{
        if (goToPage != pageNumber)
        {
            LoadPage(goToPage);
        }
        if (loadNextPage)
        {
            LoadNextPage();
            loadNextPage = false;
        }
        if (loadpreviousPage)
        {
            LoadpreviousPage();
            loadpreviousPage = false;
        }

        if (planetSizeChange == planetSizeChange_old && yearMultiplier == yearMultiplier_old && orbitSizeChange == orbitSizeChange_old)
            return;
        print("updating");
        if (planetSizeChange != planetSizeChange_old)
        {
            planetSizeChange_old = planetSizeChange;
            foreach (int index in loaddedSystemsIndex)
            {
                foreach (Planet p in stars[index].planets)
                {
                    p.UpdateRadius(planetSizeChange);
                    
                }
            }
        }
        if ( sceneType != SceneTypes.TwoD)
        {
            if (yearMultiplier != yearMultiplier_old)
            {
                yearMultiplier_old = yearMultiplier;
                foreach (int index in loaddedSystemsIndex)
                {
                    foreach (Planet p in stars[index].planets)
                    {
                        p.UpdateTimeToOrbit(yearMultiplier);
                        
                    }
                }
            }
            if (orbitSizeChange != orbitSizeChange_old)
            {
                orbitSizeChange_old = orbitSizeChange;
                foreach (int index in loaddedSystemsIndex)
                {
                    foreach (Planet p in stars[index].planets)
                    {
                        p.UpdateOrbitRadius(orbitSizeChange);
                        
                    }
                }
            }
        }
        if(sceneType == SceneTypes.ThreeDStars)
        {
            starSizeChange_old = StarSizeChange;
            foreach (int index in loaddedSystemsIndex)
            {
                foreach (Star s in stars)
                {
                    s.UpdateRadius(StarSizeChange);
                }
            }
        }
	} 
}