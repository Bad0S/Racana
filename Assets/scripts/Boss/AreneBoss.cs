using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreneBoss : MonoBehaviour {
	public patternTir boss;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") {
			GetComponent <EdgeCollider2D>().enabled = true;
			//zoneMurs.SetActive (true);
			boss.enabled = true;
			//GetComponent <BoxCollider>().enabled = false;

		}
	}
}
