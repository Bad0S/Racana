using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patternTir : MonoBehaviour {
	

	public GameObject target;
	public List<GameObject> laserListe;
	//patterns
	public int phase = 1;
	private float timerEntrePatterns;
	private float prochainPattern;
	public bool inPattern;
	public float beatLength;
	public bool beatAllowAttack;
	private Rythme rhythm;

	// casters externes
	public List<GameObject> externesBas;
	public List<GameObject> externesCotes;
	bool activationPhase2;
	bool activationPhase3;


	//phase 2
	public bool firstPattern2;

	//phace 3 
	public bool firstPattern3;

	//phase 4
	float timerDeath;
	public int finalPhase=1;

	//patternisation
	private int originalLife;

	//patch
	private int patchSize ;

	//heal
	bool heal1;
	bool heal2;
	bool heal3;

	[FMODUnity.EventRef]
	public string selectsoundTirBoss;
	FMOD.Studio.EventInstance sndTirBoss;

	// Use this for initialization
	void Start () {
		sndTirBoss = FMODUnity.RuntimeManager.CreateInstance(selectsoundTirBoss);
		rhythm = target.GetComponent <Rythme> ();

		prochainPattern = beatLength * (0 + (Random.Range ( 0 , 2)));
		originalLife = GetComponent <health> ().life;
	//	Time.timeScale = 0.5f;
	}

	void FixedUpdate(){
		beatAllowAttack = rhythm.isBeating;
		beatLength= rhythm.timeBetweenBeatsInSeconds;
	}

	// Update is called once per frame
	void Update () {
		if (inPattern == false){
			if (GetComponent <health>().life<= 3&& inPattern == false&& finalPhase<2){
				if(finalPhase == 1){
					phase = 4;
					if(target.GetComponent <health>().life<3&& heal1 == false){
						target.GetComponent <health>().Heal (1);
						heal1 = true;

					}

					finalPhase++;
				}
			}
			else if(GetComponent <health>().life<= (originalLife/6)*3){
				phase = 3;
				if(target.GetComponent <health>().life<3&& heal2 == false){
					target.GetComponent <health>().Heal (1);
					heal2 = true;

				}

			}
			else if (GetComponent <health>().life<= (originalLife/6)*5&&GetComponent <health>().life>= (originalLife/6)*4){
				phase = 2;
				if(target.GetComponent <health>().life<3&& heal3 == false){
					target.GetComponent <health>().Heal (1);
					heal3 = true;

				}
			}
		
			 
		
			timerEntrePatterns += Time.deltaTime;
		}
		if (phase == 1) {
			if (timerEntrePatterns > prochainPattern && inPattern == false&& beatAllowAttack == true) {
				int rand = Random.Range (0, 2);
				if (rand == 0) {
					StartCoroutine (Pattern1Etat1 (beatLength));
				} else if (rand == 1) {
					StartCoroutine (Pattern2Etat1 (beatLength));
				}
	
				timerEntrePatterns = 0;
				prochainPattern = beatLength * (1 + (Random.Range (0, 2)));
			}
		} else if (phase == 2) {
			if (timerEntrePatterns > prochainPattern && inPattern == false&& beatAllowAttack == true) {
				if (firstPattern2 = false) {
					firstPattern2 = true;

					StartCoroutine (Pattern1Etat2 (beatLength));

				} else {
					if (activationPhase2 == false) {
						externesBas [0].SetActive (true);
						externesBas [1].SetActive (true);
						activationPhase2 = true;
					}
					int rand = Random.Range (0, 4);
					if (rand == 0) {
						StartCoroutine (Pattern2Etat2 (beatLength));
					} else if (rand == 1) {
						StartCoroutine (Pattern3Etat2 (beatLength));
					} else if (rand == 2) {
						StartCoroutine (Pattern4Etat2 (beatLength));
					} else if (rand == 3) {
						StartCoroutine (Pattern5Etat2 (beatLength));
					}
				}

				timerEntrePatterns = 0;
				prochainPattern = beatLength * (1 + (Random.Range (0, 1)));
			}
		} else if (phase == 3) {
			if (timerEntrePatterns > prochainPattern && inPattern == false&& beatAllowAttack == true) {
				if (activationPhase3 == false) {
					externesBas [0].SetActive (true);
					externesBas [1].SetActive (true);
					externesCotes [0].SetActive (true);
					externesCotes [1].SetActive (true);
					activationPhase3 = true;
				}
				if (firstPattern3 == false) {
					StartCoroutine (Pattern0Etat3 (beatLength));
				} else {
					int rand = Random.Range (0, 4);
					//int rand = 2;
					if (rand == 0) {
						print (rand + 1);
						StartCoroutine (Pattern1Etat3 (beatLength));	

					} else if (rand == 1) {
						print (rand + 1);

						StartCoroutine (Pattern2Etat3 (beatLength));	
					} else if (rand == 2) {
						print (rand + 1);

						StartCoroutine (Pattern3Etat3 (beatLength));	
					} else if (rand == 3) {
						print (rand + 1);

						StartCoroutine (Pattern4Etat3 (beatLength));	
					}
					timerEntrePatterns = 0;
					prochainPattern = beatLength * (1);
				}

			}
		} else if (phase == 4) {
			if(finalPhase ==2){
				StartCoroutine (Pattern0Etat4 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));


			}
			else if(finalPhase == 3){
				timerDeath += Time.deltaTime/4;

				beatLength = 120;
				//GetComponent <PolygonCollider2D>().enabled = true;

				externesBas [0].SetActive (true);
				externesBas [1].SetActive (true);
				externesCotes [0].SetActive (true);
				externesCotes [1].SetActive (true);


				GetComponentInChildren <CasterGauche> ().deathPhase = true;
				GetComponentInChildren <CasterDroit> ().deathPhase = true;
				externesBas[0]. GetComponent <CasterExterne> ().deathPhase = true;
				externesBas[1]. GetComponent <CasterExterne> ().deathPhase = true;
				externesCotes[0]. GetComponent <CasterExterne> ().deathPhase = true;
				externesCotes[1]. GetComponent <CasterExterne> ().deathPhase = true;


				GetComponentInChildren <CasterGauche> ().playerDirection =  new Vector3 ( Mathf.Lerp (-1,1,timerDeath), Mathf.Lerp (0,-1,Mathf.PingPong (timerDeath*2,1)), 0);
				GetComponentInChildren <CasterDroit> ().playerDirection =  new Vector3 (Mathf.Lerp (1,-1,timerDeath), Mathf.Lerp (0,-1,Mathf.PingPong (timerDeath*2,1)), 0);
				externesBas[0]. GetComponent <CasterExterne> ().playerDirection =  new Vector3 ( Mathf.Lerp (1,0,timerDeath*1.5f), Mathf.Lerp (0,1,timerDeath*1.5f), 0);
				externesBas[1]. GetComponent <CasterExterne> ().playerDirection =  new Vector3 ( Mathf.Lerp (-1,0,timerDeath*1.5f), Mathf.Lerp (0,1,timerDeath*1.5f), 0);
				externesCotes[0]. GetComponent <CasterExterne> ().playerDirection =  new Vector3 (1, Mathf.Lerp (-1,1,timerDeath*1.75f), 0);
				externesCotes[1]. GetComponent <CasterExterne> ().playerDirection =  new Vector3 (-1, Mathf.Lerp (-1,1,timerDeath*1.75f), 0);


				GetComponentInChildren <CasterGauche> ().canShoot = true;
				GetComponentInChildren <CasterDroit> ().canShoot = true;
				externesBas[0]. GetComponent <CasterExterne> ().canShoot = true;
				externesBas[1]. GetComponent <CasterExterne> ().canShoot = true;
				externesCotes[0]. GetComponent <CasterExterne> ().canShoot = true;
				externesCotes[1]. GetComponent <CasterExterne> ().canShoot = true;

				GetComponent <SpriteRenderer>().color = new Color(1f, 0.9f,0.9f);

			}	
			else if(finalPhase == 4){
				laserListe.Clear ();
				//Events stylés et Fx!
			}

		}
	}

	void TirGauche(){
		sndTirBoss.start ();
		GetComponentInChildren <CasterGauche> ().timerCasting = 0;
		GetComponentInChildren <CasterGauche> ().petitLaserFX.SetActive (false);

		GetComponentInChildren <CasterGauche> ().shoot = true;

	}
	void TirDroit(){
		sndTirBoss.start ();
		GetComponentInChildren <CasterDroit> ().timerCasting = 0;

		GetComponentInChildren <CasterDroit> ().shoot = true;

	}
	void TirBas1(){
		sndTirBoss.start ();
		externesBas [0].GetComponent <CasterExterne>().timerCasting = 0;

		externesBas[0].GetComponent <CasterExterne> ().shoot = true;

	}
	void TirBas2(){
		sndTirBoss.start ();
		externesBas [1].GetComponent <CasterExterne>().timerCasting = 0;

		externesBas[1].GetComponent <CasterExterne> ().shoot = true;

	}
	void TirExterneGauche(){
		sndTirBoss.start ();
		externesCotes [0].GetComponent <CasterExterne>().timerCasting = 0;

		externesCotes[0].GetComponent <CasterExterne> ().shoot = true;

	}
	void TirExterneDroit(){
		sndTirBoss.start ();
		externesCotes [1].GetComponent <CasterExterne>().timerCasting = 0;

		externesCotes[1].GetComponent <CasterExterne> ().shoot = true;

	}

	IEnumerator Pattern1Etat1(float dureeBeat){
		inPattern = true;
		yield return new WaitForSeconds(2*dureeBeat);
		TirGauche ();
		yield return new WaitForSeconds(2*dureeBeat);
		TirDroit ();

		yield return new WaitForSeconds(2*dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds(2*dureeBeat);
		TirDroit ();

		inPattern = false;
	}
	IEnumerator Pattern2Etat1(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds(2*dureeBeat);
		TirDroit ();
		yield return new WaitForSeconds(2*dureeBeat);
		TirGauche ();
		yield return new WaitForSeconds(2*dureeBeat);
		TirDroit ();
		yield return new WaitForSeconds(2*dureeBeat);
		TirGauche ();

		inPattern = false;
	}
	IEnumerator Pattern1Etat2(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds(3*dureeBeat);
		TirDroit ();
		TirGauche ();
		GetComponentInChildren <CasterDroit> ().playerDirection =  new Vector3 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
		GetComponentInChildren <CasterGauche> ().playerDirection =  new Vector3 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
		GetComponentInChildren <CasterDroit> ().casterTo = 5.5f;
		GetComponentInChildren <CasterGauche> ().casterTo = 5.5f;
		yield return new WaitForSeconds(1*dureeBeat);
		TirDroit ();
		TirGauche ();
		GetComponentInChildren <CasterDroit> ().playerDirection =  new Vector3 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
		GetComponentInChildren <CasterGauche> ().playerDirection =  new Vector3 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
		yield return new WaitForSeconds(1*dureeBeat);
		TirDroit ();
		TirGauche ();
		GetComponentInChildren <CasterDroit> ().playerDirection =  new Vector3 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
		GetComponentInChildren <CasterGauche> ().playerDirection =  new Vector3 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
		yield return new WaitForSeconds(1*dureeBeat);
		TirDroit ();
		TirGauche ();
		GetComponentInChildren <CasterDroit> ().playerDirection =  new Vector3 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
		GetComponentInChildren <CasterGauche> ().playerDirection =  new Vector3 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
		yield return new WaitForSeconds(1*dureeBeat);
		TirDroit ();
		TirGauche ();
		GetComponentInChildren <CasterDroit> ().casterTo = 05f;
		GetComponentInChildren <CasterGauche> ().casterTo = 05f;
		inPattern = false;
	}
	IEnumerator Pattern2Etat2(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds(2*dureeBeat);
		TirDroit ();
		TirBas1 ();

		yield return new WaitForSeconds(2*dureeBeat);
		TirGauche ();
		TirBas2 ();
		yield return new WaitForSeconds(3*dureeBeat);
		GetComponentInChildren <CasterDroit> ().casterTo = 6f;
		GetComponentInChildren <CasterGauche> ().casterTo = 6f;
		externesBas[0].GetComponent <CasterExterne> ().casterTo = 6f;
		externesBas[1].GetComponent <CasterExterne> ().casterTo = 6f;
		TirDroit ();
		TirGauche ();
		TirBas1 ();
		TirBas2 ();
		yield return new WaitForSeconds(2*dureeBeat);
		GetComponentInChildren <CasterDroit> ().casterTo =5f;
		GetComponentInChildren <CasterGauche> ().casterTo = 5f;
		externesBas[0].GetComponent <CasterExterne> ().casterTo = 5f;
		externesBas[1].GetComponent <CasterExterne> ().casterTo = 5f;


		inPattern = false;
	}
	IEnumerator Pattern3Etat2(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds(2*dureeBeat);

		TirGauche ();
		TirBas2 ();
		yield return new WaitForSeconds(2*dureeBeat);
		TirDroit ();
		TirBas1 ();
		yield return new WaitForSeconds(3*dureeBeat);
		GetComponentInChildren <CasterDroit> ().casterTo = 6f;
		GetComponentInChildren <CasterGauche> ().casterTo = 6f;
		externesBas[0].GetComponent <CasterExterne> ().casterTo = 6f;
		externesBas[1].GetComponent <CasterExterne> ().casterTo = 6f;
		TirDroit ();
		TirGauche ();
		TirBas1 ();
		TirBas2 ();
		yield return new WaitForSeconds(2*dureeBeat);
		GetComponentInChildren <CasterDroit> ().casterTo = 5f;
		GetComponentInChildren <CasterGauche> ().casterTo = 5f;
		externesBas[0].GetComponent <CasterExterne> ().casterTo = 5f;
		externesBas[1].GetComponent <CasterExterne> ().casterTo = 5f;


		inPattern = false;
	}
	IEnumerator Pattern4Etat2(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds(1*dureeBeat);
		TirDroit ();

		yield return new WaitForSeconds(1*dureeBeat);

		TirGauche ();

		yield return new WaitForSeconds(1*dureeBeat);
		TirDroit ();

		yield return new WaitForSeconds(1*dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds(1*dureeBeat);
		TirDroit ();
		TirBas1 ();

		yield return new WaitForSeconds(1*dureeBeat);
		TirGauche ();
		TirBas2 ();

		yield return new WaitForSeconds(2*dureeBeat);

		TirDroit ();
		TirGauche ();
		TirBas1 ();
		TirBas2 ();

		yield return new WaitForSeconds(2*dureeBeat);

		inPattern = false;
	}
	IEnumerator Pattern5Etat2(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds(2*dureeBeat);

		TirDroit ();
		TirGauche ();
		TirBas1 ();

		yield return new WaitForSeconds(1*dureeBeat);
		TirBas2 ();

		yield return new WaitForSeconds(2*dureeBeat);

		TirDroit ();
		TirGauche ();
		TirBas2 ();

		yield return new WaitForSeconds(1*dureeBeat);
		TirBas1 ();

		yield return new WaitForSeconds(2*dureeBeat);

		TirDroit ();
		TirBas1 ();
		TirBas2 ();

		yield return new WaitForSeconds(1*dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds(2*dureeBeat);

		TirGauche ();
		TirBas1 ();
		TirBas2 ();

		yield return new WaitForSeconds(1*dureeBeat);
		TirDroit ();

		inPattern = false;
	}

	IEnumerator Pattern0Etat3(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds (3 * dureeBeat);
		TirDroit ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirExterneDroit ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirExterneGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirBas2 ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirBas1 ();

		yield return new WaitForSeconds (2 * dureeBeat);
		TirDroit ();
		TirGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirExterneDroit ();
		TirExterneGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirBas1 ();
		TirBas2 ();

		yield return new WaitForSeconds (2 * dureeBeat);
		TirGauche ();
		TirExterneDroit ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirDroit ();		
		TirExterneGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirExterneDroit ();
		TirBas1 ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirExterneGauche ();
		TirBas2 ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirGauche ();
		TirExterneDroit ();
		TirBas1 ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirDroit ();
		TirExterneGauche ();
		TirBas2 ();

		yield return new WaitForSeconds (2 * dureeBeat);
		TirGauche ();
		TirExterneDroit ();
		TirBas1 ();
		TirDroit ();
		TirExterneGauche ();
		TirBas2 ();

		firstPattern3 = true;
		inPattern = false;

	}


	IEnumerator Pattern1Etat3(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds(3*dureeBeat);
		GetComponentInChildren <CasterDroit> ().casterTo = 6f;
		GetComponentInChildren <CasterGauche> ().casterTo = 6f;
		externesBas[0].GetComponent <CasterExterne> ().casterTo = 6f;
		externesBas[1].GetComponent <CasterExterne> ().casterTo = 6f;
		externesCotes[0].GetComponent <CasterExterne> ().casterTo = 6f;
		externesCotes[1].GetComponent <CasterExterne> ().casterTo = 6f;

		TirDroit ();
		TirGauche ();
		TirBas1 ();
		TirBas2 ();
		TirExterneDroit ();
		TirExterneGauche ();
		yield return new WaitForSeconds(1*dureeBeat);
		GetComponentInChildren <CasterDroit> ().casterTo = 5f;
		GetComponentInChildren <CasterGauche> ().casterTo = 5f;
		externesBas[0].GetComponent <CasterExterne> ().casterTo = 5f;
		externesBas[1].GetComponent <CasterExterne> ().casterTo = 5f;
		externesCotes[0].GetComponent <CasterExterne> ().casterTo = 5f;
		externesCotes[1].GetComponent <CasterExterne> ().casterTo = 5f;
	
		inPattern = false;
	}
	IEnumerator Pattern2Etat3(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds(2*dureeBeat);
		TirGauche ();
		TirBas1 ();
		TirExterneDroit ();
		yield return new WaitForSeconds(2*dureeBeat);
		TirDroit ();
		TirBas2 ();
		TirExterneGauche ();
		yield return new WaitForSeconds(2*dureeBeat);
		TirGauche ();
		TirBas1 ();
		TirExterneDroit ();
		yield return new WaitForSeconds(2*dureeBeat);
		TirDroit ();
		TirBas2 ();
		TirExterneGauche ();


		inPattern = false;
	}
	IEnumerator Pattern3Etat3(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds(0.5f*dureeBeat);
		TirGauche ();
		yield return new WaitForSeconds(0.5f*dureeBeat);
		TirExterneGauche ();
		yield return new WaitForSeconds(0.5f*dureeBeat);
		TirBas1 ();
		yield return new WaitForSeconds(0.5f*dureeBeat);
		TirBas2 ();
		yield return new WaitForSeconds(0.5f*dureeBeat);
		TirExterneDroit ();
		yield return new WaitForSeconds(0.5f*dureeBeat);
		TirDroit ();	

		inPattern = false;
	}
	IEnumerator Pattern4Etat3(float dureeBeat){
		inPattern = true;
		yield return new WaitForSeconds(0.5f*dureeBeat);
		TirDroit ();	
		yield return new WaitForSeconds(0.5f*dureeBeat);
		TirExterneDroit ();
		yield return new WaitForSeconds(0.5f*dureeBeat);
		TirBas2 ();
		yield return new WaitForSeconds(0.5f*dureeBeat);
		TirBas1 ();
		yield return new WaitForSeconds(0.5f*dureeBeat);
		TirExterneGauche ();
		yield return new WaitForSeconds(0.5f*dureeBeat);
		TirGauche ();
		inPattern = false;
	}
	IEnumerator Pattern0Etat4(float dureeBeat){
		//inPattern = true;
		//GetComponent <PolygonCollider2D>().enabled = false;
		//GetComponent <SpriteRenderer>().color = new Color(1f, 0.9f,0.9f);
		yield return new WaitForSeconds (3 * dureeBeat);
		TirDroit ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirExterneDroit ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirBas2 ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirBas1 ();

		yield return new WaitForSeconds (1 * dureeBeat);

		TirExterneGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirDroit ();
		TirExterneDroit ();
		TirBas2 ();

		yield return new WaitForSeconds (2 * dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirExterneGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirBas1 ();

		yield return new WaitForSeconds (1 * dureeBeat);


		TirBas2 ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirExterneDroit ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirDroit ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirGauche ();
		TirExterneDroit ();
		TirBas1 ();

		yield return new WaitForSeconds (2 * dureeBeat);
		TirGauche ();
		TirDroit ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirExterneDroit ();
		TirExterneGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirBas1 ();
		TirBas2 ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirGauche ();
		TirDroit ();
		TirExterneDroit ();
		TirExterneGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirBas1 ();
		TirBas2 ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirExterneDroit ();
		TirExterneGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirDroit ();
		TirGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirBas1 ();
		TirBas2 ();
		TirExterneDroit ();
		TirExterneGauche ();

		yield return new WaitForSeconds (2 * dureeBeat);
		TirBas1 ();
		TirBas2 ();
		TirExterneDroit ();
		TirExterneGauche ();
		TirDroit ();
		TirGauche ();
		yield return new WaitForSeconds (2 * dureeBeat);
		finalPhase++;
		inPattern = false;


	}
	IEnumerator Pattern1Etat4(float dureeBeat){
		beatLength = 120;
		inPattern = true;
		GetComponentInChildren <CasterGauche> ().playerDirection =  new Vector3 (-1, 0, 0);
		GetComponentInChildren <CasterDroit> ().playerDirection =  new Vector3 (1, 0, 0);
		externesBas[0]. GetComponent <CasterExterne> ().playerDirection =  new Vector3 (1, 0, 0);
		externesBas[1]. GetComponent <CasterExterne> ().playerDirection =  new Vector3 (-1, 0, 0);
		externesCotes[0]. GetComponent <CasterExterne> ().playerDirection =  new Vector3 (1, -1, 0);
		externesCotes[1]. GetComponent <CasterExterne> ().playerDirection =  new Vector3 (-1, -1, 0);

		GetComponentInChildren <CasterGauche> ().canShoot = true;

		GetComponentInChildren <CasterDroit> ().canShoot= true;
		externesBas[0]. GetComponent <CasterExterne> ().canShoot= true;
		externesBas[1]. GetComponent <CasterExterne> ().canShoot= true;
		externesCotes[0]. GetComponent <CasterExterne> ().canShoot= true;
		externesCotes[1]. GetComponent <CasterExterne> ().canShoot= true;
		yield return new WaitForSeconds(2*dureeBeat);
		TirGauche ();
		GetComponentInChildren <CasterGauche> ().playerDirection =  new Vector3 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);
		yield return new WaitForSeconds(2*dureeBeat);
		TirDroit ();
		GetComponentInChildren <CasterDroit> ().playerDirection =  new Vector3 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);

		yield return new WaitForSeconds(2*dureeBeat);
		TirGauche ();
		GetComponentInChildren <CasterGauche> ().playerDirection =  new Vector3 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);

		yield return new WaitForSeconds(2*dureeBeat);
		TirDroit ();
		GetComponentInChildren <CasterDroit> ().playerDirection =  new Vector3 (target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y, 0);

		inPattern = false;
	}
	IEnumerator Pattern0Etat4HARD(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds (3 * dureeBeat);
		TirDroit ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirExterneGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirBas2 ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);

		TirExterneDroit ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirBas1 ();

		yield return new WaitForSeconds (2 * dureeBeat);
		TirDroit ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirDroit ();
		TirExterneGauche();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirGauche ();
		TirExterneDroit ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirDroit ();
		TirExterneGauche ();
		TirBas2 ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirGauche ();
		TirExterneDroit ();
		TirBas2 ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirDroit ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirExterneGauche ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirBas1();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirBas2 ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirExterneDroit ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirDroit ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirExterneGauche ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirBas1() ;

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirBas2 ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirExterneDroit ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirDroit ();

		yield return new WaitForSeconds (1.5f * dureeBeat);
		TirGauche ();
		TirExterneGauche ();
		TirBas1 ();
		TirBas2 ();
		TirExterneDroit ();

		yield return new WaitForSeconds (2 * dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirExterneDroit ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirBas2 ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirBas1 ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirExterneGauche ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirExterneDroit ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirBas2 ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirBas1 ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirExterneGauche ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirDroit ();
		TirExterneDroit ();
		TirExterneGauche ();
		TirBas1 ();
		TirBas2 ();

		yield return new WaitForSeconds (2 * dureeBeat);
		TirBas1 ();
		TirBas2 ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirExterneDroit ();
		TirExterneGauche ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirDroit ();
		TirGauche ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirExterneDroit ();
		TirExterneGauche ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirBas1 ();
		TirBas2 ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirExterneDroit ();
		TirExterneGauche ();

		yield return new WaitForSeconds (0.5f * dureeBeat);
		TirDroit ();
		TirGauche ();

		yield return new WaitForSeconds (1 * dureeBeat);
		TirBas1 ();
		TirBas2 ();
		TirExterneDroit ();
		TirExterneGauche ();

		yield return new WaitForSeconds (2 * dureeBeat);
		TirBas1 ();
		TirBas2 ();
		TirExterneDroit ();
		TirExterneGauche ();
		TirDroit ();
		TirGauche ();
		inPattern = false;

		firstPattern3 = true;

	}


}
