using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoManager : MonoBehaviour {

	public bool desativado = true;
	public GameObject bloco;

	// Use this for initialization
	void Start () 
	{
		//if (desativado != bloco.activeSelf) 
		//	{
			bloco.SetActive (false);
		//	}
	}

	public void Ativa()
	{
		bloco.SetActive (true);
		desativado = false;
	}
}
