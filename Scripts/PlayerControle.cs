using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerControle : MonoBehaviour {

	public float moveVelocidade;
	private float activeMoveVelocidade;

	public bool podeMover;

	private Rigidbody2D playerRigidbody;

	public float puloSpeed;

	public Transform checaChao; //CHECA onde o chão está, ele precisa checar o chão para pular novamente
	public float chaoRaio; //o raio do checa Chão
	public LayerMask chao; //qual/quem vai sei o chão

	public bool emChao; //para checar apenas uma vez, se tudo(checaChao,chaoRaio,chao) está correto, emChao fica "true"

	private Animator animação;

	public Vector3 respawn;

	public LevelManager levelManager;

	public GameObject mataInimigo;

	public float levaDanoForca; //movimento para trás qnd levahit do inimigo
	public float levaDanoLength;
	private float levaDanoContador;

	public float invensivelLength;
	private float invensivelContador;

	public AudioSource JumpSom;
	public AudioSource MachucaSom;

	private bool naPlataforma;
	public float naPlataformaVelocidadeAjuste; //velocidade qnd o player esta na plataforma

	private bool rightButtonHeld = false;
	private bool leftButtonHeld = false;

	// Use this for initialization
	void Start () 
	{
		playerRigidbody = GetComponent<Rigidbody2D> ();
		animação = GetComponent<Animator> ();

		respawn = transform.position; //vai para o começo do jogo

		levelManager = FindObjectOfType<LevelManager> ();

		activeMoveVelocidade = moveVelocidade;

		podeMover = true;
	}
	
	// Update is called once per frame
	void Update () 
	{

		emChao = Physics2D.OverlapCircle (checaChao.position, chaoRaio, chao); //desenha o circulo para fazer o checaChao usando a posição/local e raio escolhidos

		if (levaDanoContador <= 0 && podeMover) 
		{ //se não levar hit pode se mover

			if (naPlataforma) 
			{
				activeMoveVelocidade = moveVelocidade * naPlataformaVelocidadeAjuste; //velocidade de movimento quando está na plataforma igual a velocidade normal
			} else {
				activeMoveVelocidade = moveVelocidade;
			}
			 
			if (Input.GetAxisRaw ("Horizontal") > 0f)
//			if (rightButtonHeld)
			{
//				MoveRight ();
//				Move (Input.GetAxisRaw ("Horizontal"));
				playerRigidbody.velocity = new Vector3 (activeMoveVelocidade, playerRigidbody.velocity.y, 0f); //direita
				transform.localScale = new Vector3 (1f, 1f, 1f); //escala a sprite em 1 para ele virar para direita
				
			} else if (Input.GetAxisRaw ("Horizontal") < 0f)
//			else if (leftButtonHeld)
			{	
//				MoveLeft ();
//				Move (Input.GetAxisRaw ("Horizontal"));
				playerRigidbody.velocity = new Vector3 (-activeMoveVelocidade, playerRigidbody.velocity.y, 0f); //esquerda
				transform.localScale = new Vector3 (-1f, 1f, 1f); //escala a sprite em -1 para ele virar para esquerda
		
			} else {
//				Move (Input.GetAxisRaw ("Horizontal"));
				playerRigidbody.velocity = new Vector3 (0f, playerRigidbody.velocity.y, 0f); //para não deslizar
			}

			if (Input.GetButtonDown ("Jump")) 
			{ //se o pulo está sendo pressionado e "emChao" está "true", ele pula
				Jump ();
				JumpSom.Play();
			}

		}

		if (levaDanoContador > 0) 
		{
			levaDanoContador -= Time.deltaTime;

			if (transform.localScale.x > 0) {
				playerRigidbody.velocity = new Vector3 (-levaDanoForca, levaDanoForca, 0f); //direção que ele volta pra trás
			} else {
				playerRigidbody.velocity = new Vector3 (levaDanoForca, levaDanoForca, 0f);
			}
		}

		if (invensivelContador > 0) 
		{
			invensivelContador -= Time.deltaTime;
		}


		if (invensivelContador <= 0) 
		{
			levelManager.invensivel = false;
		}

		animação.SetFloat ("Velocidade", Mathf.Abs (playerRigidbody.velocity.x)); //tornar valor absoluto (qlqr número) negativo para positivo
		animação.SetBool ("noChao", emChao);

		if (playerRigidbody.velocity.y < 0) {
			mataInimigo.SetActive (true); //liga a caixa que mata o inimigo
		} else {
			mataInimigo.SetActive (false);
		}
	}

//	public void Move(float moveInput)
//	{
//		activeMoveVelocidade = moveVelocidade * moveInput;
//	}

	public void Jump()
	{
		if(emChao)	playerRigidbody.velocity = new Vector3 (playerRigidbody.velocity.x, puloSpeed, 0f);
		JumpSom.Play();
	}

	public void levaDano() //volta para trás quando leva dano do inimigo
	{
		levaDanoContador = levaDanoLength;
		invensivelContador = invensivelLength;
		levelManager.invensivel = true;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.CompareTag("KillPlane")) 
		{
			//gameObject.SetActive (false); //desliga o objeto que está com o script

			//transform.position = respawn;

			levelManager.Respawn ();
		}

		if (other.gameObject.CompareTag ("Checkpoint")) 
		{
			respawn = other.transform.position;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("PlataformaMovel")) 
		{
			transform.parent = other.transform; //ao pular na plataforma,acompanha a plataforma, player vira child da plataforma
			naPlataforma = true;
		}
	}

	void OnCollisionExit2D(Collision2D other)
	{
		if(other.gameObject.CompareTag ("PlataformaMovel"))
		{
			transform.parent = null; //ao pular fora da plataforma, deixa de acompanhar a plataforma, player volta a ser objeto normal
			naPlataforma = false;
		}
	}

	public void MoveRight ()
	{
		playerRigidbody.velocity = new Vector3 (activeMoveVelocidade, playerRigidbody.velocity.y, 0f); //direita
		transform.localScale = new Vector3 (1f, 1f, 1f); //escala a sprite em 1 para ele virar para direita
	}

	public void MoveLeft () 
	{
		playerRigidbody.velocity = new Vector3 (-activeMoveVelocidade, playerRigidbody.velocity.y, 0f); //esquerda
		transform.localScale = new Vector3 (-1f, 1f, 1f); //escala a sprite em -1 para ele virar para esquerda
	}


	public void rightpressed (BaseEventData eventData)
	{
		rightButtonHeld = true;
	}
	public void rightnotpressed(BaseEventData eventData)
	{
		rightButtonHeld = false;
	}
	public void leftpressed (BaseEventData eventData)
	{
		leftButtonHeld = true;
	}
	public void leftnotpressed(BaseEventData eventData)
	{
		leftButtonHeld = false;
	}
}
