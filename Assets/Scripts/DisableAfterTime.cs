using UnityEngine;
using System.Collections;

public class DisableAfterTime : MonoBehaviour
{
    public float delay;

    SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void StartCountdown()
    {
        Invoke("DisableSprite", delay);
    }

    void DisableSprite()
    {
        spriteRenderer.enabled = false;
    }
}
