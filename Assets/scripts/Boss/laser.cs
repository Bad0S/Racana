using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laser : MonoBehaviour {
	//GameObject caster;
	public float beat = 1;
	int damage =1;

	void Start () {
		Destroy (gameObject, GetComponentInParent <patternTir>().beatLength);

	}
	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") 
		{
			other.gameObject.GetComponent<health>().Hurt(damage);
			//StartCoroutine (PlayerDamage ());
		}
	}
	// Update is called once per frame

}
