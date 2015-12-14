using UnityEngine;
using System.Collections;

public class PlayerTwoSounds : MonoBehaviour
{
	public AudioSource mySource;
	public AudioClip[] startingLaugh;
	public AudioClip[] gruntSwingArray;
	public AudioClip[] ouchArray;
	public AudioClip[] notBadCutting;
	public AudioClip[] onSuccess;

	void Start ()
	{
		mySource = GetComponent<AudioSource> ();
	}

	public void Startlaugh() 
	{
		int soundIndex = Random.Range (0, startingLaugh.Length);
		mySource.clip = startingLaugh[soundIndex];
		mySource.Play();
	}

	public void Swinggrunt() 
	{
		int soundIndex = Random.Range (0, gruntSwingArray.Length);
		mySource.clip = gruntSwingArray[soundIndex];
		mySource.Play();
	}

	public void GettingHit() 
	{
		int soundIndex = Random.Range (0, ouchArray.Length);
		mySource.clip = ouchArray[soundIndex];
		mySource.Play();	
	}

	public void inActOfCut () 
	{
		int soundIndex = Random.Range (0, notBadCutting.Length);
		mySource.clip = ouchArray[soundIndex];
		mySource.Play();	
				
	}

	public	void successfulCut () 
	{
		int soundIndex = Random.Range (0, onSuccess.Length);
		mySource.clip = onSuccess[soundIndex];
		mySource.Play();	
	}
}
