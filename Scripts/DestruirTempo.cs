using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirTempo : MonoBehaviour {

	public float cronometro; //Depois que o tempo acabar, destruir objeto do mundo

	
	// Update is called once per frame
	void Update () 
	{
		cronometro = cronometro - Time.deltaTime; //assim que o objeto comecar no mundo, o cronometro comeca e vai ate o zero, depois é destruido.
	
		if (cronometro <= 0f) 
		{
			gameObject.SetActive (false);
			//Destroy (gameObject);
		}
	}
}
