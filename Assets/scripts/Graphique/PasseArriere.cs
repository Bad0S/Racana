using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasseArriere : MonoBehaviour 
{
	public List<Transform> elementsPasseArriere;
	private Transform childrenTrans;
	private SpriteRenderer elementRend;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach (Transform checkPasseArriere in elementsPasseArriere)
		{
            elementRend = checkPasseArriere.GetComponent<SpriteRenderer>();
            childrenTrans = getFirstChildren(checkPasseArriere);
			if (childrenTrans.position.y > transform.position.y) 
			{
				elementRend.sortingOrder = 4;
			}
			if (childrenTrans.position.y < transform.position.y) 
			{
				elementRend.sortingOrder = 6;
			}
		}
	}
    private Transform getFirstChildren(Transform parent)
    {
        Transform[] children = parent.GetComponentsInChildren<Transform>();
        Transform[] firstChildren = new Transform[parent.childCount];
        int index = 0;
        foreach (Transform child in children)
        {
            if (child.parent == parent)
            {
                firstChildren[index] = child;
                index++;
            }
        }
        if (firstChildren.Length == 0)
        {
            firstChildren = new Transform[1];
            firstChildren[0] = GetComponentInParent<Transform>();
        }
        return firstChildren[0];
    }
}
