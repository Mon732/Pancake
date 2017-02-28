using UnityEngine;
using System.Collections;

public class Pancake : MonoBehaviour
{
    public float delay;
    public AudioClip bounceSound;

    Transform[] markers;
    Transform[] missMarkers;
    int[] missMarkerPoints;

    int markerIndex;
    int missMarkerIndex;
    FlapjackState state;
    Player player;
    GameController gameController;
    AudioSource audioSource;

    enum FlapjackState
    {
        Default,
        Missed,
        Scored  
    }

	// Use this for initialization
	void Start ()
    {
        markerIndex = -1;
        missMarkerIndex = 0;

        state = FlapjackState.Default;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        audioSource = gameObject.GetComponent<AudioSource>();

        markers = gameController.GetMarkers();
        missMarkers = gameController.GetMissMarkers();
        missMarkerPoints = gameController.GetMissMarkerPoints();

        //Debug.Log(player.position);

        InvokeRepeating("NextMarker", 0, delay);
	}
	
    void OnDestroy()
    {
        switch(state)
        {
            case FlapjackState.Missed:
                gameController.SetLives(-1, true);
                gameController.Missed(missMarkerIndex);
                break;
            case FlapjackState.Scored:
                gameController.SetScore(1, true);
                break;
        }
    }

	void NextMarker()
    {
        //Am I about to fall onto the floor?
        if (markerIndex == missMarkerPoints[missMarkerIndex])
        {
            if (player.position != missMarkerIndex)
            {
                //Missed!
                transform.position = new Vector3(missMarkers[missMarkerIndex].position.x, missMarkers[missMarkerIndex].position.y, -1);
                transform.rotation = missMarkers[missMarkerIndex].rotation;

                CancelInvoke();
                state = FlapjackState.Missed;
                Destroy(gameObject, delay);
            }
            else
            {
                //Didn't miss!
                markerIndex++;
                missMarkerIndex++;
                missMarkerIndex %= missMarkerPoints.Length;

                transform.position = new Vector3(markers[markerIndex].position.x, markers[markerIndex].position.y, -1);
                transform.rotation = markers[markerIndex].rotation;

                audioSource.clip = bounceSound;
                audioSource.Play();
            }
        }
        else
        {
            markerIndex++;

            if (markerIndex < markers.Length)
            {
                //Onward!
                transform.position = new Vector3(markers[markerIndex].position.x, markers[markerIndex].position.y, markers[markerIndex].position.z - 1);
                transform.rotation = markers[markerIndex].rotation;
            }
            else
            {
                //Got to the end!
                state = FlapjackState.Scored;
                Destroy(gameObject, delay);
            }
        }
        
    }
}
