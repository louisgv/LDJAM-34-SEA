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
	public AudioClip[] taunting;
	public AudioClip[] swingHit;
	public AudioClip[] swingMiss;
	public AudioClip[] footStepsGround;
	public AudioClip[] footStepsWater;
	public AudioClip[] footStepsWood;


	void Start ()
	{
		mySource = GetComponent<AudioSource> ();
	}


	public void SwingHit ()
	{
		
		mySource.clip = swingHit [0];
		mySource.Play ();
	
	}


	public void StartLaugh ()
	{
		int soundIndex = Random.Range (0, startingLaugh.Length);
		mySource.clip = startingLaugh [soundIndex];
		mySource.Play ();
	}

	public void SwingGrunt ()
	{
		int soundIndex = Random.Range (0, gruntSwingArray.Length);
		mySource.clip = gruntSwingArray [soundIndex];
		mySource.Play ();
	}

	public void GettingHit ()
	{
		int soundIndex = Random.Range (0, ouchArray.Length);
		mySource.clip = ouchArray [soundIndex];
		mySource.Play ();	
	}

	public void inActOfCut ()
	{
		int soundIndex = Random.Range (0, notBadCutting.Length);
		mySource.clip = ouchArray [soundIndex];
		mySource.Play ();
	}

	public void successfulCut ()
	{
		int soundIndex = Random.Range (0, onSuccess.Length);
		mySource.clip = onSuccess [soundIndex];
		mySource.Play ();	
	}

	public void tauntingYou ()
	{
		int soundIndex = Random.Range (0, taunting.Length);
		mySource.clip = taunting [soundIndex];
		mySource.Play ();
	}

	public void swingAndMiss ()
	{
		mySource.clip = swingMiss [0];
		mySource.Play ();
	}

	public void slingShot ()
	{
		//mySource.clip = slingNoise [0];
		mySource.Play ();
	}

	public void walkingGround ()
	{
		mySource.clip = footStepsGround [0];
		mySource.Play ();
	}


	public void walkingWater ()
	{
		mySource.clip = footStepsWater [0];
		mySource.Play ();
	}

	public void walkingWood ()
	{
		mySource.clip = footStepsWood [0];
		mySource.Play ();
	}

}
