using UnityEngine;
using System.Collections;

public class SeedScript : MonoBehaviour {

	public float LifeTimer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		LifeTimer += Time.deltaTime;
	}
}
