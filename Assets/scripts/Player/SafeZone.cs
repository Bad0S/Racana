using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			other.GetComponent <Player> ().inDanger = !other.GetComponent <Player> ().inDanger;
			//SceneManager.LoadScene("Showcase");

		}
	}
}
