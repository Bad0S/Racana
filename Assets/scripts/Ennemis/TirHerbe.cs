using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


public class TirHerbe : MonoBehaviour {
	public float shootSpeed;
	public int damage = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.right * Time.deltaTime*shootSpeed);
	}
	void OnTriggerEnter2D( Collider2D  other)
	{
		if (other.tag == "Player") 
		{
			other.GetComponent <Player>().projectileShake = true;
			other.GetComponent<health> ().Hurt (damage);
			StartCoroutine (PlayerDamage ());

			Destroy (gameObject);
		}
		//dans l'autre, on récupère dans player la fonction hurt

	}
	IEnumerator PlayerDamage(){
		Camera.main.GetComponent<CameraBehaviour> ().ScreenShakeFunction (0.14f, 0.02f,0.04f);
		yield return new WaitForSeconds(0.10f);
	}
		
}
