using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDialTuto : MonoBehaviour {
	public bool entered;
	bool dialEnd;
	public BoxCollider2D faderBox;
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	}
	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" &&GetComponent<DialogueComponent>().inDialogue == false ) 
		{
			if(other.GetComponent <Player>().hadTuto == false){
				entered = true;

				GetComponent<DialogueComponent>().StartDialogue();
				dialEnd = true;

			}
		}
		if(dialEnd == true &&GetComponent<DialogueComponent>().inDialogue == false ){
			faderBox.enabled = true;
		}
	}

}
