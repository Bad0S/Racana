using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardienPassage : MonoBehaviour {
	public GameObject player;
	public GameObject dialWith;
	public GameObject dialWithout;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.GetComponent <Player>().canMusic ==false){
			GetComponent <BoxCollider2D>().isTrigger = false;
			dialWith.SetActive (false);
			dialWithout.SetActive (true);
		}
		else{
			GetComponent <BoxCollider2D>().isTrigger = true;
			dialWith.SetActive (true);
			dialWithout.SetActive (false);

		}
	}
}
