using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Star : MonoBehaviour{

    public GameObject star;
    public GameObject bigStar;
    public GameObject sunSupport;
    public GameObject sunText;
    public Material sunMaterial;
    public PlanetData thisStarData;
    public Orbit orbit;
    public double eclipticLatitude; // in degrees
    public double eclipticLongitude; // in degrees

    //only for 2D
    public GameObject newSidePanel;
    public GameObject habZone;
    public Material habMaterial;

    public List<Planet> planets = new List<Planet>();
    public Dictionary<string, int> PlanetSystems = new Dictionary<string, int>();

    //List<Orbit> orbits = new List<Orbit>();

    public float luminosity;// light intensity
	new public string name;
	public float distanceFromUs; // in parsecs
	public string type; // Classification of the star based on their spectral characteristics following the Morgan-Keenan system.
	public float radius;
	public int numberOfPlanets;
	public string texture;
	public char spectralType;
    // set the habitable zone based on the star's luminosity
    public float innerHab;
    public float outerHab ;


    public const int EARTH_RADIUS = 695500;

    public Star(PlanetData pd) {
        luminosity = (float)Math.Exp(pd.st_lum);
        name = pd.pl_hostname;
        distanceFromUs = (float)pd.st_dist;
        type = pd.st_spstr;
        radius = (float)pd.st_rad * EARTH_RADIUS;
        numberOfPlanets = pd.pl_pnum;
        innerHab = luminosity * 9.5F;
        outerHab = luminosity * 14F;
        thisStarData = pd;
        eclipticLatitude = pd.st_elat;
        // if (eclipticLatitude == 0) { Debug.LogError("ELat Error"); }
        eclipticLongitude = pd.st_elon;
        // if (eclipticLatitude == 0) { Debug.LogError("ELong Error"); }
        setTexture();
    }

	public Star (string[] sunArray) {
		//radius (km), name, type, spectral classification, luminosity
		//new string[5] { "695500", "Our Sun", "sol", "G2V", "1.0" };
		radius = float.Parse(sunArray[0]);
		name = sunArray[1];
		type = sunArray[3];
		luminosity = float.Parse(sunArray[4]);
		distanceFromUs = 0;
		numberOfPlanets = 8;
        innerHab = luminosity * 9.5F;
        outerHab = luminosity * 14F;
        setTexture();

		Debug.Log(this.ToString());
	}

	private void setTexture() {
		if (!string.IsNullOrEmpty(type)) {
			// Debug.LogError("Type is " + type);
			spectralType = type[0];
			spectralType = Char.ToLower(spectralType);

			switch (spectralType)
			{
				case 'a':
					texture = "astar";
					break;
				case 'b':
					texture = "bstar";
					break;
				case 'f':
					texture = "fstar";
					break;
				case 'g':
					texture = "gstar";
					break;
				case 'k':
					texture = "kstar";
					break;
				case 'm':
					texture = "mstar";
					break;
				case 'l':
					// Binary system of two brown dwarfs
					// https://en.wikipedia.org/wiki/DENIS-P_J082303.1-491201_b
					texture = "mstar";
					break;
				case 't':
					// Binary system of two brown dwarfs
					// https://en.wikipedia.org/wiki/WISE_1217%2B1626
					texture = "mstar";
					break;
				case 'o':
					texture = "ostar";
					break;
				case 'w':
					// WD https://en.wikipedia.org/wiki/NN_Serpentis
					// Handle 2 stars here.
					texture = "bstar";
					break;
				case 's':
					// sdBV https://en.wikipedia.org/wiki/Subdwarf_B_star
					texture = "bstar";
					break;
				default:
					Debug.LogError("Invalid Spectral Type: " + spectralType);
					break;
			}

		} else {
			// Debug.LogError("Spectral Type is null");
		}
	}

	public override string ToString()
	{
		return String.Format("Lum {0} : Name {1} : Dist {2} : Type {3} : Radius {4} : Planets {5}", luminosity, name, distanceFromUs, type, radius, numberOfPlanets);
	}

    public void sideDealWithStar(float panelWidth, float panelDepth, float panelHeight, float panelXScale/*, PlanetData pd*/)
    { 
        star = GameObject.CreatePrimitive(PrimitiveType.Cube);
        star.name = "Side " + name + " Star";
        star.tag = "Star";
        star.transform.position = new Vector3(-0.5F * panelWidth - 0.5F, 0, 0);
        star.transform.localScale = new Vector3(4.0F, panelHeight * 40.0F, 40.0F * panelDepth);
        star.AddComponent<ObjectSelection>();
        Debug.Log("Star2");
        newSidePanel = GameObject.CreatePrimitive(PrimitiveType.Cube);
        newSidePanel.name = "Side " + name + " Panel";
        newSidePanel.transform.position = new Vector3(0, 0, 0);
        newSidePanel.transform.localScale = new Vector3(panelWidth, panelHeight, panelDepth);
        newSidePanel.transform.parent = star.transform;
        Debug.Log("NEw side Panel1");
        sunMaterial = Resources.Load(texture, typeof(Material)) as Material;
        star.GetComponent<MeshRenderer>().material = sunMaterial;
        Debug.Log("Material3");
        sunText = new GameObject
        {
            name = "Side Star Name"
        };
        sunText.transform.position = new Vector3(-0.47F * panelWidth, 22.0F * panelHeight, 0);
        sunText.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
        var sunTextMesh = sunText.AddComponent<TextMesh>();
        sunTextMesh.text = name;
        sunTextMesh.fontSize = 150;
        sunText.transform.parent = star.transform;
        Debug.Log("Text4");
        // need to take panelXScale into account for the hab zone

        habZone = GameObject.CreatePrimitive(PrimitiveType.Cube);
        habZone.name = "Hab";
        habZone.transform.position = new Vector3((-0.5F * panelWidth) + ((innerHab + outerHab) * 0.5F * panelXScale), 0, 0);
        habZone.transform.localScale = new Vector3((outerHab - innerHab) * panelXScale, 40.0F * panelHeight, 2.0F * panelDepth);
        habZone.transform.parent = star.transform;
        Debug.Log("Habzone5");
        habMaterial = new Material(Shader.Find("Standard"));
        habZone.GetComponent<MeshRenderer>().material = habMaterial;
        habMaterial.mainTexture = Resources.Load("habitable") as Texture;
        if ((panelXScale * innerHab) > panelWidth)
        {
            habZone.SetActive(false);
        }
            Debug.Log("HAb material6");
        foreach (Planet p in planets)
        {
            p.sideDealWithPlanets(panelXScale, panelWidth, panelDepth);
            p.thisPlanet.transform.parent = star.transform;
        }
        Debug.Log("Planets7");
    }

    public void dealWithStar(Vector3 loc)
    {
        //GameObject randomStars = new GameObject("Stars Universe");
        // string[] sol = new string[5] { "695500", "Our Sun", "sol", "G2V", "1.0" };
        float sunScale = radius / 100000F;
        float centerSunSize = 0.25F;

        //generate and set small star
        star = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        star.AddComponent<rotate>();
        star.name = name;
        star.transform.position = loc;
        star.transform.localScale = new Vector3(centerSunSize, centerSunSize, centerSunSize);
        star.GetComponent<rotate>().rotateSpeed = -0.25F;
        sunMaterial = Resources.Load(texture, typeof(Material)) as Material;
        star.GetComponent<MeshRenderer>().material = sunMaterial;

        // copy the sun and make a bigger version above

        bigStar = Instantiate(star);
        bigStar.name = name + " upper";
        bigStar.tag = "Star";
        bigStar.transform.parent = star.transform;
        bigStar.transform.localScale = new Vector3(sunScale, sunScale, sunScale);
        bigStar.transform.localPosition = new Vector3(0, 10, 0);
        bigStar.AddComponent<ObjectSelection>();
        bigStar.transform.parent = star.transform;

        // draw the support between them

        sunSupport = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sunSupport.transform.parent = star.transform;
        sunSupport.transform.localScale = new Vector3(0.1F, 10.0F, 0.1F);
        sunSupport.transform.localPosition = new Vector3(0, 5, 0);
        sunSupport.name = "Sun Support";
        

        // generate Star text
        sunText = new GameObject
        {
            name = "Star Name"
        };
        sunText.transform.parent = star.transform;
        sunText.transform.localPosition = new Vector3(0, 5, 0);
        sunText.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
        var sunTextMesh = sunText.AddComponent<TextMesh>();
        sunTextMesh.text = star.name;
        sunTextMesh.fontSize = 200;
        
        
    }
    public Vector3 dealWithStarInSpace(Star star1)
    {
        // https://en.wikipedia.org/wiki/Ecliptic_coordinate_system
        float r, b, l; // Ecliptic distance, latitude, longitude
        float x, y, z; // Rectangular

        const float DISTANCE_SCALER = 25F;
        r = (float)star1.distanceFromUs * DISTANCE_SCALER;

        b = (float)star1.eclipticLatitude;
        l = (float)star1.eclipticLongitude;

        x = (float)(r * Math.Cos(b) * Math.Cos(l));
        y = (float)(r * Math.Cos(b) * Math.Sin(l));
        z = (float)(r * Math.Sin(b));

        Vector3 location = new Vector3(x, y, z);

        return location;
    }
    public void dealWithStarAndPlanets()
    {
        dealWithStar(Vector3.zero);
        foreach(Planet p in planets)
        {
            p.dealWithPlanets(/*p.orbit.orbit.transform.localScale.x*/1);
            p.thisPlanetCenter.transform.parent = star.transform;
        }
        orbit = new Orbit("Inner habitable zone orbit", innerHab * 1, Color.green, innerHab);
        orbit.orbit.transform.parent = star.transform;
        orbit = new Orbit("Outer habitable zone orbit", outerHab * 1, Color.green, outerHab);
        orbit.orbit.transform.parent = star.transform;
    }

    public void AddPlanet2D(PlanetData pd)
    {
        Planet p = new Planet(pd);
        if (!p.errorMassRadius)
        {
            planets.Add(p);
            PlanetSystems.Add(p.name, planets.Count - 1);
        }
    }

    public void AddPlanet3D(PlanetData pd)
    {
        Planet p = new Planet(pd);

        planets.Add(p);
        PlanetSystems.Add(p.name, planets.Count-1);
        //p.dealWithPlanets();

    }

    public string printFirstPlanet()
    {
        if (planets.Count > 0)
        {
            return planets[0].ToString();
        }

        return "";
    }
    public void UpdateRadius(float radiusMultiplier)
    {
        //radiusOfPlanet = radiusOfPlanet * radiusMultiplier;
        float starSize = radius * 2.0F / 10000.0F;
       star.transform.localScale = new Vector3(starSize * radiusMultiplier, starSize * radiusMultiplier, starSize * radiusMultiplier);

    }
    public void DestroyStar()
    {
        print("Destroying Star: " + name);
        foreach (Planet p in planets)
        {
            p.DestroyPlanet();
        }
        Destroy(star);
        Destroy(bigStar);
        Destroy(sunSupport);
        Destroy(sunText);
        Destroy(newSidePanel);
        Destroy(habZone);
        
    }
}