using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour {
    public void level(int lvl)
    {
        PlayerPrefs.SetInt("level", lvl);
        SceneManager.LoadScene(3);
    }
}
