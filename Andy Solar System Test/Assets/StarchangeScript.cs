using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarchangeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ChangeStarSize(string PorM)
    {
        if (PorM == "plus")
        {

            CreateSystems.StarSizeChange *= 2;
        }
        else
        {
            CreateSystems.StarSizeChange /= 2;
        }

    }
}
