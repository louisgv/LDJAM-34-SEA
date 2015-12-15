using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
	public GameObject P1Won;
	public GameObject P2Won;

	public void Update ()
	{
		switch (GameManager.state) {
		case GameManager.GameState.GAMEON:
			P1Won.SetActive (false);
			P2Won.SetActive (false);
			break;
		case GameManager.GameState.GAMEOVER_W1:
			P1Won.SetActive (true);
			break;
		case GameManager.GameState.GAMEOVER_W2:
			P2Won.SetActive (true);
			break;
		default:
			P1Won.SetActive (false);
			P2Won.SetActive (false);
			return;
		}
	}
		
		

}
