using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDashTutoOff : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			if(other.GetComponent <Player> ().RB.activeSelf == true && other.GetComponent <Player> ().isDashing == true){
				other.GetComponent <Player> ().RB.SetActive (false);

			}
		} 
	}
}
