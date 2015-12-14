using UnityEngine;
using System.Collections;

public class  P2TutorialCamera : MonoBehaviour {

	// Use this for initialization
	public GameObject P1; 
	public GameObject [] CloseToTree; 

	//playertwo
	public PlayerTwoEvents playerTwoEvents;   


	void Start () {
		transform.LookAt (P1.transform);

		playerTwoEvents = GetComponent<PlayerTwoEvents> ();
	}

	void Update () {

		if (playerTwoEvents.state.Equals (PlayerTwoEvents.PlayerState.NEAR_FLOWER)) {
			CloseToTree [0].SetActive (true);
		}

	}
	// Update is called once per frame

}
