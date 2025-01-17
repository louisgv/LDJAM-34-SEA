﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class PlayerTwoEvents : MonoBehaviour
{
	public Transform actionIndicator;
	
	private Animator anim;

	public Queue<GameObject> joints;

	//public JointSpring joint;

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

	private PlayerTwoSounds playerTwoSounds;

	public void Awake ()
	{
		playerTwoSounds = GetComponent<PlayerTwoSounds> ();

		anim = GetComponent<Animator> ();

		//joint = GetComponent<SpringJoint> ();
		joints = new Queue<GameObject> ();

	}
	
	//reffering to our enum PlayState value Playing
	//type of myState is PlayerState.playing
	public PlayerState state = PlayerState.PLAYING;

	public float explodePower = 50f;
	// Use this for initialization
	
	IEnumerator BeStunned ()
	{
		state = PlayerState.STUNNED;
		
		for (int i = 0; i < 3; ++i) {
			GamePad.SetVibration (0, 1, 1);
			yield return new WaitForSeconds (0.5f);

			playerTwoSounds.GettingHit ();
			//GetComponent<Rigidbody> ().AddForce (transform.up * explodePower);
			GamePad.SetVibration (0, 0, 0);
			yield return new WaitForSeconds (0.3f);
		}
		// respawn of player beginning to start playing again. And then laugh
		//player will start playing with sound
		playerTwoSounds.tauntingYou ();

		yield return new WaitForSeconds (0.3f);
		state = PlayerState.PLAYING;
	}

	private Flower nearbyFlower;
	private BridgeMKII nearbyRamp;

	void OnTriggerEnter (Collider collider)
	{
		if (collider.CompareTag ("Seed")) {
			//here goes the co-routine
			//playerTwoSounds.
			StartCoroutine (BeStunned ());
			Destroy (collider.gameObject);
		}
		if (collider.CompareTag ("Flower")) {
			// SHOW ACTION INDICATOR
			state = PlayerState.NEAR_FLOWER;
			nearbyFlower = collider.gameObject.GetComponent<Flower> ();
		}
		if (collider.CompareTag ("Ramp")) {
			playerTwoSounds.successfulCut ();
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

			//playerTwoSounds.
			anim.SetTrigger ("SwingAxe");
			yield return new WaitForSeconds (0.6f);
			GamePad.SetVibration (0, 1, 1);
	
			if (nearbyFlower != null &&
			    joints.Count <= MAX_JOINT) {
				GameObject joint = new GameObject ("Joint " + joints.Count.ToString (), typeof(SpringJoint));

				joint.transform.SetParent (transform);

				joint.GetComponent<Rigidbody> ().isKinematic = true;

				joint.transform.localPosition = Vector3.zero;

				joint.GetComponent<SpringJoint> ().connectedBody = nearbyFlower.GetComponent<Rigidbody> ();

				joints.Enqueue (joint);

				nearbyFlower.BeChopped ();

				state = PlayerState.DRAGGING;
			} 
			//nearbyFlower = null;
				
			yield return new WaitForSeconds (1.0f);
			if (state != PlayerState.DRAGGING) {
				state = PlayerState.PLAYING;
			} else {
				GamePad.SetVibration (0, 0, 0);
			}
		} else {
			yield return new WaitForFixedUpdate ();
		}
	}

	public int playerIndex = 2;

	void Update ()
	{		
		switch (state) {
		case PlayerState.NEAR_RAMP:
			if (joints.Count > 0 && nearbyRamp.state.Equals (Bridge.BridgeState.UNFINISHED)) {
				anim.SetBool ("BuildBridge", true);
				if (Input.GetButtonDown ("P2.Fire")) {
					nearbyRamp.BuildBridge ();
					GameObject joint = joints.Dequeue ();
					Destroy (joint.GetComponent<SpringJoint> ().connectedBody.gameObject);
					Destroy (joint);

					if (joints.Count == 0) {
						state = PlayerState.PLAYING;
					}

					anim.SetBool ("BuildBridge", false);
				}
			}
			break;
		case PlayerState.STUNNED:
			if (joints.Count > 0) {
				GameObject joint = joints.Dequeue ();
				Destroy (joint.GetComponent<SpringJoint> ().connectedBody.gameObject);
				Destroy (joint);
			}
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

