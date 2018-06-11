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
	public Vector3 Pos4;
	public Vector3 Pos5;
	public Vector3 Pos6;
    public float portée;
    public float distance;
	public bool active;
	public GameObject bambou;

	public GameObject player;
	public GameObject respawn;
	public GameObject FX;
	public bool bambouSpawn;


    // Use this for initialization
    void Start ()
    {
		Pos1 = new Vector3 (-1104f, -210f);
		Pos2 = new Vector3 (-758f,-223f);
		Pos3 = new Vector3 (-932f, -370f);
		Pos4 = new Vector3(-1300, -377f);
		Pos5 = new Vector3(-1311, -350f);
		Pos6 = new Vector3(-1323, -368f);
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
		player.GetComponent<Player> ().canMove = false;
		player.GetComponent<Player> ().canAttack = false;
		player.GetComponent<Player> ().canDash = false;
		GameObject[] ennemisArray = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject ennemi in ennemisArray) 
		{
			if (ennemi.GetComponent<EnemyBehaviour> () != null) 
			{ennemi.GetComponent<EnemyBehaviour> ().enabled = false;}
			if (ennemi.GetComponent<BambouBehaviour> () != null) 
			{ennemi.GetComponent<BambouBehaviour> ().enabled = false;}
		}
		yield return new WaitForSeconds (1.1f);

		scene = SceneManager.GetActiveScene();
		//respawn.GetComponent<PositionSetter>().RespawnPos.Add(GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);
		//respawn.GetComponent<PositionSetter>().scenes.Add(scene.name);
		player.GetComponent<Player> ().canMusic = true;
		yield return new WaitForSeconds (3f);
		player.GetComponent<Rythme> ().combo = 0f;
		//respawn.GetComponent<PositionSetter>().canMusic = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMusic;
		//respawn.GetComponent<PositionSetter>().hadTuto = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hadTuto;
		GameObject.FindGameObjectWithTag ("Player").GetComponent <Player>().enabled = true ;
		foreach (GameObject ennemi in ennemisArray) 
		{
			if (ennemi.GetComponent<EnemyBehaviour> () != null) 
			{ennemi.GetComponent<EnemyBehaviour> ().enabled = true;}
			if (ennemi.GetComponent<BambouBehaviour> () != null) 
			{ennemi.GetComponent<BambouBehaviour> ().enabled = true;}
		}

		yield return new WaitForSeconds (1.1f);
		sndBeatGardien.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
		player.GetComponent<Rythme> ().MusicPlay();
		player.GetComponent<Player> ().canDash = true;
		player.GetComponent<Player> ().canMove = true;
		player.GetComponent<Player> ().canAttack = true;
		player.GetComponent<health> ().enabled = true;
		player.GetComponent <Player>().RB.SetActive (true);
		player.GetComponent <Player>().RBActivated = true;
		print ("test");


		yield return new WaitForSeconds (1.1f);
		if (!bambouSpawn) 
		{
			Instantiate (bambou, Pos1, Quaternion.identity);
			Instantiate (bambou, Pos2, Quaternion.identity);
			Instantiate (bambou, Pos3, Quaternion.identity);
			GameObject bambouCopy1 = Instantiate(bambou, Pos4, Quaternion.identity);
			bambouCopy1.GetComponent <BambouBehaviour>().attackRangeMax *=0.5f;
			GameObject bambouCopy2 = Instantiate(bambou, Pos5, Quaternion.identity);
			bambouCopy1.GetComponent <BambouBehaviour>().attackRangeMax *=0.5f;
			GameObject bambouCopy3 = Instantiate(bambou, Pos6, Quaternion.identity);
			bambouCopy1.GetComponent <BambouBehaviour>().attackRangeMax *=0.5f;

            bambouSpawn = true;
		}
		active = true;
	}
}
