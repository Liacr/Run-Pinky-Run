using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MataInimigo : MonoBehaviour {

	private Rigidbody2D playerRigidBody;

	public float bounce;

	public GameObject explosao;


	//public SpriteRenderer spr;

	void Start () 
	{
		playerRigidBody = transform.parent.GetComponent<Rigidbody2D> ();
		//spr = GetComponent<SpriteRenderer> ();
	}
	

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Inimigo")) 
		{
			//Destroy (other.gameObject);

			other.gameObject.SetActive (false);

			Instantiate (explosao, other.transform.position, other.transform.rotation);

			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, bounce, 0f);

		}

		if(other.gameObject.CompareTag ("Boss"))
		{

			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, bounce, 0f);
			other.transform.parent.GetComponent<Boss> ().levaDano = true;
		}

	}
}
