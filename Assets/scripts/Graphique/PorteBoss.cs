using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class PorteBoss : MonoBehaviour {
	private bool activation;
	private float originalPos;
	public float descending;
	public float speed;
	public GameObject cacheBoss;
	public GameObject hitboxPorte;
	// Use this for initialization
	void Start () {
		originalPos = transform.position.y;

	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player"&& activation == false) 
		{
			activation = true;

			Camera.main.GetComponent<CameraBehaviour> ().ScreenShakeFunction (3.5f, 0.01f,0.08f);
			StartCoroutine (Vibration (3.5f, 0.5f));
		}
	}

	IEnumerator Vibration(float duree, float puissance){
		GamePad.SetVibration (0,puissance,0);// POUR LA FREQUECNE, DEMANDER A MICHAEL
		yield return new WaitForSeconds(duree);
		GamePad.SetVibration (0,0f,0f);

	}



	// Update is called once per frame
	void Update () {
		if (activation == true && descending <=1){
			descending += Time.deltaTime/(1/speed);
			transform.position = new Vector3(transform.position.x,Mathf.Lerp(originalPos, originalPos-3, descending),0);
		}
		else if(transform.position.y == originalPos-3){
			cacheBoss.GetComponent <CacheBossFader>().activation = true;
			hitboxPorte.GetComponent <BoxCollider2D>().enabled = false;
		}
	}
}
