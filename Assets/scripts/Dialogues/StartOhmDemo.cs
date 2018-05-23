using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOhmDemo : MonoBehaviour {
	private bool once;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(once == false){
			GetComponent<DialogueComponent>().StartDialogue();
			once = true;

		}
	}
}
