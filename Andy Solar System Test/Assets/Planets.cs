/// Sample Code for CS 491 Virtual And Augmented Reality Course - Fall 2017
/// written by Andy Johnson
///
/// makes use of various textures from the celestia motherlode - http://www.celestiamotherlode.net/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Planets : MonoBehaviour {
	public enum SceneTypes { TwoD, ThreeDSystems, ThreeDStars };
	public SceneTypes sceneType;
	float panelHeight = 0.1F;
	float panelWidth = 30.0F;
	float panelDepth = 0.1F;

	[SerializeField] public float planetSizeChange ;
	[SerializeField] public float orbitSizeChange ;
	[SerializeField] public float rotationSpeedChange = 1.0F;
	[SerializeField] public float timePassingChange ;

	[SerializeField] private List<GameObject> allPlanetsCenters = new List<GameObject>() ;

	float orbitWidth = 0.01F;
	float habWidth = 0.03F;

	float revolutionSpeed ;

	float panelXScale = 2.0F;
	float orbitXScale = 2.0F ;

//	private string JSONFile = "MPS5.json"; // 3 planets from 585 Multi Planet Systems
	private string JSONFile = "MPS585.json"; // 585 Multi Planet Systems

	public static Universe THE_UNIVERSE = new Universe();



	/// <summary>
	///
	/// </summary>
	/// <param name="orbitName"></param>
	/// <param name="orbitRadius"></param>
	/// <param name="orbitColor"></param>
	/// <param name="myWidth"></param>
	/// <param name="myOrbits"></param>
	void drawOrbit(string orbitName, float orbitRadius, Color orbitColor, float myWidth, GameObject myOrbits){

		GameObject newOrbit;
		GameObject orbits;


		newOrbit = new GameObject()
		{
			name = orbitName
		};
		newOrbit.AddComponent<Circle> ();
		newOrbit.AddComponent<LineRenderer> ();

		newOrbit.GetComponent<Circle> ().xradius = orbitRadius;
		newOrbit.GetComponent<Circle> ().yradius = orbitRadius;

		var line = newOrbit.GetComponent<LineRenderer> ();
		line.startWidth = myWidth;
		line.endWidth = myWidth;
		line.useWorldSpace = false;

		newOrbit.GetComponent<LineRenderer> ().material.color = orbitColor;

		orbits = myOrbits;
		newOrbit.transform.parent = orbits.transform;


	}

	//------------------------------------------------------------------------------------//
	/// <summary>
	/// Creates new planets group to be added to an orbit
	/// </summary>
	/// <param name="planets"></param>
	/// <param name="thesePlanets"></param>
	/// <param name="theseOrbits"></param>
	void dealWithPlanets (List<Planet> planets, GameObject thesePlanets, GameObject theseOrbits){
		GameObject newPlanetCenter;
		GameObject newPlanet;

		/// the sun these planets folow
		GameObject sunRelated;

		Material planetMaterial;

		int planetCounter;

		foreach (Planet planet in planets) {

			float planetDistance = (float)planet.radiusOfOrbit / 149600000.0F * 10.0F;
			float planetSize = (float)planet.radiusOfPlanet * 2.0F / 10000.0F;
			float planetSpeed = -1.0F / (float)planet.timeToOrbit;
			string textureName = planet.texture;
			string planetName = planet.name;


			newPlanetCenter = new GameObject (planetName + "Center");
			newPlanetCenter.AddComponent<rotate> ();

			newPlanet = GameObject.CreatePrimitive (PrimitiveType.Sphere);
			newPlanet.name = planetName;
			newPlanet.transform.position = new Vector3 (0, 0, planetDistance * orbitXScale);
			newPlanet.transform.localScale = new Vector3 (planetSize, planetSize, planetSize);
			newPlanet.transform.parent = newPlanetCenter.transform;

			newPlanetCenter.GetComponent<rotate> ().rotateSpeed = planetSpeed;
			newPlanetCenter.GetComponent<rotate>().SetRevolution(revolutionSpeed);

			planetMaterial = new Material (Shader.Find ("Standard"));
			newPlanet.GetComponent<MeshRenderer> ().material = planetMaterial;
			planetMaterial.mainTexture = Resources.Load (textureName) as Texture;

			drawOrbit (planetName + " orbit", planetDistance * orbitXScale, Color.white, orbitWidth, theseOrbits);

			sunRelated = thesePlanets;
			newPlanetCenter.transform.parent = sunRelated.transform;
			allPlanetsCenters.Add(newPlanetCenter);

		}
	}

	//------------------------------------------------------------------------------------//

	void sideDealWithPlanets (List<Planet> planets, GameObject thisSide, GameObject theseOrbits){
		GameObject newPlanet;

		GameObject sunRelated;

		Material planetMaterial;

		int planetCounter;

		foreach (Planet planet in planets) {

			float planetDistance = (float)planet.radiusOfOrbit / 149600000.0F * 10.0F;
			float planetSize = (float)planet.radiusOfPlanet * 1.0F / 10000.0F;
			string textureName = planet.texture;
			string planetName = planet.name;

			// limit the planets to the width of the side view
			if ((panelXScale * planetDistance) < panelWidth) {

				newPlanet = GameObject.CreatePrimitive (PrimitiveType.Sphere);
				newPlanet.name = planetName;
				newPlanet.transform.position = new Vector3 (-0.5F * panelWidth + planetDistance * panelXScale, 0, 0);
				newPlanet.transform.localScale = new Vector3 (planetSize, planetSize, 5.0F * panelDepth);

				planetMaterial = new Material (Shader.Find ("Standard"));
				newPlanet.GetComponent<MeshRenderer> ().material = planetMaterial;
				planetMaterial.mainTexture = Resources.Load (textureName) as Texture;

				sunRelated = thisSide;
				newPlanet.transform.parent = sunRelated.transform;
			}
		}
	}

	//------------------------------------------------------------------------------------//

	void sideDealWithStar (Star star, GameObject thisSide, GameObject theseOrbits){
		GameObject newSidePanel;
		GameObject newSideSun;
		GameObject sideSunText;

		GameObject habZone;

		Material sideSunMaterial, habMaterial;

		newSidePanel = GameObject.CreatePrimitive (PrimitiveType.Cube);
		newSidePanel.name = "Side " + star.name + " Panel";
		newSidePanel.transform.position = new Vector3 (0, 0, 0);
		newSidePanel.transform.localScale = new Vector3 (panelWidth, panelHeight, panelDepth);
		newSidePanel.transform.parent = thisSide.transform;

		newSideSun = GameObject.CreatePrimitive (PrimitiveType.Cube);
		newSideSun.name = "Side " + star.name + " Star";
		newSideSun.transform.position = new Vector3 (-0.5F * panelWidth - 0.5F, 0, 0);
		newSideSun.transform.localScale = new Vector3 (1.0F, panelHeight*40.0F, 2.0F * panelDepth);
		newSideSun.transform.parent = thisSide.transform;

		sideSunMaterial = new Material (Shader.Find ("Unlit/Texture"));
		newSideSun.GetComponent<MeshRenderer> ().material = sideSunMaterial;
		sideSunMaterial.mainTexture = Resources.Load (star.texture) as Texture;


		sideSunText = new GameObject();
		sideSunText.name = "Side Star Name";
		sideSunText.transform.position = new Vector3 (-0.47F * panelWidth, 22.0F * panelHeight, 0);
		sideSunText.transform.localScale = new Vector3 (0.1F, 0.1F, 0.1F);
		var sunTextMesh = sideSunText.AddComponent<TextMesh>();
		sunTextMesh.text = star.name;
		sunTextMesh.fontSize = 150;
		sideSunText.transform.parent = thisSide.transform;

		float innerHab = (float)star.luminosity * 9.5F;
		float outerHab = (float)star.luminosity * 14F;


		// need to take panelXScale into account for the hab zone

		habZone = GameObject.CreatePrimitive (PrimitiveType.Cube);
		habZone.name = "Hab";
		habZone.transform.position = new Vector3 ((-0.5F * panelWidth) + ((innerHab+outerHab) * 0.5F * panelXScale), 0, 0);
		habZone.transform.localScale = new Vector3 ((outerHab - innerHab)* panelXScale, 40.0F * panelHeight, 2.0F * panelDepth);
		habZone.transform.parent = thisSide.transform;

		habMaterial = new Material (Shader.Find ("Standard"));
		habZone.GetComponent<MeshRenderer> ().material = habMaterial;
		habMaterial.mainTexture = Resources.Load ("habitable") as Texture;

	}

	//------------------------------------------------------------------------------------//

	void dealWithStar (Star star, GameObject thisStar, GameObject theseOrbits){

		GameObject newSun, upperSun;
		Material sunMaterial;

		GameObject sunRelated;
		GameObject sunSupport;
		GameObject sunText;

		// string[] sol = new string[5] { "695500", "Our Sun", "sol", "G2V", "1.0" };
		float sunScale = (float)star.radius / 100000F;
		float centerSunSize = 0.25F;

		// set the habitable zone based on the star's luminosity
		float innerHab = (float)star.luminosity * 9.5F;
		float outerHab = (float)star.luminosity * 14F;


		newSun = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		newSun.AddComponent<rotate> ();
		newSun.name = star.name;
		newSun.transform.position = new Vector3 (0, 0, 0);
		newSun.transform.localScale = new Vector3 (centerSunSize, centerSunSize, centerSunSize);

		sunRelated = thisStar;

		newSun.GetComponent<rotate> ().rotateSpeed = -0.25F;

		sunMaterial = new Material (Shader.Find ("Unlit/Texture"));
		newSun.GetComponent<MeshRenderer> ().material = sunMaterial;
		sunMaterial.mainTexture = Resources.Load (star.texture) as Texture;

		newSun.transform.parent = sunRelated.transform;


		// copy the sun and make a bigger version above

		upperSun = Instantiate (newSun);
		upperSun.name = star.name + " upper";
		upperSun.transform.localScale = new Vector3 (sunScale,sunScale,sunScale);
		upperSun.transform.position = new Vector3 (0, 10, 0);

		upperSun.transform.parent = sunRelated.transform;

		// draw the support between them
		sunSupport = GameObject.CreatePrimitive (PrimitiveType.Cube);
		sunSupport.transform.localScale = new Vector3 (0.1F, 10.0F, 0.1F);
		sunSupport.transform.position = new Vector3 (0, 5, 0);
		sunSupport.name = "Sun Support";

		sunSupport.transform.parent = sunRelated.transform;


		sunText = new GameObject();
		sunText.name = "Star Name";
		sunText.transform.position = new Vector3 (0, 5, 0);
		sunText.transform.localScale = new Vector3 (0.1F, 0.1F, 0.1F);
		var sunTextMesh = sunText.AddComponent<TextMesh>();
		sunTextMesh.text = star.name;
		sunTextMesh.fontSize = 200;

		sunText.transform.parent = sunRelated.transform;

		if (sceneType == SceneTypes.ThreeDSystems) {
			drawOrbit ("Habitable Inner Ring", innerHab * orbitXScale, Color.green, habWidth, theseOrbits);
			drawOrbit ("Habitable Outer Ring", outerHab * orbitXScale, Color.green, habWidth, theseOrbits);
		}
	}

	//------------------------------------------------------------------------------------//
	void dealWithSystem(string[] starInfo, string[,] planetInfo, Vector3 offset, GameObject allThings){
		// Remnant of dealing with our solar system values in string array form
		Star s = new Star (starInfo);
		List<Planet> planetList = new List<Planet>();

		for (int i = 0; i < planetInfo.GetLength(0); i++) {
			string[] planetArray = new string[] { planetInfo[i,0], planetInfo[i,1], planetInfo[i,2], planetInfo[i,3], planetInfo[i,4] };
			Planet p = new Planet(planetArray, s);
			planetList.Add(p);
		}

		dealWithSystem(s, planetList, offset, allThings);
	}

	void dealWithSystem(Star star, List<Planet> planets, Vector3 offset, GameObject allThings){

		GameObject SolarCenter;
		GameObject AllOrbits;
		GameObject SunStuff;
		GameObject Planets;

		SolarCenter = new GameObject();
		AllOrbits = new GameObject();
		SunStuff = new GameObject();
		Planets = new GameObject();

		SolarCenter.name = "SolarCenter" + " " + star.name;
		AllOrbits.name = "All Orbits" + " " + star.name;
		SunStuff.name = "Sun Stuff" + " " + star.name;
		Planets.name = "Planets" + " " + star.name;

		SolarCenter.transform.parent = allThings.transform;

		AllOrbits.transform.parent = SolarCenter.transform;
		SunStuff.transform.parent = SolarCenter.transform;
		Planets.transform.parent = SolarCenter.transform;

		if (sceneType == SceneTypes.ThreeDStars) {
			dealWithStar (star, SunStuff, AllOrbits);
			// need to do this last
			SolarCenter.transform.position = offset;

		}
		if (sceneType == SceneTypes.ThreeDSystems) {
			dealWithStar (star, SunStuff, AllOrbits);
			dealWithPlanets (planets, Planets, AllOrbits);

			// need to do this last
			SolarCenter.transform.position = offset;

		}
		// add in second 'flat' representation
		if (sceneType == SceneTypes.TwoD) {
			GameObject SolarSide;
			SolarSide = new GameObject ();
			SolarSide.name = "Side View of" + star.name;


			sideDealWithStar (star, SolarSide, AllOrbits);
			sideDealWithPlanets (planets, SolarSide, AllOrbits);

			SolarSide.transform.position = new Vector3 (0, 8, 10.0F);
			SolarSide.transform.position += (offset * 0.15F);
		}

	}

	//------------------------------------------------------------------------------------//

	private void setPlanetObjects(PlanetData[] planets) {
		int count = 0;
		foreach (PlanetData planet in planets)
		{
			count++;
//			Debug.Log(count.ToString() + " " + planet.pl_hostname);
//			Debug.Log(count.ToString() + " " + JsonUtility.ToJson(planet));
			THE_UNIVERSE.addPlanetData(planet);
		}

		Debug.Log("Loaded: " + THE_UNIVERSE.StarSystems.Count.ToString() + " Star Systems.");
		Debug.Log("Loaded: " + count.ToString() + " Planets.");
	}

	//------------------------------------------------------------------------------------//

	string fixJson(string value)
	{
		value = "{\"Items\":" + value + "}";
		return value;
	}

	private void LoadPlanetData()
	{
		// Path.Combine combines strings into a file path
		// Application.StreamingAssets points to Assets/StreamingAssets in the Editor, and the StreamingAssets folder in a build
		string filePath = Path.Combine(Application.streamingAssetsPath, JSONFile);

		if(File.Exists(filePath))
		{
			string dataAsJson = fixJson(File.ReadAllText(filePath));

			PlanetData[] planets = JsonHelper.FromJson<PlanetData>(dataAsJson);
			setPlanetObjects (planets);

			Debug.Log("Planet Data loaded.");
		}
		else
		{
			Debug.LogError("Cannot load planet data!");
		}
	}

	void loadAllSystems(GameObject allCenter) {
		var systemOffset = new Vector3(0, 0, 0);
		var oneOffset = new Vector3(0, -30, 0);

		systemOffset += oneOffset;

		// dealWithSystem(TauCeti, TauCetiPlanets, systemOffset, allCenter);

		// systemOffset += oneOffset;

		// dealWithSystem(Gliese581, Gliese581Planets, systemOffset, allCenter);

		// int count = 0;

		foreach(KeyValuePair<string, StarSystem> entry in THE_UNIVERSE.StarSystems) {
			// do something with entry.Value or entry.Key
			StarSystem ss = entry.Value;
			// count++;
			// if (count%50 == 0) {
			// 	Debug.Log(count.ToString() + " " + ss.ToString());
			// }
			dealWithSystem(ss.star, ss.planets, systemOffset, allCenter);
			systemOffset += oneOffset;
		}
	}

	//------------------------------------------------------------------------------------//

	void Start () {
		planetSizeChange = 1.0F;//To change planet size
		orbitSizeChange = 1.0F;//To change orbit size
		rotationSpeedChange = 1.0F;//To change rotation speed (revolution speed variable)
		timePassingChange = 1.0F;//To change time passing
		revolutionSpeed = 0.2F ;
		LoadPlanetData();

		SaveState s = new SaveState();
		s.saveData();
		s.loadData();

		//radius (km), name, type, spectral classification, luminosity

		string[] sol = new string[5] { "695500", "Our Sun", "sol", "G2V", "1.0" };

		//orbit radius (km), radius (km), orbit period (yr), texture, name
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


		// string[] TauCeti = new string[5] { "556400", "Tau Ceti", "gstar", "G8.5V", "0.52" };
		//orbit radius (km), radius (km), orbit period (yr), texture, name
		// string[,] TauCetiPlanets = new string[5, 5] {
		// 	{ "15707776",  "9009",   "0.04", "venus",   "b" },
		// 	{ "29171585", "11217",   "0.09", "venus", "c" },
		// 	{ "55949604", "12088",   "0.26", "mercury",  "d" },
		// 	{ "82578024", "13211",   "0.46", "mercury", "e" },
		// 	{"201957126", "16454",   "1.75", "uranus",  "f" }
		// };


		// string[] Gliese581 = new string[5] { "201750", "Gliese 581", "mstar", "M3V", "0.013" };

		// string[,] Gliese581Planets = new string[3, 5] {
		// 	{ "4188740",  "8919",   "0.009", "venus",   "e" },
		// 	{ "6133513", "30554",   "0.014", "jupiter",   "b" },
		// 	{"10920645", "20147",   "0.18", "neptune",  "c" }
		// };

		GameObject allCenter = new GameObject
		{
			name = "all systems"
		};


		var systemOffset = new Vector3(0, 0, 0);
		var oneOffset = new Vector3(0, -30, 0);

		dealWithSystem(sol, solPlanets, systemOffset, allCenter);

		loadAllSystems(allCenter);

		// systemOffset += oneOffset;

		// dealWithSystem(TauCeti, TauCetiPlanets, systemOffset, allCenter);

		// systemOffset += oneOffset;

		// dealWithSystem(Gliese581, Gliese581Planets, systemOffset, allCenter);


		allCenter.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);


	}
	float revolutionSpeedOld;

	// Update is called once per frame
	void Update()
	{
		revolutionSpeedOld = revolutionSpeed;
		revolutionSpeed = 0.2F * rotationSpeedChange;
		if (revolutionSpeed != revolutionSpeedOld)
		{
			foreach(GameObject planetC in allPlanetsCenters)
			{
				planetC.GetComponent<rotate>().SetRevolution(revolutionSpeed);

			}

		}
	}
}