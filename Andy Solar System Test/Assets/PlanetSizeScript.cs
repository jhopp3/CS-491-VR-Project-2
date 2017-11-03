using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSizeScript : MonoBehaviour {

    CreateSystems systems;

	// Use this for initialization
	void Start () {
        systems = GameObject.Find("Scene Generator").GetComponent<CreateSystems>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangePlanetSize(string PorM)
    {
        if (PorM == "plus")
        {
          
            CreateSystems.planetSizeChange *= 2;
        }
        else
        {
            CreateSystems.planetSizeChange /= 2;
        }

    }
}
