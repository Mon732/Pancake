using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public AudioClip scoreSound;
    public AudioClip missSound;
    public AudioClip gameOverSound;

    public Transform[] markers;
    public Transform[] missMarkers;
    public int[] missMarkerPoints;
    public Transform[] playerPositions;
    public RectTransform[] blockingUI;

    int score;
    int lives;
    GameState gameState;
    AudioSource audioSource;
    RectTransform livesCounter;
    Text scoreCounter;
    Text[] gameOverText;
    SpriteRenderer[] missMarkerSprites;
    GameObject pauseMenu;

    public enum GameState
    {
        Default,
        Paused,
        GameOver
    }

    // Use this for initialization
    void Start()
    {
        gameState = GameState.Default;

        audioSource = gameObject.GetComponent<AudioSource>();

        livesCounter = GameObject.FindWithTag("LivesCounter").GetComponent<RectTransform>();
        scoreCounter = GameObject.FindWithTag("Score").GetComponent<Text>();

        GameObject[] objects = GameObject.FindGameObjectsWithTag("GameOver");
        gameOverText = new Text[objects.Length];

        for (int i = 0; i < objects.Length; i++)
        {
            gameOverText[i] = objects[i].GetComponent<Text>();
        }

        for (int i = 0; i < gameOverText.Length; i++)
        {
            gameOverText[i].enabled = false;
        }

        missMarkerSprites = new SpriteRenderer[missMarkers.Length];

        for (int i = 0; i < missMarkers.Length; i++)
        {
            missMarkerSprites[i] = missMarkers[i].gameObject.GetComponent<SpriteRenderer>();
        }

        pauseMenu = GameObject.FindWithTag("PauseMenu");

        SetLives(5);
        SetScore(0);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (gameState == GameState.GameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
            }
        }
	}

    public Transform[] GetMarkers()
    {
        return markers;
    }

    public Transform[] GetMissMarkers()
    {
        return missMarkers;
    }

    public int[] GetMissMarkerPoints()
    {
        return missMarkerPoints;
    }

    public Transform[] GetPlayerPositions()
    {
        return playerPositions;
    }

    public void Pause()
    {
        switch (gameState)
        {
            case GameState.Default:
                gameState = GameState.Paused;
                pauseMenu.transform.GetChild(0).gameObject.SetActive(true);
                Time.timeScale = 0;
                break;

            case GameState.Paused:
                gameState = GameState.Default;
                pauseMenu.transform.GetChild(0).gameObject.SetActive(false);
                Time.timeScale = 1;
                break;
        }
    }

    public void Missed(int position)
    {
        if ((position < missMarkerSprites.Length) && (position >= 0))
        {
            missMarkerSprites[position].enabled = true;
            missMarkers[position].GetComponent<DisableAfterTime>().StartCountdown();
        }
    }

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void GameOver()
    {
        gameState = GameState.GameOver;
        Time.timeScale = 0;

        for (int i = 0; i < gameOverText.Length; i++)
        {
            gameOverText[i].enabled = true;
        }

        audioSource.clip = gameOverSound;
        audioSource.Play();
    }

    public void SetScore(int newScore, bool additive = false)
    {
        audioSource.clip = scoreSound;
        audioSource.Play();

        if (additive)
        {
            score += newScore;
        }
        else
        {
            score = newScore;
        }

        if (score >= 0 || score <= 9999)
        {
            if (scoreCounter != null)
            {
                scoreCounter.text = score.ToString().PadLeft(4, '0');
            }
        }

        Debug.Log("Score: " + score);
    }

    public void SetLives(int newLives, bool additive = false)
    {
        audioSource.clip = missSound;
        audioSource.Play();

        if (additive)
        {
            lives += newLives;
        }
        else
        {
            lives = newLives;
        }

        if (lives >= 0)
        {
            if (livesCounter != null)
            {
                livesCounter.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50 * lives);
            }
        }

        if (lives <= 0)
        {
            GameOver();
            Debug.Log("GAME OVER!");
        }

        Debug.Log("Lives: " + lives);
    }

    public GameState GetState()
    {
        return gameState;
    }

    public bool IsInsideBounds(Vector2 point)
    {
        Debug.Log(point);

        foreach (RectTransform ui in blockingUI)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(ui, point))
            {
                return true;
            }
        }

        return false;
    }
}
