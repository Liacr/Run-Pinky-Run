using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoIA : MonoBehaviour {

	public Transform alvo;
	public Transform self;
	//private InimigoIA inimigo;
	private GameObject Player;
	//private float range;
	public float Speed;

	public Vector2 velocity;

	//public int currentPoint;
	//public Vector3 patrolPoints;

	private Rigidbody2D meuRigidBody;

	// Use this for initialization
	void Start () 
	{
		meuRigidBody = GetComponent<Rigidbody2D> ();
		//inimigo = FindObjectOfType<InimigoIA>();
		Player = GameObject.Find ("Player");

	}
	
	void OnBecameVisible()
	{
		enabled = true;
	}

	void OnBecameInvisible()
	{
		enabled = false;
	}


	void chasePlayer()
	{

		//transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, Speed * Time.deltaTime);

		Vector2 velocity = new Vector2 ((transform.position.x - Player.transform.position.x) * Speed, (transform.position.y - Player.transform.position.y) * Speed);
		meuRigidBody.velocity = -velocity.normalized * Speed;

		if (Player.transform.position.x >= self.transform.position.x) //players spot in world space as opposed to enemy "self" spot 
		{ 
			self.transform.rotation = Quaternion.Euler(0,180,0); // flips enemy around to face the player on x axis only
		} 
		else if (Player.transform.position.x <= self.transform.position.x) //players spot in world space as opposed to enemy "self" spot
		{ 
			self.transform.rotation = Quaternion.Euler(0,0,0); // flips enemy around to face the player on x axis only }
		}

	}

	/* void enemyPatrol()
	{
		if (transform.position == patrolPoints[currentPoint].position)
		{
			transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
			currentPoint++;
		}
		if (currentPoint >= patrolPoints.Length)
		{
			currentPoint = 0;
		}
		transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, moveSpeed = Time.deltaTime);
	} */

	void Update()
	{

		if (enabled)
		{
			chasePlayer();
		}


		/*if (!enabled)
		{
			enemyPatrol();
		}*/
	}
}