using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// reference to gameObjects
	public GameObject playerShip;
	public GameObject playButton;
	public GameObject rockSpawner; // reference to rock spawner
	public GameObject GameOverGO; // reference to game over image

	public enum GameManagerState{
		Opening,
		GamePlay,
		GameOver,
	}
	GameManagerState GMState;
	// Use this for initialization
	void Start () {
		GMState = GameManagerState.Opening;

	}
	
	// Update GameManager State
	void UpdateGameManagerState () {

		switch (GMState) {
		case GameManagerState.Opening:
			GameOverGO.SetActive(false);
			//rockSpawner.GetComponent<RockSpawner>().ScheduleRockSpawner();
			print ("expand your brain");
			playButton.SetActive(true);

			break;
		case GameManagerState.GamePlay:
			// hide play button
			playButton.SetActive(false);

			// set ship to avtive
			playerShip.GetComponent<PlayerController>().Init();

			rockSpawner.GetComponent<RockSpawner>().ScheduleRockSpawner();

			break;
		case GameManagerState.GameOver:

			GameOverGO.SetActive(true);
			rockSpawner.GetComponent<RockSpawner>().UnscheduleRockSpawner ();

			Invoke ("ChangeToOpeningState",2.0f);

			break;

		}
		
	}

	// function to set game manager state

	public void SetGameManagerState(GameManagerState state){

		GMState = state;
		UpdateGameManagerState ();
	} 

	// when play button is called
	public void StartGamePlay(){

		GMState = GameManagerState.GamePlay;
		UpdateGameManagerState ();
	} 

	//function to change Gamemanager to opening state
	public void ChangeToOpeningState(){
		SetGameManagerState (GameManagerState.Opening);
	}
}
