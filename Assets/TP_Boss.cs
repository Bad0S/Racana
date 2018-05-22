using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP_Boss : MonoBehaviour {
	public GameObject boss;
	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			other.transform.position = new Vector3 (9.5f, 94, 0);
			//SceneManager.LoadScene("Showcase");
			boss.SetActive (true);
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}
