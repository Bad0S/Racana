using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheBossFader : MonoBehaviour {
	private Color alphaColor;
	private float time;
	public bool activation;
	// Use this for initialization
	void Start () {
		alphaColor = GetComponent<SpriteRenderer>().color;
		alphaColor.a = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (activation == true){
			time += Time.deltaTime;
			GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, alphaColor, time);
		}

	}
}
