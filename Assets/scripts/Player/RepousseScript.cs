﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepousseScript : MonoBehaviour {
	public float beat=100;
	public float speed;
	public float speedGrowth=0.01f;
	private Rigidbody2D rb;
	public Vector3 direction;
	public ParticleSystem.MainModule particSysMain;
	public ParticleSystem particSys;
	public ParticleSystem.EmitParams particSysParams;


	// Use this for initialization
	void Awake () {
		Destroy (gameObject,  2);
		rb = GetComponent <Rigidbody2D> ();
		//particSys = GetComponentInChildren <ParticleSystem> ();
		print (particSys);
		particSysMain = particSys.main;
		particSysParams.startSize = 1;
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = direction * speed;
		transform.localScale = new Vector3 (transform.localScale.x + speedGrowth,transform.localScale.y + speedGrowth, 1); 

	}

	private void OnCollisionEnter2D(Collision2D other){
		print (other.gameObject);
	}
}
