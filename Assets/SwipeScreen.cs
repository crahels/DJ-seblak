using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeScreen : MonoBehaviour {

    private float startTime;
    private float endTime;
    private Vector3 startPos;
    private Vector3 endPos;
    private float swipeDistance;
    private float swipeTime;
    private float maxTime = 0.5f;
    private float minSwipeDist = 10.0f;
    private int fingerId;
    private int pageNow;
    private GameObject pageSettings;
    private GameObject pageMain;
    private GameObject pageHighscore;

    // Use this for initialization
    void Start () {
        pageNow = 1;
        // 0: settings
        // 1: main
        // 2: high score

        pageSettings = GameObject.Find("SettingsContainer");
        pageMain = GameObject.Find("MainContainer");
        pageHighscore = GameObject.Find("HighscoreContainer");

        changeBackground();
    }

    void changeBackground()
    {
        if (pageNow == 0)
        {
            pageSettings.SetActive(true);
            pageMain.SetActive(false);
            pageHighscore.SetActive(false);
        } else if (pageNow == 1)
        {
            pageSettings.SetActive(false);
            pageMain.SetActive(true);
            pageHighscore.SetActive(false);
        } else
        {
            pageSettings.SetActive(false);
            pageMain.SetActive(false);
            pageHighscore.SetActive(true);
        }
    }

    void swipeLeftScreen()
    {
        pageNow++;
        pageNow = pageNow % 3;

        changeBackground();
    }

    void swipeRightScreen()
    {
        pageNow--;
        pageNow = pageNow % 3;

        changeBackground();
    }

    void detectTouch(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            fingerId = touch.fingerId;
            startTime = Time.time;
            startPos = touch.position;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            endTime = Time.time;
            endPos = touch.position;

            swipeDistance = (endPos - startPos).magnitude;
            swipeTime = endTime - startTime;

            if (touch.fingerId == fingerId && swipeTime < maxTime && swipeDistance > minSwipeDist)
            {
                Vector2 distance = endPos - startPos;
                if (Mathf.Abs(distance.x) > Mathf.Abs(distance.y))
                {
                    // Debug.Log("Horizontal swipe");
                    if (distance.x > 0)
                    {
                        // Debug.Log("Right Swipe");
                        swipeRightScreen();
                    }
                    else if (distance.x < 0)
                    {
                        // Debug.Log("Left Swipe");
                        swipeLeftScreen();
                    }
                }
                /*else if (Mathf.Abs(distance.y) > Mathf.Abs(distance.x))
                {
                    // Debug.Log("Vertical swipe");
                    if (distance.y > 0)
                    {
                        // Debug.Log("Up Swipe");
                        swipeUpScreen();
                    }
                    else if (distance.y < 0)
                    {
                        // Debug.Log("Down Swipe");
                        swipeDownScreen();
                    }
                }*/
            }
            /*else if (touch.fingerId == fingerId && swipeTime < maxTime && swipeDistance < tapRange)
            {
                // Debug.Log("Tap");
                circleScreen();
            }*/

            fingerId = -1;
        }
    }

    // Update is called once per frame
    void Update () {
        foreach (Touch touch in Input.touches)
        {
            detectTouch(touch);
        }
    }
}
