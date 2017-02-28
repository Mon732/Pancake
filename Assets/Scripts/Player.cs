using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public int position;
    public Transform[] positions;
    public Sprite[] sprites;

    GameController gameController;
    SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        positions = gameController.GetPlayerPositions();

        position = -1;
        NextMarker();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (gameController.GetState() == GameController.GameState.Default)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!gameController.IsInsideBounds(Input.mousePosition))
                {
                    float halfway = Screen.width / 2;
                    if (Input.mousePosition.x < halfway)
                    {
                        NextMarker();
                    }
                    else
                    {
                        PreviousMarker();
                    }
                }
            }

            if (Input.touchCount > 0)
            {
                Debug.Log(Input.GetTouch(0));
            }
        }
	}

    void NextMarker()
    {
        if (position < (positions.Length - 1))
        {
            position++;
            transform.position = new Vector3(positions[position].position.x, positions[position].position.y, positions[position].position.z - 1);
            transform.rotation = positions[position].rotation;
            spriteRenderer.sprite = sprites[position];
        }
    }

    void PreviousMarker()
    {
        if (position > 0)
        {
            position--;
            transform.position = new Vector3(positions[position].position.x, positions[position].position.y, positions[position].position.z - 1);
            transform.rotation = positions[position].rotation;
            spriteRenderer.sprite = sprites[position];
        }
    }
}
