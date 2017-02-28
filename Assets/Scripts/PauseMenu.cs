using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        transform.GetChild(0).gameObject.SetActive(false);
	}
}
