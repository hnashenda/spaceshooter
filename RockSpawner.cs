using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour {

	public GameObject Enemy_Rock;
	public GameObject Enemy_Rock_Med;
	public GameObject Enemy_Rock_Small;
	public GameObject Player;
	GameObject myShip ;
	RectTransform rt;
	//Transform playerTransform;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnRock(float x, float y){
		//Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		//Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		GameObject aRock = (GameObject)Instantiate(Enemy_Rock);
		//aRock.transform.position = new Vector2(Random.Range(min.x,max.x),Random.Range(min.y,max.y));
		aRock.transform.position = new Vector2(x,y);

	}

	void SpawnRockMed(float x, float y){
		//Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		//Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		GameObject mRock = (GameObject)Instantiate(Enemy_Rock_Med);
		//aRock.transform.position = new Vector2(Random.Range(min.x,max.x),Random.Range(min.y,max.y));
		mRock.transform.position = new Vector2(x,y);

	}

	void SpawnRockSmall(float x, float y){
		//Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		//Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		GameObject sRock = (GameObject)Instantiate(Enemy_Rock_Small);
		//aRock.transform.position = new Vector2(Random.Range(min.x,max.x),Random.Range(min.y,max.y));
		sRock.transform.position = new Vector2(x,y);

	}


	//function to start rock spawner
	public void ScheduleRockSpawner(){
		//myShip = (GameObject)Instantiate(Player);
		myShip = GameObject.Find("Player");
		////playerTransform = myShip.transform;
		RectTransform rectTransform = myShip.GetComponent<RectTransform>();
		//rt = (RectTransform)myShip.transform;
		//var collider = myShip.collider as BoxCollider;

		BoxCollider2D collider = myShip.GetComponent<BoxCollider2D>();
		//print ("the x position "+myShip.transform.position.x +" the width "+ myShip.GetComponent<BoxCollider2D>().size.x +" and the height " + myShip.GetComponent<Renderer>().bounds.size.y );
		//float shipHalf = myShip.GetComponent<Renderer> ().bounds.size.x / 2;
		float shipHalf = (myShip.GetComponent<BoxCollider2D>().size.x)/2;
		float shipSplit = (myShip.GetComponent<BoxCollider2D>().size.y)/2;
		float shipLeft = myShip.transform.position.x - shipHalf;
		float shipRight =myShip.transform.position.x + shipHalf;

		float shipTop = myShip.transform.position.y + shipSplit;
		float shipBottom =myShip.transform.position.y - shipSplit;

		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
		float xPos;
		float yPos;

		for(int i = 0; i < 15;i++){

			xPos = Random.Range (min.x, max.x);
			yPos = Random.Range(min.y,max.y);

			//print ("the xpos: " + xPos + " Shipleft " + shipLeft + " shipRight: " + shipRight);
			if (i % 2 == 0) {
				if ((xPos < shipLeft) || (xPos > shipRight) && (yPos < shipBottom) || (yPos > shipTop)) {
					SpawnRockSmall (xPos, yPos);
				}
			}
			if (i % 2 == 0) {
				if ((xPos < shipLeft) || (xPos > shipRight) && (yPos < shipBottom) || (yPos > shipTop)) {
					SpawnRockMed (xPos, yPos);
				}
			}
			if((xPos < shipLeft) || (xPos > shipRight) && (yPos < shipBottom) || (yPos > shipTop)){
				SpawnRock (xPos,yPos);
			}
			else{
				//	print ("cannot spawn");
				//if (xPos < shipRight && xPos > shipLeft) {

				//}
				//if (xPos > shipLeft) {

				//}
				xPos = xPos + (shipHalf*2);
				yPos = yPos + (shipSplit * 2);
				//xPos = xPos + 15;
				//yPos = yPos + 15;
				SpawnRock (xPos,yPos);
			}


			//SpawnRock();
		}
	}

	//function to stop rock spwaner
	public void UnscheduleRockSpawner(){
		//CancelInvoke ("SpawnRock");
		//GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);
		GameObject[] GameObjects =  GameObject.FindGameObjectsWithTag ("RockTag");

		for (int i = 0; i < GameObjects.Length; i++)
		{
			Destroy(GameObjects[i]);
		}

		GameObject[] GameObjectsM =  GameObject.FindGameObjectsWithTag ("RockMedTag");

		for (int i = 0; i < GameObjectsM.Length; i++)
		{
			Destroy(GameObjectsM[i]);
		}

		GameObject[] GameObjectsSm =  GameObject.FindGameObjectsWithTag ("RockSmallTag");

		for (int i = 0; i < GameObjectsSm.Length; i++)
		{
			Destroy(GameObjectsSm[i]);
		}

	}


}
