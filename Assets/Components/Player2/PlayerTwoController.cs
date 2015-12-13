using UnityEngine;
using System.Collections;

public class PlayerTwoController : MonoBehaviour
{
	[Range(0.0f, 45.0f)]
	public float
		rotationSpeed = 27.0f;
	
	[Range(0.0f, 45.0f)]
	public float
		movementSpeed = 27.0f;

	private PlayerTwoEvents playerTwoEvents;

	public void Awake ()
	{
		playerTwoEvents = GetComponent<PlayerTwoEvents> ();
	}




	 
	void Start() 

		{
		
	}


	// Update is called once per frame
	void Update ()
	{
<<<<<<< HEAD
		
		Vector3 input = new Vector3 (
			0, 
			Input.GetAxis ("P2Horizontal"), 
			0);
		transform.Rotate (input * rotationSpeed * Time.smoothDeltaTime);
=======
		if (!playerTwoEvents.state.Equals (PlayerTwoEvents.PlayerState.STUNNED)) {
			Vector3 input = new Vector3 (0, Input.GetAxis ("P2.Horizontal"), 0);
			transform.Rotate (input * rotationSpeed * Time.smoothDeltaTime);
>>>>>>> 30ca28a89ac11e4d1c2014bcb5b8bb5a3ea5d487
		
			transform.position -= (
				transform.forward * 
				Input.GetAxis ("P2.Vertical") * 
				movementSpeed * 
				Time.smoothDeltaTime
				);
		}
//		transform.rotation = Quaternion.Euler (
//			transform.rotation.eulerAngles + 
//			input * Time.smoothDeltaTime * rotationSpeed);
//		
		
//		Quaternion target = Quaternion.Euler (transform.rotation.eulerAngles + input);
//		
//		transform.rotation = Quaternion.Slerp (
//			transform.rotation,
//			target,
//			 Time.deltaTime * rotationSpeed);		
		
		/*switch (myState) {
		case PlayerState.STUNNED:
			timer -= Time.deltaTime;

			if (timer < 0) {
				//timer
				myState = PlayerState.PLAYING;
				timer = 3.0f;
			}
			break;
		default:
			break;

		}*/
	}
}
