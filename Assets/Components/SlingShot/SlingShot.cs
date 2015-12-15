using UnityEngine;
using System.Collections;

public class SlingShot : MonoBehaviour
{
	public float chargeLevel = 0;
	public float chargeSpeed = 1.0f;


	private SlingShotSound slingShotSound;

	public void Awake() {
		
		slingShotSound = GetComponent<SlingShotSound> ();
	}


	[Range(3.6f, 9.0f)]
	public float
		CHARGE_MAX = 3.6f;

	public enum SlingState
	{
		RELEASED,
		CHARGING
	}
	
	public SlingState state = SlingState.RELEASED;
	
	void Shoot ()
	{
		slingShotSound.slingShot ();
	}
	
	protected void Update ()
	{
		switch (state) {
		case SlingState.RELEASED:
			if (chargeLevel > 0) {
				Shoot ();
			}
			chargeLevel = 0;
			break;
		case SlingState.CHARGING:
			chargeLevel += Time.smoothDeltaTime * chargeSpeed;
			break;
		default:
			return;
		}
	}
		
}
