using UnityEngine;
using System.Collections;

public class BridgeSounds : MonoBehaviour 
{
	public AudioSource mySource;

	public AudioClip[] sounds;

	// Use this for initialization
	void Start () {
		mySource = GetComponent<AudioSource> ();
	
	}

	void Bridgebuilt() {
		mySource.clip = sounds[0];
		mySource.Play();
	}
		

}
