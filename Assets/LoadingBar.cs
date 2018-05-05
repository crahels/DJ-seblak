using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingBar : MonoBehaviour
{

    Image loadingBar;
    float maxLoading = 100.0f;
    float loading;

    // Use this for initialization
    void Start()
    {
        loadingBar = GetComponent<Image>();
        loading = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        loadingBar.fillAmount = loading / maxLoading;
        if (loading >= 100)
        {
            SceneManager.LoadScene(4);
        }

        loading++;
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(1.0f);
    }
}