using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
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
	private Vector2 déplacement;
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

	//FightEnemies
	public bool grabbed;
	public float speedMultiplicator=1;

	//Dash
	public bool isDashing;
	public bool canDash = true;
	public float DashSpeed = 4;
	public Transform dashTarget;

	//Rythm
	public bool transcendance = false;

	public bool projectileShake;

	// Use this for initialization
	void Start () 
	{
		body = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		animAttaques = GetComponentInChildren<Animator> ();
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
			chargeAttaque = 0;
		}
		else if (chargeAttaque > beat && tauxCharge >= 4){
			tauxCharge = 1;
			chargeAttaque = 0;
		}
		//Le dash en transcendance
		dashTranscendanceTargeter.transform.localPosition = déplacement;
		dashTranscendanceTargeter.transform.localRotation = Quaternion.Euler (0, 0, ((Mathf.Atan2 (Input.GetAxisRaw ("Horizontal"), (Input.GetAxisRaw ("Vertical"))) * -Mathf.Rad2Deg)+90));
		Attack ();
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
				body.velocity = (déplacement*MovSpeed*speedMultiplicator);
				anim.SetBool ("IsIdle", false);
				anim.SetBool ("IsMoving", true);
				//float angle = (Mathf.Atan2(Input.GetAxisRaw("Horizontal"), (Input.GetAxisRaw("Vertical"))) * -Mathf.Rad2Deg);
				//body.transform.rotation = Quaternion.Euler(0, 0, angle);
			} else if (isDashing == false){
				body.velocity = Vector3.zero;

			}else
			{
				anim.SetBool ("IsIdle", true);
				anim.SetBool ("IsMoving", false);
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
				anim.SetTrigger ("Attack_Slash");
				StartCoroutine(slashCoroutine ());
               // soundCharge.start();
            }
			if (Input.GetButtonDown ("Fire2") && isAttacking == false) 
			{
				StartCoroutine (repousseCoroutine ());
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
		GetComponent<health> ().damage = tauxCharge;
		attaqueSlash.SetActive (true);
		isAttacking = true;
        canAttack = false;
		yield return new WaitForSeconds (GetComponent <Rythme>().timeBetweenBeatsInSeconds);
		attaqueSlash.SetActive (false);
		isAttacking = false;
		canAttack = true;

	}
	IEnumerator repousseCoroutine()
	{
       // soundRepousse.start();
		//float angleShoot = Mathf.Atan2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")) * Mathf.Rad2Deg;
		//GameObject projectileRepousse = (GameObject)Instantiate (attaqueRepousse, transform.position, Quaternion.Euler(0, 0, angleShoot));
		//projectileRepousse.GetComponent<Rigidbody2D> ().AddForce (Vector3.up, ForceMode2D.Impulse);

		//attaqueRepousse.SetActive (true);
		isAttacking = true;
        canAttack = false;
        yield return new WaitForSeconds (0.5f);
        //attaqueRepousse.SetActive (false);
        isAttacking = false;
		canAttack = true;

	}

	IEnumerator dashCoroutine()
	{
		canDash = false;
		isDashing=true;

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

		isDashing=false;


		yield return new WaitForSeconds (0.5f);
		canDash = true;
	}
}
