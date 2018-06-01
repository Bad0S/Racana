using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;


public class Player: MonoBehaviour 
{
    //Sound
//    [FMODUnity.EventRef]
    public string selectsoundCharge;
    public string selectsoundAttaque;
    public string selectsoundRepousse;
    public string selectsoundDash;
   /* FMOD.Studio.EventInstance soundCharge;
    FMOD.Studio.EventInstance soundAttaque;
    FMOD.Studio.EventInstance soundRepousse;
    FMOD.Studio.EventInstance soundDash;*/


    //Visual
    public Animator anim;
	private Animator animAttaques;
	private SpriteRenderer render;
	private Shader shaderDeBase;


	//Move
	public Vector2 déplacement;
	public bool canMove = true;
	public float MovSpeed = 0.13f;

	//Physics
	private Rigidbody2D body;
	private Collider2D playerColl;

	//Fight
	public bool canAttack = true;
	public GameObject attaqueSlash;
	public GameObject attaqueRepousse;
	public GameObject dashTranscendanceTargeter;
	private bool isAttacking = false;
	public float chargeAttaque;
	public int tauxCharge=1;
	private float beat;

	//ANIM
	public bool inDanger;

	//FX
	public List<GameObject> paliers;

	//FightEnemies
	public bool grabbed;
	public float speedMultiplicator=1;

	//Dash
	public bool isDashing;
	public bool canDash = true;
	public float DashSpeed = 4;
	public Transform dashTarget;
	public List<GameObject>  dashFX;
	float timerDash;

	//Repousse
	public GameObject repousse;

	//Rythm
	public bool transcendance = false;

	public bool projectileShake;

	// Use this for initialization
	void Start () 
	{
		body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		//animAttaques = GetComponentInChildren<Animator> ();
		render = GetComponent<SpriteRenderer> ();
		playerColl = GetComponent<Collider2D> ();
		shaderDeBase = Shader.Find("Sprites/Default");
      /*  soundCharge = FMODUnity.RuntimeManager.CreateInstance(selectsoundCharge);
        soundAttaque = FMODUnity.RuntimeManager.CreateInstance(selectsoundAttaque);
        soundRepousse = FMODUnity.RuntimeManager.CreateInstance(selectsoundRepousse);
        soundDash = FMODUnity.RuntimeManager.CreateInstance(selectsoundDash);*/
		beat = GetComponent <Rythme> ().timeBetweenBeatsInSeconds;

    }

	// Update is called once per frame
	void Update () 
	{
		//charge sur beat
		//print (body.velocity);

		beat = GetComponent <Rythme> ().timeBetweenBeatsInSeconds;
		chargeAttaque += Time.deltaTime;
		if (chargeAttaque > beat && tauxCharge < 4) {

			tauxCharge++;
			DisableListElements (paliers);
			paliers [tauxCharge-1].SetActive (true);
			chargeAttaque = 0;
		}
		else if (chargeAttaque > beat && tauxCharge >= 4){
			tauxCharge = 1;
			DisableListElements (paliers);
			paliers [tauxCharge-1].SetActive (true);
			chargeAttaque = 0;
		}
		//Le dash en transcendance
		dashTranscendanceTargeter.transform.localPosition = déplacement;
		dashTranscendanceTargeter.transform.localRotation = Quaternion.Euler (0, 0, ((Mathf.Atan2 (Input.GetAxisRaw ("Horizontal"), (Input.GetAxisRaw ("Vertical"))) * -Mathf.Rad2Deg)+90));
		Attack ();
	//	DashFx ();
		//	Debug.Log (body.velocity.sqrMagnitude);
		if (body.velocity.sqrMagnitude > 2f) 
		{
			Physics2D.IgnoreLayerCollision (8, 11, true);
		}
		if (body.velocity.sqrMagnitude < 2f) 
		{
			Physics2D.IgnoreLayerCollision (8, 11, false);
		}

		if(projectileShake == true)
        {
			StartCoroutine (Vibration (0.07f, 0.6f));
			projectileShake = false;
		}
        if (GetComponent<Rythme>().isBeating == true)
        {
            chargeAttaque += 1;
        }
        if (chargeAttaque == 5)
        {
            chargeAttaque = 1;
        }
	}

	void FixedUpdate()
	{
		Move ();
		if(isDashing == true){
			timerDash -= Time.deltaTime;
		}

		//désactivation du collider lors du dash
	}

	void Move()
	{
		if (canMove == true  ) 
		{
			if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) 
			{
				déplacement = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
                déplacement = déplacement.normalized;
				if(DashSpeed*timerDash > 0.2f ){
					if(body.velocity.x <= ((MovSpeed*speedMultiplicator)+(DashSpeed*timerDash)) && body.velocity.x >=- ((MovSpeed*speedMultiplicator)+(DashSpeed*timerDash) )){
						if(body.velocity.y <= ((MovSpeed*speedMultiplicator)+(DashSpeed*timerDash)) && body.velocity.y >=- ((MovSpeed*speedMultiplicator)+(DashSpeed*timerDash)) )
							body.velocity += (déplacement*MovSpeed*speedMultiplicator);
					}
				}
				else{
					body.velocity = déplacement*MovSpeed*speedMultiplicator;
				}


				anim.SetBool ("IsIdleFighting", false);
				anim.SetBool ("IsIdle", false);
				anim.SetBool ("IsMoving", true);
				//float angle = (Mathf.Atan2(Input.GetAxisRaw("Horizontal"), (Input.GetAxisRaw("Vertical"))) * -Mathf.Rad2Deg);
				//body.transform.rotation = Quaternion.Euler(0, 0, angle);
			} else 
			{
				if (isDashing == false){
					body.velocity = Vector3.zero;
					print((Input.GetAxisRaw ("Horizontal") ) );


				}
				if(inDanger){
					anim.SetBool ("IsIdleFighting", true);
				}else{
					anim.SetBool ("IsIdle", true);

				}
				anim.SetBool ("IsMoving", false);
				anim.SetFloat ("LastX", déplacement.x);
				anim.SetFloat ("LastY", déplacement.y);
			}
			anim.SetFloat ("XSpeed", Input.GetAxisRaw ("Horizontal"));
			anim.SetFloat ("YSpeed", Input.GetAxisRaw ("Vertical"));
		}
	}

	void Attack()
	{
		if (isAttacking == false) 
		{
			attaqueSlash.transform.localPosition = déplacement*0.1f;
			attaqueSlash.transform.localRotation = Quaternion.Euler (0, 0, (Mathf.Atan2 (Input.GetAxisRaw ("Horizontal"), (-Input.GetAxisRaw ("Vertical"))) * Mathf.Rad2Deg));
			//attaqueRepousse.transform.localPosition = déplacement*0.1f;
			//attaqueRepousse.transform.localRotation = Quaternion.Euler (0, 0, (Mathf.Atan2 (Input.GetAxisRaw ("Horizontal"), (Input.GetAxisRaw ("Vertical"))) * -Mathf.Rad2Deg));
		}
		if (canAttack == true )
		{

			if (Input.GetButtonDown ("Fire1") && isAttacking == false) 
			{


				StartCoroutine(slashCoroutine ());


               // soundCharge.start();
            }

			if (Input.GetButtonDown ("Fire2") && isAttacking == false) 
			{


				StartCoroutine (repousseCoroutine ());
				anim.SetTrigger ("Attack_Repousse");


			}

			if (transcendance == true) 
			{
				/*for (float i = 0f; i < 1f; i += 0.1f) 
				{
					RaycastHit2D hit = Physics2D.Raycast (transform.position, new Vector2(déplacement.x - 0.5f + i,déplacement.y), 6f);
					if (hit.collider != null && hit.collider.tag == "Enemy") 
					{
						dashTarget = hit.transform;
					}
				}*/
			}
			if(canDash==true)
            {
				dashTranscendanceTargeter.transform.localPosition = déplacement;
				dashTranscendanceTargeter.transform.localRotation = Quaternion.Euler (0, 0, ((Mathf.Atan2 (Input.GetAxisRaw ("Horizontal"), (Input.GetAxisRaw ("Vertical"))) * -Mathf.Rad2Deg)+90));

				if (Input.GetButtonDown ("Fire3")&& déplacement != new Vector2(0f,0f)) 
				{
					StartCoroutine (dashCoroutine ());
				}
			}
		}
	}
		

	/*private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "EnemyShoot") 
		{

			//StartCoroutine (Vibration (0.07f, 0.6f));
		}
	}*/
	IEnumerator Vibration(float duree, float puissance)
    {
		GamePad.SetVibration (0,puissance,puissance);
		yield return new WaitForSeconds(duree);
		GamePad.SetVibration (0,0f,0f);
	}
	public void GrabUngrab()
	{
		if (grabbed == true) 
		{
			speedMultiplicator = 0.6f;	
		}
		else{
			speedMultiplicator = 1;
		}
	}
	IEnumerator backUpNormal(){
		yield return new WaitForSeconds (0.12f);
		render.material.shader = shaderDeBase;
		render.material.color = Color.white;
	}

	IEnumerator slashCoroutine()
    {
      //  soundAttaque.start();
     //  soundCharge.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
       // soundAttaque.start();
		anim.SetBool("IsAttacking", true);

		anim.SetTrigger ("Attack_Slash");

		GetComponent<health> ().damage = tauxCharge;
		attaqueSlash.SetActive (true);
		isAttacking = true;
        canAttack = false;
		yield return new WaitForSeconds (GetComponent <Rythme>().timeBetweenBeatsInSeconds);
		attaqueSlash.SetActive (false);
		isAttacking = false;
		canAttack = true;
		anim.SetBool("IsAttacking", false);

		//anim.ResetTrigger("Attack_Slash");
	}
	IEnumerator repousseCoroutine()
	{
		GameObject repousseInstance = (GameObject)Instantiate (repousse, (transform.position), Quaternion.identity);
		repousseInstance.GetComponent <RepousseScript>().beat = GetComponent <Rythme> ().timeBetweenBeatsInSeconds;

		repousseInstance.SetActive (true);
		repousseInstance.GetComponent<RepousseScript>().particSys.Emit ( 1);
		if (Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0) {
			repousseInstance.GetComponent <RepousseScript>().direction = déplacement ;
			repousseInstance.transform.localRotation = Quaternion.Euler (180, 0,- (Mathf.Atan2 (Input.GetAxisRaw ("Horizontal"), (Input.GetAxisRaw ("Vertical"))) * -Mathf.Rad2Deg));
			repousseInstance.transform.SetParent (transform);
			repousseInstance.transform.localPosition  = new Vector3 (repousseInstance.transform.localPosition.x + déplacement.x*30, repousseInstance.transform.localPosition.y+déplacement.y*30, repousseInstance.transform.localPosition.z);
			repousseInstance.transform.SetParent (null);
		}
		else{
			repousseInstance.GetComponent <RepousseScript>().direction = new Vector3 (0,1,0) ;
			repousseInstance.transform.localRotation = Quaternion.Euler (180, 0,0);
			repousseInstance.transform.SetParent (transform);
			repousseInstance.transform.localPosition  = new Vector3 (repousseInstance.transform.localPosition.x , 1+déplacement.y*30, repousseInstance.transform.localPosition.z);
			repousseInstance.transform.SetParent (null);
		}
		repousseInstance.GetComponent <Rigidbody2D>().freezeRotation = true;



		//attaqueRepousse.SetActive (true);
		isAttacking = true;
        canAttack = false;
        yield return new WaitForSeconds (0.5f);
        //attaqueRepousse.SetActive (false);
        isAttacking = false;
		canAttack = true;
		//anim.ResetTrigger ("Attack_Repousse");

	}


	IEnumerator dashCoroutine()
	{
		timerDash = 0.5f;
		canDash = false;
		isDashing=true;
		dashFX[0].SetActive  (true);
		dashFX[2].SetActive  (true);


		GetComponent <health> ().invincible = true;
		GetComponent <health> ().currentTime =GetComponent <health> ().invincibleTime - 1 ;
       // soundDash.start();
        if (transcendance == false||GetComponentInChildren <DashTranscendance> ().enemyList.Count == 0) 
		{
			body.AddForce (déplacement * DashSpeed, ForceMode2D.Impulse);
			print (déplacement);

		}
		else
		{
			Vector2 vecTmp = GetComponentInChildren <DashTranscendance> ().SelectEnemy (GetComponentInChildren <DashTranscendance> ().enemyList);
			body.AddForce ( vecTmp*7 , ForceMode2D.Impulse);
		}
		yield return new WaitForSeconds (0.5f);
		dashFX[0].SetActive  (false);
		dashFX[2].SetActive  (false);
		//dashFX[1].SetActive  (true);

		isDashing=false;

		//dashFX.SetActive (false);

		yield return new WaitForSeconds (0.5f);
		canDash = true;

		//dashFX[1].SetActive  (false);

	}
	void FlipX(){
		GetComponent <SpriteRenderer>().flipX = !GetComponent <SpriteRenderer>().flipX ;
	}
	void DisableListElements(List<GameObject> liste){
		for (int i = 0; i < liste.Count ; i++) {
			liste [i].SetActive (false) ;
		}

	}

}
