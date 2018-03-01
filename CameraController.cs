using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject PlayerGO; // reference to out game manager
	private GameObject thePlayer;

	public void Init(){
		thePlayer = (GameObject)Instantiate(PlayerGO);
		thePlayer.transform.position = new Vector2(0,0);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
