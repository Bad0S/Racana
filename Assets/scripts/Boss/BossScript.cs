using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class BossScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerAttack") 
		{

			GetComponent <health> ().Hurt (other.GetComponentInParent<health> ().damage);

			Camera.main.GetComponent<CameraBehaviour> ().ScreenShakeFunction (0.05f, 0.025f,0.01f);
			StartCoroutine (Vibration (0.05f, 0.012f));

		}
	}
	IEnumerator Vibration(float duree, float puissance){
		GamePad.SetVibration (0,puissance,puissance);
		yield return new WaitForSeconds(duree);
		GamePad.SetVibration (0,0f,0f);

	}

}
