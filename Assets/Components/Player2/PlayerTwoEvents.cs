using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerTwoEvents : MonoBehaviour
{
	public Transform actionIndicator;
	
	public float timer = 3.0f;
	
	public enum PlayerState
	{
		PLAYING,
		NEAR_FLOWER,
		DRAGGING,
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
	
	private GameObject nearbyFlower;
	
	void OnTriggerEnter (Collider collider)
	{
		if (collider.CompareTag ("Seed")) {
			//here goes the co-routine
			StartCoroutine (BeStunned ());
		}
		if (collider.CompareTag ("Flower")) {
			// SHOW ACTION INDICATOR
			myState = PlayerState.NEAR_FLOWER;
			nearbyFlower = collider.gameObject;
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
		if (myState.Equals (PlayerState.NEAR_FLOWER)) {
			actionIndicator.localPosition = Vector3.up * 2.0f - Vector3.forward * 3.0f;
		} else {
			actionIndicator.localPosition = Vector3.zero;
		}
	}
	
	void Update ()
	{
		ShowIndicator ();
		switch (myState) {
		case PlayerState.NEAR_FLOWER:
			if (Input.GetButtonDown ("P2.Fire")) {
				//Vibrate here is fine. We will do it later on with an axe prefab
				GamePad.SetVibration (0, 1, 1);
				nearbyFlower.GetComponent<Flower> ().BeChoppedDead ();
				StartCoroutine (CoolDown (0.5f));
			}
			break;
		case PlayerState.DRAGGING:
			GamePad.SetVibration (0, Input.GetAxis ("P2Vertical"), Input.GetAxis ("P2Vertical"));
			break;
		case PlayerState.PLAYING:
			GamePad.SetVibration (0, 0, 0);
			break;
		default:
			
			return;
		}
	}
	
	IEnumerator CoolDown (float seconds)
	{
		yield return new WaitForSeconds (seconds);
		myState = PlayerState.PLAYING;
	}
	
}

