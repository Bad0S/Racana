using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class PorteBoss : MonoBehaviour {
	private bool activation;
	private float originalPos;
	public float descending;
	public float speed;
	public GameObject cacheBoss;
	public GameObject hitboxPorte;
	public List<GameObject> disabling;
	bool disable;
	bool enable;
	public GameObject Particle;


	// Use this for initialization
	void Start () {
		originalPos = transform.position.y;
        Particle.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player"&& activation == false) 
		{
			activation = true;

			Camera.main.GetComponent<CameraBehaviour> ().ScreenShakeFunction (2f, 0.01f,0.05f);
			StartCoroutine (Vibration (2f, 0.5f));
			StartCoroutine(EmitParticle(2f));
		}
	}

	IEnumerator Vibration(float duree, float puissance){
		GamePad.SetVibration (0,puissance,0);// POUR LA FREQUECNE, DEMANDER A MICHAEL
		yield return new WaitForSeconds(duree);
		GamePad.SetVibration (0,0f,0f);

	}



	// Update is called once per frame
	void Update () {

		if (activation == true && descending<=1){
			if (disable== false){
				foreach (var vivant in disabling) {
					if(vivant.tag == "Player"){
						vivant.GetComponent <Player>().enabled = false;
						vivant.GetComponent <Animator>().speed=0;

					}
					else{
						try{
							vivant.GetComponent <EnemyBehaviour>().enabled = false;
							vivant.GetComponent <Animator>().speed=0;

						}
						catch{}
						try{
							vivant.GetComponent <BambouBehaviour>().enabled = false;
							vivant.GetComponent <Animator>().speed=0;

						}
						catch{}
					}
				}
				disable = true;
			}

			descending += Time.deltaTime/(1/speed);
			transform.position = new Vector3(transform.position.x,Mathf.Lerp(originalPos, originalPos-3, descending),0);
		}
		else if(transform.position.y == originalPos-3){
			cacheBoss.GetComponent <CacheBossFader>().activation = true;
			hitboxPorte.GetComponent <BoxCollider2D>().enabled = false;
			if (enable == false){
				foreach (var vivant in disabling) {
					if(vivant.tag == "Player"){
						vivant.GetComponent <Player>().enabled = true;
						vivant.GetComponent <Animator>().speed=1;

					}
					else{
						try{
							vivant.GetComponent <EnemyBehaviour>().enabled = true;
							vivant.GetComponent <Animator>().speed=1;

						}
						catch{}
						try{
							vivant.GetComponent <BambouBehaviour>().enabled = true;
							vivant.GetComponent <Animator>().speed=1;



						}
						catch{}
					}
				}
				enable = true;

			}

		}
	}

    IEnumerator EmitParticle(float duree)    {        Particle.SetActive(true);
		yield return new WaitForSeconds(duree);        Particle.SetActive(false);
    }

}