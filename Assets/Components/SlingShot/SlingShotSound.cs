using UnityEngine;
using System.Collections;

public class SlingShotSound : MonoBehaviour {


	public AudioClip[] slingNoise;
	public AudioSource mySource;

	void Start () {
		mySource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void slingShot() 
	{
		mySource.clip = slingNoise [0];
		mySource.Play ();
	}
}
