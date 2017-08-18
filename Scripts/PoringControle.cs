using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoringControle : MonoBehaviour {


	public Transform esquerda; //para direita e esquerda
	public Transform direita;

	public float velocidade;

	private Rigidbody2D meuRigidBody;

	public bool moveDireita; //checa se passou pra direita

	private float ySize;

	void Start () 
	{

		meuRigidBody = GetComponent<Rigidbody2D> ();
		ySize = transform.localScale.y;
	}

	void Update () 
	{

		if (moveDireita && transform.position.x > direita.position.x) //se move direita é true e maior que o ponto da direita
		{
			moveDireita = false;//para de mover direita e move esquerda
			transform.localScale = new Vector3 (ySize, transform.localScale.y, transform.localScale.z);
		}	

		if(!moveDireita && transform.position.x < esquerda.position.x) //se moveDireita é falso e menor que a esquerda
		{
			moveDireita = true; //volta pra direita
			transform.localScale = new Vector3 (-ySize, transform.localScale.y, transform.localScale.z);
		}

		if (moveDireita) 
		{
			meuRigidBody.velocity = new Vector3 (velocidade, meuRigidBody.velocity.y, 0f);
		} else {
			meuRigidBody.velocity = new Vector3 (-velocidade, meuRigidBody.velocity.y, 0f);
		}

	}
}
