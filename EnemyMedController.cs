using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMedController : MonoBehaviour {

	const int MaxLives = 2; // maximum player lives
	int medrock_lives; //current player lives

	float medSpeed; // for rock speed
	float medxShift; // the shift in x position on initialise
	float medyShift; // the shift in y position on initialisation
	public GameObject ExplosionGO; // this is the explosion prefab


	private Color redLvl1; // very light red colour
	private Color redLvl2; // medium red colour
	private Color redLvl3; // red colour

	SpriteRenderer rockColour; // SpriteRender for Gameobject

	public GameObject SmallRock;


	// Use this for initialization
	void Start () {
		
		redLvl1 = new Vector4(1, 0.627451f, 0.478431f, 1);
		redLvl2 = new Vector4(0.803922f, 0.360784f, 0.360784f, 1);
		redLvl3 = new Color(0.698039f, 0.133333f, 0.133333f, 1);

		rockColour = this.GetComponent<SpriteRenderer>();

		medrock_lives = 2; // 
		medSpeed = 0.06f;  // set speed

		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
		medxShift = Random.Range (min.x, max.x);
		medyShift = Random.Range(min.y, max.y);
	}
	
	// Update is called once per frame
	void Update () {

		CheckBounds ();

		Vector2 position = transform.position;
		position = new Vector2 (position.x + medxShift * Time.deltaTime * medSpeed, position.y + medyShift * Time.deltaTime * medSpeed);

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
			medrock_lives--;

			switch (medrock_lives)
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
			//print (" the og position "+this.transform.position);
			if(medrock_lives == 0){
				
					//PlayExplosion ();
					Destroy (gameObject); //destroy the rock

					for(int i = 0; i < 4;i++){
						SpawnSmallRock(this.transform.position.x,this.transform.position.y);
					}

				}
		}
	}

	void SpawnSmallRock(float x, float y){
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		GameObject sRock = (GameObject)Instantiate(SmallRock);
		//mRock.transform.position = new Vector2(Random.Range(min.x,max.x),Random.Range(min.y,max.y));
		sRock.transform.position = new Vector2(x,y);

	}







}
