using UnityEngine;
using System.Collections;

public class FlowerSound : MonoBehaviour {

	public AudioSource mySource;
	public AudioClip[] flowerGrows;


	// Use this for initialization
	void Start() 
	{
		mySource = GetComponent<AudioSource> ();
	
	}


	public void blossom ()
	{
		int soundIndex = Random.Range (0, flowerGrows.Length);
		mySource.clip = flowerGrows [soundIndex];
		mySource.Play ();
	}

}
