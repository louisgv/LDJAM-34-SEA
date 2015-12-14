using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour
{
	[Range (1f, 18f)]
	public float
		GROW_MAX = 9f;

	public float growthProgress = 0;
	
	public float growthForBridge = 4.5f;
	
	[Range (1, 10)]
	public float
		growthSpeed = 1f;

	public static int flowerCount = 0;

	private Rigidbody rigidBody;
	
	private BoxCollider bCollider;

	public enum FlowerState
	{
		IS_GROWING,
		CHOPPED,
		CHOPPED_DEAD,
		FULL_GROWN,
		INVICIBLE
	}

	public FlowerState state = FlowerState.IS_GROWING;

	void Awake ()
	{
		rigidBody = GetComponent<Rigidbody> ();
		bCollider = GetComponent<BoxCollider> ();
	}

	public void BeChopped ()
	{
		if (growthProgress < growthForBridge) {
			Destroy (this.gameObject);
			return;
		}
		state = FlowerState.CHOPPED;

		gameObject.layer = 8;

		rigidBody.isKinematic = false;
		
		bCollider.isTrigger = false;
		
		rigidBody.AddForce (Vector3.up * 1800f);
		
	}

	void BeFullGrown ()
	{
		state = FlowerState.FULL_GROWN;
		// TODO: Game OVER, P1 Win. 
		// ANIMATION with Flower full bloom. UNCOMMENT THIS FOR WINNER SCENE
		
		//		GameManager.state = GameManager.GameState.GAMEOVER_W2;
		StartCoroutine (CountDownToInvicible ());
	}

	IEnumerator CountDownToInvicible ()
	{
		transform.localScale *= 1.8f;
		yield return new WaitForSeconds (5.0f);
		BeInvicible ();
	}

	public int flowerWinningCondition = 5;

	public void BeInvicible ()
	{
		state = FlowerState.INVICIBLE;
		transform.localScale /= 1.8f;
		flowerCount++;
		if (flowerCount == flowerWinningCondition) {
			GameManager.ToGameOver (1);
		}
	}

	public void BeChoppedDead ()
	{
		state = FlowerState.CHOPPED_DEAD;
		// TODO: Call the building bridge method, could be a static method.
		if (growthProgress > growthForBridge) {
//			centralBridge.BuildBridge ();
		}
		Destroy (this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (state.Equals (FlowerState.IS_GROWING)) {
			Grow ();
		}
		if (transform.position.y < 100.0f) {
			Destroy (this.gameObject);
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
