using UnityEngine;
using System.Collections;

public class FlowerMKI : Flower
{


	private FlowerSound flowerSounds;

	public void Awake ()
	{
		flowerSounds = GetComponent<FlowerSound> ();	
	
	}

	[Range (0.001f, 0.1f)]
	public float
		prefabGrowthSpeed = 0.01f;
	
	// Update is called once per frame
	void Update ()
	{
		// Grow the block
		if (state.Equals (FlowerState.IS_GROWING)) {
			Grow ();
		}
	}

	new void Grow ()
	{
		base.Grow ();
		flowerSounds.blossom ();
		transform.localScale += Vector3.up * growthProgress * prefabGrowthSpeed;
	}
}
