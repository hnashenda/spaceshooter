using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
	
	const int MaxLives = 4; // maximum player lives
	int rock_lives; //current player lives

	float speed; // for rock speed
	// Constant speed of the ball
	private float speedBounce = 5f;
	Vector2 position;
	float xShift; // the shift in x position on initialise
	float yShift; // the shift in y position on initialisation
	public GameObject ExplosionGO; // this is the explosion prefab


	//public Color aColor = new Vector4(0.5F, 1, 0.5F, 1);

	private Color redLvl1; // very light red colour
	private Color redLvl2; // medium red colour
	private Color redLvl3; // red colour

	SpriteRenderer rockColour; // SpriteRender for Gameobject

	public GameObject MediumRock;


	// bouncing
	// Keep track of the direction in which the ball is moving
	private Vector2 velocity;

	// used for velocity calculation
	private Vector2 lastPos;




	// Use this for initialization

	void Start () {

		redLvl1 = new Vector4(1, 0.627451f, 0.478431f, 1);
		redLvl2 = new Vector4(0.803922f, 0.360784f, 0.360784f, 1);
		redLvl3 = new Color(0.698039f, 0.133333f, 0.133333f, 1);
		//143, 24, 18
		rockColour = this.GetComponent<SpriteRenderer>();



		rock_lives = MaxLives;
		speed = 0.01f; //set speed

		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
		xShift = Random.Range (min.x, max.x);
		yShift = Random.Range(min.y, max.y);
		//aRock.transform.position = new Vector2(Random.Range(min.x,max.x),Random.Range(min.y,max.y));

	}

	void FixedUpdate ()
	{
		// Get pos 2d of the ball.
		Vector3 pos3D = transform.position;
		Vector2 pos2D = new Vector2(pos3D.x, pos3D.y);

		// Velocity calculation. Will be used for the bounce
		velocity = pos2D - lastPos;
		lastPos = pos2D;
	}

	// Update is called once per frame
	void Update () {
		CheckBounds ();
		position = transform.position;
		position = new Vector2 (position.x + xShift * Time.deltaTime * speed, position.y + yShift * Time.deltaTime * speed);

		//transform.Translate (player.transform.up * Time.deltaTime * projectileVelocity);
		transform.position = position;
	}

	void CheckBounds(){
		var cam = Camera.main;
		Vector3 stageDimensions = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));

		// horizontal plane
		if (this.transform.position.x < -stageDimensions.x)
		{
			this.transform.position = new Vector3(stageDimensions.x, this.transform.position.y, this.transform.position.z);
		}

		if (this.transform.position.x > stageDimensions.x)
		{
			this.transform.position = new Vector3(-stageDimensions.x, this.transform.position.y, this.transform.position.z);
		}

		// vertical plane

		if (this.transform.position.y < -stageDimensions.y)
		{
			this.transform.position = new Vector3(this.transform.position.x, stageDimensions.y, this.transform.position.z);
		}

		if (this.transform.position.y > stageDimensions.y)
		{
			this.transform.position = new Vector3(this.transform.position.x, -stageDimensions.y, this.transform.position.z);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		// detect collision with bullet
		if ((col.tag == "BulletTag")) {
			rock_lives--;


			position = -position;

		//print ("the lives" + rock_lives);
		//print (" the og position "+this.transform.position);

			switch (rock_lives)
			{
			case 3:
				rockColour.color = redLvl1;
				break;
			case 2:
				rockColour.color = redLvl2;
				break;
			case 1:
				rockColour.color = redLvl3;
				break;		
			default:
				print ("Incorrect intelligence level.");
				break;
			}

		
			// Normal
			//Vector3 N = col.contacts[0].normal;

			//Direction
			//Vector3 V = velocity.normalized;

			// Reflection
			//Vector3 R = Vector3.Reflect(V, N).normalized;

			// Assign normalized reflection with the constant speed
			//rigidbody2D.velocity = new Vector2(R.x, R.y) * speedBounce;





			if(rock_lives == 0){
				
					//PlayExplosion ();
				Destroy (gameObject); //destroy the rock

				for(int i = 0; i < 4;i++){
					SpawnMediumRock(this.transform.position.x,this.transform.position.y);
				}

			}
		}
	}

	void SpawnMediumRock(float x, float y){
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		GameObject mRock = (GameObject)Instantiate(MediumRock);
		//mRock.transform.position = new Vector2(Random.Range(min.x,max.x),Random.Range(min.y,max.y));
		mRock.transform.position = new Vector2(x,y);

	}

	//instantiate an explosion
	void PlayExplosion(){
		GameObject explosion = (GameObject)Instantiate(ExplosionGO);	
		explosion.transform.position = transform.position;
	}

}
