using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ativaBloco : MonoBehaviour {

	private BlocoManager bloco;
	public Sprite blocoAtivado, blocoDesativado;

	private SpriteRenderer spriteRenderer;

	void Start()
	{	
		bloco = FindObjectOfType<BlocoManager> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
		
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			spriteRenderer.sprite = blocoAtivado;
			bloco.Ativa();
		}
	}
}

