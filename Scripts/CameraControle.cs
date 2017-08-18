using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControle : MonoBehaviour {

	public GameObject alvo; //foco da camera
	public float seguirFrente; //camera seguir em frente do jogador

	private Vector3 alvoPosicao;

	public float suavizacao;

	public bool followTarget;

	void Start () {
		followTarget = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (followTarget) {
			
			alvoPosicao = new Vector3 (alvo.transform.position.x, transform.position.y, transform.position.z); //pega a posição do player para seguir o player em X e fixa a camera do Y e Z
			//move o alvo da camera adiante do jogador
			if (alvo.transform.localScale.x > 0f) { //se está na direção frente...
				alvoPosicao = new Vector3 (alvoPosicao.x + seguirFrente, alvoPosicao.y, alvoPosicao.z); //camera move para frente
			} else {
				alvoPosicao = new Vector3 (alvoPosicao.x - seguirFrente, alvoPosicao.y, alvoPosicao.z);
			}

			//transform.position = alvoPosicao; 

			//Lerp:Move a camera gradualmente com o mundo
			transform.position = Vector3.Lerp (transform.position, alvoPosicao, suavizacao * Time.deltaTime);
		}
	}
}
