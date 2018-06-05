using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntréeTuto : MonoBehaviour {
	public GameObject player;
	public RawImage fadeOutUIImage;
	public float fadeSpeed = 0.8f; 
	bool inDial;

	public enum FadeDirection

	{
		In, //Alpha = 1
		Out // Alpha = 0
	}
	public string scene;
	// Update is called once per frame
	void Start(){
		player = GameObject.FindWithTag ("Player");
	}

	void Update () {
		player.GetComponent <Player> ().inDanger = false;
		if (player.GetComponent <Player>().hadTuto ==false){
			scene = "Racana_Tuto";
		}
		else{
			scene = "Racana_Foret";

		}
		inDial = GetComponentInChildren <DialogueComponent> ().inDialogue;
		if(inDial == true){
			GetComponent <BoxCollider2D> ().enabled = false;

		}else {
			GetComponent <BoxCollider2D> ().enabled = true;

		}
	}
	void OnEnable()
	{
		StartCoroutine(Fade(FadeDirection.Out));
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player" && inDial == false  && GetComponentInChildren <StartDialTuto>().entered == true) {
			StartCoroutine (FadeAndLoadScene (FadeDirection.In, scene));
			player.GetComponent <Player> ().hadTuto = true;

			//SceneManager.LoadScene("Showcase");

		}
	}
	private IEnumerator Fade(FadeDirection fadeDirection) 
	{
		float alpha = (fadeDirection == FadeDirection.Out)? 1 : 0;
		float fadeEndValue = (fadeDirection == FadeDirection.Out)? 0 : 1;
		if (fadeDirection == FadeDirection.Out) {
			while (alpha >= fadeEndValue)
			{
				SetColorImage (ref alpha, fadeDirection);
				yield return null;
			}
			fadeOutUIImage.enabled = false; 
		} else {
			fadeOutUIImage.enabled = true; 
			while (alpha <= fadeEndValue)
			{
				SetColorImage (ref alpha, fadeDirection);
				yield return null;
			}
		}
	}
	public IEnumerator FadeAndLoadScene(FadeDirection fadeDirection, string sceneToLoad) 
	{
		yield return Fade(fadeDirection);
		SceneManager.LoadScene(sceneToLoad);
	}
	private void SetColorImage(ref float alpha, FadeDirection fadeDirection)
	{
		fadeOutUIImage.color = new Color (fadeOutUIImage.color.r,fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
		alpha += Time.deltaTime * (1.0f / fadeSpeed) * ((fadeDirection == FadeDirection.Out)? -1 : 1) ;
	}
}
