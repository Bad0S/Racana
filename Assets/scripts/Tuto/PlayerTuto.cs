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
	}
	
	// Update is called once per frame
	void Update () {
		dials [0].SetActive (true);

		if(enemy.GetComponent <health>().life<8){
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
		if(enemy.GetComponent<health> (). life == 1){
			Destroy (enemy);
			fader.SetActive (true);
			GetComponent <Player> ().hadTuto = true;
		}
	}

	IEnumerator dial3(){
		yield return new WaitForSeconds (0.1f);
		print (dials);
		if(enemy.GetComponent <EnemyBehaviour> ().timerWaitRepousse <01f ){
			dials [2].SetActive ((true));
			enemy.GetComponent <EnemyBehaviour> ().target = null;

		}
	}
}
