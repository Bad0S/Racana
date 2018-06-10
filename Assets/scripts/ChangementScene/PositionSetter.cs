using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PositionSetter : MonoBehaviour 
{
	private bool created;
	public List<Vector2> RespawnPos;
	public bool canMusic;
	public bool hadTuto;
	public bool killedBoss;
	public List<string> scenes;
	public bool hasPlayed;
	public bool firstVillage;
	private Animator camAnim;

	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
		created = true;
		scenes = new List<string>();
        scenes.Add(SceneManager.GetActiveScene().name);
        RespawnPos.Add(new Vector2(48.6f, 92.1f));
	}
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		scenes.Insert (0,scene.name);
		RespawnPos.Insert (0,GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ().position);
		if (scene.name == "Racana_Village" && firstVillage == false) 
		{
			StartCoroutine (IntroVillage ());
			firstVillage = true;
		}
	}

	IEnumerator IntroVillage()
	{
		camAnim = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Animator> ();
		camAnim.SetTrigger ("StartVillage");
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().canMove = false;
		yield return new WaitForSecondsRealtime (9.5f);
		camAnim.enabled = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ().canMove = true;
	}
}
