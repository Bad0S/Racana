using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreneBoss : MonoBehaviour {
	public patternTir boss;
	public List<GameObject> vieBoss;

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

			vieBoss [0].SetActive (true);
			vieBoss [1].SetActive (true);
			//GetComponent <BoxCollider>().enabled = false;

		}
	}
}
