using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	public GameObject GameManagerGO; // reference to out game manager

	public GameObject GameLivesGO; // reference to out player lives
	private GameObject playerLives; 
	public GameObject projectilePrefab;
	public GameObject ExplosionGO; // this is the explosion prefab
	private List<GameObject> Projectiles = new List<GameObject>();

	private List<GameObject> ShipLives = new List<GameObject>();
	//private float projectileVelocity;
	private bool KeysEnabled = true;

	//refernce to lives text
	public Text LivesUIText;

	const int MaxLives = 12; // maximum player lives
	private int TotalLives;
	int lives; //current player lives


	private float moveDirection;
	public float speed;
	public float moveSpeed = 0.01f;
	public float turnSpeed = 50f;
	private float respawnTimer = 0f;
	//private float delayTime = 5f;

	SpriteRenderer shipColour; // SpriteRender for Gameobject
	//Transform originalRotationValue;

	private Color redSmallLvl1; // blanchedalmond
	private Color redSmallLvl2; // bisque

	private Color redLvl1; // very light red colour
	private Color redLvl2; // medium red colour
	private Color redLvl3; // red colour
	private Color redLvl4; // dark red colour
	// Update is called once per frame

	public void Init(){
	
		lives = MaxLives;
		TotalLives = 3;
		moveDirection = 1;
		//update LivesUIText
		//LivesUIText.text = lives.ToString();

		//set player GameObject to true
		gameObject.SetActive (true);
		//GameObject mRock = (GameObject)Instantiate(MediumRock);
		//originalRotationValue = transform.rotation; // save the initial rotation

		PlaceLives ();

		gameObject.GetComponent<PlayerController>().enabled = true; 

	}

	// Use this for initialization
	void Start()
	{
		
		//projectileVelocity = 3;
		redSmallLvl1= new Color(1, 0.921569f, 0.803922f); //blanchedalmond
		redSmallLvl2  = new Color(1, 0.894118f,  0.768627f, 1); // bisque

		redLvl1 = new Color(1, 0.627451f, 0.478431f, 1);
		redLvl2 = new Color(0.803922f, 0.360784f, 0.360784f, 1);
		redLvl3 = new Color(0.698039f, 0.133333f, 0.133333f, 1); //firebrick
		redLvl4 = new Color(0.545098f, 0, 0);//dark red

		shipColour = this.GetComponent<SpriteRenderer>();
		
	}
	void Update () {
	
		float translation = Time.deltaTime * 20;

		if (Input.GetButtonDown ("Jump")) {
			//print ("space key was pressed");
			GameObject bullet = (GameObject)Instantiate(projectilePrefab,transform.position,Quaternion.identity);
			Projectiles.Add(bullet);

			for(int i = 0; i < Projectiles.Count;i++){
				GameObject goBullet = Projectiles[i];
				if (goBullet != null) {
					//goBullet.transform.Translate (new Vector3(0,1) * Time.deltaTime * projectileVelocity);
				//	print ("the translation "+translation);
					//goBullet.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
				}
			}

		}
	
	
	}

	void FixedUpdate () {

		//Initialize rigid2D
		//rigid = GetComponent<Rigidbody>();
		/*if (Input.GetKeyDown("space"))
			print("space key was pressed");

		if (Input.GetKeyDown ("left")) {
			print ("left key was pressed");
			transform.Rotate (0, 0.0f, 5f);
		}
		if (Input.GetKeyDown("right")){
			print("right key was pressed");
			transform.Rotate (0, 0.0f, -5f);
		}
		if (Input.GetKeyDown ("up")) {
			print ("up key was pressed");
			speed = 20.0f;
		}*/
		//float moveHorizontal = Input.GetAxis("Horizontal");
		//float moveVertical = Input.GetAxis("Vertical");

		//Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		//rigid.velocity = movement * speed;
		CheckBounds();

		/*if (Input.GetButtonDown ("Jump")) {
			print ("space key was pressed");
			GameObject bullet = (GameObject)Instantiate(projectilePrefab,transform.position,Quaternion.identity);
			Projectiles.Add(bullet);

			for(int i = 0; i < Projectiles.Count;i++){
				GameObject goBullet = Projectiles[i];
				if (goBullet != null) {
					goBullet.transform.Translate (new Vector3(0,1) * Time.deltaTime * projectileVelocity);
				}
			}

		}*/

		ControllerActive(KeysEnabled);



	}

	void ControllerActive(bool Keys)
	{
		if(Keys == true){
			if(Input.GetKey(KeyCode.UpArrow))
				transform.Translate(Vector3.up * (moveSpeed * Time.deltaTime * moveDirection*0.5f));

			if(Input.GetKey(KeyCode.DownArrow))
				transform.Translate(-Vector3.up * (moveSpeed * Time.deltaTime * moveDirection*0.5f));

			if(Input.GetKey(KeyCode.LeftArrow))
				transform.Rotate(Vector3.back, -turnSpeed * Time.deltaTime);

			if(Input.GetKey(KeyCode.RightArrow))
				transform.Rotate(Vector3.back, turnSpeed * Time.deltaTime);
		}else{
			if(Input.GetKey(KeyCode.UpArrow))
				transform.Rotate(Vector3.back, turnSpeed * Time.deltaTime);

			if(Input.GetKey(KeyCode.DownArrow))
				transform.Rotate(Vector3.back, turnSpeed * Time.deltaTime);

			if (Input.GetKey (KeyCode.LeftArrow));
				
				

			if (Input.GetKey (KeyCode.RightArrow));

		}	
	}

	void CheckBounds()
	{
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
		// detect collision with rock or a bullet
		if ((col.tag == "RockTag")) {

			lives=lives-4;
			//LivesUIText.text = lives.ToString();

			moveDirection = -1;
			StartCoroutine(ReStartCoroutine());

			//transform.Translate(Vector3.up * moveSpeed * 0);

			if ((lives > 4) && (lives <= 8))
			{
				shipColour.color = redLvl1;
			}
			else if ((lives < 4))
			{
				shipColour.color = redLvl3;
			}

			else
			{

			}					

		}

		if ((col.tag == "RockMedTag")) {

			lives=lives-2;
			//LivesUIText.text = lives.ToString();

			moveDirection = -1;
			StartCoroutine(ReStartCoroutine());


			if ((lives > 4) && (lives <= 8))
			{
				shipColour.color = redLvl2;
			}
			else if ((lives < 4))
			{
				shipColour.color = redLvl4;
			}

			else
			{

			}
		}

		if ((col.tag == "RockSmallTag")) {

			lives--;
			//LivesUIText.text = lives.ToString();

			if ((lives > 9) && (lives < 12))
			{
				shipColour.color = redSmallLvl1;
			}
			if ((lives > 7) && (lives <= 9))
			{
				shipColour.color = redSmallLvl2;
			}
			if ((lives > 5) && (lives <= 7))
			{
				shipColour.color = redLvl1;
			}
			if ((lives > 3) && (lives <= 5))
			{
				shipColour.color = redLvl2;
			}
			if ((lives > 1) && (lives <= 3))
			{
				shipColour.color = redLvl3;
			}
			else if ((lives <= 1))
			{
				shipColour.color = redLvl4;
			}

			else
			{

			}

		}


		if(lives <= 0){

			TotalLives--;

			shipColour.color = new Color(1, 1, 1, 1);

			Destroy (GameObject.FindWithTag("LiveTag"));

			PlayExplosion ();
			//Destroy (gameObject); //destroy the spacehip
			lives = MaxLives;
			gameObject.transform.position = new Vector3(0,0,0);
			//gameObject.transform.rotation=Quaternion.Euler (Vector3 (0,0,0));

			gameObject.transform.rotation = Quaternion.identity;

			gameObject.GetComponent<PlayerController>().enabled = false; 

			StartCoroutine(ReStartCoroutine());

			if(TotalLives == 0){
			// get GameManager gameover state
				gameObject.SetActive(false);
				GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
			}



			// hide the spaceship
			//gameObject.SetActive(false);
			//ControllerActive(false);


			//StartCoroutine(ReStartCoroutine());

			//print ("the time is "+ Time.deltaTime * 10);

			//respawnTimer += Time.deltaTime;
			//if (respawnTimer > delayTime) {
			//	print("hello there hubert");				
			//}
		}




	}

	//instantiate an explosion
	void PlayExplosion(){
		GameObject explosion = (GameObject)Instantiate(ExplosionGO);	
		explosion.transform.position = transform.position;
	}

	void PlaceLives(){
		var cam = Camera.main;
		Vector3 stageDimensions = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));

		//BoxCollider2D collider = myShip.GetComponent<BoxCollider2D>();
		//print ("the x position "+myShip.transform.position.x +" the width "+ myShip.GetComponent<BoxCollider2D>().size.x +" and the height " + myShip.GetComponent<Renderer>().bounds.size.y );
		//float shipHalf = myShip.GetComponent<Renderer> ().bounds.size.x / 2;
		//float shipHalf = (myShip.GetComponent<BoxCollider2D>().size.x)/2;

		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		float xPos;
		float yPos;

		min.x = min.x + 1;
		max.y = max.y - 1;
		for (int i = 0; i < 3; i++) {
			
			playerLives = (GameObject)Instantiate(GameLivesGO);	
			//BoxCollider2D collider = playerLives.GetComponent<BoxCollider2D>();
			//float shipHalf = playerLives.GetComponent<BoxCollider2D>().size.x;
			//min.x = min.x + + i;
			xPos = min.x + i;

			playerLives.transform.position = new Vector3(xPos, max.y, this.transform.position.z);

			ShipLives.Add (playerLives);
		}



	}

	IEnumerator ReStartCoroutine()
	{
		//This is a coroutine
		//print("hello there");
		//gameObject.SetActive(false);

		yield return new WaitForSeconds(0.0001f);   //Wait one frame

		moveDirection = 1;
		//print (" ..waited for 5 seconds.. ");
		gameObject.GetComponent<PlayerController>().enabled = true; 
		//gameObject.SetActive(true);
		//(GameObject)Instantiate(MediumRock);
		//DoSomethingElse();
	}



}
