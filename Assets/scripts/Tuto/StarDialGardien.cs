using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDialGardien : MonoBehaviour {
	public BoxCollider2D gardien;
	bool reading;
	public bool read;
	private Animator anim;
	bool startAnim;
	void Start(){
		anim = GetComponentInParent <Animator> ();

	}

	void Update () {
		if(reading == true &&GetComponent<DialogueComponent>().inDialogue == false ){
			read = true;
			GetComponent<DialogueComponent>().enabled = false;
		}
	}
	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && GetComponent<DialogueComponent>().inDialogue == false && read == false ) 
		{
			anim.SetBool ("isActive", true);
			GetComponent<DialogueComponent>().StartDialogue();
			reading = true;

		}
	}
}
