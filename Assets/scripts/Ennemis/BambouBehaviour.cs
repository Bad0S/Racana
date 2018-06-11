using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BambouBehaviour : MonoBehaviour {

	private Animator anim;
	[SerializeField]private float speed;
	public GameObject herbeTir;
	[SerializeField]private float maxDetectionRange;
	public float attackRangeMax;
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
	private float angleShoot;

	//arrêt du saut et saut
	bool isJumping;
	public float vitesseBond =1;
	public float timerWaitRepousse=0;

	//Idle
	public bool idleCanMove;
	public bool idling;

	//fuite post morsure

	int damage =1;

    [FMODUnity.EventRef]
    public string selectsoundBambou;
    FMOD.Studio.EventInstance sndBambou;

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
	private Rythme rythmeScript;
	private int rythmeRange;
	public int rythmeRangeMax;
	private int counterRythme;

	void Start ()
	{
		rythmeRange = Random.Range (1, rythmeRangeMax +1);
		//print (rythmeRange); 

		target = GameObject.FindGameObjectWithTag ("Player");
		rythmeScript = target.GetComponent <Rythme> ();
		anim = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();

		shaderDeCouleur = Shader.Find("GUI/Text Shader");
		shaderDeBase = Shader.Find("Sprites/Default"); // or whatever sprite shader is being used

		enemyRenderer = GetComponent <SpriteRenderer>();
        sndBambou = FMODUnity.RuntimeManager.CreateInstance(selectsoundBambou);
        couleurDeBase = enemyRenderer.color;
		playerRB= target.GetComponent <Rigidbody2D>();

	}
	// Update is called once per frame
	void FixedUpdate(){
		beatAllowAttack = rythmeScript.isBeating;

	}
	void Update () 
	{
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
			anim.SetFloat ("YSpeed",1);
		}
		else if (rb2D.velocity.y>0){
			anim.SetFloat ("YSpeed",-1);

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
				StartCoroutine ("ShootSequence");
				
			}
		}

		//PATHFINDING
		if (targetVector.magnitude < maxDetectionRange && isFighting == false) {
			if (idling == true) 
			{
				StopCoroutine("MoveAndWait" ); 
			}

			anim.SetBool ("IsMoving", true);
			if (timerWaitRepousse > 0.35f && aEteRepousse == false) {
				rb2D.velocity = Vector3.Normalize (targetVector) * speed;
			} else if (aEteRepousse = false) {
				aEteRepousse = false;
				timerWaitRepousse = 0;
			}

		}
		anim.SetFloat ("XSpeed", targetVector.x);
		anim.SetFloat ("YSpeed", targetVector.y);
		//anim.SetBool ("Walking", true);
		if (targetVector.x < 0) {
			GetComponent<SpriteRenderer> ().flipX = true;
		} else {
			GetComponent<SpriteRenderer> ().flipX = false;
		}
		if (idleCanMove == true && isFighting == false)
		{
			StartCoroutine(MoveAndWait(Random.Range(0.7f,1.2f),Random.Range(1f,2f)));
		}
		else if (idling == false)
		{
			idleCanMove = true;
		}
	}
	IEnumerator MoveAndWait(float secMove,float secWait) // l'idle
	{
		idleCanMove = false;
		idling = true;
		rb2D.velocity = (new Vector2 (Random.Range(-0.3f*25f,0.3f*25f),Random.Range(-0.3f*25f,0.3f*25f)));
		anim.SetBool ("IsMoving", true);
		yield return new WaitForSeconds(secMove);
		rb2D.velocity = (new Vector2 (0,0));
		anim.SetBool ("IsMoving", false);
		yield return new WaitForSeconds (secWait);
		idleCanMove = true;
		idling = false;
	}

	IEnumerator ShootSequence()
	{
		yield return new WaitForSeconds (0.15f);
		//WhiteSprite ();
		isFighting = true;
		yield return new WaitForSeconds (0.16f);
		//NormalSprite ();
		yield return new WaitForSeconds (0.14f);
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds);
		anim.SetBool ("IsAttacking", true);
		//groupieSource.Play();

		//anim.SetTrigger ("Fighting");
		rb2D.velocity = new Vector3 (0,0,0);


		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds);
		angleShoot = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;
		anim.SetTrigger ("IsAttacking");

        sndBambou.start();
		GameObject herbeTirInstance = (GameObject)Instantiate (herbeTir, transform.position,Quaternion.Euler(0, 0, angleShoot/2));
		herbeTirInstance.SetActive (true);
		herbeTirInstance.transform.localScale = new Vector3 (1.2f, 1.2f, 1);
		herbeTirInstance.GetComponent <TirHerbe>().beat = rythmeScript.timeBetweenBeatsInSeconds;


		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds);

		//herbeTirInstance.GetComponent <RoarScript>().beat = rythmeScript.timeBetweenBeatsInSeconds;
		yield return new WaitForSeconds (rythmeScript.timeBetweenBeatsInSeconds);
		isFighting = false;
	}



	IEnumerator PlayerDamage(){

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
			isJumping = false;
			StartCoroutine (Knockback ());
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
		StopCoroutine ("ShootSequence"); //idée pour le knock: si player fait du dégât, dash arrière, si aucune dash côté, si player subidash avant

		GetComponent<BoxCollider2D> ().isTrigger = false;
		rb2D.velocity = Vector2.zero;
		rb2D.AddForce (new Vector2(-targetVector.x,-targetVector.y).normalized*0.8f,ForceMode2D.Impulse);
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
