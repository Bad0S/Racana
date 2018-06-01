﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntréeTuto : MonoBehaviour {
	public GameObject player;
	public RawImage fadeOutUIImage;
	public float fadeSpeed = 0.8f; 
	public enum FadeDirection

	{
		In, //Alpha = 1
		Out // Alpha = 0
	}
	public string scene;
	// Update is called once per frame
	void Update () {
		if (player.GetComponent <Player>().hadTuto ==false){
			GetComponent <SceneChange>().scene = "Racana_Tuto";
		}
		else{
			GetComponent <SceneChange>().scene = "Racana_Foret";

		}
	}
	void OnEnable()
	{
		StartCoroutine(Fade(FadeDirection.Out));
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player"){
			StartCoroutine (FadeAndLoadScene (FadeDirection.In, scene));
			player.GetComponent <Player> ().hadTuto = true;

			//SceneManager.LoadScene("Showcase");

		}
	}
	private IEnumerator Fade(FadeDirection fadeDirection) 
	{
		float alpha = (fadeDirection == FadeDirection.Out)? 1 : 0;
		float fadeEndValue = (fadeDirection == FadeDirection.Out)? 0 : 1;
		if (fadeDirection == FadeDirection.Out) {
			while (alpha >= fadeEndValue)
			{
				SetColorImage (ref alpha, fadeDirection);
				yield return null;
			}
			fadeOutUIImage.enabled = false; 
		} else {
			fadeOutUIImage.enabled = true; 
			while (alpha <= fadeEndValue)
			{
				SetColorImage (ref alpha, fadeDirection);
				yield return null;
			}
		}
	}
	public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad) 
	{
		yield return Fade(fadeDirection);
		SceneManager.LoadScene(sceneToLoad);
	}
	private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
	{
		fadeOutUIImage.color = new Color (fadeOutUIImage.color.r,fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
		alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out)? -1 : 1) ;
	}
}
