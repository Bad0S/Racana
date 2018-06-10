using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuPrincipal : MonoBehaviour
{
	public List<Button> boutons;
	public int numSelect = 0;
	public float delayPause = 0.25f;
	public bool pause;
	private Color selected = new Color (255, 255, 255, 1f);
	private Color unselected = new Color (255,255,255,0.4f);
	public Animator animMenu;
	public string sceneToLoad;
	public RawImage fadeOutUIImage;
	public float fadeSpeed = 0.8f; 
	public enum FadeDirection
	{
		In, //Alpha = 1
		Out // Alpha = 0
	}

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxisRaw("Vertical") < 1)
        {
            boutons[selected].GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.5f);
            if (selected == boutons.Count - 1)
            { selected = 0; }
            else
            { selected += 1; }
        }
        if (Input.GetAxisRaw("Vertical") > 1)
        {
            boutons[selected].GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.5f);
            if (selected == 0 )
            { selected = boutons.Count - 1; }
            else
            { selected -= 1; }
        }

        boutons[selected].GetComponent<SpriteRenderer>().color = new Color(255f,255f,255f,1f);
	}

	void QuitGame()
	{
		Application.Quit ();
	}

	void FadeAndLoadFunction()
	{
		StartCoroutine (FadeAndLoadScene(FadeDirection.In, sceneToLoad));
	}

	IEnumerator StartGame()
	{
		animMenu.SetTrigger ("Start");
		yield return new WaitForSeconds (0.1f);
	}

	IEnumerator Pause()
	{
		yield return new WaitForSeconds (delayPause);
		pause = false;
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

public class MenuPrincipal : MonoBehaviour
	void Start ()
    {
		
	void Update ()
    {
        if (Input.GetAxisRaw("Vertical") < 1)
        {
            boutons[selected].GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.5f);
            if (selected == boutons.Count - 1)
            { selected = 0; }
            else
            { selected += 1; }
        }
        if (Input.GetAxisRaw("Vertical") > 1)
        {
            boutons[selected].GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.5f);
            if (selected == 0 )
            { selected = boutons.Count - 1; }
            else
            { selected -= 1; }
        }

        boutons[selected].GetComponent<SpriteRenderer>().color = new Color(255f,255f,255f,1f);