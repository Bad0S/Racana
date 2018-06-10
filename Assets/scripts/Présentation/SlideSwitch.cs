using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideSwitch : MonoBehaviour {
	[System.Serializable]
	public class GO
	{
		public List<GameObject> list;
	}

	[System.Serializable]
	public class NestedGOList
	{
		public List<GO> list;
	}

	public int slide = 0;
	public List<GameObject> slideObjet;
	public NestedGOList Animations;
	public List<GameObject> slideNumber;
	public List<int> eventsParSlide;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < slideObjet.Count; i++) {
			eventsParSlide [i] = Animations.list [i].list.Count;
		}
	}
	
	// Update is called once per frame
	void Update () {



		if(Input.GetButtonDown ("DiapoSuivant") == true){
			if(eventsParSlide[slide] ==0){
				slide++;

			}
			else{
				Animations.list [slide].list [eventsParSlide [slide ]- 1].GetComponent <Animator> ().SetBool ("animate", true);

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
