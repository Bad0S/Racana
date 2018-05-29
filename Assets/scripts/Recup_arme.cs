using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recup_arme : MonoBehaviour {
	
	public GameObject Fauve;
	public GameObject Bambou;
	private Vector3 PosEnnemi1;
	private Vector3 PosEnnemi2;
	private Vector3 PosEnnemi3;
	private bool active_stele;

	// Use this for initialization
	void Start () {
		PosEnnemi1 = new Vector3 (-41f, -11f);
		PosEnnemi2 = new Vector3 (-33f, -11f);
		PosEnnemi3 = new Vector3 (-38f, -20f);
		active_stele = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerExit2D (Collider2D other){
		if (other.tag=="Player" && active_stele == false){
			
			print ("test stele");
			GameObject instance = Instantiate<GameObject> (Bambou,PosEnnemi1,Quaternion.identity);
			GameObject Instance = Instantiate<GameObject> (Bambou,PosEnnemi2,Quaternion.identity);
			GameObject INstance = Instantiate<GameObject> (Bambou,PosEnnemi3,Quaternion.identity);
			active_stele = true;

		}

}
}
