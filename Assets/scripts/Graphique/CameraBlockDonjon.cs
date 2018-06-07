using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlockDonjon : MonoBehaviour {
	Camera mainCam;
	Transform transCam;
	// Use this for initialization
	void Start () {
		mainCam = Camera.main;
		transCam = mainCam.transform;
	}


	// Update is called once per frame
	void Update () {
		if(transCam.position.y < -46 ){
			transCam.position = new Vector3( transCam.position.x, -46, transCam.position.z);
		}
		
	}
}
