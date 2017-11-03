using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSizeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangeOrbitSize(string PorM)
    {
        if (PorM == "plus")
        {
            CreateSystems.orbitSizeChange *= 2;
        }
        else
        {
            CreateSystems.orbitSizeChange /= 2;
        }
    }
}
