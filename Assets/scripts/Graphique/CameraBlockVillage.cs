using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBlockVillage : MonoBehaviour {
	Camera mainCam;
	Transform transCam;
	// Use this for initialization
	void Start () {
		mainCam = Camera.main;
		transCam = mainCam.transform;
	}

	
	// Update is called once per frame
	void Update () {
		if(transCam.position.x<-620 &&transCam.position.y < -690 ){
			transCam.position = new Vector3( transCam.position.x, -690, transCam.position.z);
		}
		else if(transCam.position.x<-225 &&transCam.position.y < -750 ){
			transCam.position = new Vector3( transCam.position.x, -750, transCam.position.z);
		}
	}
}
