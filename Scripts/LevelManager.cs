using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public float respawnTime; //tempo que vai levar pra respawnar
	public PlayerControle jogador;

	public GameObject sangueExplosao;

	public int moedaContador;
	private int moedaBonusVidaContador;
	public int bonusVidaLimite;

	public Text moedaText;
	public AudioSource pegarSom;

	public Image coracao1;
	public Image coracao2;
	public Image coracao3;

	public Sprite coracaoCheio;
	public Sprite coracaoMeio;
	public Sprite coracaoVazio;

	public int maxVida;
	public int contadorVida;

	private bool respawning; //para não respawnar novamente depois de já ter respawnado

	public ResetNoRespawn[] objetosParaResetar; //array com objetos que vão resetar

	public bool invensivel;

	public Text livesText;
	public int currentLives; 
	public int startingLives; //as duas vidas rosas

	public GameObject gameOverTela;
	public AudioSource levelMusic;
	public AudioSource gameOverMusic;

	public bool respawnCoActive;

	// Use this for initialization
	void Start () 
	{
		jogador = FindObjectOfType<PlayerControle> ();

		moedaText.text = " : " + moedaContador;

		contadorVida = maxVida;

		objetosParaResetar = FindObjectsOfType <ResetNoRespawn> ();

		currentLives = startingLives;

		livesText.text = currentLives + " x"; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (contadorVida <= 0 && !respawning) //se vida menor igual a zero e respawning não true 
		{
			Respawn ();
			respawning = true;
		}

		if (moedaBonusVidaContador >= bonusVidaLimite) 
		{
			currentLives += 1;
			livesText.text = currentLives + " x"; 
			moedaBonusVidaContador -= bonusVidaLimite; //toda vez que atinge 10 moedas(o limite determinadp), ele ganha uma vida, se tiver 20, vai contar as 10+(o limite) e dar vida
		}
	}

	public void Respawn()
	{
		currentLives -= 1;
		livesText.text = currentLives + " x";

		if (currentLives > 0) 
		{ //checa se tem vidas o suficiente para respawnar
			StartCoroutine ("RespawnCo"); //pausa a execução e retorna para o frame de onde parou
		} else {
			jogador.gameObject.SetActive (false); //desativa todo o player
			gameOverTela.SetActive(true);
			levelMusic.Stop ();
			gameOverMusic.Play ();
		}
	}

	public IEnumerator RespawnCo() //correlacionado com a função respawn
	{
		respawnCoActive = true;

		jogador.gameObject.SetActive (false); //desativa todo o player
		Instantiate(sangueExplosao, jogador.transform.position, jogador.transform.rotation);

		yield return new WaitForSeconds (respawnTime); //esperar um pouco(respawnTime) antes de retorna a próxima linha

		respawnCoActive = false;

		contadorVida = maxVida; //renova vida
		respawning = false;
		UpdateCoracao ();

		//----------------------------VOLTA MOEDA E CONTADOR----------------------
		moedaContador = 0;
		moedaText.text = " : " + moedaContador;
		moedaBonusVidaContador = 0;
		//----------------------------FIM RESETA MOEDA E CONTADOR----------------------

		jogador.transform.position = jogador.respawn; //volta a viver tocando no respawn
		jogador.gameObject.SetActive (true); //ativa o player

		//----------------------------------PARA RESETAR--------------------------------------------
		for(int i = 0; i < objetosParaResetar.Length; i++) //se i for menor que o numero de objetos na array
		{  //primeiro objeto (0) e depois os outros (0+1) -> (i++)
			objetosParaResetar[i].gameObject.SetActive(true);
			objetosParaResetar[i].ResetObject();
		}
	}

	public void AddMoeda(int moedaAdicionar)
	{
		moedaContador += moedaAdicionar; //moedas para adicionar no local de moedas
		moedaBonusVidaContador += moedaAdicionar;

		moedaText.text = " : " + moedaContador;

		pegarSom.Play ();

	}

	public void MachucaPlayer(int damageVida) //hurtPlayer
	{
		if (!invensivel) 
		{
			contadorVida -= damageVida; //perde vida
			UpdateCoracao ();

			jogador.levaDano ();

			jogador.MachucaSom.Play ();
		}
	}

	public void GanharVida(int vidaGanha)
	{
		contadorVida += vidaGanha;

		if (contadorVida > maxVida) 
		{
			contadorVida = maxVida; //não passar de 6 vidinhas
		}

		pegarSom.Play ();

		UpdateCoracao ();
	}

	public void UpdateCoracao() //coracaoMeter não contar a cada frame
	{
		switch (contadorVida) 
		{
		case 6: //vida cheia
			coracao1.sprite = coracaoCheio;
			coracao2.sprite = coracaoCheio;
			coracao3.sprite = coracaoCheio;
			return;

		case 5:
			coracao1.sprite = coracaoCheio;
			coracao2.sprite = coracaoCheio;
			coracao3.sprite = coracaoMeio;
			return;
		
		case 4:
			coracao1.sprite = coracaoCheio;
			coracao2.sprite = coracaoCheio;
			coracao3.sprite = coracaoVazio;
			return;
		
		case 3:
			coracao1.sprite = coracaoCheio;
			coracao2.sprite = coracaoMeio;
			coracao3.sprite = coracaoVazio;
			return;
		
		case 2:
			coracao1.sprite = coracaoCheio;
			coracao2.sprite = coracaoVazio;
			coracao3.sprite = coracaoVazio;
			return;
		
		case 1:
			coracao1.sprite = coracaoMeio;
			coracao2.sprite = coracaoVazio;
			coracao3.sprite = coracaoVazio;
			return;
		
		case 0:
			coracao1.sprite = coracaoVazio;
			coracao2.sprite = coracaoVazio;
			coracao3.sprite = coracaoVazio;
			return;

		default: // o que vai acontecer caso tenha alguma situação sem ser 0
			coracao1.sprite = coracaoVazio;
			coracao2.sprite = coracaoVazio;
			coracao3.sprite = coracaoVazio;
			return;
		}
	}

	public void AddVidas (int ganharVidas)
	{
		pegarSom.Play ();
		currentLives += ganharVidas;
		livesText.text = currentLives + " x";
	}
}
