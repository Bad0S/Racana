using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardienManager : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string selectsoundTransitionGardien;
    [FMODUnity.EventRef]
    public string selectsoundBeatGardien;
    [FMODUnity.EventRef]
    public string selectsoundTransition;
    FMOD.Studio.EventInstance sndTransitionGardien;
    FMOD.Studio.EventInstance sndBeatGardien;
    FMOD.Studio.EventInstance sndTransition;

    public float portée;
    public float distance;


    // Use this for initialization
    void Start ()
    {
        sndTransitionGardien = FMODUnity.RuntimeManager.CreateInstance(selectsoundTransitionGardien);
        sndBeatGardien = FMODUnity.RuntimeManager.CreateInstance(selectsoundBeatGardien);
        sndTransition = FMODUnity.RuntimeManager.CreateInstance(selectsoundTransition);
        sndBeatGardien.start();
        sndBeatGardien.setVolume(0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        distance = Vector2.SqrMagnitude(transform.position - GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position);
        sndBeatGardien.setVolume((portée-distance)/portée);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.GetComponent<Player>().canMusic == false)
        {
            sndTransition.start();
            sndTransitionGardien.start();
            collision.GetComponent<Player>().canMusic = true;
            collision.GetComponent<Rythme>().combo = 0f;
            sndBeatGardien.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
}
