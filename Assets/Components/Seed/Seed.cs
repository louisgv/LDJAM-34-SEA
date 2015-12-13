using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour
{
	public GameObject flowerPrefab;
	
	public Transform flowerParent;
	
	public void Awake ()
	{
		flowerParent = GameObject.FindGameObjectWithTag ("Ground").transform;
	}
	
	public void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("P2")) {
			// STUN PLAYER 2
			//			other.GetComponent<>().beStunned();
		}
		if (other.CompareTag ("Ground")) {
			// INSTANTIATE A FLOWER
			GameObject flowerInstance = 
				Instantiate (flowerPrefab, transform.position, Quaternion.identity) as GameObject;
			
			// SET PARENT
			flowerInstance.transform.SetParent (flowerParent);
			Destroy (this.gameObject);
		}
	}
	
	public void Update ()
	{
		if (transform.position.y < -90) {
			Destroy (this.gameObject);
		}
	}
	
}
