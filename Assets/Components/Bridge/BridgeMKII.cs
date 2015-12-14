using UnityEngine;
using System.Collections;

public class BridgeMKII : Bridge
{
	
	public new void Awake ()
	{
		buildProgress = 0;
		pieceInstances = new GameObject [BUILD_MAX];
	}
	
	public new void BuildBridge ()
	{
		if (buildProgress == BUILD_MAX) {
			GameManager.ToGameOver (2);
			return;
		}
		// NOTE: Change the Forward Vector to whatever direction for future reference
		pieceInstances [buildProgress] = Instantiate (piecePrefab) as GameObject;

		pieceInstances [buildProgress].transform.SetParent (transform);
		
		pieceInstances [buildProgress].transform.localScale = new Vector3 (1.5f, 0.25f, 3f);
		
		pieceInstances [buildProgress].transform.localPosition = new Vector3 (0, -1f, -1.5f);
		
		pieceInstances [buildProgress].transform.localPosition -= Vector3.forward * (float)buildProgress * 3.0f;
		
		buildProgress++;
	}
	
}
