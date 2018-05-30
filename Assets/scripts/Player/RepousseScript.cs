using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepousseScript : MonoBehaviour {
	public float beat=100;
	public float speed;
	public float speedGrowth=0.01f;
	private Rigidbody2D rb;
	public Vector3 direction;
	private Color alphaColor;
	float timerFade;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, beat * 2);
		rb = GetComponent <Rigidbody2D> ();
		alphaColor = GetComponent<SpriteRenderer>().color;
		alphaColor.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = direction * speed;
		transform.localScale = new Vector3 (transform.localScale.x + speedGrowth,transform.localScale.y + speedGrowth, 0); 
		timerFade += Time.deltaTime;

		GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, alphaColor,timerFade-beat);
	}

	private void OnCollisionEnter2D(Collision2D other){
		print (other.gameObject);
	}
}
