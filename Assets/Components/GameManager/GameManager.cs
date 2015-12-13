using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;

	public enum GameState
	{
		GAMEOVER_W1,
		GAMEOVER_W2,
		GAMEON
	}

	public static GameState state = GameState.GAMEON;
	// Use this for initialization
	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}
	
	public static void ToGameOver (int winner)
	{
		switch (winner) 
		{
		case 1: 
			state = GameState.GAMEOVER_W1;
			break;
		case 2: 
			state = GameState.GAMEOVER_W2;
			break;
		default :
			return;
		}

	}

	public void Update ()
	{
		if (
			state.Equals(GameState.GAMEOVER_W1) || 
			state.Equals(GameState.GAMEOVER_W2)) {
			Time.timeScale = 0.0f;
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
	
}
