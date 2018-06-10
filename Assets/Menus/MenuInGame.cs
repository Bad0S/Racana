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
		boutons [numSelect].image.color = selected;
		boutons [1].image.color = unselected;
		boutons [2].image.color = unselected;
		boutons [3].image.color = unselected;
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown (KeyCode.Joystick1Button7)) 
		{
			Time.timeScale = 1f;
			gameObject.SetActive (false);
		}
		if (Input.GetButtonDown ("Submit")) {
			switch (numSelect) {
			case 0:
				Time.timeScale = 1f;
				gameObject.SetActive(false);
				break;
			case 1:
				Time.timeScale = 1f;
				gameObject.SetActive(false);
				GameObject.FindGameObjectWithTag("Player").GetComponent<Rythme>().MusicStop();
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
				break;
			case 2:
				Time.timeScale = 1f;
				SceneManager.LoadScene("Racana_Menu");
				break;
			case 3:
				Time.timeScale = 1f;
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
		yield return new WaitForSecondsRealtime (delayPause);
		pause = false;
	}

}
