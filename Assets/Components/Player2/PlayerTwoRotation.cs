using UnityEngine;
using System.Collections;

public class PlayerTwoRotation : MonoBehaviour {

	[Range(1.0f, 9.0f)]
	public float rotationSpeed = 9.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (
			-Input.GetAxis ("Rotation.Vertical"),
			Input.GetAxis ("Rotation.Horizontal"),
			0));
	}
}
