using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarVidaItem : MonoBehaviour {

	public int vidaGanha;

	private LevelManager levelManager;

	// Use this for initialization
	void Start () 
	{
		levelManager = FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player")) 
		{
			levelManager.GanharVida (vidaGanha);
			gameObject.SetActive(false);
		}
	}
}
