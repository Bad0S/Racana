using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patternTir : MonoBehaviour {
	public GameObject target;
	private int phase = 1;
	private float timerEntrePatterns;
	private float prochainPattern;
	private bool inPattern;
	public float beatLength;

	//patternisation
	private int originalLife;

	//patch
	private int patchSize ;

	// Use this for initialization
	void Start () {
		beatLength = target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds;
		prochainPattern = beatLength * (6 + (Random.Range ( 0 , 4)));
		originalLife = GetComponent <health> ().life;
	}
	
	// Update is called once per frame
	void Update () {
		if (inPattern == false){
			if (GetComponent <health>().life<= (originalLife/6)*5){
				phase = 2;
			}
			timerEntrePatterns += Time.deltaTime;
		}
		if (phase == 1){
			if(timerEntrePatterns> prochainPattern && inPattern ==false){
				if (Random.Range (0,2)== 0){
					StartCoroutine (Pattern1Etat1 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));
				}
				else{
					StartCoroutine (Pattern2Etat1 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));
				}

				timerEntrePatterns = 0;
				beatLength = target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds;
				prochainPattern = beatLength * (1 + (Random.Range ( 0 , 4)));
			}
		}
		else if (phase == 2){
			if(timerEntrePatterns> prochainPattern && inPattern ==false){
				StartCoroutine (Pattern1Etat2 (target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds));

				timerEntrePatterns = 0;
				beatLength = target.GetComponent <Rythme> ().timeBetweenBeatsInSeconds;
				prochainPattern = beatLength * (1 + (Random.Range ( 1 , 3)));
			}
		}
	}

	void TirGauche(){
		GetComponentInChildren <CasterGauche> ().shoot = true;
	}

	void TirDroit(){
		GetComponentInChildren <CasterDroit> ().shoot = true;
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
		GetComponentInChildren <CasterDroit> ().casterTo = 0.4f;
		GetComponentInChildren <CasterGauche> ().casterTo = 0.4f;
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
		GetComponentInChildren <CasterDroit> ().casterTo = 0.3f;
		GetComponentInChildren <CasterGauche> ().casterTo = 0.3f;
		inPattern = false;
	}
}
