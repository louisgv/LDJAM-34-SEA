using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour
{
	[Range(18,90)]
	public float
		GROW_MAX = 99;

	public float growthProgress = 0;
	
	public float growthForBridge = 45;
	
	[Range(1, 10)]
	public float
		growthSpeed = 1;
	
	public enum FlowerState
	{
		IS_GROWING,
		CHOPPED_DEAD,
		FULL_GROWN
	}
	
	public FlowerState state = FlowerState.IS_GROWING;
	
	private Bridge CentralBridge;
	
	void Start ()
	{
		CentralBridge = GameObject.FindGameObjectWithTag ("Bridge").GetComponent<Bridge> ();
	}
	
	void BeFullGrown ()
	{
		state = FlowerState.FULL_GROWN;
		// TODO: Game OVER, P1 Win. 
		// ANIMATION with Flower full bloom
	}
	
	public void BeChoppedDead ()
	{
		state = FlowerState.CHOPPED_DEAD;
		// TODO: Call the building bridge method, could be a static method.
		if (growthProgress > growthForBridge) {
			CentralBridge.BuildBridge ();
		}
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (state.Equals (FlowerState.IS_GROWING)) {
			Grow ();
		}
	}
	
	protected void Grow ()
	{
		if (growthProgress > GROW_MAX) {			
			BeFullGrown ();
			return;
		}
		growthProgress += Time.deltaTime * growthSpeed;
	}
}
