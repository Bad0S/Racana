using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patternTir : MonoBehaviour {
	

	public GameObject target;
	//patterns
	private int phase = 1;
	private float timerEntrePatterns;
	private float prochainPattern;
	private bool inPattern;
	public float beatLength;

	// casters externes
	public List<GameObject> externesBas;
	public List<GameObject> externesCotes;

	//phase 2
	public bool firstPattern2;

	//patternisation
	private int originalLife;

	//patch
	private int patchSize ;

	// Use this for initialization
	void Start () {
		beatLength = target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds;
		prochainPattern = beatLength * (0 + (Random.Range ( 0 , 2)));
		originalLife = GetComponent <health> ().life;
	}
	
	// Update is called once per frame
	void Update () {
		if (inPattern == false){
			if (GetComponent <health>().life<= (originalLife/6)*5&&GetComponent <health>().life>= (originalLife/6)*4){
				phase = 2;
			}
			else if(GetComponent <health>().life<= (originalLife/6)*3){
				phase = 3;
			}
			timerEntrePatterns += Time.deltaTime;
		}
		if (phase == 1) {
			if (timerEntrePatterns > prochainPattern && inPattern == false) {
				if (Random.Range (0, 2) == 0) {
					StartCoroutine (Pattern4Etat3 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));
				} else {
					StartCoroutine (Pattern4Etat3 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));
				}

				timerEntrePatterns = 0;
				beatLength = target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds;
				prochainPattern = beatLength * (1 + (Random.Range (0, 2)));
			}
		} else if (phase == 2) {
			if (timerEntrePatterns > prochainPattern && inPattern == false) {
				if (firstPattern2 = false) {
					firstPattern2 = true;

					StartCoroutine (Pattern1Etat2 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));

				} else {
					externesBas [0].SetActive (true);
					externesBas [1].SetActive (true);
					if (Random.Range (0, 2) == 0) {
						StartCoroutine (Pattern2Etat2 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));
					} else {
						StartCoroutine (Pattern3Etat2 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));
					}
				}

				timerEntrePatterns = 0;
				beatLength = target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds;
				prochainPattern = beatLength * (1 + (Random.Range (0, 1)));
			}
		} else if (phase == 3) {
			externesBas [0].SetActive (true);
			externesBas [1].SetActive (true);
			externesCotes [0].SetActive (true);
			externesCotes [1].SetActive (true);
			int rand = Random.Range (0, 6);
			if (rand == 0) {
				StartCoroutine (Pattern1Etat3 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));	

			}
			else if(rand ==1){
				StartCoroutine (Pattern2Etat3 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));	
			}
			else if(rand ==2){
				StartCoroutine (Pattern3Etat3 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));	
			}
			else if(rand ==3){
				StartCoroutine (Pattern4Etat3 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));	
			}
			else if(rand ==4){
				StartCoroutine (Pattern5Etat3 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));	
			}
			timerEntrePatterns = 0;
			beatLength = target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds;
			prochainPattern = beatLength * (1);
		}
	}

	void TirGauche(){
		GetComponentInChildren <CasterGauche> ().shoot = true;
	}

	void TirDroit(){
		GetComponentInChildren <CasterDroit> ().shoot = true;
	}
	void TirBas1(){
		externesBas[0].GetComponent <CasterExterne> ().shoot = true;
	}

	void TirBas2(){
		externesBas[1].GetComponent <CasterExterne> ().shoot = true;
	}

	void TirExterneGauche(){
		externesCotes[0].GetComponent <CasterExterne> ().shoot = true;
	}

	void TirExterneDroit(){
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
		GetComponentInChildren <CasterDroit> ().casterTo = 1.25f;
		GetComponentInChildren <CasterGauche> ().casterTo = 1.25f;
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
		GetComponentInChildren <CasterDroit> ().casterTo = 0.75f;
		GetComponentInChildren <CasterGauche> ().casterTo = 0.75f;
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
		GetComponentInChildren <CasterDroit> ().casterTo = 1.4f;
		GetComponentInChildren <CasterGauche> ().casterTo = 1.4f;
		externesBas[0].GetComponent <CasterExterne> ().casterTo = 1.4f;
		externesBas[1].GetComponent <CasterExterne> ().casterTo = 1.4f;
		TirDroit ();
		TirGauche ();
		TirBas1 ();
		TirBas2 ();
		yield return new WaitForSeconds(2*dureeBeat);
		GetComponentInChildren <CasterDroit> ().casterTo = 1.25f;
		GetComponentInChildren <CasterGauche> ().casterTo = 1.25f;
		externesBas[0].GetComponent <CasterExterne> ().casterTo = 1.25f;
		externesBas[1].GetComponent <CasterExterne> ().casterTo = 1.25f;


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
		GetComponentInChildren <CasterDroit> ().casterTo = 1.4f;
		GetComponentInChildren <CasterGauche> ().casterTo = 1.4f;
		externesBas[0].GetComponent <CasterExterne> ().casterTo = 1.4f;
		externesBas[1].GetComponent <CasterExterne> ().casterTo = 1.4f;
		TirDroit ();
		TirGauche ();
		TirBas1 ();
		TirBas2 ();
		yield return new WaitForSeconds(2*dureeBeat);
		GetComponentInChildren <CasterDroit> ().casterTo = 1.25f;
		GetComponentInChildren <CasterGauche> ().casterTo = 1.25f;
		externesBas[0].GetComponent <CasterExterne> ().casterTo = 1.25f;
		externesBas[1].GetComponent <CasterExterne> ().casterTo = 1.25f;


		inPattern = false;
	}
	IEnumerator Pattern1Etat3(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds(3*dureeBeat);
		GetComponentInChildren <CasterDroit> ().casterTo = 1.4f;
		GetComponentInChildren <CasterGauche> ().casterTo = 1.4f;
		externesBas[0].GetComponent <CasterExterne> ().casterTo = 1.4f;
		externesBas[1].GetComponent <CasterExterne> ().casterTo = 1.4f;
		externesCotes[0].GetComponent <CasterExterne> ().casterTo = 1.4f;
		externesCotes[1].GetComponent <CasterExterne> ().casterTo = 1.4f;

		TirDroit ();
		TirGauche ();
		TirBas1 ();
		TirBas2 ();
		TirExterneDroit ();
		TirExterneGauche ();
		yield return new WaitForSeconds(1*dureeBeat);
		GetComponentInChildren <CasterDroit> ().casterTo = 1.25f;
		GetComponentInChildren <CasterGauche> ().casterTo = 1.25f;
		externesBas[0].GetComponent <CasterExterne> ().casterTo = 1.25f;
		externesBas[1].GetComponent <CasterExterne> ().casterTo = 1.25f;
		externesCotes[0].GetComponent <CasterExterne> ().casterTo = 1.25f;
		externesCotes[1].GetComponent <CasterExterne> ().casterTo = 1.25f;
	
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

		yield return new WaitForSeconds(2*dureeBeat);
		TirBas1 ();		
		TirBas2 ();

		TirExterneDroit ();
		TirExterneGauche ();
		yield return new WaitForSeconds(1*dureeBeat);
		TirDroit ();
		TirGauche ();

		yield return new WaitForSeconds(1*dureeBeat);
		TirGauche ();

		TirDroit ();



		inPattern = false;
	}
	IEnumerator Pattern4Etat3(float dureeBeat){
		inPattern = true;

		yield return new WaitForSeconds(01f*dureeBeat);
		TirGauche ();
		yield return new WaitForSeconds(01f*dureeBeat);
		TirExterneGauche ();
		yield return new WaitForSeconds(01f*dureeBeat);
		TirBas1 ();
		yield return new WaitForSeconds(01f*dureeBeat);
		TirBas2 ();
		yield return new WaitForSeconds(01f*dureeBeat);
		TirExterneDroit ();
		yield return new WaitForSeconds(01f*dureeBeat);
		TirDroit ();	

		inPattern = false;
	}
	IEnumerator Pattern5Etat3(float dureeBeat){
		inPattern = true;
		yield return new WaitForSeconds(01f*dureeBeat);
		TirDroit ();	
		yield return new WaitForSeconds(01f*dureeBeat);
		TirExterneDroit ();
		yield return new WaitForSeconds(01f*dureeBeat);
		TirBas2 ();
		yield return new WaitForSeconds(01f*dureeBeat);
		TirBas1 ();
		yield return new WaitForSeconds(01f*dureeBeat);
		TirExterneGauche ();
		yield return new WaitForSeconds(01f*dureeBeat);
		TirGauche ();
		inPattern = false;
	}
}
