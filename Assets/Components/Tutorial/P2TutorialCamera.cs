using UnityEngine;
using System.Collections;

public class  P2TutorialCamera : MonoBehaviour {

	// Use this for initialization
	public GameObject P1; 

	void Start () {
		transform.LookAt (P1.transform);
	}

	// Update is called once per frame

}
