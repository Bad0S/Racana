using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlockForet : MonoBehaviour {
	Camera mainCam;
	Transform transCam;
	// Use this for initialization
	void Start () {
		mainCam = Camera.main;
		transCam = mainCam.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(transCam.position.x<-330 &&transCam.position.y < -1550 ){
			transCam.position = new Vector3( -330, transCam.position.y, transCam.position.z);
		}


	}
}
