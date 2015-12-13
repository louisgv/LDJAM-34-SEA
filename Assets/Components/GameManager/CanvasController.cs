using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {

	public GameObject P2Won;

	public void Update(){
		if (GameManager.state.Equals (GameManager.GameState.GAMEOVER_W2)) {
			P2Won.SetActive (true);
		}
	}
		
		

}
