using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmallController : MonoBehaviour {

	const int MaxLives = 1; // maximum player lives
	int smallrock_lives; //current player lives

	float smallSpeed; // for rock speed
	float smallxShift; // the shift in x position on initialise
	float smallyShift; // the shift in y position on initialisation
	public GameObject ExplosionGO; // this is the explosion prefab

	// Use this for initialization
	void Start () {
		smallrock_lives = MaxLives;
		smallSpeed = 0.06f; //set speed

		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
		smallxShift = Random.Range (min.x, max.x);
		smallyShift = Random.Range(min.y, max.y);
	}
	
	// Update is called once per frame
	void Update () {
		CheckBounds ();

		Vector2 position = transform.position;
		position = new Vector2 (position.x + smallxShift * Time.deltaTime * smallSpeed, position.y + smallyShift * Time.deltaTime * smallSpeed);

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
			smallrock_lives--;
			print ("a bubble");
			//print (" the og position "+this.transform.position);
			if(smallrock_lives == 0){				
					//PlayExplosion ();
					Destroy (gameObject); //destroy the rock
			}
		}
	}





}
