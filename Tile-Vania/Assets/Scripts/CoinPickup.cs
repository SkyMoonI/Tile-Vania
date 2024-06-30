using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
	[SerializeField] AudioClip coinPickupSFX;
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			Destroy(gameObject);
			FindObjectOfType<GameSession>().AddToScore(10);
			AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
			Debug.Log("Score: " + FindObjectOfType<GameSession>().score);

		}
	}
}
