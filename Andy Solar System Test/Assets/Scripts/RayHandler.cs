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
        Debug.DrawRay(transform.position, transform.forward *250, Color.red, 0.5f) ;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 250))
        {
            if (hit.transform.tag == "UIElement")
            {
                Debug.Log("Handler");
                //hit.transform.GetComponent<Button>().Select();
                hit.transform.GetComponent<Button>().onClick.Invoke();
            }
            else if (hit.transform.tag == "Alien")
            {
                if (!hit.transform.GetComponent<AudioSource>().isPlaying)
                {
                    hit.transform.GetComponent<AudioSource>().Play();
                }
            }
        }
    }
}
