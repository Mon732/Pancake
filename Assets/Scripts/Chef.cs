using UnityEngine;
using System.Collections;

public class Chef : MonoBehaviour
{
    public float delay;
    public float waitTime;
    public float speedupDelay;
    public float speedupAmount;
    public GameObject flapjackPrefab;

    float startDelay;
    float flapjackDelay;
    float cooldown;
    float speedupCooldown;
    bool canSpawn;

    GameController gameController;
    Transform firstMarker;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        firstMarker = gameController.GetMarkers()[0];

        startDelay = delay;
        cooldown = Time.time + delay;
        speedupCooldown = Time.time + speedupDelay;
        
        flapjackDelay = flapjackPrefab.GetComponent<Pancake>().delay;

        canSpawn = false;

        Invoke("FirstSpawn", waitTime);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (canSpawn)
        {
            if (Time.time > cooldown)
            {
                Spawn();
                cooldown = Time.time + delay;
            }

            if (Time.time > speedupCooldown)
            {
                delay -= speedupAmount;
                flapjackDelay -= speedupAmount;
                speedupCooldown = Time.time + delay;
            }
        }
	}

    void Spawn()
    {
        GameObject flapjack = (GameObject)GameObject.Instantiate(flapjackPrefab, firstMarker.position, firstMarker.rotation);
        flapjack.GetComponent<Pancake>().delay = flapjackDelay;
    }

    void FirstSpawn()
    {
        Spawn();
        canSpawn = true;
        cooldown = Time.time + delay;
    }
}
