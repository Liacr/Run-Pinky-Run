using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaTeletransporte : MonoBehaviour {

	public bool fechada = true;
	public Transform portaAberta;
	public Sprite fechadaBottom, fechadaTop, abertaBottom;
	public Sprite portaBottom;

	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () 
	{
		transform.FindChild ("Top").GetComponent<SpriteRenderer> ().sprite = fechadaTop;
		transform.FindChild ("Bottom").GetComponent<SpriteRenderer> ().sprite = fechadaBottom;
		//portaBottom = transform.FindChild ("Bottom").GetComponent<SpriteRenderer>().sprite;

		//spriteRenderer = GetComponent<SpriteRenderer> ();
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		if  (!fechada && other.gameObject.CompareTag ("Player"))
		{
			other.transform.position = portaAberta.position;
		}
	}

	public void Destrava()
	{
		transform.FindChild ("Bottom").GetComponent<SpriteRenderer>().sprite = abertaBottom;
		//portaBottom = abertaBottom;
		//spriteRenderer.sprite = abertaBottom;
		fechada = false;
	}
}
