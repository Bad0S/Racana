using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PositionSetter : MonoBehaviour {
	public List<GameObject> players;
	private Scene scenePlaying;
	public string nameScene;
	public string previousName;
	public bool positionReseted = false;
	public bool sceneSorted;
	// Use this for initialization
	void Awake (){
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
		DontDestroyOnLoad (gameObject);
		positionReseted = false;
		scenePlaying = SceneManager.GetActiveScene ();
		previousName = nameScene;
		nameScene = scenePlaying.name;
		players = GameObject.FindGameObjectsWithTag ("Player").ToList<GameObject>();
		if(players [players.Count -1] == gameObject){
			for (int i = 1; i < players.Count ; i++) {
				players[i].GetComponent <Player>().canMusic =players[0].GetComponent <Player>().canMusic;
				players[i].GetComponent <Player>().inDanger =players[0].GetComponent <Player>().inDanger;
				//players[i].GetComponent <PositionSetter>().nameScene =players[0].GetComponent <PositionSetter>().nameScene;
				players[i].GetComponent <PositionSetter>().previousName =players[0].GetComponent <PositionSetter>().previousName;
				print (players[i].GetComponent <PositionSetter>().previousName);
			}
			players[players.Count - 1].GetComponent <PositionSetter>().sceneSorted = true;
		}


	}
	void SetPosition(GameObject player){ //no checkpoint yet, but implementable easily through bools

		if(nameScene == "Racana_Village" ){
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

	void DestroyCopy(){
		players.RemoveAt (players.Count - 1);
		for (int i = 0; i < players.Count; i++) {
			players [i].SetActive (false);

		}
	}

	void Update () {
		if (sceneSorted) {
			SetPosition (players [players.Count - 1]);
			sceneSorted = false;
			players [players.Count - 1].GetComponent <PositionSetter> ().positionReseted = true;
			print (players.Count);
			print (positionReseted);
			if(players.Count>1&&positionReseted ==true){
				DestroyCopy ();
			}
		//	Destroy (players [1]);
		
		}


	}
}
