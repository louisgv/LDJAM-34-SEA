using UnityEngine;
using System.Collections;

public class SlingShotMKI : SlingShot
{
	public GameObject seedPrefab;
	
	private GameObject seedInstance;

	new void Update ()
	{
		base.Update ();
		
		if (Input.GetButton ("P1.Charge")) {
			state = SlingState.CHARGING;
			if (seedInstance == null) {
				seedInstance = Instantiate (seedPrefab, transform.position, transform.rotation) as GameObject;
				seedInstance.GetComponent<Rigidbody> ().isKinematic = true;
			} else if (chargeLevel < CHARGE_MAX) {
				seedInstance.transform.localPosition -= transform.forward * Time.smoothDeltaTime;
			}
		}
		if (Input.GetButtonUp ("P1.Charge")) {
			state = SlingState.RELEASED;
			if (seedInstance != null) {
				Rigidbody sRigidbody = seedInstance.GetComponent<Rigidbody> ();
				sRigidbody.isKinematic = false;
				
				sRigidbody.AddForce ((transform.position - seedInstance.transform.position) * 500);
				
				seedInstance = null;
				// FIRE the Seed
			}
		}
	}
}
