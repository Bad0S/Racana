using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialogues : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player" &&GetComponent<DialogueComponent>().inDialogue == false ) 
		{
			if(Input.GetButtonDown ("Fire3")||Input.GetButtonDown ("Fire1")){
				GetComponent<DialogueComponent>().StartDialogue();

			}
		}
	}
}
