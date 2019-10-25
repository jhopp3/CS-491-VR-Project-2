using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuSceneButton : MonoBehaviour {
	public RectTransform rt;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void ShowPanel()
	{
        rt.gameObject.SetActive (!rt.gameObject.activeSelf);
	}
}
