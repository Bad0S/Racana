using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnim : MonoBehaviour {
	public GameObject animated;

	public void StartAnimation(){
		animated.GetComponent <Animator>().SetBool ("animate", true);//1391
	}
}
