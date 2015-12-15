using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{

	public GameObject gameManager;
//	public GameObject soundManager;


	public AudioSource mySource;
	public AudioClip[] gameMusic;


	void Start ()
	{
		mySource = GetComponent<AudioSource> ();
	}


	public void GameMusic ()
	{
		mySource.clip = gameMusic[0];
		mySource.Play ();
	}

	void Update () {

		GameMusic ();


	}

	void Awake ()
	{
		if (GameManager.instance == null) {
			Instantiate (gameManager);
		}
	}
}
