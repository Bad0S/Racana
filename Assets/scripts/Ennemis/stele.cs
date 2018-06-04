using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stele : MonoBehaviour {

	public Vector3 Pos1;
	public Vector3 Pos2;
	public Vector3 Pos3;
	public bool active;
	public GameObject bambou;


	// Use this for initialization
	void Start () {
		Pos1 = new Vector3 (-814f, 410);
		Pos2 = new Vector3 (-857f,301);
		Pos3 = new Vector3 (-1012, 390);
		active = false;
	}
	void OntriggerExit(Collider2D other) {
		
		if (other.tag == "Player" && active == false) {
			print ("spawn_stele");
			GameObject Bambou = (GameObject)Instantiate (bambou, Pos1 ,Quaternion.identity);
			GameObject Bambou2 = (GameObject)Instantiate (bambou, Pos1 ,Quaternion.identity);
			GameObject Bambou3 = (GameObject)Instantiate (bambou, Pos1 ,Quaternion.identity);
			active = true;

		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
