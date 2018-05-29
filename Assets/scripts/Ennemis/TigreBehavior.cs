using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TigreBehavior : MonoBehaviour {

	private Animator anim;
	[SerializeField]private float speed;
	public GameObject attackRoar;
	[SerializeField]private float maxDetectionRange;
	[SerializeField]private float attackRangeMax;
	public GameObject target;
	private ContactFilter2D cFilter; 
	private Collider2D[] resultings = new Collider2D[1];

	private Rigidbody2D rb2D;
	private Vector3 targetVector;
	public bool isFighting = false ;

	//pathfinding Frankenstein
	float timerRandAngle;
	float angle;
	Vector3 moveVector;

	//combat
	public float dashMultiplicator = 1;
	private float timerDegats;
	private Vector3 targetVectorAttacking;
	private EdgeCollider2D head;
	private int probaAttaque;
	private int probaPattern;
	private float timerChase;
	bool chase;

	//morsure
	public GameObject bite;

	//arrêt du saut et saut
	bool isJumping;
	public float vitesseBond =1;
	public float timerWaitRepousse=0;

	//fuite post morsure

	int damage =1;

	//SON
	//public List<AudioClip> groupieClips;
	//private AudioSource groupieSource;

	//ATTENTION IL VA ATTAQUER
	private SpriteRenderer enemyRenderer;
	private Shader shaderDeCouleur;
	private Shader shaderDeBase;
	public Color couleurDeBase;
	private Color couleurRougeShader;
	private Color couleurBlancheShader;
	public float opacityShader=0f;
	private Rigidbody2D playerRB;

	//feedback
	public bool aEteRepousse;

	// le rythme
	public bool beatAllowAttack;
	public Rythme rythmeScript;
	private int rythmeRange;
	public int rythmeRangeMax;
	private int counterRythme;

	//un flip propre
	private float timerFlip;
	private float pastFlip;

	void Start ()
	{
		rythmeRange = Random.Range (1, rythmeRangeMax +1);
		//print (rythmeRange); 
		rythmeScript = target.GetComponent <Rythme> ();
		anim = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();
		//target = GameObject.FindGameObjectWithTag ("Player");

		shaderDeCouleur = Shader.Find("GUI/Text Shader");
		shaderDeBase = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used

		enemyRenderer = GetComponent <SpriteRenderer>();
	//	groupieSource = GetComponent<AudioSource>();
		couleurDeBase = enemyRenderer.color;
		playerRB= target.GetComponent <Rigidbody2D>();
		head = GetComponent <EdgeCollider2D> ();
	}
	// Update is called once per frame
	void Update () 
	{
		timerFlip += Time.deltaTime;
		timerRandAngle += Time.deltaTime;
		timerWaitRepousse += Time.deltaTime;
		timerDegats += Time.deltaTime;
		timerChase += Time.deltaTime;
		beatAllowAttack = rythmeScript.isBeating;
		//print (rb2D.velocity);
		/*if (beatAllowAttack == true){
			if(counterRythme <= rythmeRangeMax){

				if(counterRythme != rythmeRange){
					beatAllowAttack = false;
				}
				counterRythme++;

			}
			else{
				counterRythme = 1;
			}
		}*/

		print (rythmeScript.timeBetweenBeatsInSeconds);
		if (timerWaitRepousse>0.35f && aEteRepousse == true){
			isFighting = false;
			aEteRepousse = false;
		}
		targetVector = target.transform.position -bite.transform.position;
		if(timerRandAngle>0.5f){
			angle = Random.Range (-30, 31);
			moveVector = targetVector;
			timerRandAngle = 0;
		}
		if (targetVector.magnitude < attackRangeMax  && isFighting == false &&beatAllowAttack == true) {

			if(aEteRepousse == false){
				if (targetVector.magnitude<11.5f&& timerChase>rythmeScript.timeBetweenBeatsInSeconds*4){
					chase = true;
					timerChase = 0;
				}

				float test= targetVector.normalized.magnitude*10;
				if(test + probaPattern> 5|| chase == true){
					StartCoroutine ("BiteSequence");
					probaPattern -= Random.Range (1, 3);
					chase = false;
				}
				else{
					StartCoroutine ("RoarSequence");
					probaPattern += Random.Range (1, 3);

				}
			}
		}

		//PATHFINDING
		if (targetVector.magnitude < maxDetectionRange && isFighting == false) {
			
			anim.SetBool ("IsMoving", true);
			if (timerWaitRepousse > 0.35f && aEteRepousse == false) {

				rb2D.velocity = Vector3.Normalize (Quaternion.Euler (0, 0, angle)*moveVector) * speed;
			} else if (aEteRepousse = false) {
				aEteRepousse = false;
				timerWaitRepousse = 0;
			}
			anim.SetBool ("IsMoving", true);

		}
		else if(isFighting == false) {
			anim.SetBool ("IsMoving", false);
			anim.SetBool ("IsJumping",false);
			anim.SetBool ("IsAttacking",false);

			anim.SetBool ("IsIdle", true);
		}
		if(timerFlip >0.5f&&rb2D.velocity.x != pastFlip ){
			if (rb2D.velocity.x > 0f ) {
				transform.rotation = Quaternion.Euler(0,180,0);		

			} else  {
				transform.rotation =Quaternion.Euler(0,-0,0);		
			}
			pastFlip = rb2D.velocity.x;
			timerFlip = 0;
		}

		/*if (Physics2D.OverlapCollider (GetComponent<Collider2D> (), cFilter, resultings) > 0) {
			if (resultings [0].tag == "AttackHitbox") {
				Vector3 KnockbackVector = Vector3.Normalize(transform.position - resultings[0].transform.position);
				StartCoroutine (Hurt(KnockbackVector,resultings[0].GetComponent<AttackEffect>())); 
			}
		}*/			

	}

	IEnumerator BiteSequence()
	{
		isFighting = true;
		rb2D.velocity = new Vector3 (0,0,0);
		anim.SetBool ("IsMoving", false);
		anim.SetBool ("IsIdle", true);

		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds*0.5f);
		GetComponent<SpriteRenderer> ().flipX = true;
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds*0.25f);
		GetComponent<SpriteRenderer> ().flipX = false;
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds*0.5f);
		GetComponent<SpriteRenderer> ().flipX = true;
		int test = Random.Range (0, 10);
		if(test - probaAttaque<7){
			probaAttaque -= 1;
			anim.SetBool ("IsIdle", false);

		}
		else{
			probaAttaque += 2;
			isFighting = false;

			yield break;

		}

		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds*0.25f);
		WhiteSprite ();
		anim.SetBool ("IsMoving", true);
		targetVectorAttacking = targetVector;

	//	yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds*0.25f);


		NormalSprite ();
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds*0.25f);

		//anim.SetBool ("IsAttacking", true);
		//groupieSource.Play();
		anim.SetBool ("IsMoving", false);
		bite.SetActive (true);
		bite.GetComponent <BiteScript> ().bite = false;
		//anim.SetBool ("Fighting",true);
		rb2D.velocity = new Vector3 (0,0,0);
		isJumping = true;
		anim.SetBool ("IsJumping",true);
		rb2D.AddForce (targetVectorAttacking * vitesseBond, ForceMode2D.Impulse);

		//Instantiate (attackHitbox, transform);
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds/2);
		rb2D.velocity = new Vector3 (0,0,0);
		//anim.SetBool ("IsBiting", true);
		bite.SetActive (false);

		anim.SetBool ("IsJumping",false);

		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds/4);
		//anim.SetBool ("IsBiting", false);

		isJumping = false;

		//print (Quaternion.Euler (0, 0, 90)*targetVectorAttacking);
		//anim.SetBool ("IsJumping", true);
		//rb2D.AddForce ( Quaternion.Euler (0, 0, 90)*targetVector*vitesseBond/2, ForceMode2D.Impulse);
		//rb2D.AddForce ((new Vector3 ((targetVector.y+Random.Range (-targetVector.y*0.1f,targetVector.y*0.1f )),(targetVector.x+Random.Range (-targetVector.x*0.1f,targetVector.x*0.1f )),0) * vitesseBond), ForceMode2D.Impulse);
		//print (new Vector3 ((targetVectorAttacking.x+Random.Range (-targetVectorAttacking.x*2f,targetVectorAttacking.x*2f )),(targetVectorAttacking.y+Random.Range (-targetVectorAttacking.y*2f,targetVectorAttacking.y*02f )),0));
		//yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds*1f);
		isFighting = false;
	//	anim.SetBool ("IsJumping",false);
		//anim.SetBool ("IsAttacking", false);
	}

	IEnumerator RoarSequence()
	{
		isFighting = true;

		anim.SetBool ("IsMoving", false);

		anim.SetBool ("IsAttacking",true);

		//anim.SetBool ("IsAttack", true);
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds*1.35f);
		GameObject laserInstance = (GameObject)Instantiate (attackRoar, transform);
		laserInstance.SetActive (true);
		laserInstance.GetComponent <RoarScript>().beat = rythmeScript.timeBetweenBeatsInSeconds;
		laserInstance.transform.SetParent (null);
		anim.SetBool ("IsAttacking",false);

		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds);
		isFighting = false;

	}



	//DEGATS
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "PlayerAttack") 
		{
			if (timerDegats > rythmeScript.timeBetweenBeatsInSeconds) {
				GetComponent <health> ().Hurt (target.GetComponentInParent<health> ().damage);
				timerDegats = 0;
			}
			isJumping = false;
			//StartCoroutine (Knockback ());
		}
	}

	//KNOCK
	IEnumerator Knockback(){
		timerWaitRepousse = 0;
		aEteRepousse = true;
		StopCoroutine ("FightSequence"); //idée pour le knock: si player fait du dégât, dash arrière, si aucune dash côté, si player subidash avant

		GetComponent<BoxCollider2D> ().isTrigger = false;
		rb2D.velocity = Vector2.zero;
		rb2D.AddForce (new Vector2(-targetVector.x,-targetVector.y).normalized*8f,ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.07f);
		yield return new WaitForSeconds(0.07f);

	}

	//FEEDBACKS
	void WhiteSprite() {
		enemyRenderer.material.shader = shaderDeCouleur;
		enemyRenderer.color = new Color(1f,0.9f,0.9f);
	}



	void NormalSprite() {
		enemyRenderer.material.shader = shaderDeBase;
		enemyRenderer.color = couleurDeBase;
	}

}
