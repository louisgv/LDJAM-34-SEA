using UnityEngine;
using System.Collections;

public class FlowerSound : MonoBehaviour {

	public AudioSource mySource;

	public AudioClip[] sounds;

	// Use this for initialization
	void Start() {
		mySource = GetComponent<AudioSource> ();
	
	}

	void Flowergrown() {
		mySource.clip = sounds[0];
		mySource.Play();
	}
	
	// Update is called once per frame

}
