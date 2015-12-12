using UnityEngine;
using System.Collections;

public class Bridge : MonoBehaviour
{

	public static float buildProgress = 0;
	
	public static float buildMAX = 3;
	
	public enum BridgeState
	{
		FINISHED,
		UNFINISHED
	}
	
	public BridgeState state = BridgeState.UNFINISHED;
	
	// There's only one bridge anyhow?
	public static void BuildBridge ()
	{
		++buildProgress;
	}
	
}
