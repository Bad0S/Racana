using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform target;
    public float Camspeed;
	public Transform triggerForêt;
	public Transform triggerEntréeDonjon;
	public Transform triggerEntréeBoss;
	public Transform triggerBoss;
	private bool cinematique;
	private Camera cam;
	private Vector3 originalPos;
	public bool Effect;
	Vector3 targetPositionX;
	Vector3 targetPositionY;

	void Start()
	{
		cam = GetComponent<Camera>();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}
	public IEnumerator ScreenShake (float TimeToShake, float magnitude, float frequency)
	{
		Effect = true;
		originalPos = transform.position;
		float elapsed = 0.0f;
		float elapsedFrequency = 0.0f;

		while (elapsed < TimeToShake) {
			if (elapsedFrequency> frequency){
				float x = Random.Range (-1f, 1f) * magnitude + originalPos.x;
				float y = Random.Range (-1f, 1f) * magnitude + originalPos.y;
				transform.localPosition = new Vector3 (x, y, originalPos.z);
				elapsedFrequency = 0;
			}
			elapsedFrequency += Time.fixedDeltaTime;
			elapsed += Time.deltaTime;

			yield return null;

		}
		Effect = false;

		transform.localPosition = originalPos;

	}
	public void ScreenShakeFunction(float TimeToShakeFunction, float magnitudeFunction, float frequencyFunction){

		if (Effect == false){
			StartCoroutine(ScreenShake( TimeToShakeFunction , magnitudeFunction, frequencyFunction));
		}
	}

	void Update()
	{


		if (target.position.y > triggerForêt.position.y && target.position.y < triggerEntréeDonjon.position.y) 
		{
			cinematique = false;
			cam.orthographicSize = Mathf.Lerp (cam.orthographicSize, 110f, Time.deltaTime*0.9f);
		}
		if (target.position.y >triggerEntréeDonjon.position.y )
		{
			cinematique = true;
			transform.position = Vector3.Lerp(transform.position, new Vector3 (254f,1924f, -10f),Time.deltaTime);
			cam.orthographicSize = Mathf.Lerp (cam.orthographicSize, 240f, Time.deltaTime*0.7f);
		}
		if (target.position.y > triggerEntréeBoss.position.y && target.position.y < triggerBoss.position.y) 
		{
			cinematique = true;
			transform.position = Vector3.Lerp(transform.position, new Vector3 (261f,2142f, -10f),Time.deltaTime);
			cam.orthographicSize = Mathf.Lerp (cam.orthographicSize, 486f, Time.deltaTime*0.9f);
		}
		if (target.position.y >triggerBoss.position.y )
		{
			cinematique = false;
			cam.orthographicSize = Mathf.Lerp (cam.orthographicSize, 180f, Time.deltaTime*0.7f);
		}
    }

    void FixedUpdate()
    {
		//création de 2 nouveaux vecteurs, un qui suit la target sur x, l'autre sur y, si le joueur s'éloigne trop du centre de l'écran(valeurs numériques float), la caméra le suit
		if(Effect == false){
			targetPositionX = new Vector3(target.position.x, transform.position.y, transform.position.z);
			targetPositionY = new Vector3(transform.position.x, target.position.y, transform.position.z);
		}

		if (cinematique == false) 
				{
			if (target.position.x <= transform.position.x - 0.6f || target.position.x >= transform.position.x + 0.6f) {
				transform.position = Vector3.Lerp (transform.position, targetPositionX, Time.deltaTime * Camspeed);
			}
			if (target.position.y <= transform.position.y - 0.35f || target.position.y >= transform.position.y + 0.35f) {
				transform.position = Vector3.Lerp (transform.position, targetPositionY, Time.deltaTime * Camspeed);
			}
		}
    }
}
