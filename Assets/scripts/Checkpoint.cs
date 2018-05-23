using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Vector3 lastCheckedPos;
    private bool instrument;
	// Use this for initialization
	void Start ()
    {
        lastCheckedPos = GameObject.FindGameObjectWithTag("Player").transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.tag == "PlayerAttack")
        {
            lastCheckedPos = other.GetComponent<Transform>().position;
        }
    }

    void Respawn()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckedPos;
    }
}
