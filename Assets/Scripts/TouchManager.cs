﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour {
    public Text textStatus;
	public Text scoreText;
	public Text comboText;
    public int left_or_right; // 0: left, 1: right

    private Rect screen;

    private bool[] foodStatus = new bool[4]; // 0: ceker, 1: kerupuk, 2: siomay, 3: bakso
    //private bool[] foodPressed = new bool[4];
    private bool fingerTouch = false;
	private float score;
	private float combo;
	private float bonus;
	private float matchMultiplication;

    private GameObject[] objectAbove = new GameObject[4];

	// Use this for initialization
	void Start () {
		score = 0;
        comboText.text = "0".ToString();
        for (int i = 0; i < foodStatus.Length; i++)
        {
            foodStatus[i] = false;
            //foodPressed[i] = false;
            objectAbove[i] = null;
        }

        if (left_or_right == 0) {
            screen = new Rect(0, 0, Screen.width / 2, Screen.height); // left side
        } else
        {
            screen = new Rect(Screen.width / 2, 0, Screen.width / 2, Screen.height); // right side
        }
	}
	
    void swipeLeftScreen() // untuk ceker
    {
        if (fingerTouch)
        {
            //text.text = "swipe left";
            if (foodStatus[0])
            {
                if (objectAbove[0].transform.position.y >= transform.position.y + 5 || objectAbove[0].transform.position.y <= transform.position.y - 5)
                {
                    Destroy(objectAbove[0]);
					textStatus.text = "ceker good";
                    //foodPressed[0] = true;
                    UpdateScore(0, 1);

                    foodStatus[0] = false;
                    objectAbove[0] = null;
                } else if (objectAbove[0].transform.position.y <= transform.position.y + 5 && objectAbove[0].transform.position.y >= transform.position.y - 5)
                {
                    Destroy(objectAbove[0]);
					textStatus.text = "ceker perfect";
                    //foodPressed[0] = true;
                    UpdateScore(0,0);

                    foodStatus[0] = false;
                    objectAbove[0] = null;
                }
            }

            fingerTouch = false;
        }
    }

    void swipeRightScreen() 
    {
        if (fingerTouch)
        {
            //text.text = "swipe right";
            fingerTouch = false;
        }
    }

    void swipeUpScreen() // untuk kerupuk
    {
        if (fingerTouch)
        {
            //text.text = "swipe up";
            if (foodStatus[1])
            {
                if (objectAbove[1].transform.position.y >= transform.position.y + 5 || objectAbove[1].transform.position.y <= transform.position.y - 5)
                {
                    Destroy(objectAbove[1]);
					textStatus.text = "kerupuk good";
                    //foodPressed[1] = true;
                    UpdateScore(1,1);

                    foodStatus[1] = false;
                    objectAbove[1] = null;
                }
                else if (objectAbove[1].transform.position.y <= transform.position.y + 5 && objectAbove[1].transform.position.y >= transform.position.y - 5)
                {
                    Destroy(objectAbove[1]);
					textStatus.text = "kerupuk perfect";
                    //foodPressed[1] = true;
                    UpdateScore(1,0);

                    foodStatus[1] = false;
                    objectAbove[1] = null;
                }
            }

            fingerTouch = false;
        }
    }

    void swipeDownScreen() // untuk siomay
    {
        if (fingerTouch)
        {
            //text.text = "swipe down";
            if (foodStatus[2])
            {
                if (objectAbove[2].transform.position.y >= transform.position.y + 5 || objectAbove[2].transform.position.y <= transform.position.y - 5)
                {
                    Destroy(objectAbove[2]);
					textStatus.text = "siomay good";
                    //foodPressed[2] = true;
                    UpdateScore(2,1);

                    foodStatus[2] = false;
                    objectAbove[2] = null;
                }
                else if (objectAbove[2].transform.position.y <= transform.position.y + 5 && objectAbove[2].transform.position.y >= transform.position.y - 5)
                {
                    Destroy(objectAbove[2]);
					textStatus.text = "siomay perfect";
                    //foodPressed[2] = true;
                    UpdateScore(2,0);

                    foodStatus[2] = false;
                    objectAbove[2] = null;
                }
                
            } 

            fingerTouch = false;
        }
    }

    void tapScreen() 
    {
        if (fingerTouch)
        {
            //text.text = "tap";
            fingerTouch = false;
        }
    }

    void circleScreen() // untuk bakso
    {
        if (fingerTouch)
        {
            //text.text = "circle";
            if (foodStatus[3])
            {
                if (objectAbove[3].transform.position.y >= transform.position.y + 5 || objectAbove[3].transform.position.y <= transform.position.y - 5)
                {
                    Destroy(objectAbove[3]);
					textStatus.text = "bakso good";
                    //foodPressed[3] = true;
                    UpdateScore(3,1);

                    foodStatus[3] = false;
                    objectAbove[3] = null;
                }
                else if (objectAbove[3].transform.position.y <= transform.position.y + 5 && objectAbove[3].transform.position.y >= transform.position.y - 5)
                {
                    Destroy(objectAbove[3]);
					textStatus.text = "bakso perfect";
                    //foodPressed[3] = true;
                    UpdateScore(3,0);

                    foodStatus[3] = false;
                    objectAbove[3] = null;
                } 
            } 

            fingerTouch = false;
        }
    }

    void zigZagScreen()
    {
        if (fingerTouch)
        {
            //text.text = "zig zag";
            fingerTouch = false;
        }
    }

    void Update() {
		foreach(Touch touch in Input.touches)
        {
            // check if touch occurs
            if(screen.Contains(touch.position))
            {
                fingerTouch = true;
            }
            
            //SimpleGesture.OnZigZag(zigZagScreen);
            SimpleGesture.OnCircle(circleScreen);
            SimpleGesture.On4AxisSwipeLeft(swipeLeftScreen);
            //SimpleGesture.On4AxisSwipeRight(swipeRightScreen);
            SimpleGesture.On4AxisSwipeUp(swipeUpScreen);
            SimpleGesture.On4AxisSwipeDown(swipeDownScreen);
            //SimpleGesture.OnTap(tapScreen);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Food"))
        {
            // 0: ceker, 1: kerupuk, 2: siomay, 3: bakso
            if (col.gameObject.name.Equals("Bakso(Clone)"))
            {
                objectAbove[3] = col.gameObject;
                foodStatus[3] = true;
            } else if (col.gameObject.name.Equals("Ceker(Clone)"))
            {
                objectAbove[0] = col.gameObject;
                foodStatus[0] = true; 
            } else if (col.gameObject.name.Equals("Kerupuk(Clone)"))
            {
                objectAbove[1] = col.gameObject;
                foodStatus[1] = true;
            } else // Siomay
            {
                objectAbove[2] = col.gameObject;
                foodStatus[2] = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        int code = -1;
        if (col.gameObject.tag.Equals("Food"))
        {
            // 0: ceker, 1: kerupuk, 2: siomay, 3: bakso
            if (col.gameObject.name.Equals("Bakso(Clone)"))
            {
                code = 3;
            }
            else if (col.gameObject.name.Equals("Ceker(Clone)"))
            {
                code = 0;
            }
            else if (col.gameObject.name.Equals("Kerupuk(Clone)"))
            {
                code = 1;
            }
            else // Siomay
            {
                code = 2;
            }

            foodStatus[code] = false;
            objectAbove[code] = null;
        }

        //Destroy(col.gameObject);
    }

	// Melakukan perbaharuan score
	// foodNumber 0: ceker, 1: kerupuk, 2: siomay, 3: bakso
	// match 0: perfect, 1:good
	void UpdateScore(int foodNumber, int match) {
        combo = PlayerPrefs.GetFloat("combo") + 1;
        comboText.text = combo.ToString();
        PlayerPrefs.SetFloat("combo", PlayerPrefs.GetFloat("combo") + 1);

        score = (float)(IntParse(scoreText.text));
		
		if (combo < 10) {
			bonus = 0.1f;
		} else if ((combo >= 10) && (combo < 20)) {
			bonus = 0.2f;
		} else if ((combo >= 20) && (combo < 50)) {
			bonus = 0.4f;
		} else if ((combo >= 50) && (combo < 100)) {
			bonus = 0.8f;
		} else {
			bonus = combo / 100;
		}

		if (match == 0) {
			matchMultiplication = 1;
		} else {
			matchMultiplication = 0.6f;
		}
		if (foodNumber == 0) {
			score += (matchMultiplication * (1000 + (1000 * bonus)));
		} else if (foodNumber == 1) {
			score += (matchMultiplication * (1000 + (1000 * bonus)));
		} else if (foodNumber == 2) {
			score += (matchMultiplication * (1000 + (1000 * bonus)));
		} else if (foodNumber == 3) {
			score += (matchMultiplication * (1500 + (1500 * bonus)));
		}

		scoreText.text = ((int)(score)).ToString();

	}

	// Mengubah sebuah string menjadi integer
	int IntParse (string value) {
		int result = 0;
		for (int i = 0; i < value.Length; i++)
		{
			char letter = value[i];
			result = 10 * result + (letter - 48);
		}
		return result;
	}
}