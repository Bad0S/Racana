using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PositionSetter : MonoBehaviour {
	private GameObject[] players;
	private Scene scenePlaying;
	public string nameScene;
	public string previousName;
	// Use this for initialization
	void Awake (){
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		DontDestroyOnLoad (gameObject);
		scenePlaying = SceneManager.GetActiveScene ();
		previousName = nameScene;
		nameScene = scenePlaying.name;
		players = GameObject.FindGameObjectsWithTag ("Player");
		print (players [0]);
		if(players [0] == gameObject){
			for (int i = 0; i < players.Length ; i++) {
				players[i].GetComponent <Player>().canMusic =players[0].GetComponent <Player>().canMusic;
				players[i].GetComponent <Player>().inDanger =players[0].GetComponent <Player>().inDanger;
				players[i].GetComponent <PositionSetter>().nameScene =players[0].GetComponent <PositionSetter>().nameScene;
				players[i].GetComponent <PositionSetter>().previousName =players[0].GetComponent <PositionSetter>().previousName;
				SetPosition (players[i]);
			}
		}
		

	}
	void SetPosition(GameObject player){ //no checkpoint yet, but implementable easily through bools

		if(nameScene == "Racana_Village" ){
			print ("bite");
			if(previousName == "Racana_Maison_chef"){
				player.transform.position = new Vector3 (-1336f, -204.6f, 0);
			}
			if(previousName == "Racana_Maison_hero"){
				player.transform.position = new Vector3 (-1387f, -656f, 0);
			}			
			if(previousName == "Racana_Foret"){
				player.transform.position = new Vector3 (-144f, -657.6f, 0);
			}
		}
		else if (nameScene == "Racana_Foret") {
			if (previousName == "Racana_Village") {
				player.transform.position = new Vector3 (-371f, -1629f, 0);
			}
			if (previousName == "Racana_Donjon_LD") {
				player.transform.position = new Vector3 (-247f, -1873.3f, 0);
			}
		}
		else if (nameScene == "Racana_Donjon_LD") {
			player.transform.position = new Vector3 (249f, -155f, 0);

		}
		else if (nameScene == "Racana_Tuto") {
			player.transform.position = new Vector3 (303f, 70f, 0);

		}
		else if(nameScene == "Racana_Maison_chef"){
			player.transform.position = new Vector3 (-185.96f, -116.67f, 0);
		}
		else if(nameScene == "Racana_Maison_hero"){
			player.transform.position = new Vector3 (-52.94f, -10.34f, 0);
		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}
