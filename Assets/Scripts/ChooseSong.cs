using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseSong : MonoBehaviour {
    public void fancy()
    {
        PlayerPrefs.SetInt("songs", 0);
        SceneManager.LoadScene(1);
    }

    public void beethoven()
    {
        PlayerPrefs.SetInt("songs", 1);
        SceneManager.LoadScene(1);
    }

    public void elektronokimia()
    {
        PlayerPrefs.SetInt("songs", 2);
        SceneManager.LoadScene(1);
    }

    public void sceptre()
    {
        PlayerPrefs.SetInt("songs", 3);
        SceneManager.LoadScene(1);
    }
}
