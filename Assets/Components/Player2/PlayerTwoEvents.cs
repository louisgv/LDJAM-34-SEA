using UnityEngine;
using System.Collections;

public class PlayerTwoEvents : MonoBehaviour
{
	public Transform actionIndicator;
	
	public float timer = 3.0f;
	
	public enum PlayerState
	{
		PLAYING,
		NEARFLOWER,
		STUNNED,
		DEAD
	}
	
	//reffering to our enum PlayState value Playing
	//type of myState is PlayerState.playing
	public PlayerState myState = PlayerState.PLAYING;
	
	// Use this for initialization
	
	IEnumerator BeStunned ()
	{
		myState = PlayerState.STUNNED;
		
		yield return new WaitForSeconds (3.0f);
		
		myState = PlayerState.PLAYING;
	}
	
	private FlowerMKI nearByFlower;
	
	void OnTriggerEnter (Collider collider)
	{
		if (collider.CompareTag ("Seed")) {
			//here goes the co-routine
			StartCoroutine (BeStunned ());
		}
		if (collider.CompareTag ("Flower")) {
			// SHOW ACTION INDICATOR
			myState = PlayerState.NEARFLOWER;
			nearByFlower = collider.GetComponent<FlowerMKI> ();
		}
	}
	
	void OnTriggerExit (Collider collider)
	{
		if (collider.CompareTag ("Flower")) {
			// SHOW ACTION INDICATOR
			myState = PlayerState.PLAYING;
		}
	}
	
	void ShowIndicator ()
	{
		if (myState.Equals (PlayerState.NEARFLOWER)) {
			actionIndicator.localPosition = Vector3.up * 2.0f;
		} else {
			actionIndicator.localPosition = Vector3.zero;
		}
	}
	
	void Update ()
	{
		ShowIndicator ();
		switch (myState) {
		case PlayerState.NEARFLOWER:
			if (Input.GetButtonDown ("P2Fire")) {
				nearByFlower.BeChoppedDead ();
				myState = PlayerState.PLAYING;
			}
			break;
		default:
			
			return;
		}
	}
	
}
