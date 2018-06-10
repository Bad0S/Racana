using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuInGame : MonoBehaviour 
{

	public List<Button> boutons;
	public int numSelect = 0;
	public float delayPause = 0.25f;
	public bool pause;
	private Color selected = new Color (255, 255, 255, 1f);
	private Color unselected = new Color (255,255,255,0.4f);

	// Use this for initialization
	void Start () 
	{
		Time.timeScale = 0f;
		boutons [numSelect].image.color = selected;
		boutons [1].image.color = unselected;
		boutons [2].image.color = unselected;
		boutons [3].image.color = unselected;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Joystick1Button7) ) 
		{
			gameObject.SetActive (false);
		}
		if (Input.GetButtonDown ("Submit")) {
			switch (numSelect) {
			case 0:
				Time.timeScale = 1f;
				Destroy (gameObject);
				break;
			case 1:
				Time.timeScale = 1f;
				Destroy (gameObject);
				break;
			case 2:
				Time.timeScale = 1f;
				StartCoroutine (GameObject.FindGameObjectWithTag ("Player").GetComponent<health> ().PlayerDeath ());
				Destroy (gameObject);
				break;
			case 3:
				SceneManager.LoadScene("Racana_Menu");
				break;
			case 4:
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



	IEnumerator Pause()
	{
		yield return new WaitForSeconds (delayPause);
		pause = false;
	}

}
