using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class Rythme : MonoBehaviour
{
    public float bpmInitial = 110;
    public float bpm;
    private AudioSource sourceSon;
    public float timeBetweenBeatsInSeconds;
    private float timeRBetweenBeats;
    private float musicTime;
    public int beats = 1;
    public float combo;
    public PostProcessingProfile initial;
    public PostProcessingProfile transe;
	public PostProcessingProfile Transcendance;
	public bool isBeating;
    private AudioSource[] sources;
    // Use this for initialization
    void Start()
    {
        sourceSon = GetComponent<AudioSource>();
        bpm = bpmInitial;
        sources = GameObject.FindGameObjectWithTag("MainCamera").GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        musicTime += Time.deltaTime;
        timeRBetweenBeats += Time.deltaTime;
        bpm = bpmInitial * sourceSon.pitch;
        timeBetweenBeatsInSeconds = 60 / bpm;
		//print (isBeating); 

		if (musicTime >= timeBetweenBeatsInSeconds * beats) {
			beats += 1;
			isBeating = true;
			timeRBetweenBeats = 0;
		} else{
			isBeating = false;

		}
		if (combo <= 0) 
		{
            foreach (AudioSource source in sources)
            {
                if (source.priority == 128)
                {
                    source.volume = 1f;
                }
                if (source.priority != 128)
                {
                    source.volume = 0f;
                }
            }
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = initial;
            //GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled = false;
		}
		if (combo < 30 && combo > 0)
        {
            foreach (AudioSource source in sources)
            {
                if (source.priority == 129)
                {
                    source.volume = combo/30f;
                }
                if (source.priority != 129)
                {
                    source.volume = 0f;
                }
            }
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = transe;
			//GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled = false;
			GetComponent<Player> ().MovSpeed = 0.1f + combo/600;
			sourceSon.pitch = 1 + combo / 300;
        }
		if (combo >= 30)
		{
            foreach (AudioSource source in sources)
            {
                if (source.priority == 130)
                {
                    source.volume = 1f;
                }
                if (source.priority != 130)
                {
                    source.volume = 0f;
                }
            }
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = Transcendance;
			//GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled= true;
			GetComponent<Player> ().MovSpeed = 0.1f;
			GetComponent<Player> ().transcendance = true;
		}
    }
}