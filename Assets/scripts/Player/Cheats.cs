using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Cheats : MonoBehaviour 
{
	public Text titresPartie;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.R) == true || Input.GetKey(KeyCode.JoystickButton6))
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<Rythme>().MusicStop();
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMusic = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rythme>().combo = 29;
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMusic = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rythme>().combo = 500;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMusic = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rythme>().combo = 0;
        }
        if (Input.GetKeyDown(KeyCode.I) && GameObject.FindGameObjectWithTag("Player").GetComponent<health>().life < 4)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<health>().life = 4;
        }
        if (Input.GetKeyDown(KeyCode.I) && GameObject.FindGameObjectWithTag("Player").GetComponent<health>().life > 3)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<health>().life = 3;
        }
        if (Input.GetKeyDown(KeyCode.O) && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hadTuto == false)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hadTuto = true;
        }
        if (Input.GetKeyDown(KeyCode.O) && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hadTuto == true)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hadTuto = false;
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rythme>().MusicStop();
            SceneManager.LoadScene("Racana_Maison_hero");
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rythme>().MusicStop();
            SceneManager.LoadScene("Racana_Village");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rythme>().MusicStop();
            SceneManager.LoadScene("Racana_Foret");
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Rythme>().MusicStop();
            SceneManager.LoadScene("Racana_Donjon_LD");
        }
		if (Input.GetKeyDown(KeyCode.Keypad4))
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<Rythme>().MusicStop();
			SceneManager.LoadScene("Racana_Donjon_LD");
			//GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraBehaviour>().triggerBoss.transform.position;
		}
		if (Input.GetKeyDown(KeyCode.Keypad5))
		{
			GameObject.FindGameObjectWithTag("Player").GetComponent<Rythme>().MusicStop();
			SceneManager.LoadScene("Racana_Presentation");
		}
    }
}
