using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextColorSelect : MonoBehaviour {
	public int x;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = Color.white;
	}
	void OnMouseEnter()
	{
		GetComponent<Renderer>().material.color = Color.gray;
	}
	void OnMouseExit()
	{
		GetComponent<Renderer>().material.color = Color.white;
	}
	void OnMouseUp()
	{
        SceneManager.LoadScene(x);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
