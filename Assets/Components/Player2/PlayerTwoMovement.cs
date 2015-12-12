using UnityEngine;
using System.Collections;

public class PlayerTwoMovement : MonoBehaviour {

	[Range(1.0f, 9.0f)]

	public float speed = 9.0f;

	// Use this for initialization
	void Start () {

//for postion of player
//		transform.position = new Vector3(0, 0, 0 ); 
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right *  Input.GetAxis("Horizontal") * Time.smoothDeltaTime * speed);
		transform.Translate(Vector3.forward *  Input.GetAxis("Vertical") *  speed * Time.smoothDeltaTime);
			
	}
}
