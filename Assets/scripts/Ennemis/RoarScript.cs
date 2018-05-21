using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class RoarScript : MonoBehaviour {
	int damage =1;
	bool canShake;
	public float beat;
	public float speedGrowth=0.1f;
	private Color alphaColor;
	public float timerFade;
	private bool startFade = false;


	// Use this for initialization
	void Start () {
	//	beat = GetComponentInParent <TigreBehavior> ().rythmeScript.timeBetweenBeatsInSeconds;
		Destroy (gameObject,beat*4);
		canShake = true;
		//transform.SetParent (transform);
		alphaColor = GetComponent<SpriteRenderer>().color;
		alphaColor.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = new Vector3 (transform.localScale.x + speedGrowth,transform.localScale.y + speedGrowth, 0); 
		timerFade += Time.deltaTime;
		//print (startFade);

		if (startFade = false) {
			print (startFade);

			if (timerFade > beat * 3) {
				timerFade = 0;
				startFade = true;
			} 
		}
		else {
				//print (startFade);

			GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, alphaColor,timerFade);


		}

		//

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			
			other.gameObject.GetComponent<health>().Hurt(damage);

			StartCoroutine (PlayerDamage ());
		
		}
	}
	IEnumerator PlayerDamage(){
		if(canShake == true){
			print ("test");

			Camera.main.GetComponent<CameraBehaviour> ().ScreenShakeFunction (0.14f, 0.02f,0.04f);
			StartCoroutine (Vibration (0.07f, 0.6f));
			canShake = false;
		}
		//playerRB.velocity = Vector2.zero;
		//playerRB.AddForce (new Vector2(targetVector.x,targetVector.y).normalized*2f,ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.10f);
		canShake = true;
	}
	IEnumerator Vibration(float duree, float puissance){
		GamePad.SetVibration (0,puissance,puissance);
		yield return new WaitForSeconds(duree);
		GamePad.SetVibration (0,0f,0f);
	}
}
