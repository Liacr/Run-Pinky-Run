using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moeda : MonoBehaviour {

	private LevelManager levelManager;

	public int moedaValor;

	//private SpriteRenderer spr;

	// Use this for initialization
	void Start () 
	{
		levelManager = FindObjectOfType<LevelManager> ();
		//spr = GetComponent<SpriteRenderer> ();
	}


	public bool IsActive ()
	{
		return gameObject.activeInHierarchy;
	}

	public void ToggleActive (bool b)
	{
		gameObject.SetActive (b);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			levelManager.AddMoeda (moedaValor);
			ToggleActive (false);
			//spr.enabled = false;
			return;
			//Destroy (gameObject);
		}
	}
}
