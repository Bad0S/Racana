using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterGauche : MonoBehaviour {
	public float timerCasting;
	public bool canShoot;
	public int isAiming=1;
	public bool casting;
	public float timeCast; // ATTENTION, si on change le timeCast faut aussi changer le speedGrowth proportionnellement
	public Transform player;
	public float casterFrom; //grossit en commençant à cette taille
	public float casterTo; //pour arriver à celle-là
	int casterTemp;
	public bool decasting;
	public float speedGrowth=1;
	public GameObject laser;
	Vector3 hitV3;
	public float angleShoot;
	public Vector3 playerDirection;
	RaycastHit2D hit;
	float laserLength;
	public float frame = 0.5f; // frame d'esquive du player
	public LayerMask layerLaser;

	//FX 
	public GameObject casterFX;
	public GameObject chargeTirFX;
	public GameObject laserFX;
	public GameObject laserFXSphere;
	public GameObject petitLaserFX;
	public float timerLaser;
	public bool hasShoot;
	//ce qui dit tire au caster
	public bool shoot;

	//death
	public bool deathPhase = false;

	void Start(){
	}

	// Update is called once per frame
	void Update () {
		if (shoot) {
			casting = true;
			decasting = false;
			shoot = false;
			casterFX.SetActive (true);
			chargeTirFX.SetActive (true);

		}

		if (casting == true) {
			//	casterFX.SetActive (true);

			timerCasting += Time.deltaTime;
		} else if (decasting == true) {
			timerCasting -= Time.deltaTime;
		}

		if (hasShoot == true) {
			timerLaser += Time.deltaTime;
		}

		//casterFX.GetComponentInChildren <ParticleSystem>().Play ();

		//transform.localScale = new Vector3(Mathf.Lerp(casterFrom,casterTo, timerCasting*(timeCast)), Mathf.Lerp(casterFrom,casterTo, timerCasting*(timeCast)), 0);


		if (timerCasting >= 0 && decasting == false&& casting == true) {
			if (isAiming == 1) {
				isAiming = 2;
			}	

			if (isAiming == 2) {
				if (deathPhase == false) {
					playerDirection = new Vector3 (player.position.x - transform.position.x, player.position.y - transform.position.y, 0);

				}
				hit = Physics2D.Raycast (transform.position, playerDirection, Mathf.Infinity, layerLaser);
				laserLength = hit.distance;
				hitV3 = new Vector3 (hit.point.x, hit.point.y, 0);
				angleShoot = Mathf.Atan2 (playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

				petitLaserFX.SetActive (true);
				petitLaserFX.transform.rotation = Quaternion.Euler (0, 0, angleShoot + 90);
				isAiming = 3;

			}

		}



		if (timerCasting >= timeCast && casting == true) {
			timerCasting = 1;
			casting = false;
			canShoot = true;
			decasting = true;
			speedGrowth *= 2;
		}

		if (timerCasting <= 0 && decasting == true) {
			timerCasting = 0;
			decasting = false;
			speedGrowth /= 2;
		}
		// créé le cast
		if (canShoot == true) {
			if (deathPhase == true) {
				hit = Physics2D.Raycast (transform.position, playerDirection, Mathf.Infinity, layerLaser);
				laserLength = hit.distance;
				hitV3 = new Vector3 (hit.point.x, hit.point.y, 0);
				angleShoot = Mathf.Atan2 (playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
				petitLaserFX.SetActive (true);
				petitLaserFX.transform.rotation = Quaternion.Euler (0, 0, angleShoot);

				isAiming = 3;
			}
			if (hit.collider != null) {

				petitLaserFX.SetActive (false);
				laserFX.SetActive (true);
				laserFXSphere.transform.position = new Vector3 (hitV3.x, hitV3.y, 0);
				GameObject laserInstance = (GameObject)Instantiate (laser, (transform.position + hitV3) / 2, Quaternion.Euler (0, 0, angleShoot));
				laserInstance.transform.localScale = new Vector3 (laserLength / 4, 002f, 1);
				laserInstance.transform.parent = gameObject.transform.parent;
				hasShoot = true;

				isAiming = 1;
			}
			canShoot = false;
		}
		if (timerLaser > GetComponentInParent <patternTir> ().beatLength) {
			casterFX.SetActive (false);
			chargeTirFX.SetActive (true);

			laserFX.SetActive (false);
			timerLaser = 0;
			hasShoot = false;
		}
	}
}