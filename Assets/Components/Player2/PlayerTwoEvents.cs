using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class PlayerTwoEvents : MonoBehaviour
{
	public Transform actionIndicator;
	
	private Animator anim;

	public Queue<GameObject> joints;
	
	public int MAX_JOINT = 3;
	
	public float timer = 3.0f;



	public enum PlayerState

	{
		PLAYING,
		NEAR_FLOWER,
		NEAR_RAMP,
		SWING_AXE,
		DRAGGING,
		STUNNED,
		DEAD
	}

	private PlayerTwoSounds playertwosounds;

	public void Awake ()

	{
	
		playertwosounds = GetComponent<PlayerTwoSounds> ();

		anim = GetComponent<Animator> ();
		
		joints = new Queue<GameObject> ();

	}
	
	//reffering to our enum PlayState value Playing
	//type of myState is PlayerState.playing
	public PlayerState state = PlayerState.PLAYING;


	// Use this for initialization
	
	IEnumerator BeStunned ()
	{
		state = PlayerState.STUNNED;
		
		for (int i = 0; i < 3; ++i) {
			GamePad.SetVibration (0, 1, 1);
			yield return new WaitForSeconds (0.5f);
			playertwosounds.GettingHit();

			GamePad.SetVibration (0, 0, 0);
			yield return new WaitForSeconds (0.3f);
		}
		// respawn of player beginning to start playing again. And then laugh
		//player will start playing with sound
		playertwosounds.tauntingYou();
		yield return new WaitForSeconds (0.3f);
		state = PlayerState.PLAYING;
	}

	private Flower nearbyFlower;
	private BridgeMKII nearbyRamp;

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
			nearbyFlower = collider.gameObject.GetComponent<Flower> ();
		}
		if (collider.CompareTag ("Ramp")) {
			state = PlayerState.NEAR_RAMP;
			nearbyRamp = collider.gameObject.GetComponent<BridgeMKII> ();
		}
	}

	void OnTriggerExit (Collider collider)
	{
		if (collider.CompareTag ("Flower")) {
			nearbyFlower = null;
			state = PlayerState.PLAYING;
		}
		if (collider.CompareTag ("Ramp")) {
			nearbyRamp = null;
			state = PlayerState.PLAYING;
		}
	}

	IEnumerator SwingAxe ()
	{
		if (!nearbyFlower.state.Equals (Flower.FlowerState.CHOPPED) &&
		    !nearbyFlower.state.Equals (Flower.FlowerState.INVICIBLE) &&
		    Input.GetButtonDown ("P2.Fire")) {
			
			anim.SetTrigger ("SwingAxe");
			yield return new WaitForSeconds (0.6f);
			GamePad.SetVibration (0, 1, 1);
	
			if (nearbyFlower != null && joints.Count <= MAX_JOINT) {
				GameObject joint = new GameObject ("Joint " + joints.Count.ToString (), typeof(SpringJoint));
			
				joint.transform.SetParent (transform);
			
				joint.transform.localPosition = Vector3.zero;
			
				joints.Enqueue (joint);
			
				joint.GetComponent<SpringJoint> ().connectedBody = nearbyFlower.GetComponent<Rigidbody> ();
			
				joint.GetComponent<Rigidbody> ().isKinematic = true;
			
				nearbyFlower.BeChopped ();
			}
			yield return new WaitForSeconds (1.0f);
			state = PlayerState.PLAYING;
		} else {
			yield return new WaitForFixedUpdate ();
		}
	}

	public int playerIndex = 2;

	void Update ()
	{		
		switch (state) {
		case PlayerState.NEAR_RAMP:
			if (joints.Count > 0) {
				anim.SetBool ("BuildBridge", true);
				if (Input.GetButtonDown ("P2.Fire")) {
					nearbyRamp.BuildBridge ();
					GameObject joint = joints.Dequeue ();
					Destroy (joint.GetComponent<SpringJoint> ().connectedBody.gameObject);
					Destroy (joint);
					anim.SetBool ("BuildBridge", false);
				}
			}
			break;
		case PlayerState.STUNNED:
			break;
		case PlayerState.NEAR_FLOWER:
				// Vibrate here is fine. We will do it later on with an axe prefab
				//	GamePad.SetVibration (0, 1, 1);
			StartCoroutine (SwingAxe ());
			
			if (!nearbyFlower.state.Equals (Flower.FlowerState.CHOPPED) ||
			    !nearbyFlower.state.Equals (Flower.FlowerState.INVICIBLE)) {
				anim.SetBool ("NearFlower", true);
			}
			break;
		case PlayerState.DRAGGING:
			GamePad.SetVibration (
				0, Input.GetAxis ("P" + playerIndex.ToString () + ".Vertical"),
				Input.GetAxis ("P" + playerIndex.ToString () + ".Vertical")
			);
			
			break;
		case PlayerState.PLAYING:
		
			nearbyFlower = null;
			GamePad.SetVibration (0, 0, 0);
			anim.SetBool ("BuildBridge", false);
			anim.SetBool ("NearFlower", false);
			break;
		default:
			return;
		}
	}
}

