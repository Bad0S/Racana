﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poussiere : MonoBehaviour {

	private float floatHeight;
	private float liftForce;
	private float damping;
	private float old_pos ;
	public Rigidbody2D rb2D;
	public GameObject PoussiereParticule;
	public GameObject WaterParticule;
	public LayerMask layermaskSol;
	public LayerMask layermaskWater;


	void Start()
	{
		PoussiereParticule.SetActive (false);
		WaterParticule.SetActive (false);
	}
	void Update() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,10,layermaskSol);
		if (hit.collider != null) {
			if (old_pos < transform.position.x || old_pos > transform.position.x) {
				old_pos = transform.position.x;
				PoussiereParticule.SetActive(true);
			}
		}

		hit = Physics2D.Raycast(transform.position, -Vector2.up,10,layermaskWater);
		if (hit.collider != null) {
			if (old_pos < transform.position.x || old_pos > transform.position.x ){
				old_pos = transform.position.x;
				WaterParticule.SetActive(true);								 
			}
		}
	}
}
/*    public bool Sol;
	public GameObject PoussiereParticule;
	public GameObject WaterParticule;
    public Collider2D coll;
	private float old_pos ;

    void OnTriggerStay2D(Collider2D other)
    {
        Sol = true;
    }
    // Use this for initialization
    void Start()
    {
        PoussiereParticule.SetActive(false);
        WaterParticule.SetActive(false);

        old_pos = transform.position.x;
        //Check if the isTrigger option on th Collider2D is set to true or false
        if (coll.isTrigger)
        {
            if (Sol == true)

                PoussiereParticule.SetActive(true);
        }

		rb2D = GetComponent<Rigidbody2D>();

    }
*/