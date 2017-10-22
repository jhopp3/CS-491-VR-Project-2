using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        


    }
	
	// Update is called once per frame
	void Update () {
        RaySys();

    }

    public void RaySys()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward *20, Color.red, 0.5f) ;
        //Debug.DrawRay(transform.position, new Vector3(0,-12,12) * 20, Color.red, 0.5f);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 20))
        {
            if (hit.transform.tag == "UIElement")
            {
                hit.transform.GetComponent<Button>().onClick.Invoke();
                hit.transform.GetComponent<Button>().Select();
                //hit.transform.GetComponent<Button>().
                print("horray");
                print(hit.transform.name);
            }
        }
    }
}
