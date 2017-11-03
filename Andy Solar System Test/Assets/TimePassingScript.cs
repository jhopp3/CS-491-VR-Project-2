using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePassingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TimePasser(string pOrM)
    {
        if (pOrM == "plus")
        {
            CreateSystems.yearMultiplier *= 2;
        }
        else
        {
            CreateSystems.yearMultiplier /= 2;
        }
    }
}
