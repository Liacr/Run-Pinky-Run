using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour {

	private PortaTeletransporte porta;

	void Start()
	{
		porta = FindObjectOfType<PortaTeletransporte> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			porta.Destrava();
			gameObject.SetActive (false);
		}
	}
}
