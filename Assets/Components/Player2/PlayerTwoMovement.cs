using UnityEngine;
using System.Collections;

public class PlayerTwoMovement : MonoBehaviour {

	[Range(1.0f, 9.0f)]

	public float movementSpeed = 9.0f;


	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right *  Input.GetAxis("Movement.Horizontal") * movementSpeed * Time.smoothDeltaTime);
		transform.Translate(Vector3.forward *  Input.GetAxis("Movement.Vertical") *  movementSpeed * Time.smoothDeltaTime);
	}
}


