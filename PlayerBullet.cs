using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

	private float projectileVelocity;
	public GameObject ExplosionGO; // this is the explosion prefab
	//private Vector2;
	// Use this for initialization
	GameObject player;
	Vector3 playerDirection;
	Transform playerTransform;
	void Start () {
		projectileVelocity = 10;
		player = GameObject.Find("Player");
		playerDirection = new Vector3(0,0,0);
		playerDirection = player.transform.up;
	}
	
	// Update is called once per frame
	void Update () {
		CheckBounds ();
		transform.Translate (playerDirection * Time.deltaTime * projectileVelocity);
	}

	void OnTriggerEnter2D(Collider2D col){
		// detect collision with rock or a bullet
		if ((col.tag == "RockTag") || (col.tag == "RockMedTag") || (col.tag == "RockSmallTag")) {
			PlayExplosion ();
			Destroy (gameObject); //destroy the spacehip
		}

	}

	//instantiate an explosion
	void PlayExplosion(){
		GameObject explosion = (GameObject)Instantiate(ExplosionGO);	
		explosion.transform.position = transform.position;
	}


	void CheckBounds()
	{
		var cam = Camera.main;
		Vector3 stageDimensions = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));

		// horizontal plane
		if (this.transform.position.x < -stageDimensions.x)
		{
			Destroy (gameObject);	
		}

		if (this.transform.position.x > stageDimensions.x)
		{
			Destroy (gameObject);
		}

		// vertical plane

		if (this.transform.position.y < -stageDimensions.y)
		{
			Destroy (gameObject);
		}

		if (this.transform.position.y > stageDimensions.y)
		{
			Destroy (gameObject);
		}
	}



}
