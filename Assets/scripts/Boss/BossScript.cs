using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.UI;


public class BossScript : MonoBehaviour 
{
	public Image BossBar;
	public Image BossBarFill;
	// Use this for initialization
	void Start () 
	{
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		BossBarFill.fillAmount = GetComponent <health> ().life / 72;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerAttack") 
		{

			GetComponent <health> ().Hurt (other.GetComponentInParent<health> ().damage);


		}
	}


}
