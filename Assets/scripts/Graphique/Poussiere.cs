﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poussiere : MonoBehaviour {
    public bool Sol;
	public ParticleSystem PoussiereParticule;
	public ParticleSystem WaterParticule;
    public Collider2D coll;
	private float old_pos ;

    void OnTriggerStay2D(Collider2D other)
    {
        Sol = true;
    }
    // Use this for initialization
    void Start()
    {
		old_pos = transform.position.x;
        //Check if the isTrigger option on th Collider2D is set to true or false
        if (coll.isTrigger)
        {
            if (Sol =true)
	        
			PoussiereParticule.Emit(1);
        }

		rb2D = GetComponent<Rigidbody2D>();

    }

	private float floatHeight;
	private float liftForce;
	private float damping;
	public Rigidbody2D rb2D;
	public LayerMask layermaskSol;
	public LayerMask layermaskWater;


	/*void Update() {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,10,layermaskSol);
		if (hit.collider != null) {
			if (old_pos < transform.position.x || old_pos > transform.position.x) {
				old_pos = transform.position.x;
				PoussiereParticule.EmitParams.startSize = 0.2f;
				PoussiereParticule.Emit (1);
				//var emitParams = PoussiereParticule.EmitParams ();
				//emitParams.startSize = 0.2f;

			}
		}

		hit = Physics2D.Raycast(transform.position, -Vector2.up,10,layermaskWater);
		if (hit.collider != null) {
			if (old_pos < transform.position.x || old_pos > transform.position.x ){
				old_pos = transform.position.x;
				var emitParams = new ParticleSystem.EmitParams();
				emitParams.startSize = 0.2f;
				WaterParticule.Emit (1);
								 
			}
		}
	}*/
}