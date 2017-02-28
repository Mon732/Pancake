using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject[] menuPanels;
    public GameObject[] menuObjects;

	// Use this for initialization
	void Start ()
    {
        ToMenu(0);
	}

    public void ToGame()
    {
        Debug.Log("ToGame");
        SceneManager.LoadScene(1);
    }

    public void ToMenu(int index)
    {
        for (int i = 0; i < menuPanels.Length; i++)
        {
            menuPanels[i].SetActive(false);
            menuObjects[i].SetActive(false);
        }

        menuPanels[index].SetActive(true);
        menuObjects[index].SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
