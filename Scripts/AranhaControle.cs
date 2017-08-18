using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AranhaControle : MonoBehaviour {

	public float velocidade;
	private bool andar;

	private Rigidbody2D meuRigidbody;


	// Use this for initialization
	void Start () 
	{
		meuRigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (andar) 
		{
			meuRigidbody.velocity = new Vector3 (-velocidade, meuRigidbody.velocity.y, 0f);
		}
	}

	void OnBecameVisible()
	{
		andar = true; //inimigo entrar na camera
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag ("KillPlane")) 
		{
			//Destroy (gameObject);

			gameObject.SetActive (false);
		}
	}

	void OnEnable()
	{
		andar = false;
	}
}
