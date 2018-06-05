using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class PositionSetter : MonoBehaviour 
{
	private static bool created;
	public List<Vector2> RespawnPos;
	public bool canMusic;
	public bool hadTuto;
	public List<string> scenes;
	public static bool hasPlayed;

	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
		created = true;
		scenes = new List<string>();
	}
	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	void Start ()
	{
		if (!hasPlayed) 
		{
			GameObject.FindGameObjectWithTag ("Player").GetComponent<Rythme> ().MusicPlay ();
			hasPlayed = true;
		}
	}
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		scenes.Insert (0,scene.name);
		RespawnPos.Insert (0,GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ().position);
	}
}
