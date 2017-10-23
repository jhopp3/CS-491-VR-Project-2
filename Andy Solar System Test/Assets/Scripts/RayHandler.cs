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
                hit.transform.GetComponent<Button>().Select();
                hit.transform.GetComponent<Button>().onClick.Invoke();
            }
            else if (hit.transform.tag == "Alien")
            {
                if (!hit.transform.GetComponent<AudioSource>().isPlaying)
                {
                    hit.transform.GetComponent<AudioSource>().Play();
                }
            }
            else if (hit.transform.tag == "Planet")
            {
                
                if (Input.GetAxis("Submit") > 0.4f)
                {
                    string name = hit.transform.GetComponent<MeshRenderer>().material.ToString();
                    string[] arr = name.Split(' ');
                    int tester = arr[0].Length;
                    Debug.Log(tester);
                    if (tester > 9)
                    {
                        string edited = arr[0].Substring(0, arr[0].Length - 6);
                        Debug.Log(edited);
                        Material planetMaterial = Resources.Load(edited, typeof(Material)) as Material;
                        hit.transform.GetComponent<MeshRenderer>().material = planetMaterial;
                        hit.transform.GetComponent<ObjectSelection>().isSelected = false;

                    }
                    else
                    {
                        Material planetMaterial = Resources.Load(arr[0] + "Shaded", typeof(Material)) as Material;
                        hit.transform.GetComponent<MeshRenderer>().material = planetMaterial;
                        hit.transform.GetComponent<ObjectSelection>().isSelected = true;
                    }
                    Debug.Log("Click Planet");
                }
            }
            else if (hit.transform.tag == "Star")
            {
                if (Input.GetAxis("Submit") > 0.4f)
                {
                    Debug.Log("Click star");
                }
            }
            else if (hit.transform.tag == "Panel")
            {
               if (Input.GetAxis("Submit") > 0.4f )
                {
                   /* do {
                        hit.transform.GetComponent<Transform>().localPosition = hit.point;
                    } while (Input.GetAxis("Submit") > 0.4f);*/
                    Debug.Log("Click Panel");
                }
            }
        }
    }
}
