using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteScript : MonoBehaviour {
	private Animator anim;
	public bool bite;
	private int damage= 1;
	// Use this for initialization
	void Start () {
		anim =GetComponent <Animator>();
	}

	void OnEnable () {
		if(bite == false){
			anim.SetTrigger ("StartBite");
			bite = true;
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") 
		{
			print ("test");
			other.gameObject.GetComponent<health>().Hurt(damage);
			other.GetComponent <Rigidbody2D>().velocity = Vector2.zero;
			other.GetComponent <Rigidbody2D>().AddForce (new Vector2( other.transform.position.x -transform.position.x,other.transform.position.y -transform.position.y).normalized*2f,ForceMode2D.Impulse);

		}
	}

}
