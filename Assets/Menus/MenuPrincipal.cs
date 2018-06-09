﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuPrincipal : MonoBehaviour 
{
	public List<Button> boutons;
	public int numSelect = 0;
	public float delayPause = 0.25f;
	public bool pause;
	private Color selected = new Color (255, 255, 255, 1f);
	private Color unselected = new Color (255,255,255,0.4f);
	public Animator animMenu;
	public string sceneToLoad;
	public RawImage fadeOutUIImage;
	public float fadeSpeed = 0.8f; 
	public enum FadeDirection
	{
		In, //Alpha = 1
		Out // Alpha = 0
	}

	// Use this for initialization
	void Start () 
	{
		boutons [numSelect].image.color = selected;
		boutons [1].image.color = unselected;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Submit")) {
			switch (numSelect) {
			case 0:
				StartCoroutine (StartGame ());
				break;
			case 1:
				QuitGame ();
				break;
			}
		} else if (Input.GetAxisRaw ("Vertical") > 0 && !pause) 
		{
			boutons [numSelect].image.color = unselected;
			if (numSelect > 0)
			{
				numSelect--;
			}
			else if (numSelect == 0)
			{
				numSelect = boutons.Count - 1;
			}
			boutons [numSelect].image.color = selected;
			pause = true;
			StartCoroutine (Pause ());
			
		} else if (Input.GetAxisRaw ("Vertical") < 0 && !pause) 
		{
			boutons [numSelect].image.color = unselected;
			if (numSelect < boutons.Count - 1)
			{
				numSelect++;
			}
			else if (numSelect == boutons.Count - 1)
			{
				numSelect = 0;
			}
			boutons [numSelect].image.color = selected;
			pause = true;
			StartCoroutine (Pause ());
		}
	}

	void QuitGame()
	{
		Application.Quit ();
	}

	void FadeAndLoadFunction()
	{
		StartCoroutine (FadeAndLoadScene(FadeDirection.In, sceneToLoad));
	}

	IEnumerator StartGame()
	{
		animMenu.SetTrigger ("Start");
		yield return new WaitForSeconds (0.1f);
	}

	IEnumerator Pause()
	{
		yield return new WaitForSeconds (delayPause);
		pause = false;
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