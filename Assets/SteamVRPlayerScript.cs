using UnityEngine;
using System.Collections;
using Valve.VR;
using System.Collections.Generic;

public class SteamVRPlayerScript : MonoBehaviour
{

	public SteamVR_TrackedObject controllerLeftSlingshot;
	public SteamVR_TrackedObject controllerRightLoader;

	public Vector3 nudgeVector;

	EVRButtonId[] buttonIds = new EVRButtonId[] {
		EVRButtonId.k_EButton_ApplicationMenu,
		EVRButtonId.k_EButton_Grip,
		EVRButtonId.k_EButton_SteamVR_Touchpad,
		EVRButtonId.k_EButton_SteamVR_Trigger
	};

	EVRButtonId[] axisIds = new EVRButtonId[] {
		EVRButtonId.k_EButton_SteamVR_Touchpad,
		EVRButtonId.k_EButton_SteamVR_Trigger
	};

	List<int> controllerIndices = new List<int> ();
	GameObject newSeed;

	public GameObject seedPrefab;
	public Vector3 seedPosition;

	private void OnDeviceConnected (params object[] args)
	{
		var index = (int)args [0];

		var vr = SteamVR.instance;
		if (vr.hmd.GetTrackedDeviceClass ((uint)index) != ETrackedDeviceClass.Controller)
			return;

		var connected = (bool)args [1];
		if (connected) {
			Debug.Log (string.Format ("Controller {0} connected.", index));
			//PrintControllerStatus(index);
			controllerIndices.Add (index);
		} else {
			Debug.Log (string.Format ("Controller {0} disconnected.", index));
			//PrintControllerStatus(index);
			controllerIndices.Remove (index);
		}
	}

	void OnEnable ()
	{
		SteamVR_Utils.Event.Listen ("device_connected", OnDeviceConnected);
	}

	void OnDisable ()
	{
		SteamVR_Utils.Event.Remove ("device_connected", OnDeviceConnected);
	}

	public Material rubberMaterial;

	// Use this for initialization
	void Start ()
	{
		// TODO: REPLACE THIS WITH PUBLIC UNITY OBJECT
		LineRenderer rubber = gameObject.AddComponent<LineRenderer> ();

		rubber.material = rubberMaterial;

		rubber.SetWidth (0.009F, 0.009F);

		//rubber.SetColors (Color.white, Color.white);

		rubber.SetVertexCount (3);

		rubber.SetPosition (0, slingPoint1.position);
		rubber.SetPosition (1, slingPoint2.position);
		rubber.SetPosition (2, slingPoint2.position);
	}

	public Transform point, pointer;

	public Transform slingPoint1, slingPoint2;

	[Range (2500f, 9000f)]
	public float
		shootPower = 2500f;
	// Update is called once per frame
	void Update ()
	{
		//foreach (var index in controllerIndices) {
		int index = (int)controllerLeftSlingshot.index;
		/*foreach (int buttonId in axisIds) {
			if (SteamVR_Controller.Input (buttonId).GetTouchDown (buttonId))
					Debug.Log (buttonId + " touch down");
				if (SteamVR_Controller.Input (index).GetTouchUp (buttonId))
					Debug.Log (buttonId + " touch up");
				if (SteamVR_Controller.Input (index).GetTouch (buttonId)) {
					var axis = SteamVR_Controller.Input (index).GetAxis (buttonId);
					Debug.Log ("axis: " + axis);
				}
			}*/
		
		LineRenderer rubber = GetComponent<LineRenderer> ();
		if (SteamVR_Controller.Input ((int)controllerRightLoader.index).GetPressDown (EVRButtonId.k_EButton_Axis0)) {

			newSeed = (GameObject)GameObject.Instantiate (seedPrefab, controllerLeftSlingshot.transform.position, Quaternion.identity);

			newSeed.GetComponent<Rigidbody> ().isKinematic = true;

			Debug.Log ("B1" + " touch down" + SteamVR_Controller.Input (0).transform.pos.ToString ());
		} else if (SteamVR_Controller.Input ((int)controllerRightLoader.index).GetPress (EVRButtonId.k_EButton_Axis0) && newSeed != null) {

			newSeed.transform.position = controllerRightLoader.transform.position;
			// Link the RUBBER BAND TOGETHER WITH THE Seed HERE, with the SEED's WIDTH? HACK: LOOK UP Start()
			// Link SlingPoint1's position with newSeed's position with SlingPOint2's position

			rubber.SetPosition (1, newSeed.transform.position); 

		} else if (SteamVR_Controller.Input ((int)controllerRightLoader.index).GetPressUp (EVRButtonId.k_EButton_Axis0) && newSeed != null) {
			//* if time alive less than 1 second detroy.
			if (newSeed.GetComponent<SeedScript> ().LifeTimer < 0.5f) {
				Destroy (newSeed);
			} else {
				newSeed.GetComponent<Rigidbody> ().isKinematic = false;
				newSeed.GetComponent<Rigidbody> ().AddForce ((
				    controllerLeftSlingshot.transform.position -
				    controllerRightLoader.transform.position + nudgeVector)
				* shootPower);
				
				newSeed = null;
			}

			rubber.SetPosition (1, slingPoint2.position);
		
		} else if (newSeed != null) {
			//* if time alive less than 1 second detroy.
			if (newSeed.GetComponent<SeedScript> ().LifeTimer < 0.5f) {
				Destroy (newSeed);
			} else {
				newSeed.GetComponent<Rigidbody> ().isKinematic = false;
				newSeed.GetComponent<Rigidbody> ().AddForce ((
				    controllerLeftSlingshot.transform.position -
				    controllerRightLoader.transform.position + nudgeVector)
				* shootPower
				);
				newSeed = null;
			}

			rubber.SetPosition (1, slingPoint2.position);

		} else {
			//rubber.SetPosition (1, controllerLeftSlingshot.transform.position); 
			rubber.SetPosition (1, slingPoint2.position);
		}
		rubber.SetPosition (0, slingPoint1.position);
		rubber.SetPosition (2, slingPoint2.position);
		//}
	}
}
