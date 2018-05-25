using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoarScript : MonoBehaviour {
	int damage =1;
	bool canShake;
	public float beat;
	public float speedGrowth=0.1f;
	private Color alphaColor;
	private float timerFade;
	private bool startFade = false;


	// Use this for initialization
	void Start () {
	//	beat = GetComponentInParent <TigreBehavior> ().rythmeScript.timeBetweenBeatsInSeconds;
		Destroy (gameObject,beat*2);
		print ((beat)); 
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


		GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, alphaColor,timerFade-beat);

		//

	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") 
		{
			
			other.gameObject.GetComponent<health>().Hurt(damage);
			if(other.GetComponent <Player>().isDashing== false){

			}
		
		}
	}

}
