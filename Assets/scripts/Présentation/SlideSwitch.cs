using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideSwitch : MonoBehaviour {
	public int slide = 0;
	public List<GameObject> slideObjet;
	public List<GameObject> slideNumber;
	public List<int> eventsParSlide;
	public GameObject text1;
	public GameObject image1;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {



		if(Input.GetButtonDown ("DiapoSuivant") == true){
			if(eventsParSlide[slide] ==0){
				slide++;

			}
			else{
				Debug.Log ("bite");
				Instantiate (text1,slideObjet[slide].transform);
				Instantiate (image1,slideObjet[slide].transform);

				eventsParSlide [slide]--;
			}
			slideObjet [slide ].SetActive (true);
			slideObjet [slide - 1].SetActive (false);
			numberSlide (slide);

		}
		if(Input.GetButtonDown ("DiapoPrécédent") == true&& slide>=0){
			slide--;
			slideObjet [slide ].SetActive (true);
			slideObjet [slide + 1].SetActive (false);
			numberSlide (slide);

		}


	}
	void numberSlide(int page){
		for (int i = 0; i < slideNumber.Count; i++) {
			slideNumber [i].GetComponent <Text> ().fontSize = 23;
			slideNumber [i].GetComponent <Text> ().color = Color.grey;
		}
		slideNumber [page].GetComponent <Text> ().fontSize = 42;
		slideNumber [page].GetComponent <Text> ().color = Color.white;
	}
}
