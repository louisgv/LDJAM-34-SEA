using UnityEngine;
using System.Collections;

public class PlayerTwoSounds : MonoBehaviour
{
	public AudioSource mySource;

	public AudioClip[] sounds;

	void Start ()
	{

		//mySource is now equal to the component audioSource  
		mySource = GetComponent<AudioSource> ();
		
	}

	void Playstunt() {
		
		mySource.clip = sounds [0];
		mySource.Play();
	}


	void Playstart () {
		mySource.clip = sounds [1];
		mySource.Play(); 
	}
}
