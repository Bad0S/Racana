using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PositionSetter : MonoBehaviour {
	private GameObject[] players;
	private Scene scenePlaying;
	private string nameScene;
	private string previousName;
	// Use this for initialization
	void Start () {
		scenePlaying = SceneManager.GetActiveScene ();
		previousName = nameScene;
		nameScene = scenePlaying.name;
		players = GameObject.FindGameObjectsWithTag ("Player");
		print (players .Length);

		for (int i = 1; i < players.Length ; i++) {
			Destroy (players [i]);
		}

		Camera.main.GetComponent <CameraBehaviour>().target = GameObject.FindWithTag ("Player").transform;

	}
	void SetPosition(){ //no checkpoint yet, but implementable easily through bools

		if(name == "Racana_Village"){
			if(previousName == "Racana_Maison_chef"){
				transform.position = new Vector3 (-1336f, -204.6f, 0);
			}
			if(previousName == "Racana_Maison_hero"){
				transform.position = new Vector3 (-1387f, -552f, 0);
			}			
			if(previousName == "Racana_Foret"){
				transform.position = new Vector3 (-144f, -657.6f, 0);
			}
		}
		else if (name == "Racana_Foret") {
			if (previousName == "Racana_Village") {
				transform.position = new Vector3 (-371f, -1629f, 0);
			}
			if (previousName == "Racana_Donjon_LD") {
				transform.position = new Vector3 (-247f, -1873.3f, 0);
			}
		}
		else if (name == "Racana_Donjon_LD") {
			transform.position = new Vector3 (249f, -155f, 0);

		}
		else if (name == "Racana_Tuto") {
			transform.position = new Vector3 (303f, 70f, 0);

		}
		else if(name == "Racana_Maison_chef"){
			transform.position = new Vector3 (-185.96f, -116.67f, 0);
		}
		else if(name == "Racana_Maison_hero"){
			transform.position = new Vector3 (-52.94f, -10.34f, 0);
		}

	}

	// Update is called once per frame
	void Update () {
		
	}
}
