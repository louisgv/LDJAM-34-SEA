using UnityEngine;
using System.Collections;

public class P1TutorialCamera : MonoBehaviour {

	// Use this for initialization
	public GameObject P2; 

	void Start () {
		transform.LookAt (P2.transform);
	}
	
	// Update is called once per frame

}
