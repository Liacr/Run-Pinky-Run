using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoMovel : MonoBehaviour {

	public GameObject objetoParaMover;

	public Transform comeco;
	public Transform final;

	public float velocidadeMovimento;

	private Vector3 alvoAtual;

	// Use this for initialization
	void Start () 
	{
		alvoAtual = final.position; //alvo termina no final...duh
	}
	
	// Update is called once per frame
	void Update () 
	{
		objetoParaMover.transform.position = Vector3.MoveTowards (objetoParaMover.transform.position, alvoAtual, velocidadeMovimento * Time.deltaTime); //move constantemente na mesma direção na mesma velocidade

		if (objetoParaMover.transform.position == final.position) 
		{
			alvoAtual = comeco.position; //se chegou no final ele volta para o começo
		}

		if (objetoParaMover.transform.position == comeco.position) 
		{
			alvoAtual = final.position;
		}
	}
}
