using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class BaseBehavior : MonoBehaviour {

	private Animator anim;
	[SerializeField]private float speed;
	[SerializeField]private GameObject attackRoar;
	[SerializeField]private float maxDetectionRange;
	[SerializeField]private float attackRangeMax;
	public GameObject target;
	private ContactFilter2D cFilter; 
	private Collider2D[] resultings = new Collider2D[1];

	private Rigidbody2D rb2D;
	private Vector3 targetVector;
	private bool isFighting = false ;

	//combat
	public float dashMultiplicator = 1;
	private float timerDegats;
	private Vector3 targetVectorAttacking;


	//arrêt du saut et saut
	bool isJumping;
	public float vitesseBond =1;
	public float timerWaitRepousse=0;

	//fuite post morsure

	int damage =1;

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

	void Start ()
	{
		rythmeRange = Random.Range (1, rythmeRangeMax +1);
		//print (rythmeRange); 
		rythmeScript = target.GetComponent <Rythme> ();
		//anim = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();
		//target = GameObject.FindGameObjectWithTag ("Player");

		shaderDeCouleur = Shader.Find("GUI/Text Shader");
		shaderDeBase = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used

		enemyRenderer = GetComponent <SpriteRenderer>();
	//	groupieSource = GetComponent<AudioSource>();
		couleurDeBase = enemyRenderer.color;
		playerRB= target.GetComponent <Rigidbody2D>();

	}
	// Update is called once per frame
	void Update () 
	{
		beatAllowAttack = rythmeScript.isBeating;
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


		if(rb2D.velocity.y <0){
			//anim.SetFloat ("YSpeed",1);
		}
		else if (rb2D.velocity.y>0){
			//anim.SetFloat ("YSpeed",-1);

		}
		timerWaitRepousse += Time.deltaTime;
		timerDegats += Time.deltaTime;
		if (timerWaitRepousse>0.35f && aEteRepousse == true){
			isFighting = false;
			aEteRepousse = false;
		}
		targetVector = target.transform.position -transform.position;

		if (targetVector.magnitude < attackRangeMax  && isFighting == false&&beatAllowAttack == true) {

			if(aEteRepousse == false){
				if(Random.Range (0,2)== 0){
					StartCoroutine ("BiteSequence");
				}
				else{
					StartCoroutine ("BiteSequence");
				}
			}
		}

		//PATHFINDING
		if (targetVector.magnitude < maxDetectionRange && isFighting == false) {
			
			//anim.SetBool ("IsMoving", true);
			if (timerWaitRepousse > 0.35f && aEteRepousse == false) {
				rb2D.velocity = Vector3.Normalize (targetVector) * speed;
			} else if (aEteRepousse = false) {
				aEteRepousse = false;
				timerWaitRepousse = 0;
			}
			//anim.SetBool ("Walking", true);
			if (targetVector.x > 0) {
				GetComponent<SpriteRenderer> ().flipX = true;
			} else {
				GetComponent<SpriteRenderer> ().flipX = false;
			}
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
		yield return new WaitForSeconds (0.15f);
		//WhiteSprite ();
		isFighting = true;
		yield return new WaitForSeconds (0.16f);
		//NormalSprite ();
		yield return new WaitForSeconds (0.14f);
		targetVectorAttacking = targetVector;
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds);
		//anim.SetBool ("IsAttacking", true);
		//groupieSource.Play();

		//anim.SetTrigger ("Fighting");
		rb2D.velocity = new Vector3 (0,0,0);
		isJumping = true;
		rb2D.AddForce (targetVectorAttacking * vitesseBond, ForceMode2D.Impulse);
		//Instantiate (attackHitbox, transform);
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds);
		rb2D.velocity = new Vector3 (0,0,0);
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds);
		isJumping = false;

		print (targetVectorAttacking);
		rb2D.AddForce ((new Vector3 ((targetVectorAttacking.x+Random.Range (-targetVectorAttacking.x*0.3f,targetVectorAttacking.x*0.3f )),(targetVectorAttacking.y+Random.Range (-targetVectorAttacking.y*0.3f,targetVectorAttacking.y*0.3f )),0) * vitesseBond), ForceMode2D.Impulse);
		//print (new Vector3 ((targetVectorAttacking.x+Random.Range (-targetVectorAttacking.x*2f,targetVectorAttacking.x*2f )),(targetVectorAttacking.y+Random.Range (-targetVectorAttacking.y*2f,targetVectorAttacking.y*02f )),0));
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds);
		isFighting = false;
		//anim.SetBool ("IsAttacking", false);
	}

	IEnumerator RoarSequence()
	{
		isFighting = true;
		//anim.SetTrigger ("Fighting");
		yield return new WaitForSeconds (2.3f);
		//	Instantiate (attackHitbox, transform);
		yield return new WaitForSeconds (1);
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
			if(isJumping == true){
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
