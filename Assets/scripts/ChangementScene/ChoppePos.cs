﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoppePos : MonoBehaviour 
{
	public Scene scene;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			scene = SceneManager.GetActiveScene ();
			GameObject.FindGameObjectWithTag ("Respawn").GetComponent<PositionSetter> ().RespawnPos.Add (GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ().position);
			GameObject.FindGameObjectWithTag ("Respawn").GetComponent<PositionSetter> ().scenes.Add (scene.name);
		}
	}
}