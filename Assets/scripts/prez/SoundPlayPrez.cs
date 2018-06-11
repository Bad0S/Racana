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
	[FMODUnity.EventRef]
	public string selectsoundBeat;
	FMOD.Studio.EventInstance sndTranquille;
	FMOD.Studio.EventInstance sndNRV;
	FMOD.Studio.EventInstance sndCoupSansInstru;
	FMOD.Studio.EventInstance sndCoupAvecInstru;
	FMOD.Studio.EventInstance sndBeat;

	// Use this for initialization
	void Start () 
	{
		sndTranquille = FMODUnity.RuntimeManager.CreateInstance(selectsoundTranquille);
		sndNRV = FMODUnity.RuntimeManager.CreateInstance(selectsoundNRV);
		sndCoupSansInstru = FMODUnity.RuntimeManager.CreateInstance(selectsoundCoupSansInstru);
		sndCoupAvecInstru = FMODUnity.RuntimeManager.CreateInstance(selectsoundCoupAvecInstru);
		sndBeat = FMODUnity.RuntimeManager.CreateInstance(selectsoundBeat);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			StartBeat();
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			StopBeat();

		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			StartTranquille();

		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			StopTranquille();

		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			StartNRV();

		}
		if (Input.GetKeyDown(KeyCode.H))
		{
			StopNRV();

		}
		if (Input.GetKeyDown(KeyCode.J))
		{
			StartCoupSansInstru();

		}
		if (Input.GetKeyDown(KeyCode.K))
		{
			StartCoupAvecInstru();

		}
	}
	void StartBeat()
	{
		sndBeat.start ();
	}
	void StopBeat()
	{
		sndBeat.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
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
