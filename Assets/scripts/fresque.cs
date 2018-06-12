using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fresque : MonoBehaviour {
	public GameObject texte1;
	bool started;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
		if(started == false){
			Invoke ("textPlay", 0.8f);
			started = true;
		}
	}
	void textPlay(){
		texte1.GetComponent <DialogueComponent>().StartDialogue ();	

	}
}
