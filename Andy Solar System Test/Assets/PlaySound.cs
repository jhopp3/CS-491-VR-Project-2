using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

	public AudioSource audio;

	void Start () {
		audio = GetComponent<AudioSource>();
	}


	private void OnMouseEnter(Collider other)
	{
		if(!audio.isPlaying)
			audio.Play();
	}
}
