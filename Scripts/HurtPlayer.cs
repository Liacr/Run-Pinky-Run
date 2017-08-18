using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

	private LevelManager levelManager;

	public int damageAttack;

	// Use this for initialization
	void Start () 
	{
		levelManager = FindObjectOfType<LevelManager> ();
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			//levelManager.Respawn ();
			levelManager.MachucaPlayer(damageAttack); //leva dano
		}
	}
}
