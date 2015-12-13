using UnityEngine;
using System.Collections;

public class seedSound : MonoBehaviour {

	public AudioSource mySource;

	public AudioClip[] sounds;

	// Use this for initialization
	void Start () {
		mySource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Seedcollide() {
		mySource.clip = sounds[0];
		mySource.Play();
	}
}
