using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour {
    public Text text;
    public int left_or_right; // 0: left, 1: right

    private Rect screen;

    private bool[] foodStatus = new bool[4]; // 0: ceker, 1: kerupuk, 2: siomay, 3: bakso
    private bool fingerTouch = false;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < foodStatus.Length; i++)
        {
            foodStatus[i] = false;
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
                text.text += " ok";
            }
            fingerTouch = false;
        }
    }

    void swipeRightScreen() // untuk bakso
    {
        if (fingerTouch)
        {
            text.text = "swipe right";
            if (foodStatus[3])
            {
                text.text += " ok";
            }
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
                text.text += " ok";
            }
            fingerTouch = false;
        }
    }

    void swipeDownScreen()
    {
        if (fingerTouch)
        {
            text.text = "swipe down";
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

    void circleScreen() // untuk siomay
    {
        if (fingerTouch)
        {
            text.text = "circle";
            if (foodStatus[2])
            {
                text.text += " ok";
            }
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
            
            SimpleGesture.OnZigZag(zigZagScreen);
            SimpleGesture.OnCircle(circleScreen);
            SimpleGesture.On4AxisSwipeLeft(swipeLeftScreen);
            SimpleGesture.On4AxisSwipeRight(swipeRightScreen);
            SimpleGesture.On4AxisSwipeUp(swipeUpScreen);
            SimpleGesture.On4AxisSwipeDown(swipeDownScreen);
            SimpleGesture.OnTap(tapScreen);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Food"))
        {
            // 0: ceker, 1: kerupuk, 2: siomay, 3: bakso
            if (col.gameObject.name.Equals("Bakso(Clone)"))
            {
                foodStatus[3] = true;
            } else if (col.gameObject.name.Equals("Ceker(Clone)"))
            {
                foodStatus[0] = true; 
            } else if (col.gameObject.name.Equals("Kerupuk(Clone)"))
            {
                foodStatus[1] = true;
            } else // Siomay
            {
                foodStatus[2] = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        for(int i = 0; i < foodStatus.Length; i++)
        {
            foodStatus[i] = false;
        }
    }
}