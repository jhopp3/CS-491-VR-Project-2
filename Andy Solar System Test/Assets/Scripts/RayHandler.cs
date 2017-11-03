using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RayHandler : MonoBehaviour
{
    public CreateSystems systems;
    bool isTriggered = false;
    // Use this for initialization
    public static string selectedSys;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaySys();

    }

    public void RaySys()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 250, Color.red, 0.5f);

        if (SceneManager.GetActiveScene().name == "EnterScene")
        {
<<<<<<< Updated upstream
            if (hit.transform.tag == "UIElement")
            {
                Debug.Log("Handler");
                //hit.transform.GetComponent<Button>().Select();
                hit.transform.GetComponent<Button>().onClick.Invoke();
            }
            else if (hit.transform.tag == "Alien")
=======
            if (Physics.Raycast(transform.position, transform.forward, out hit, 250))
>>>>>>> Stashed changes
            {
                if (hit.transform.tag == "Alien")
                {
                    if (!hit.transform.GetComponent<AudioSource>().isPlaying)
                    {
                        hit.transform.GetComponent<AudioSource>().Play();
                    }
                }
                else if (hit.transform.tag == "UIElement")
                {
                    hit.transform.GetComponent<Button>().Select();
                    if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)) && !isTriggered)
                    {
                        hit.transform.GetComponent<Button>().onClick.Invoke();
                        isTriggered = true;
                    }
                }
            }
<<<<<<< Updated upstream
=======
        }

        else
        {
            if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)) && !isTriggered)
            {
                
                if (Physics.Raycast(transform.position, transform.forward, out hit, 250))
                {
                    if (hit.transform.tag == "UIElement")
                    {
                        hit.transform.GetComponent<Button>().Select();
                        hit.transform.GetComponent<Button>().onClick.Invoke();
                        isTriggered = true;
                    }
                    else if (hit.transform.tag == "Planet")
                    {

                        string name = hit.transform.GetComponent<MeshRenderer>().material.ToString();
                        string[] arr = name.Split(' ');
                        if (hit.transform.GetComponent<ObjectSelection>().isSelected)
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
                            selectedSys = "Sol";
                            Scene scene = SceneManager.GetActiveScene();
                            if (scene.name == "3DView")
                            {

                            }
                            else
                            {
                                //  SceneChangerGoto.ChangeScenetoLoc(selectedSys);
                            }
                            Debug.Log("Click Planet");
                        }
                        SaveManager.MoveTo3DScene();

                        isTriggered = true;

                    }
                    else if (hit.transform.tag == "Star")
                    {
                        Debug.Log("Click star");
                        isTriggered = true;
                    }
                    else if (hit.transform.tag == "Panel")
                    {
                        Debug.Log("Click Panel");
                        isTriggered = true;
                    }
                    else if (hit.transform.tag == "LoadMore")
                    {
                        systems.LoadNextPage();
                        Debug.Log("loaded next");
                        isTriggered = true;
                    }
                    else if (hit.transform.tag == "LoadLess")
                    {
                        systems.LoadpreviousPage(); ;
                        Debug.Log("loaded back");
                        isTriggered = true;
                    }
                }
            }

            else
            {
                isTriggered = false;
            }
>>>>>>> Stashed changes
        }
    }
}



