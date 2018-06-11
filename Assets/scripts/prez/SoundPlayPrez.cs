using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayPrez : MonoBehaviour 
{
	[FMODUnity.EventRef]
	public string selectsoundTranquille;
	[FMODUnity.EventRef]
	public string selectsoundNRV;
	[FMODUnity.EventRef]
	public string selectsoundCoupSansInstru;
	[FMODUnity.EventRef]
	public string selectsoundCoupAvecInstru;
	FMOD.Studio.EventInstance sndTranquille;
	FMOD.Studio.EventInstance sndNRV;
	FMOD.Studio.EventInstance sndCoupSansInstru;
	FMOD.Studio.EventInstance sndCoupAvecInstru;

	// Use this for initialization
	void Start () 
	{
		sndTranquille = FMODUnity.RuntimeManager.CreateInstance(selectsoundTranquille);
		sndNRV = FMODUnity.RuntimeManager.CreateInstance(selectsoundNRV);
		sndCoupSansInstru = FMODUnity.RuntimeManager.CreateInstance(selectsoundCoupSansInstru);
		sndCoupAvecInstru = FMODUnity.RuntimeManager.CreateInstance(selectsoundCoupAvecInstru);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void StartTranquille()
	{
		sndTranquille.start ();
	}
	void StopTranquille()
	{
		sndTranquille.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}
	void StartNRV()
	{
		sndNRV.start ();
	}
	void StopNRV()
	{
		sndNRV.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
	}
	void StartCoupSansInstru()
	{
		sndCoupSansInstru.start ();
	}
	void StartCoupAvecInstru()
	{
		sndCoupAvecInstru.start ();
	}
}
