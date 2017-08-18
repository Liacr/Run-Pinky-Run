using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	public bool bossActive;

	public float timeBetweenDrops;
	private float timeBetweenDropStore;
	private float dropCount;

	public float waitForPlatform;
	private float platformCount;

	public Transform pontoEsq;
	public Transform pontoDir;
	public Transform dropSpawnEspinho;

	public GameObject dropEspinho;

	public GameObject theBoss;
	public bool bossDir;

	public GameObject platDir;
	public GameObject platEsq;

	public bool levaDano;

	public int vidaInicial;
	private int vidaAtual;

	public GameObject levelExit;

	private CameraControle theCamera;

	private LevelManager theLevelManager;

	public bool waitingForRespawn;

	// Use this for initialization
	void Start () 
	{
		vidaAtual = vidaInicial;
		timeBetweenDropStore = timeBetweenDrops;
		dropCount = timeBetweenDrops;
		platformCount = waitForPlatform;

		theBoss.transform.position = pontoDir.position;
		bossDir = true;

		theCamera = FindObjectOfType<CameraControle>();

		theLevelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(theLevelManager.respawnCoActive)
		{
			bossActive = false;
			waitingForRespawn = true;
		}

		if(waitingForRespawn && !theLevelManager.respawnCoActive)
		{
			theBoss.SetActive(false);
			platEsq.SetActive(false);
			platDir.SetActive(false);

			timeBetweenDrops = timeBetweenDropStore;

			platformCount = waitForPlatform;
			dropCount = timeBetweenDrops;

			theBoss.transform.position = pontoDir.position;
			bossDir = true;

			vidaAtual = vidaInicial;

			theCamera.followTarget = true;

			waitingForRespawn = false;
		}

		if (bossActive) 
		{
			theCamera.followTarget = false;
			theCamera.transform.position = Vector3.Lerp(theCamera.transform.position, new Vector3(transform.position.x, theCamera.transform.position.y, theCamera.transform.position.z), theCamera.suavizacao * Time.deltaTime);

			theBoss.SetActive (true);

			if (dropCount > 0) 
			{
				dropCount -= Time.deltaTime;
			} else {
				dropSpawnEspinho.position = new Vector3(Random.Range(pontoEsq.position.x, pontoDir.position.x), dropSpawnEspinho.position.y, dropSpawnEspinho.position.z);
				Instantiate (dropEspinho, dropSpawnEspinho.position, dropSpawnEspinho.rotation);
				dropCount = timeBetweenDrops;
			}

			if (bossDir) 
			{
				if (platformCount > 0) {
					platformCount -= Time.deltaTime;
				} else {
					platDir.SetActive (true);
				}
			} else {
					if (platformCount > 0) 
					{
						platformCount -= Time.deltaTime;
					} else {
						platEsq.SetActive (true);
				}
			}

			if (levaDano) 
			{
				vidaAtual -= 1;

				if (vidaAtual <= 0) 
				{
					levelExit.SetActive (true);

					theCamera.followTarget = true;

					gameObject.SetActive (false);
				}

				if (bossDir) 
				{
					theBoss.transform.position = pontoEsq.position;
				} else {
					theBoss.transform.position = pontoDir.position;
				}

				bossDir = !bossDir;

				platDir.SetActive (false);
				platEsq.SetActive (false);

				platformCount = waitForPlatform;

				timeBetweenDrops = timeBetweenDrops / 2f;

				levaDano = false;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			bossActive = true;
		}
	}
}
