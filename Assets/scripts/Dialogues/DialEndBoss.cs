using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialEndBoss : MonoBehaviour {
	public GameObject withKill;
	public GameObject withoutKill;
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(player.GetComponent <Player>().killedBoss == true){
			withKill.SetActive (true);
			withoutKill.SetActive (false);
		}
		else{
			withKill.SetActive (false);
			withoutKill.SetActive (true);
		}
	}
}
