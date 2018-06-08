using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDashTuto : MonoBehaviour {
	public bool RBActivated;
	ParticleSystem.MainModule particRBMain;
	ParticleSystem.EmissionModule particRBEmit;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			particRBMain = other.GetComponent <Player> ().RB.GetComponent <ParticleSystem> ().main;
			particRBEmit = other.GetComponent <Player> ().RB.GetComponent <ParticleSystem> ().emission;
				
			if (other.GetComponent <Player> ().canMusic == true && RBActivated == false) {
				other.GetComponent <Player> ().RB.SetActive (true);				
				particRBMain.startColor = Color.red;
				particRBMain.startLifetime = 0.2f;
				particRBEmit.rateOverTime = 5;
				RBActivated = true;
			}
 
		}
	}
}
