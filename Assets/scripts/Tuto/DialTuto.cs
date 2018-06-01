using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialTuto : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		GetComponent<DialogueComponent>().StartDialogue();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
