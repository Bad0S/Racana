using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GardienManager : MonoBehaviour
{
    public Scene scene;
    [FMODUnity.EventRef]
    public string selectsoundTransitionGardien;
    [FMODUnity.EventRef]
    public string selectsoundBeatGardien;
    [FMODUnity.EventRef]
    public string selectsoundTransition;
    FMOD.Studio.EventInstance sndTransitionGardien;
    FMOD.Studio.EventInstance sndBeatGardien;
    FMOD.Studio.EventInstance sndTransition;

	public Vector3 Pos1;
	public Vector3 Pos2;
	public Vector3 Pos3;
    public float portée;
    public float distance;
	public bool active;
	public GameObject bambou;

	public GameObject player;
	public GameObject respawn;
	public GameObject FX;

    // Use this for initialization
    void Start ()
    {
		Pos1 = new Vector3 (-1104f, -210f);
		Pos2 = new Vector3 (-758f,-223f);
		Pos3 = new Vector3 (-932f, -370f);
		active = false;
        sndTransitionGardien = FMODUnity.RuntimeManager.CreateInstance(selectsoundTransitionGardien);
        sndBeatGardien = FMODUnity.RuntimeManager.CreateInstance(selectsoundBeatGardien);
        sndTransition = FMODUnity.RuntimeManager.CreateInstance(selectsoundTransition);
        sndBeatGardien.start();
        sndBeatGardien.setVolume(0f);
		player = GameObject.FindGameObjectWithTag ("Player");
		respawn = GameObject.FindGameObjectWithTag ("Respawn");
	}
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector2.SqrMagnitude(transform.position - GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);
        sndBeatGardien.setVolume((portée-distance)/portée);

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
		if (collision.tag == "Player" && collision.GetComponent<Player>().canMusic == false && active == false&& GetComponentInChildren <StarDialGardien>().read == true)
        {
			FX.SetActive (true);
			StartCoroutine (GardienCoroutine ());
            //sndTransition.start();
			//sndTransitionGardien.start();
			player.GetComponent<Rythme> ().MusicStop();




        }
    }

	IEnumerator GardienCoroutine()
	{
		yield return new WaitForSeconds (1.1f);
		player.GetComponent<Player> ().canMusic = true;
		player.GetComponent<Player> ().canMove = true;
		player.GetComponent<Player> ().canAttack = true;
		player.GetComponent<Player> ().canDash = true;
		player.GetComponent<Rythme> ().combo = 0f;

		yield return new WaitForSeconds (1.1f);

		scene = SceneManager.GetActiveScene();
		respawn.GetComponent<PositionSetter>().RespawnPos.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);
		respawn.GetComponent<PositionSetter>().scenes.Add(scene.name);
		respawn.GetComponent<PositionSetter>().canMusic = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMusic;
		respawn.GetComponent<PositionSetter>().hadTuto = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hadTuto;

		yield return new WaitForSeconds (1.1f);

		sndBeatGardien.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		player.GetComponent<Rythme> ().MusicPlay();

		yield return new WaitForSeconds (1.1f);

		GameObject Bambou = (GameObject)Instantiate (bambou, Pos1 ,Quaternion.identity);
		GameObject Bambou2 = (GameObject)Instantiate (bambou, Pos2 ,Quaternion.identity);
		GameObject Bambou3 = (GameObject)Instantiate (bambou, Pos3 ,Quaternion.identity);
		active = true;
	}
}
