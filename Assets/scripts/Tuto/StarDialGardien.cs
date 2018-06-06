using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDialGardien : MonoBehaviour {
	public BoxCollider2D gardien;
	bool reading;
	public bool read;

	void Update () {
		if(reading == true &&GetComponent<DialogueComponent>().inDialogue == false ){
			read = true;
			GetComponent<DialogueComponent>().enabled = false;
		}
	}
	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" &&GetComponent<DialogueComponent>().inDialogue == false ) 
		{
				GetComponent<DialogueComponent>().StartDialogue();
			reading = true;

		}
	}
}
