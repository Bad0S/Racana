using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class Rythme : MonoBehaviour
{
    public float bpmInitial;
    public float bpm;
    private AudioSource sourceSon;
    public float timeBetweenBeatsInSeconds;
    public float musicTime;
    public int beats = 1;
    public float combo;
    public PostProcessingProfile initial;
    public PostProcessingProfile transe;
	public PostProcessingProfile Transcendance;
	public bool isBeating;
    public bool boss;

    [FMODUnity.EventRef]
    public string selectsoundVillage;
    [FMODUnity.EventRef]
    public string selectsoundForêt;
    [FMODUnity.EventRef]
    public string selectsoundForêtBase;
    [FMODUnity.EventRef]
    public string selectsoundForêtTranse;
    [FMODUnity.EventRef]
    public string selectsoundForêtTranscendance;
    [FMODUnity.EventRef]
    public string selectsoundDonjonBase;
    [FMODUnity.EventRef]
    public string selectsoundDonjonTranse;
    [FMODUnity.EventRef]
    public string selectsoundDonjonTranscendance;
    [FMODUnity.EventRef]
    public string selectsoundBossBase;
    [FMODUnity.EventRef]
    public string selectsoundBossTranse;
    [FMODUnity.EventRef]
    public string selectsoundBossTranscendance;
    FMOD.Studio.EventInstance sndTheme;
    FMOD.Studio.EventInstance sndBase;
    FMOD.Studio.EventInstance sndTranse;
    FMOD.Studio.EventInstance sndTranscendance;
	FMOD.Studio.PLAYBACK_STATE playState;
    // Use this for initialization

    //diminution combo
    float timerComboPas;
	public float timerCombo;
	public float timerComboSpeed;
	public float comboDecreaseSpeed =1;

	public bool tuto;


    void Start()
    {
		if (SceneManager.GetActiveScene().name != "Racana_Foret" && SceneManager.GetActiveScene().name != "Racana_Donjon_LD")
        {
            sndTheme = FMODUnity.RuntimeManager.CreateInstance(selectsoundVillage);
            sndBase = FMODUnity.RuntimeManager.CreateInstance(selectsoundVillage);
            sndTranse = FMODUnity.RuntimeManager.CreateInstance(selectsoundVillage);
            sndTranscendance = FMODUnity.RuntimeManager.CreateInstance(selectsoundVillage);
        }
        if (SceneManager.GetActiveScene().name == "Racana_Foret" )
        {
            sndTheme = FMODUnity.RuntimeManager.CreateInstance(selectsoundForêt);
            sndBase = FMODUnity.RuntimeManager.CreateInstance(selectsoundForêtBase);
            sndTranse = FMODUnity.RuntimeManager.CreateInstance(selectsoundForêtTranse);
            sndTranscendance = FMODUnity.RuntimeManager.CreateInstance(selectsoundForêtTranscendance);
        }
        if (SceneManager.GetActiveScene().name == "Racana_Donjon_LD")
        {
            gameObject.GetComponent<Player>().canMusic = true;
            combo = 0;
            sndTheme = FMODUnity.RuntimeManager.CreateInstance(selectsoundDonjonBase);
            sndBase = FMODUnity.RuntimeManager.CreateInstance(selectsoundDonjonBase);
            sndTranse = FMODUnity.RuntimeManager.CreateInstance(selectsoundDonjonTranse);
            sndTranscendance = FMODUnity.RuntimeManager.CreateInstance(selectsoundDonjonTranscendance);
        }
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMusic && gameObject.tag == "Player")
		{ 
			combo = 0;
		} 
        bpm = bpmInitial;

        //if (playState != FMOD.Studio.PLAYBACK_STATE.PLAYING) 
        if (!tuto)
        {
            MusicPlay();
        }
		
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (!GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMusic && gameObject.tag == "Player")
        { 
			combo = -1;
			GetComponent<Player> ().MovSpeed = 150;
		}
        if (boss == true)
        {
            sndTheme = FMODUnity.RuntimeManager.CreateInstance(selectsoundBossBase);
            sndBase = FMODUnity.RuntimeManager.CreateInstance(selectsoundBossBase);
            sndTranse = FMODUnity.RuntimeManager.CreateInstance(selectsoundBossTranse);
            sndTranscendance = FMODUnity.RuntimeManager.CreateInstance(selectsoundBossTranscendance);
        }

        timerCombo += Time.deltaTime;
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
        timeBetweenBeatsInSeconds = 60 / bpm;
		if (musicTime >= timeBetweenBeatsInSeconds * beats)
        {
			beats += 1;
			isBeating = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canAttack = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canDash = true;
        }
        else
        {
            isBeating = false;
		}
        if (combo < 0)
        {
            sndTheme.setVolume(1f);
            sndBase.setVolume(0f);
            sndTranse.setVolume(0f);
            sndTranscendance.setVolume(0f);
        }
        if (combo == 0 && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMusic) 
		{
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = initial;
			//GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled = false;
			GetComponent<Player> ().MovSpeed = 150;
            sndTheme.setVolume(0f);
            sndBase.setVolume(1f);
            sndTranse.setVolume(0f);
            sndTranscendance.setVolume(0f);
        }
		if (combo < 20 && combo > 0 && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMusic)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = transe;
            //GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled = false;
            GetComponent<Player> ().MovSpeed = 150 + combo*2;
            sndTheme.setVolume(0f);
            sndBase.setVolume(0f);
            sndTranse.setVolume(1f);
            sndTranscendance.setVolume(0f);
        }
		if (combo >= 20 && GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().canMusic)
		{
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = Transcendance;
            //GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled= true;
            GetComponent<Player> ().MovSpeed = 200;
			GetComponent<Player> ().transcendance = true;
            sndTheme.setVolume(0f);
            sndBase.setVolume(0f);
			sndTranse.setVolume (0f);
            sndTranscendance.setVolume(1f);
        }
    }

    public void MusicStop()
    {
        sndTheme.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        sndBase.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        sndTranse.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        sndTranscendance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void MusicPlay()
    {
        sndTheme.start();
        sndBase.start();
        sndTranse.start();
        sndTranscendance.start();
    }
}