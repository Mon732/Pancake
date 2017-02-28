using UnityEngine;
using System.Collections;

public class Screenshot : MonoBehaviour
{
    public string filename;

	// Use this for initialization
	void OnEnable ()
    {
        Application.CaptureScreenshot(filename);
        Debug.Log("Saved screenshot to " + filename);
        this.enabled = false;
	}
}
