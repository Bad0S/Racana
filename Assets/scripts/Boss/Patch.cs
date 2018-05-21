using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patch : MonoBehaviour {
	private Animator anim;
	[SerializeField]private float speed;
	//[SerializeField]private GameObject attackHitbox;
	[SerializeField]private float maxAggroRange;
	public GameObject target;
	private ContactFilter2D cFilter; 
	private Collider2D[] resultings = new Collider2D[1];

	private Rigidbody2D rb2D;
	private Vector3 targetVector;
	private bool isFighting = false ;

	//attraction
	public float attractionStrength;
	private Rigidbody2D rbTarget;

	void Start () {
		anim = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();
		rbTarget = target.GetComponent<Rigidbody2D> ();
	}
	// Update is called once per frame
	void Update () {
		if (isFighting == false) 
		{
			targetVector = target.transform.position -transform.position;
			rb2D.velocity = Vector3.Normalize(targetVector)*speed;
			anim.SetBool ("Walking", true);
			if (targetVector.x > 0) {
				GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				GetComponent<SpriteRenderer> ().flipX = false;
			}

		} else {
			anim.SetBool("Walking",false);
		}
		if (targetVector.magnitude < 1.2f  && isFighting == false) {
			isFighting = true;
			StartCoroutine ("FightSequence");
		}

	}

	void OnTriggerStay2D (Collider2D other){
		if (other.tag == "Player") {
			rbTarget.AddForce (new Vector2(-targetVector.x,-targetVector.y).normalized*attractionStrength,ForceMode2D.Force);
			print ("test");
		}
	}

	IEnumerator FightSequence()
	{
		anim.SetTrigger ("Fighting");
		yield return new WaitForSeconds (2.3f);
	//	Instantiate (attackHitbox, transform);
		yield return new WaitForSeconds (1);
		isFighting = false;
	}
}
