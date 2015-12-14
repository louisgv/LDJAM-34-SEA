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

	public void Awake ()
	{
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
		
			GamePad.SetVibration (0, 0, 0);
			yield return new WaitForSeconds (0.3f);
		}
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
				}
			}
			break;
		case PlayerState.STUNNED:
			break;
		case PlayerState.NEAR_FLOWER:
			if (!nearbyFlower.state.Equals (Flower.FlowerState.CHOPPED) && Input.GetButtonDown ("P2.Fire")) {
				// Vibrate here is fine. We will do it later on with an axe prefab
				//	GamePad.SetVibration (0, 1, 1);
				StartCoroutine (SwingAxe ());
			}
			if (!nearbyFlower.state.Equals (Flower.FlowerState.CHOPPED)) {
				anim.SetBool ("NearFlower", true);
			}
			break;
		case PlayerState.DRAGGING:
			GamePad.SetVibration (0, Input.GetAxis ("P" + playerIndex.ToString () + ".Vertical"), Input.GetAxis ("P" + playerIndex.ToString () + ".Vertical"));
			
			break;
		case PlayerState.PLAYING:
			GamePad.SetVibration (0, 0, 0);
			
			anim.SetBool ("NearFlower", false);
			break;
		default:
			return;
		}
	}
}

