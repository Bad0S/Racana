using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TirHerbe : MonoBehaviour {
	public float shootSpeed;
	public int damage = 1;
	public float beat;
	private Color alphaColor;
	private float timerFade;
	private bool startFade = false;

	// Use this for initialization
	void Start () {
		Destroy (gameObject,beat*2);
		alphaColor = GetComponent<SpriteRenderer>().color;
		alphaColor.a = 0;

	}
	
	// Update is called once per frame
	void Update () {
		timerFade += Time.deltaTime;
		GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, alphaColor,timerFade-beat);
		transform.Translate(transform.right * Time.deltaTime*shootSpeed);
	}
	void OnTriggerEnter2D( Collider2D  other)
	{
		if (other.tag == "Player") 
		{
			other.GetComponent <Player>().projectileShake = true;
			other.GetComponent<health> ().Hurt (damage);

			Destroy (gameObject);
		}
		//dans l'autre, on récupère dans player la fonction hurt

	}

		
}
