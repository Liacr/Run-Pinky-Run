using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointControle : MonoBehaviour {

	public Sprite flagFechada; //para sprite especifica
	public Sprite flagAberta;

	private SpriteRenderer spriteRenderer;

	public bool checkpointAtivo;

	// Use this for initialization
	void Start () 
	{
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			spriteRenderer.sprite = flagAberta;
			checkpointAtivo = true;
		}
	}
}
