using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterDroit : MonoBehaviour {
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

	//ce qui dit tire au caster
	public bool shoot;

	//death
	public bool deathPhase = false;

	void Start(){
	}

	// Update is called once per frame
	void Update () {
		if (shoot){
			casting = true;
			decasting = false;
			shoot = false;
		}

		if(casting==true){
			timerCasting += Time.deltaTime;
		}
		else if(decasting == true){
			timerCasting -= Time.deltaTime;
		}

		transform.localScale = new Vector3(Mathf.Lerp(casterFrom,casterTo, timerCasting*(timeCast)), Mathf.Lerp(casterFrom,casterTo, timerCasting*(timeCast)), 0);


		if(timerCasting>=timeCast  - player.GetComponent <Rythme>().timeBetweenBeatsInSeconds && decasting == false ){
			if(isAiming == 1){
				isAiming= 2;
			}	

			if(isAiming == 2){
				if(deathPhase ==false){
					playerDirection = new Vector3 (player.position.x - transform.position.x, player.position.y - transform.position.y, 0);

				}
				hit = Physics2D.Raycast(transform.position, playerDirection,Mathf.Infinity, layerLaser);
				laserLength = hit.distance;
				hitV3 = new Vector3 (hit.point.x, hit.point.y, 0);
				angleShoot = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

				GameObject laserInstance = (GameObject)Instantiate (laser, (transform.position+hitV3)/2, Quaternion.Euler(0, 0, angleShoot));
				laserInstance.transform.localScale = new Vector3 (laserLength/4,00.8f,1);
				laserInstance.transform.parent = gameObject.transform.parent;
				laserInstance.GetComponent <BoxCollider2D>().enabled = false;
				isAiming= 3;

			}

		}



		if (timerCasting >= timeCast&& casting ==true) {
			timerCasting = 1;
			casting = false;
			canShoot = true;
			decasting = true;
			speedGrowth *= 2;
		}

		if (timerCasting <= 0 &&decasting == true){
			timerCasting = 0;
			decasting = false;
			speedGrowth /= 2;
		}
		// créé le cast
		if (canShoot == true){
			if(deathPhase == true){
				hit = Physics2D.Raycast(transform.position, playerDirection,Mathf.Infinity, layerLaser);
				laserLength = hit.distance;
				hitV3 = new Vector3 (hit.point.x, hit.point.y, 0);
				angleShoot = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;

				GameObject laserInstanceMini = (GameObject)Instantiate (laser, (transform.position+hitV3)/2, Quaternion.Euler(0, 0, angleShoot));
				laserInstanceMini.transform.localScale = new Vector3 (laserLength/4,00.8f,1);
				laserInstanceMini.transform.parent = gameObject.transform.parent;
				laserInstanceMini.GetComponent <BoxCollider2D>().enabled = false;
				isAiming= 3;
			}
			if (hit.collider != null)
			{

				print ("yes");
				GameObject laserInstance = (GameObject)Instantiate (laser, (transform.position+hitV3)/2, Quaternion.Euler(0, 0, angleShoot));
				laserInstance.transform.localScale = new Vector3 (laserLength/4,004f,1);
				laserInstance.transform.parent = gameObject.transform.parent;
				isAiming = 1;
			}
			canShoot = false;
		}

	}
}
