 using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerTwoEvents : MonoBehaviour
{
	public Transform actionIndicator;
	
	private Animator anim;
	
	private SpringJoint sjoint;
	
	public float timer = 3.0f;
	
	public enum PlayerState
	{
		PLAYING,
		NEAR_FLOWER,
		SWING_AXE,
		DRAGGING,
		STUNNED,
		DEAD
	}
	
	public void Awake ()
	{
		anim = GetComponent<Animator> ();
		
		sjoint = GetComponent<SpringJoint> ();
	}
	
	//reffering to our enum PlayState value Playing
	//type of myState is PlayerState.playing
	public PlayerState state = PlayerState.PLAYING;


	// Use this for initialization
	
	IEnumerator BeStunned ()
	{
		state = PlayerState.STUNNED;
		
		for (int i =0; i < 3; ++i) {
			GamePad.SetVibration (0, 1, 1);
			yield return new WaitForSeconds (0.5f);
		
			GamePad.SetVibration (0, 0, 0);
			yield return new WaitForSeconds (0.3f);
		}
		state = PlayerState.PLAYING;
	}
	
	private GameObject nearbyFlower;
	
	void OnTriggerEnter (Collider collider)
	{
		if (collider.CompareTag ("Seed")) {
			//here goes the co-routine
			StartCoroutine (BeStunned ());
			Destroy (collider.gameObject);
		}
		if (collider.CompareTag ("Flower")) {
			// SHOW ACTION INDICATOR
			state = PlayerState.NEAR_FLOWER;
			nearbyFlower = collider.gameObject;
		}
	}
	
	void OnTriggerExit (Collider collider)
	{
		if (collider.CompareTag ("Flower")) {
			// SHOW ACTION INDICATOR
			state = PlayerState.PLAYING;
		}
	}
	
	void ShowIndicator ()
	{
		if (state.Equals (PlayerState.NEAR_FLOWER)) {
			actionIndicator.localPosition = Vector3.Lerp (
				actionIndicator.localPosition, 
				Vector3.up * 2.0f - Vector3.forward * 3.0f,
				Time.deltaTime * 10.0f);
		} else {
			if (Mathf.Floor (actionIndicator.position.magnitude) == 0) {
				return;
			}
			actionIndicator.localPosition = Vector3.Lerp (
				actionIndicator.localPosition, 
				Vector3.zero,
				Time.deltaTime * 10.0f);
		}
	}
	
	IEnumerator SwingAxe ()
	{
		anim.SetTrigger ("SwingAxe");
		yield return new WaitForSeconds (0.6f);
		GamePad.SetVibration (0, 1, 1);
		yield return new WaitForSeconds (1.0f);
		if (nearbyFlower != null) {
		
			nearbyFlower.GetComponent<Flower> ().BeChoppedDead ();
			
			
		}
		state = PlayerState.PLAYING;
	}
		
	void Update ()
	{
		ShowIndicator ();
		
		switch (state) {
		case PlayerState.STUNNED:
			break;
		case PlayerState.NEAR_FLOWER:
			if (Input.GetButtonDown ("P2.Fire")) {
				// Vibrate here is fine. We will do it later on with an axe prefab
				//	GamePad.SetVibration (0, 1, 1);
				StartCoroutine (SwingAxe ());
			}
			break;
		case PlayerState.DRAGGING:
			GamePad.SetVibration (0, Input.GetAxis ("P2.Vertical"), Input.GetAxis ("P2.Vertical"));
			break;
		case PlayerState.PLAYING:
			//GamePad.SetVibration (0, 0, 0);
			break;
		default:
			
			return;
		}
	}	
}

