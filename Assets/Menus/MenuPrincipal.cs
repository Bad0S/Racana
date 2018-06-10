using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public List<GameObject> boutons;
    public int selected;
    public bool axisInUse = false;
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
}
