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

    public string selectsoundNormal;
    public string selectsoundTranse;
    public string selectsoundTranscendance;
    FMOD.Studio.EventInstance soundNormal;
    FMOD.Studio.EventInstance soundTranse;
    FMOD.Studio.EventInstance soundTranscendance;
    // Use this for initialization

	//diminution combo
	float timerComboPas;
	public float timerCombo;
	public float timerComboSpeed;
	public float comboDecreaseSpeed =1;

    void Start()
    {
        soundNormal = FMODUnity.RuntimeManager.CreateInstance(selectsoundNormal);
        soundTranse = FMODUnity.RuntimeManager.CreateInstance(selectsoundTranse);
        soundTranscendance = FMODUnity.RuntimeManager.CreateInstance(selectsoundTranscendance);

        sourceSon = GetComponent<AudioSource>();
        bpm = bpmInitial;
        sources = GameObject.FindGameObjectWithTag("MainCamera").GetComponents<AudioSource>();
        soundNormal.start();
        soundTranse.start();
        soundTranscendance.start();
        soundTranse.setVolume(0f);
        soundTranscendance.setVolume(0f);
    }

    // Update is called once per frame
    void Update()
    {
		timerCombo+= Time.deltaTime;
		timerComboSpeed+= Time.deltaTime;
		timerComboPas += Time.deltaTime;
		if(timerCombo> timeBetweenBeatsInSeconds *4*comboDecreaseSpeed&&timerComboPas>timeBetweenBeatsInSeconds&& combo>0){
			timerComboPas = 0;
			combo--;
			timerCombo = 0;
		}
		if(timerComboSpeed>timeBetweenBeatsInSeconds*4&&comboDecreaseSpeed>0f&&combo<=20){
			comboDecreaseSpeed -= 0.25f;
			timerComboSpeed = 0;
		}
		else if(combo>20 && comboDecreaseSpeed>0.5f){
			comboDecreaseSpeed = 0.5f;
		}
		else if(timerComboSpeed>timeBetweenBeatsInSeconds*4&&comboDecreaseSpeed>0){
			comboDecreaseSpeed -= 0.5f;
			timerComboSpeed = 0;
		}

        musicTime += Time.deltaTime;
        timeRBetweenBeats += Time.deltaTime;
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
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = initial;
            //GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled = false;
            soundNormal.setVolume(1f);
            soundTranse.setVolume(0f);
            soundTranscendance.setVolume(0f);
        }
		if (combo < 20 && combo > 0)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = transe;
			//GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled = false;
			GetComponent<Player> ().MovSpeed = 0.1f + combo/600;
            soundNormal.setVolume(0f);
            soundTranse.setVolume(combo/30);
            soundTranscendance.setVolume(0f);
        }
		if (combo >= 20)
		{
            foreach (AudioSource source in sources)
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = Transcendance;
			//GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled= true;
			GetComponent<Player> ().MovSpeed = 0.1f;
			GetComponent<Player> ().transcendance = true;
            soundNormal.setVolume(0f);
            soundTranse.setVolume(0f);
            soundTranscendance.setVolume(1f);
        }
    }
}