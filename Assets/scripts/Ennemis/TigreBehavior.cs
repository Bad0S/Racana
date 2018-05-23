using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


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
	private bool isFighting = false ;

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
	private bool canShake=false;

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
		beatAllowAttack = rythmeScript.isBeating;
		print (rb2D.velocity);
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

		timerWaitRepousse += Time.deltaTime;
		timerDegats += Time.deltaTime;
		if (timerWaitRepousse>0.35f && aEteRepousse == true){
			isFighting = false;
			aEteRepousse = false;
		}
		targetVector = target.transform.position -transform.position;
		if(timerRandAngle>0.5f){
			angle = Random.Range (-30, 31);
			moveVector = targetVector;
			timerRandAngle = 0;
		}
		if (targetVector.magnitude < attackRangeMax  && isFighting == false&&beatAllowAttack == true) {

			if(aEteRepousse == false){
				int test= Random.Range(0,10);
				if(test + probaPattern< 5){
					StartCoroutine ("BiteSequence");
					probaPattern += Random.Range (1, 3);
				}
				else{
					StartCoroutine ("RoarSequence");
					probaPattern -= Random.Range (1, 3);

				}
			}
		}

		//PATHFINDING
		if (targetVector.magnitude < maxDetectionRange && isFighting == false && targetVector.magnitude>=attackRangeMax) {
			
			anim.SetBool ("IsMoving", true);
			if (timerWaitRepousse > 0.35f && aEteRepousse == false) {

				rb2D.velocity = Vector3.Normalize (Quaternion.Euler (0, 0, angle)*moveVector) * speed;
			} else if (aEteRepousse = false) {
				aEteRepousse = false;
				timerWaitRepousse = 0;
			}
			anim.SetBool ("IsMoving", true);

		}
		else if(targetVector.magnitude<attackRangeMax -1&& isFighting == false){
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
		if(timerFlip >0.3f&&rb2D.velocity.x != pastFlip ){
			if (rb2D.velocity.x > 0f ) {
				GetComponent<SpriteRenderer> ().flipX = false;
			} else  {
				GetComponent<SpriteRenderer> ().flipX = true;
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
		canShake = true;
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
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds*0.5f);
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


		WhiteSprite ();
		anim.SetBool ("IsMoving", true);
		targetVectorAttacking = targetVector;

		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds*0.25f);
		NormalSprite ();

		//anim.SetBool ("IsAttacking", true);
		//groupieSource.Play();
		anim.SetBool ("IsMoving", false);

		//anim.SetBool ("Fighting",true);
		rb2D.velocity = new Vector3 (0,0,0);
		isJumping = true;
		anim.SetBool ("IsJumping",true);
		if (targetVector.magnitude < maxDetectionRange){
			rb2D.AddForce (targetVectorAttacking * vitesseBond, ForceMode2D.Impulse);

		}
		//Instantiate (attackHitbox, transform);
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds/2);
		rb2D.velocity = new Vector3 (0,0,0);
		//anim.SetBool ("IsBiting", true);

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

	IEnumerator PlayerDamage(){
		if(canShake == true){
			Camera.main.GetComponent<CameraBehaviour> ().ScreenShakeFunction (0.14f, 0.02f,0.04f);
			StartCoroutine (Vibration (0.07f, 0.6f));
			canShake = false;
		}
		playerRB.velocity = Vector2.zero;
		playerRB.AddForce (new Vector2(targetVector.x,targetVector.y).normalized*2f,ForceMode2D.Impulse);
		yield return new WaitForSeconds(0.10f);
	}

	//DEGATS
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "PlayerAttack") 
		{
			if (timerDegats > 0.28f) {
				GetComponent <health> ().Hurt (target.GetComponentInParent<health> ().damage);
				timerDegats = 0;
			}
			Camera.main.GetComponent<CameraBehaviour> ().ScreenShakeFunction (0.05f, 0.03f,0.01f);
			StartCoroutine (Vibration (0.05f, 0.014f));
			//print ("zgeg");
			isJumping = false;
			//StartCoroutine (Knockback ());
		}
	}
	private void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player") 
		{
			if(isJumping == true&&other.otherCollider == head){
				print ("test");
				other.gameObject.GetComponent<health>().Hurt(damage);
				StartCoroutine (PlayerDamage ());

			}
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
	IEnumerator Vibration(float duree, float puissance){
		GamePad.SetVibration (0,puissance,puissance);
		yield return new WaitForSeconds(duree);
		GamePad.SetVibration (0,0f,0f);
	}
	void WhiteSprite() {
		enemyRenderer.material.shader = shaderDeCouleur;
		enemyRenderer.color = new Color(1f,0.9f,0.9f);
	}

	void RedSprite() {
		enemyRenderer.material.shader = shaderDeCouleur;
		enemyRenderer.color = new Color(0.95f,0,0);

	}

	void NormalSprite() {
		enemyRenderer.material.shader = shaderDeBase;
		enemyRenderer.color = couleurDeBase;
	}

}
