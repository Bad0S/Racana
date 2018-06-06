using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerTuto : MonoBehaviour {
	public List<GameObject> dials;
	public GameObject enemy;
	public GameObject fader;
	public GameObject loin;
	// Use this for initialization
	void Start () {
		Camera.main.GetComponent <CameraTuto>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		dials [0].SetActive (true);
		try{
			
		
		if(enemy.GetComponent <health>().life<4){
			dials [1].SetActive (true);
			enemy.GetComponent <EnemyBehaviour> ().target = loin;
		}
		if(dials[1].activeSelf == true && Input.GetButtonDown ("Fire2")){
			StartCoroutine (dial3 ());

		}
		if(dials[1].activeSelf == true && dials[1].GetComponent<DialogueComponent> ().inDialogue ==false){
			enemy.GetComponent <EnemyBehaviour> ().target = gameObject;

		}
		if(dials[2].activeSelf == true && dials[2].GetComponent<DialogueComponent> ().inDialogue ==false){
			enemy.GetComponent <EnemyBehaviour> ().target = gameObject;

		}
		}
		catch{}
		if(enemy== null){
			dials [3].SetActive ((true));
			fader.GetComponent <BoxCollider2D>().enabled = true;
			GetComponent <Player> ().hadTuto = true;
			Camera.main.GetComponent <CameraTuto>().enabled = true;

		}
	}

	IEnumerator dial3(){
		yield return new WaitForSeconds (0.1f);
		print (dials);
		if(enemy.GetComponent <EnemyBehaviour> ().timerWaitRepousse <02f ){
			dials [2].SetActive ((true));
			enemy.GetComponent <EnemyBehaviour> ().target = loin;

		}
	}
}
