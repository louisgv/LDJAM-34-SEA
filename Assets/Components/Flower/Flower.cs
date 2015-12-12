using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour
{
	[Range(99,450)]
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
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	void BeFullGrown ()
	{
		state = FlowerState.FULL_GROWN;
		// TODO: Game OVER, P1 Win. 
		// ANIMATION with Flower full bloom
	}
	
	void BeChoppedDead ()
	{
		state = FlowerState.CHOPPED_DEAD;
		// TODO: Call the building bridge method, could be a static method.
		if (growthProgress == growthForBridge) {
			Bridge.ImproveBridge ();
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (state.Equals (FlowerState.IS_GROWING)) {
			Grow ();
		}
	}
	
	void Grow ()
	{
		if (growthProgress == GROW_MAX) {
			BeFullGrown ();
		}
		growthProgress += Time.deltaTime * growthSpeed;
	}
}
