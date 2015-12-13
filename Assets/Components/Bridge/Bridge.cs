using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bridge : MonoBehaviour
{

	public GameObject piecePrefab;
	
	private GameObject[] pieceInstances;
	
	[Range(3,9)]
	public static int
		BUILD_MAX = 3;
	
	public int buildProgress;
	
	public enum BridgeState
	{
		FINISHED,
		UNFINISHED
	}
	
	public void Awake ()
	{
		buildProgress = 0;
		pieceInstances = new GameObject [BUILD_MAX];
	}
	
	public BridgeState state = BridgeState.UNFINISHED;
	
	// There's only one bridge anyhow?
	public void BuildBridge ()
	{
		if (buildProgress == BUILD_MAX) {

			GameManager.ToGameOver (2);

			return;
		}
		// NOTE: Change the Forward Vector to whatever direction for future reference
		pieceInstances [buildProgress] = Instantiate (
			piecePrefab, 
			transform.position + Vector3.forward * buildProgress, 
			transform.rotation) as GameObject;
		
		pieceInstances [buildProgress++].transform.SetParent (transform);
	}

	void Update(){
//		if (state.Equals (BridgeState.FINISHED)) {
//			GameManager.ToGameOver (2);
//
//		}
	}
}
