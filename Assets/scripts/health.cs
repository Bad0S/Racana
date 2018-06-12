using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XInputDotNetPure;


public class health : MonoBehaviour {
	public GameObject healItem;
	public int counterHeal;// compte les combos ici
	public static bool counterReset;// remet le compteur de combos à 0
	public int life = 1;
	public int damage = 1;
	public bool invincible;
	public float invincibleTime;
	public float currentTime;
	public List<Image> LifeBar;
	public Sprite pvVide;

	//feedback damage
	private Shader shaderDeCouleur;
	private Shader shaderDeBase;
	private SpriteRenderer rend;
	private Color couleurDeBase;
	private GameObject hitFX;
	bool hitFXRoutine;

    [FMODUnity.EventRef]
    public string selectsoundHurt;
    [FMODUnity.EventRef]
    public string selectPlayerHit;

    FMOD.Studio.EventInstance sndHurt;
	FMOD.Studio.EventInstance playerHit;

    private GameObject player;
	// Use this for initialization

	//bossDeath
	private Color alphaColor;
	private float timerFade;
	public bool startFade = false;
	public bool killedboss;

	public bool playerDiedededed;

	public GameObject porteBoss;

	void Start () 
	{
		shaderDeCouleur = Shader.Find("GUI/Text Shader");
		shaderDeBase = Shader.Find("Sprites/Default");
		rend = GetComponent <SpriteRenderer> ();
		couleurDeBase = rend.color;
		player = GameObject.FindGameObjectWithTag ("Player");
        sndHurt = FMODUnity.RuntimeManager.CreateInstance(selectsoundHurt);

		alphaColor = GetComponent<SpriteRenderer>().color;
		alphaColor.a = 0;
    }
	//si se fait soigner
	public void Heal( int lifeToGain)// la fonction pour soigner
	{
		life += lifeToGain;
		if(lifeToGain == 1){
			LifeBar [life-1].sprite = LifeBar [0].sprite;

		}
		if(lifeToGain == 2){
			LifeBar [life-1].sprite = LifeBar [0].sprite;
			LifeBar [life-2].sprite = LifeBar [0].sprite;

		}
	}
	//si prend du dégât
	public void Hurt( int lifeToLose)
	{
		//permet des frames d'invincibilité
		if (invincible == false) 
		{
			if (gameObject.tag == "Player") 
			{
                playerHit.start();
				LifeBar [life-1].sprite = pvVide;
				StartCoroutine (Vibration (0.08f, 0.8f));

				StartCoroutine (Damage (0.2f,0.14f, 0.02f));
				invincible = true;
				currentTime = 0;


			}
			if (gameObject.tag == "Enemy") 
			{
                sndHurt.start();
				StartCoroutine (Damage (0.2f,0.05f, 0.1f));
				if(GameObject.FindGameObjectWithTag ("Player").GetComponent <Player>().tauxCharge<4){
					StartCoroutine (Vibration (0.05f, 0.5f));

				}
				else{
					StartCoroutine (Vibration (0.05f, 0.9f));

				}
				StartCoroutine (PlayerHitFX (player));

				player.GetComponent <Rythme>().combo += lifeToLose ;
				player.GetComponent <Rythme>().timerCombo =0 ;
				player.GetComponent <Rythme>().timerComboSpeed =0 ;
				player.GetComponent <Rythme>().comboDecreaseSpeed =1 ;
			}
			if (gameObject.tag == "Boss") 
			{
				StartCoroutine (Damage (0.2f,0.05f, 0.1f));
				if(GameObject.FindGameObjectWithTag ("Player").GetComponent <Player>().tauxCharge<4){
					StartCoroutine (Vibration (0.05f, 0.7f));

				}
				else{
					StartCoroutine (Vibration (0.05f, 1f));

				}				StartCoroutine (PlayerHitFX (player));

				player.GetComponent <Rythme>().combo += lifeToLose ;
				player.GetComponent <Rythme>().timerCombo =0 ;
				player.GetComponent <Rythme>().timerComboSpeed =0 ;
				player.GetComponent <Rythme>().comboDecreaseSpeed =1 ;
				if(lifeToLose == 4){
					lifeToLose *= 2;
				}
			}
			life -= lifeToLose;
		}
		//fait drop un objet de soin
	}
		
	// Update is called once per frame
	void Update () 
	{
		if (life <= 0f)
		{

			if(gameObject.tag == "Enemy" ||gameObject.tag == "EnemyShoot")
			{
				//GameObject drop = (GameObject)Instantiate (healItem, transform.position, transform.rotation);
				try
				{
					if(GetComponent <EnemyBehaviour>().grabbed == true)
					{
						GameObject.FindGameObjectWithTag ("Player").GetComponent <Player>().grabbed = false ;
						GameObject.FindGameObjectWithTag ("Player").GetComponent <Player>().GrabUngrab () ;
					}
				}
				catch 
				{

				}
				if(hitFXRoutine== false){
					Destroy (gameObject);

				}

			}
			if ( gameObject.tag == "Boss"){
				if(gameObject.GetComponent <patternTir>().finalPhase ==1){
					life = 1;
				}
				else{
					if(	gameObject.GetComponent <patternTir>().finalPhase == 3);
					StartCoroutine (BossDeath(2));
				}

				//Events stylés et Fx!


			}
			if (gameObject.tag == "Player") 
			{
				if(playerDiedededed == false)
				{
					StartCoroutine (PlayerDeath());
					playerDiedededed = true;
				}
				GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMove = false;
				GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = false;
				GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canDash = false;
            }
		}
		// /!\NE PAS TOUCHER, pour les futurs combos
		/*counterHeal = combo.counter;//appelle dans combo
		if (counterHeal >= 5){
			GameObject clone = (GameObject)Instantiate (healItem, transform.position, transform.rotation);// créé les objets de soin
			counterReset = true;
		}
		if (counterHeal == 0){
			counterReset = false;
		}*/
		//gère les frmes d'invincibilité, par défaut à 0
		currentTime += Time.deltaTime;
		if(currentTime < invincibleTime)
		{
			invincible = true;
		}
		else
		{
			invincible = false;

		}

		if(startFade == true){
			timerFade += Time.deltaTime;
			GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, alphaColor,timerFade-1);

		}
	}

	/*void OnTriggerEnter2D (Collider2D other)
	{
		if (gameObject.tag == "Enemy" && other.tag == "PlayerAttack") 
		{
			Hurt (other.GetComponentInParent<health>().damage);
		}
	}*/
	IEnumerator BossDeath(float duree){
		Time.timeScale = 0.2f;
		startFade = true;
		yield return new WaitForSeconds (duree/10);
		killedboss = true;
		yield return new WaitForSeconds (duree/10);
		Time.timeScale = 1f;

		Destroy (gameObject);


	}

	IEnumerator PlayerHitFX(GameObject player){
		hitFXRoutine = true;
		player.GetComponentInParent<Player> ().inDanger = true;
		hitFX = player.GetComponentInParent<Player> ().hitFX [player.GetComponentInParent<Player> ().tauxCharge-1];
		hitFX.SetActive (true);

		hitFX.GetComponent<ParticleSystem> ().Emit (1);
		hitFX.transform.position = new Vector3 (transform.position.x,transform.position.y, 0);

		yield return new WaitForSeconds(0.35f);
		GamePad.SetVibration (0,0f,0f);

		hitFX.SetActive (false);
		hitFXRoutine = false;


	}

	IEnumerator Vibration(float duree, float puissance){
		GamePad.SetVibration (0,puissance,puissance);
		yield return new WaitForSeconds(duree);
		GamePad.SetVibration (0,0f,0f);

	}

	public IEnumerator PlayerDeath()
	{
		GetComponent<Rythme>(). sndTheme.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		GetComponent<Rythme>(). sndBase.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		GetComponent<Rythme>(). sndTranse.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		GetComponent<Rythme>(). sndTranscendance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

		gameObject.GetComponent<Animator>().SetTrigger ("Mort");
		yield return new WaitForSeconds (1f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		rend.material.shader = shaderDeBase;
		rend.color = couleurDeBase ;
		Destroy (gameObject);
	}
	IEnumerator Damage(float timeRed , float timeShake, float magShake )
    {
		rend.material.shader = shaderDeCouleur;
		rend.color = Color.red;
		Camera.main.GetComponent<CameraBehaviour> ().ScreenShakeFunction (timeShake, timeShake,0.04f);
		yield return new WaitForSeconds(timeRed);
		rend.material.shader = shaderDeBase;
		rend.color = couleurDeBase ;
	}
}
