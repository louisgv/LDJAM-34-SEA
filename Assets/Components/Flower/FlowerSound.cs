using UnityEngine;
using System.Collections;

public class FlowerSound : MonoBehaviour {

	public AudioSource mySource;
	public AudioClip[] flowerGrows;
	public AudioClip[] footStepsGround;
	public AudioClip[] footStepsWater;
	public AudioClip[] footStepsWood;


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

	public void walkingGround() 
	{
		mySource.clip = footStepsGround [0];
		mySource.Play ();
	}


	public void walkingWater() 
	{
		mySource.clip = footStepsWater [0];
		mySource.Play ();
	}

	public void walkingWood() 
	{
		mySource.clip = footStepsWood [0];
		mySource.Play ();
	}
}
