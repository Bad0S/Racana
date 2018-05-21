using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OisoBehaviour : MonoBehaviour {
	public bool idleCanMove = true;
	public bool idling;
	private Rigidbody2D rb2D;
	private Animator anim;
	// Use this for initialization
	void Start () {
		rb2D = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		idleCanMove = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (rb2D.velocity.x > 0 ) {
			anim.SetFloat ("XSpeed", 1);
			anim.SetFloat ("LastX", 1);

		} else if(rb2D.velocity.x < 0) {
			anim.SetFloat ("XSpeed", -1);
			anim.SetFloat ("LastX", -1);
		}

		if (idleCanMove == true) 
		{
			StartCoroutine(MoveAndWait(1f,2f) ); 
		}
	}

	IEnumerator MoveAndWait(float secMove,float secWait) // l'idle
	{
		idleCanMove = false;
		idling = true;
		rb2D.velocity = (new Vector2 (Random.Range(-0.3f,0.3f),Random.Range(-0.3f,0.3f)));
		anim.SetBool ("IsMoving", true);
		yield return new WaitForSeconds(secMove);
		rb2D.velocity = (new Vector2 (0,0));
		anim.SetBool ("IsMoving", false);
		yield return new WaitForSeconds (secWait);
		idleCanMove = true;
		idling = false;
	}
}
