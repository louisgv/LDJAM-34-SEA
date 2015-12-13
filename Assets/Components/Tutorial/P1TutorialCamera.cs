using UnityEngine;
using System.Collections;

public class P1TutorialCamera : MonoBehaviour {

	// Use this for initialization
	public GameObject P2; 

	public GameObject[] Popups; 

	void Start () {

		Popups [0].SetActive (true);
		
		transform.LookAt (P2.transform);
	}
	
	// Update is called once per frame

}
