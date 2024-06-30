using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSession : MonoBehaviour
{

	[SerializeField] int playerLives = 3;
	[SerializeField] public int score = 0;
	void Awake()
	{
		int numGameSessions = FindObjectsOfType<GameSession>().Length;
		if (numGameSessions > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	public void ProcessPlayerDeath()
	{
		if (playerLives > 1)
		{
			TakeLife();
		}
		else
		{
			ResetGameSession();
		}
	}

	private void TakeLife()
	{
		playerLives--;
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex);
	}

	private void ResetGameSession()
	{
		SceneManager.LoadScene(0);
		Destroy(gameObject);
	}

	public void AddToScore(int pointsToAdd)
	{
		score += pointsToAdd;
	}
}
