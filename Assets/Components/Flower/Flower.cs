using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour
{
	[Range (1f, 18f)]
	public float
		GROW_MAX = 9f;

	public float growthProgress = 0;
	
	public float growthForBridge = 4.5f;
	
	[Range (0, 1f)]
	public float
		growthSpeed = 1f;

	public static int flowerCount = 0;
	// HACK: Change this to winning condition for P1
	public static int flowerWinningCondition = 5;

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

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody> ();
		bCollider = GetComponent<BoxCollider> ();
	}

	public void BeChopped ()
	{
		state = FlowerState.CHOPPED;

		//if (growthProgress < growthForBridge) {
		//Destroy (gameObject);
		//	return;
		//}

		//state = FlowerState.CHOPPED;
		rigidBody = GetComponent<Rigidbody> ();

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

	public int INVICIBLE_TIME = 5;

	IEnumerator CountDownToInvicible ()
	{
		// transform.localScale *= 1.8f;
		GameObject counter = new GameObject ("Counter", typeof(TextMesh));

		counter.transform.SetParent (transform);

		counter.transform.localPosition = Vector3.up * 15.0f;	

		TextMesh counterText = counter.GetComponent<TextMesh> ();

		counterText.characterSize = 3;

		counterText.anchor = TextAnchor.MiddleCenter;
		counterText.alignment = TextAlignment.Center;
		counterText.color = Color.white;
		counterText.fontSize = 200;

		for (int i = INVICIBLE_TIME; i >= 0; --i) {
			yield return new WaitForSeconds (1.0f);
			counterText.text = i.ToString ();
		}
		Destroy (counter);
		BeInvicible ();
	}

	public void BeInvicible ()
	{
		state = FlowerState.INVICIBLE;
		//transform.localScale /= 1.8f;
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

	//public bool bridgable;

	protected void Grow ()
	{
		if (growthProgress > GROW_MAX) {			
			BeFullGrown ();
			return;
		}
		//bridgable = true;

		growthProgress += Time.deltaTime * growthSpeed;
	}
}
