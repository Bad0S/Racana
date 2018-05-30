using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

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

    public string selectsoundNormal;
    public string selectsoundTranse;
    public string selectsoundTranscendance;
 //   FMOD.Studio.EventInstance soundNormal;
   // FMOD.Studio.EventInstance soundTranse;
 //   FMOD.Studio.EventInstance soundTranscendance;
    // Use this for initialization

	//diminution combo
	float timerComboPas;
	public float timerCombo;
	public float timerComboSpeed;
	public float comboDecreaseSpeed =1;

    void Start()
    {
     // 	soundNormal = FMODUnity.RuntimeManager.CreateInstance(selectsoundNormal);
     //   soundTranse = FMODUnity.RuntimeManager.CreateInstance(selectsoundTranse);
     //   soundTranscendance = FMODUnity.RuntimeManager.CreateInstance(selectsoundTranscendance);
        
        bpm = bpmInitial;
    //	FMOD.Studio.PLAYBACK_STATE fmodPbState;
     //   soundNormal.getPlaybackState(out fmodPbState);
     //   if (fmodPbState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
     //   {
    //        soundNormal.start();
    //        soundTranse.start();
    //        soundTranscendance.start();
     //   }
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
		if (combo <= 0) 
		{
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = initial;
            //GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled = false;
         //   soundNormal.setVolume(1f);
         //   soundTranse.setVolume(0f);
         //   soundTranscendance.setVolume(0f);
        }
		if (combo < 20 && combo > 0)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = transe;
			//GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled = false;
			GetComponent<Player> ().MovSpeed = 200 + combo*2;
           // soundNormal.setVolume(0f);
          //  soundTranse.setVolume(combo/30);
          //  soundTranscendance.setVolume(0f);
        }
		if (combo >= 20)
		{
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile = Transcendance;
			//GameObject.FindGameObjectWithTag ("MainCamera").GetComponentInChildren<SpriteRenderer> ().enabled= true;
			GetComponent<Player> ().MovSpeed = 250;
			GetComponent<Player> ().transcendance = true;
         //   soundNormal.setVolume(0f);
			//soundTranse.setVolume (0f);
          //  soundTranscendance.setVolume(1f);
        }
    }
}