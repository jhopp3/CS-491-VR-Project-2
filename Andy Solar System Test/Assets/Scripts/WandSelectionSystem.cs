using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WandSelectionSystem : MonoBehaviour {

    LineRenderer lr;
    public Vector3 target;
    public Vector3 root;
    

    // Use this for initialization
    void Start () {
        lr = GetComponent<LineRenderer>();
        lr.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3[] poss = new Vector3[2];
        poss[0] = root;
        poss[1] = target;
        lr.SetPositions(poss);
        if (Input.GetAxis("Fire1")>0.4f)
        {
            print("click");
        }
        






	}

   
}
