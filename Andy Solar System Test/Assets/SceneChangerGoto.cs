using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerGoto : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public static void ChangeScenetoLoc( string Loc)
    {
      //  Scene sceneToLoad = SceneManager.GetSceneByName("3DView");
        SceneManager.LoadScene("3DView", LoadSceneMode.Additive);
    }
}

