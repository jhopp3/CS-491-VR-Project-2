using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

	public float rotateSpeed = -5.0f;
	private float revolutionSpeed = 1.0f;
	public void SetRevolution(float revolutionSpeed)
	{
		this.revolutionSpeed = revolutionSpeed;
	}
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		transform.Rotate (Vector3.up, rotateSpeed* revolutionSpeed * Time.deltaTime);

	}
}
