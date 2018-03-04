using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour {
    public Text text;
    public Text textStatus;
    public int left_or_right; // 0: left, 1: right

    private Rect screen;

    private bool[] foodStatus = new bool[4]; // 0: ceker, 1: kerupuk, 2: siomay, 3: bakso
    private bool[] foodPressed = new bool[4];
    private bool fingerTouch = false;

    private GameObject[] objectAbove = new GameObject[4];

	// Use this for initialization
	void Start () {
        for (int i = 0; i < foodStatus.Length; i++)
        {
            foodStatus[i] = false;
            foodPressed[i] = false;
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
            text.text = "swipe left";
            if (foodStatus[0])
            {
                if (objectAbove[0].transform.position.y >= transform.position.y + 5 || objectAbove[0].transform.position.y <= transform.position.y - 5)
                {
                    Destroy(objectAbove[0]);
                    textStatus.text = "ceker good";
                } else if (objectAbove[0].transform.position.y <= transform.position.y + 5 && objectAbove[0].transform.position.y >= transform.position.y - 5)
                {
                    Destroy(objectAbove[0]);
                    textStatus.text = "ceker perfect";
                }
                foodPressed[0] = true;
            } /*else
            {
                // miss
                textStatus.text = "ceker miss";
            }*/
            fingerTouch = false;
        }
    }

    void swipeRightScreen() 
    {
        if (fingerTouch)
        {
            text.text = "swipe right";
            fingerTouch = false;
        }
    }

    void swipeUpScreen() // untuk kerupuk
    {
        if (fingerTouch)
        {
            text.text = "swipe up";
            if (foodStatus[1])
            {
                if (objectAbove[1].transform.position.y >= transform.position.y + 5 || objectAbove[1].transform.position.y <= transform.position.y - 5)
                {
                    Destroy(objectAbove[1]);
                    textStatus.text = "kerupuk good";
                }
                else if (objectAbove[1].transform.position.y <= transform.position.y + 5 && objectAbove[1].transform.position.y >= transform.position.y - 5)
                {
                    Destroy(objectAbove[1]);
                    textStatus.text = "kerupuk perfect";
                }
                foodPressed[1] = true;
            } /*else
            {
                // miss
                textStatus.text = "kerupuk miss";
            }*/
            fingerTouch = false;
        }
    }

    void swipeDownScreen() // untuk siomay
    {
        if (fingerTouch)
        {
            text.text = "swipe down";
            if (foodStatus[2])
            {
                if (objectAbove[2].transform.position.y >= transform.position.y + 5 || objectAbove[2].transform.position.y <= transform.position.y - 5)
                {
                    Destroy(objectAbove[2]);
                    textStatus.text = "siomay good";
                }
                else if (objectAbove[2].transform.position.y <= transform.position.y + 5 && objectAbove[2].transform.position.y >= transform.position.y - 5)
                {
                    Destroy(objectAbove[2]);
                    textStatus.text = "siomay perfect";
                }
                foodPressed[2] = true;
            } /*else
            {
                // miss
                textStatus.text = "siomay miss";
            }*/
            fingerTouch = false;
        }
    }

    void tapScreen() 
    {
        if (fingerTouch)
        {
            text.text = "tap";
            fingerTouch = false;
        }
    }

    void circleScreen() // untuk bakso
    {
        if (fingerTouch)
        {
            text.text = "circle";
            if (foodStatus[3])
            {
                if (objectAbove[3].transform.position.y >= transform.position.y + 5 || objectAbove[3].transform.position.y <= transform.position.y - 5)
                {
                    Destroy(objectAbove[3]);
                    textStatus.text = "bakso good";
                }
                else if (objectAbove[3].transform.position.y <= transform.position.y + 5 && objectAbove[3].transform.position.y >= transform.position.y - 5)
                {
                    Destroy(objectAbove[3]);
                    textStatus.text = "bakso perfect";
                }
                foodPressed[3] = true;
            } /*else
            {
                // miss
                textStatus.text = "bakso miss";
            }*/
            fingerTouch = false;
        }
    }

    void zigZagScreen()
    {
        if (fingerTouch)
        {
            text.text = "zig zag";
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
        for (int i = 0; i < foodStatus.Length; i++)
        {
            if (foodStatus[i] != foodPressed[i])
            {
                // miss
                string item = "";
                if (i == 0)
                {
                    item = "ceker";
                } else if (i == 1)
                {
                    item = "kerupuk";
                } else if (i == 2)
                {
                    item = "siomay";
                } else
                {
                    item = "bakso";
                }
                textStatus.text = item + " miss";
            }
        }

        for (int i = 0; i < foodStatus.Length; i++)
        {
            foodStatus[i] = false;
            foodPressed[i] = false;
            objectAbove[i] = null;
        }
        
        Destroy(col.gameObject);
    }
}