using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		Application.LoadLevel(x);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
