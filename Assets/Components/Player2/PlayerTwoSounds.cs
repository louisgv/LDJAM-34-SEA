using UnityEngine;
using System.Collections;

public class PlayerTwoSounds : MonoBehaviour
{
	public AudioSource mySource;

	public AudioClip[] sounds;

	void Start ()
	{
		mySource = GetComponentInParent<AudioSource> ();
		
	}
	
}
