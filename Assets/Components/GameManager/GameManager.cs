using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

	public static GameManager instance = null;
	
	public enum GameState
	{
		GAMEOVER,
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
		state = GameState.GAMEOVER;
		// SHOW WINNER APPROPIRATELY	
	}
	
	public void Update ()
	{
		if (state == GameState.GAMEOVER) {
			Time.timeScale = 0.0f;
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
	
}
