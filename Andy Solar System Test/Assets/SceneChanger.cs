using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ChangeScene(string level_)
	{
<<<<<<< Updated upstream
		Application.LoadLevel (level_);
=======
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(level_);
		//Application.LoadLevel (level_);
>>>>>>> Stashed changes
		//return true;
	}
}
