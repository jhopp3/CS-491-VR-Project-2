using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Planet : MonoBehaviour{
    //orbit radius (km), radius (km), orbit period (yr), texture, name
    //	{ "82578024", "13211",   "0.46", "mercury", "e" }

    public GameObject thisPlanet;
    public GameObject thisPlanetCenter;
    public Orbit orbit; 

    //properties
    new public string name;
    public float radiusOfOrbit;
    public float radiusOfPlanet; // Jupiter radii
    public float timeToOrbit; // Days
    public float mass; // Jupiter Mass
    public Material planetMaterial;

    //info
    public string discovered;
    public string texture;
    public bool errorMassRadius;  // Does this have a non-zero mass or radius?
    
    // constants
	private const float AU_TO_KM = 149597870.7f;
	private const int JUPITER_RADIUS_TO_KM = 69911;
	private const float YEAR_TO_DAYS = 365.2422f;
	private const float EARTH_MASS = 0.00315f; // Jupiter Mass Units
	private const int EARTH_RADIUS = 6371;

    //Functions
    /// <summary>
    /// 
    /// </summary>
    /// <param name="pd"></param>
    /// <param name="s"></param>
    public Planet (PlanetData pd) {
		// pd.pl_orbsmax is in AU, convert to KM
		radiusOfOrbit = (float)pd.pl_orbsmax * AU_TO_KM;
        print("radiusOfOrbit : " + radiusOfOrbit);

        radiusOfPlanet = pd.pl_radj * JUPITER_RADIUS_TO_KM;
		mass = (float)pd.pl_bmassj;
		name = pd.pl_name;
		discovered = pd.pl_discmethod;
		timeToOrbit = (float)pd.pl_orbper / YEAR_TO_DAYS;
		errorMassRadius = setMassRadius();
		setTexture ();


    }

	public Planet (string[] planetArray) {
		//orbit radius (km), radius (km), orbit period (yr), texture, name
		// {   "57910000",  "2440",    "0.24", "mercury", "mercury" }
		radiusOfOrbit = float.Parse(planetArray[0]);
		radiusOfPlanet = float.Parse(planetArray[1]);
		timeToOrbit = float.Parse(planetArray[2]);
		texture = planetArray[3];
		name = planetArray[4];
        
    }

	private bool setMassRadius() {
		// If either the Mass or Radius is null from the import, guess it's value based on the other.

		if ((mass <= 0) && (radiusOfPlanet <= 0)) {
			// Debug.LogError("Mass and Radius of 0.");
			return true;
		} else {
			if (mass <= 0) {
				// Set mass based on radius
				// Use formula JupiterMass = 0.00672 * EXP(0.0000706*(Radius))
				mass = (float)0.00672 * (float)Math.Exp(0.0000706 * (radiusOfPlanet));
			} else if (radiusOfPlanet <= 0) {
				// Set radius based on mass
				// http://phl.upr.edu/library/notes/standardmass-radiusrelationforexoplanets
				
				if (mass < EARTH_MASS) {
					radiusOfPlanet = (float)Math.Pow(mass/EARTH_MASS, 0.3) * EARTH_RADIUS;
				} else if (mass < EARTH_MASS * 200) {
					radiusOfPlanet = (float)Math.Pow(mass/EARTH_MASS, 0.5) * EARTH_RADIUS;
				} else {
					radiusOfPlanet = (float)Math.Pow(mass/EARTH_MASS, -0.0886) * (float)22.6 * EARTH_RADIUS;
				}
			}
		}
		return false;
	}

	private void setTexture() {
		// Set the texture based on the radius/mass

		if (mass <= 0) {
			// Debug.LogError("Can't set the texture.");
		} else {
			if (mass < 0.05) {
				texture = "uranus";
			} else if (mass < 0.1) {
				texture = "neptune";
			} else if (mass < 0.65) {
				texture = "saturn";
			} else {
				texture = "jupiter";
			}
		}
	}

    /// <summary>
    /// Creates new planets group to be added to an orbit
    /// </summary>
    /// <param name="planets"></param>
    /// <param name="thesePlanets"></param>
    /// <param name="theseOrbits"></param>
    public void dealWithPlanets(float orbitXScale)
    {
        orbitXScale /= 1000000;
        float planetDistance = radiusOfOrbit /*/ 149600000.0F * 10.0F*/;
        float planetSize = radiusOfPlanet * 2.0F / 10000.0F;
        float planetSpeed = -1.0F / timeToOrbit;

        thisPlanetCenter = new GameObject(name + "Center");
        thisPlanetCenter.AddComponent<rotate>();
        thisPlanet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        thisPlanet.name = name;
        thisPlanet.tag = "Planet";
        //thisPlanet.transform.position = new Vector3(0, 0, planetDistance * orbitXScale);
        thisPlanet.AddComponent<ObjectSelection>();
        thisPlanetCenter.GetComponent<rotate>().rotateSpeed = planetSpeed;
        planetMaterial = Resources.Load(texture, typeof(Material)) as Material;
        thisPlanet.GetComponent<MeshRenderer>().material = planetMaterial;

        //changing
        thisPlanet.transform.localScale = new Vector3(planetSize, planetSize, planetSize);
        thisPlanetCenter.GetComponent<rotate>().SetRevolution(timeToOrbit);
        thisPlanet.transform.parent = thisPlanetCenter.transform;

        orbit = new Orbit(name + " orbit", radiusOfOrbit * orbitXScale, Color.white, radiusOfOrbit);
        orbit.orbit.transform.parent = thisPlanetCenter.transform;

        thisPlanet.transform.localPosition = orbit.orbit.GetComponent<LineRenderer>().GetPosition(0);
        UpdateRadius(CreateSystems.planetSizeChange);
        UpdateTimeToOrbit(CreateSystems.yearMultiplier);
        //UpdateOrbitRadius(CreateSystems.orbitSizeChange);
    }

    public void sideDealWithPlanets(float panelXScale, float panelWidth, float panelDepth)
    {
        float planetDistance = radiusOfOrbit / 149600000.0F * 10.0F;
        float planetSize = radiusOfPlanet * 1.0F / 10000.0F;
        string textureName = texture;
        string planetName = name;

        // limit the planets to the width of the side view
        if ((panelXScale * planetDistance) < panelWidth)
        {
            thisPlanet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            thisPlanet.name = planetName;
            thisPlanet.tag = "Planet";
            thisPlanet.transform.position = new Vector3(-0.5F * panelWidth + planetDistance * panelXScale, 0, 0);
            thisPlanet.AddComponent<ObjectSelection>();
            planetMaterial = Resources.Load(textureName, typeof(Material)) as Material;
            thisPlanet.GetComponent<MeshRenderer>().material = planetMaterial;

            //changing
            thisPlanet.transform.localScale = new Vector3(planetSize, planetSize, 5.0F * panelDepth);
            UpdateRadius(CreateSystems.planetSizeChange);
            //UpdateTimeToOrbit(CreateSystems.yearMultiplier);
            //UpdateOrbitRadius(CreateSystems.orbitSizeChange);
        }
        else
        {
            thisPlanet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            thisPlanet.name = planetName;
            thisPlanet.tag = "Planet";
            thisPlanet.transform.position = new Vector3(-0.5F * panelWidth + planetDistance * panelXScale, 0, 0);
            thisPlanet.AddComponent<ObjectSelection>();
            planetMaterial = Resources.Load(textureName, typeof(Material)) as Material;
            thisPlanet.GetComponent<MeshRenderer>().material = planetMaterial;

            //changing
            thisPlanet.transform.localScale =  Vector3.zero;
            UpdateRadius(CreateSystems.planetSizeChange);
            thisPlanet.SetActive(false);
            //UpdateTimeToOrbit(CreateSystems.yearMultiplier);
            //UpdateOrbitRadius(CreateSystems.orbitSizeChange);
        }
    }

    public void UpdateRadius(float radiusMultiplier)
    {
        //radiusOfPlanet = radiusOfPlanet * radiusMultiplier;
        float planetSize = radiusOfPlanet * 2.0F / 10000.0F;
        thisPlanet.transform.localScale = new Vector3(planetSize * radiusMultiplier, planetSize * radiusMultiplier, planetSize * radiusMultiplier);

    }
    public void UpdateTimeToOrbit(float timeToOrbitMultiplier)
    {
        //timeToOrbit = timeToOrbit * timeToOrbitMultiplier;
        float planetSpeed = -1.0F / timeToOrbit;
        thisPlanetCenter.GetComponent<rotate>().rotateSpeed = planetSpeed * timeToOrbitMultiplier;
    }
    public void UpdateOrbitRadius(float orbitRadiusMultiplier)
    {
        //radiusOfOrbit = radiusOfOrbit * orbitRadiusMultiplier;
        float planetDistance = radiusOfOrbit / 149600000.0F * 10.0F;
        Vector3 newPos = new Vector3(thisPlanet.transform.position.x * orbitRadiusMultiplier,
                                    thisPlanet.transform.position.y,
                                    thisPlanet.transform.position.z * orbitRadiusMultiplier);
        thisPlanet.transform.position = newPos;
        //TODO calculate new planet location
        orbit.updateRadius(orbitRadiusMultiplier);
    }
   
    public override string ToString()
	{
		return String.Format("OrbRad {0:e2} : PlanRad {1} : Mass {2} : Name {3} : Disc {4} : Orbit {5} : Tex {6}", radiusOfOrbit, radiusOfPlanet, mass, name, discovered, timeToOrbit, texture);
	}

    public void DestroyPlanet()
    {
       // if (orbit != null)
           // orbit.DestroyOrbit();
        Destroy(thisPlanet);
        Destroy(thisPlanetCenter);
        //Destroy(planetMaterial);

    }
}
