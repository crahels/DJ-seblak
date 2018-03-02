using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour {
	Rect leftScreen = new Rect(0, 0, Screen.width/2, Screen.height);
	Rect rightScreen = new Rect(Screen.width/2, 0, Screen.width/2, Screen.height);
    public Text textLeft;
    public Text textRight;
    public Text textDown;
    bool left = false;
    bool right = false;

	// Use this for initialization
	void Start () {
	}
	
    void swipeLeftScreen()
    {
        if (left)
        {
            textLeft.text = "swipe left";
            left = false;
        }
        if (right)
        {
            textRight.text = "swipe left";
            right = false;
        }
    }

    void swipeRightScreen()
    {
        if (left)
        {
            textLeft.text = "swipe right";
            left = false;
        }
        if (right)
        {
            textRight.text = "swipe right";
            right = false;
        }
    }

    void swipeUpScreen()
    {
        if (left)
        {
            textLeft.text = "swipe up";
            left = false;
        }
        if (right)
        {
            textRight.text = "swipe up";
            right = false;
        }
    }

    void swipeDownScreen()
    {
        if (left)
        {
            textLeft.text = "swipe down";
            left = false;
        }
        if (right)
        {
            textRight.text = "swipe down";
            right = false;
        }
    }

    void tapScreen()
    {
        if (left)
        {
            textLeft.text = "tap left";
            left = false;
        }
        if (right)
        {
            textRight.text = "tap right";
            right = false;
        }
    }

    void circleScreen()
    {
        if (left)
        {
            textLeft.text = "circle left";
            left = false;
        }
        if (right)
        {
            textRight.text = "circle right";
            right = false;
        }
    }

    void zigZagScreen()
    {
        if (left)
        {
            textLeft.text = "zig zag left";
            left = false;
        }
        if (right)
        {
            textRight.text = "zig zag right";
            right = false;
        }
    }

    void Update() {
		foreach(Touch touch in Input.touches)
        {
            // check if touch occurs in left side
            if(leftScreen.Contains(touch.position))
            {
                left = true;
            }

            // check if touch occurs in right side
            if (rightScreen.Contains(touch.position))
            {
                right = true;
            }

            SimpleGesture.OnZigZag(zigZagScreen);
            SimpleGesture.OnCircle(circleScreen);
            SimpleGesture.On4AxisSwipeLeft(swipeLeftScreen);
            SimpleGesture.On4AxisSwipeRight(swipeRightScreen);
            SimpleGesture.On4AxisSwipeUp(swipeUpScreen);
            SimpleGesture.On4AxisSwipeDown(swipeDownScreen);
            SimpleGesture.OnTap(tapScreen);

            // show hit object
            Ray touchRay = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit[] hits = Physics.RaycastAll(touchRay);
            textDown.text = "o: " + hits[0].transform.gameObject.tag.ToString();
        }
    }
    
	/*GameObject gObj = null;
	Plane objPlane;
	Vector3 m0;

	Ray GenerateMouseRay (Vector3 touchPos) {
		Vector3 mousePosFar = new Vector3 (touchPos.x, touchPos.y, Camera.main.farClipPlane);
		Vector3 mousePosNear = new Vector3 (touchPos.x, touchPos.y, Camera.main.nearClipPlane);
		Vector3 mousePosF = Camera.main.ScreenToWorldPoint (mousePosFar);
		Vector3 mousePosN = Camera.main.ScreenToWorldPoint (mousePosNear);

		Ray mr = new Ray (mousePosN, mousePosF-mousePosN);
		return mr;
	}
	
	// Update is called once per frame
	void Update () {
		//tCount.text = Input.touchCount.ToString();

		if (Input.touchCount > 0) {
			if (Input.GetTouch(0).phase == TouchPhase.Began) {
				tCount.text = "begin";
				Ray mouseRay = GenerateMouseRay (Input.GetTouch(0).position);
				RaycastHit hit;

				if (Physics.Raycast (mouseRay.origin, mouseRay.direction, out hit)) {
					gObj = hit.transform.gameObject;
					objPlane = new Plane (Camera.main.transform.forward * -1, gObj.transform.position);

					// calculate touch offset
					Ray mRay = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
					float rayDistance;
					objPlane.Raycast (mRay, out rayDistance);
					m0 = gObj.transform.position - mRay.GetPoint (rayDistance);
				}
			} else if (Input.GetTouch(0).phase == TouchPhase.Moved && gObj) {
				tCount.text = "move";
				Ray mRay = Camera.main.ScreenPointToRay (Input.GetTouch(0).position);
				float rayDistance;
				if (objPlane.Raycast (mRay, out rayDistance)) {
					gObj.transform.position = mRay.GetPoint (rayDistance) + m0;
				}
			} else if (Input.GetTouch(0).phase == TouchPhase.Ended && gObj) {
				tCount.text = "stop";
				gObj = null;
			}
		} 
	}*/
}
