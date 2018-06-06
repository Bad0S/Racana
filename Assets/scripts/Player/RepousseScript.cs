using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepousseScript : MonoBehaviour {
	public float beat;
	public float speed;
	public float speedGrowth=0.02f;
	private Rigidbody2D rb;
	public Vector3 direction;
	public ParticleSystem particSys;


	// Use this for initialization
	void Awake () 
	{
		if (!GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().canMusic) 
		{Destroy (gameObject, beat * 0.5f);}
		if (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().canMusic) 
		{Destroy (gameObject, beat);}
		if (GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().transcendance) 
		{Destroy (gameObject, beat *2);}
		rb = GetComponent <Rigidbody2D> ();
		//particSys = GetComponentInChildren <ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		rb.velocity = direction * speed;
		transform.localScale = new Vector3 (transform.localScale.x + speedGrowth,transform.localScale.y + speedGrowth, 1); 

	}

	private void OnCollisionEnter2D(Collision2D other){
	}
}
