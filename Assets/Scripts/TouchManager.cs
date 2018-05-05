using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour {
    public Text textStatus;
	public Text scoreText;
	public Text comboText;
    public int left_or_right; // 0: left, 1: right
    public Queue<GameObject> queueOfNotes = new Queue<GameObject>();
    public Text status;

    // private bool[] foodStatus = new bool[4]; // 0: ceker, 1: kerupuk, 2: siomay, 3: bakso
    //private bool[] foodPressed = new bool[4];
    private Rect screen;
    private bool fingerTouch = false;
	private float score;
	private float combo;
	private float bonus;
	private float matchMultiplication;

    // private GameObject[] objectAbove = new GameObject[4];

	// Use this for initialization
	void Start () {
		score = 0;
        comboText.gameObject.SetActive(false);
        queueOfNotes.Clear();
        /* for (int i = 0; i < foodStatus.Length; i++)
        {
            foodStatus[i] = false;
            //foodPressed[i] = false;
            objectAbove[i] = null;
        } */

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
            if (queueOfNotes.Count > 0 && queueOfNotes.Peek().name.Equals("Ceker(Clone)"))
            {
                if (queueOfNotes.Peek().transform.position.y >= transform.position.y + 5 || queueOfNotes.Peek().transform.position.y <= transform.position.y - 5)
                {
                    GameObject ceker = queueOfNotes.Dequeue();
                    Destroy(ceker);
                    textStatus.color = new Color(0f, 0f, 255f);
                    textStatus.text = "good";//"ceker good";
                    StartCoroutine(showStatus(1));
                    UpdateScore(0, 1);
                }
                else if (queueOfNotes.Peek().transform.position.y <= transform.position.y + 5 && queueOfNotes.Peek().transform.position.y >= transform.position.y - 5)
                {
                    GameObject ceker = queueOfNotes.Dequeue();
                    Destroy(ceker);

                    textStatus.color = new Color(0f, 255f, 0f);
                    textStatus.text = "perfect";//"ceker perfect";
                    StartCoroutine(showStatus(2));
                    UpdateScore(0, 0);
                }
            }
            /*if (foodStatus[0])
            {
                if (objectAbove[0].transform.position.y >= transform.position.y + 5 || objectAbove[0].transform.position.y <= transform.position.y - 5)
                {
                    Destroy(objectAbove[0]);
                    textStatus.text = "good";//"ceker good";
                    //foodPressed[0] = true;
                    UpdateScore(0, 1);

                    foodStatus[0] = false;
                    objectAbove[0] = null;
                } else if (objectAbove[0].transform.position.y <= transform.position.y + 5 && objectAbove[0].transform.position.y >= transform.position.y - 5)
                {
                    Destroy(objectAbove[0]);
                    textStatus.text = "perfect";//"ceker perfect";
                    //foodPressed[0] = true;
                    UpdateScore(0, 0);

                    foodStatus[0] = false;
                    objectAbove[0] = null;
                }
            }*/

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

            if (queueOfNotes.Count > 0 && queueOfNotes.Peek().name.Equals("Kerupuk(Clone)"))
            {
                if (queueOfNotes.Peek().transform.position.y >= transform.position.y + 5 || queueOfNotes.Peek().transform.position.y <= transform.position.y - 5)
                {
                    GameObject kerupuk = queueOfNotes.Dequeue();
                    Destroy(kerupuk);
                    textStatus.color = new Color(0f, 0f, 255f);
                    textStatus.text = "good";//"kerupuk good";
                    StartCoroutine(showStatus(1));
                    UpdateScore(1, 1);
                }
                else if (queueOfNotes.Peek().transform.position.y <= transform.position.y + 5 && queueOfNotes.Peek().transform.position.y >= transform.position.y - 5)
                {
                    GameObject kerupuk = queueOfNotes.Dequeue();
                    Destroy(kerupuk);
                    textStatus.color = new Color(0f, 255f, 0f);
                    textStatus.text = "perfect";//"kerupuk perfect";
                    StartCoroutine(showStatus(2));
                    UpdateScore(1, 0);
                }
            }

            /*if (foodStatus[1])
            {
                if (objectAbove[1].transform.position.y >= transform.position.y + 5 || objectAbove[1].transform.position.y <= transform.position.y - 5)
                {
                    Destroy(objectAbove[1]);
                    textStatus.text = "good";//"kerupuk good";
                    //foodPressed[1] = true;
                    UpdateScore(1, 1);

                    foodStatus[1] = false;
                    objectAbove[1] = null;
                }
                else if (objectAbove[1].transform.position.y <= transform.position.y + 5 && objectAbove[1].transform.position.y >= transform.position.y - 5)
                {
                    Destroy(objectAbove[1]);
					textStatus.text = "perfect";//"kerupuk perfect";
                    //foodPressed[1] = true;
                    UpdateScore(1, 0);

                    foodStatus[1] = false;
                    objectAbove[1] = null;
                }
            }*/

            fingerTouch = false;
        }
    }

    void swipeDownScreen() // untuk siomay
    {
        if (fingerTouch)
        {
            //text.text = "swipe down";

            if (queueOfNotes.Count > 0 && queueOfNotes.Peek().name.Equals("Siomay(Clone)"))
            {
                if (queueOfNotes.Peek().transform.position.y >= transform.position.y + 5 || queueOfNotes.Peek().transform.position.y <= transform.position.y - 5)
                {
                    GameObject siomay = queueOfNotes.Dequeue();
                    Destroy(siomay);
                    textStatus.color = new Color(0f, 0f, 255f);
                    textStatus.text = "good";//"siomay good";
                    StartCoroutine(showStatus(1));
                    UpdateScore(2, 1);
                }
                else if (queueOfNotes.Peek().transform.position.y <= transform.position.y + 5 && queueOfNotes.Peek().transform.position.y >= transform.position.y - 5)
                {
                    GameObject siomay = queueOfNotes.Dequeue();
                    Destroy(siomay);
                    textStatus.color = new Color(0f, 255f, 0f);
                    textStatus.text = "perfect";//"siomay perfect";
                    StartCoroutine(showStatus(2));
                    UpdateScore(2, 0);
                }
            }

            /*if (foodStatus[2])
            {
                if (objectAbove[2].transform.position.y >= transform.position.y + 5 || objectAbove[2].transform.position.y <= transform.position.y - 5)
                {
                    Destroy(objectAbove[2]);
					textStatus.text = "good";//"siomay good";
                    //foodPressed[2] = true;
                    UpdateScore(2,1);

                    foodStatus[2] = false;
                    objectAbove[2] = null;
                }
                else if (objectAbove[2].transform.position.y <= transform.position.y + 5 && objectAbove[2].transform.position.y >= transform.position.y - 5)
                {
                    Destroy(objectAbove[2]);
					textStatus.text = "perfect";//"siomay perfect";
                    //foodPressed[2] = true;
                    UpdateScore(2,0);

                    foodStatus[2] = false;
                    objectAbove[2] = null;
                }
                
            } */

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

            if (queueOfNotes.Count > 0 && queueOfNotes.Peek().name.Equals("Bakso(Clone)"))
            {
                if (queueOfNotes.Peek().transform.position.y >= transform.position.y + 5 || queueOfNotes.Peek().transform.position.y <= transform.position.y - 5)
                {
                    GameObject bakso = queueOfNotes.Dequeue();
                    Destroy(bakso);
                    textStatus.color = new Color(0f, 0f, 255f);
                    textStatus.text = "good";//"bakso good";
                    StartCoroutine(showStatus(1));
                    UpdateScore(3, 1);
                }
                else if (queueOfNotes.Peek().transform.position.y <= transform.position.y + 5 && queueOfNotes.Peek().transform.position.y >= transform.position.y - 5)
                {
                    GameObject bakso = queueOfNotes.Dequeue();
                    Destroy(bakso);
                    textStatus.color = new Color(0f, 255f, 0f);
                    textStatus.text = "perfect";//"bakso perfect";
                    StartCoroutine(showStatus(2));
                    UpdateScore(3, 0);
                }
            }

            /*if (foodStatus[3])
            {
                if (objectAbove[3].transform.position.y >= transform.position.y + 5 || objectAbove[3].transform.position.y <= transform.position.y - 5)
                {
                    Destroy(objectAbove[3]);
					textStatus.text = "good";//"bakso good";
                    //foodPressed[3] = true;
                    UpdateScore(3,1);

                    foodStatus[3] = false;
                    objectAbove[3] = null;
                }
                else if (objectAbove[3].transform.position.y <= transform.position.y + 5 && objectAbove[3].transform.position.y >= transform.position.y - 5)
                {
                    Destroy(objectAbove[3]);
                    textStatus.text = "perfect";//"bakso perfect";
                    //foodPressed[3] = true;
                    UpdateScore(3,0);

                    foodStatus[3] = false;
                    objectAbove[3] = null;
                } 
            } */

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

    private float startTime;
    private float endTime;
    private Vector3 startPos;
    private Vector3 endPos;
    private float swipeDistance;
    private float swipeTime;
    private float maxTime = 0.5f;
    private float minSwipeDist = 10.0f;
    private float tapRange = 0.5f;
    private int fingerId;

    void detectTouch(Touch touch)
    {
        if (touch.phase == TouchPhase.Began)
        {
            fingerId = touch.fingerId;
            startTime = Time.time;
            startPos = touch.position;
        } else if (touch.phase == TouchPhase.Ended)
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
                        // swipeRightScreen();
                    } else if (distance.x < 0)
                    {
                        // Debug.Log("Left Swipe");
                        swipeLeftScreen();
                    }
                } else if (Mathf.Abs(distance.y) > Mathf.Abs(distance.x))
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
                }
            } else if (touch.fingerId == fingerId && swipeTime < maxTime && swipeDistance < tapRange)
            {
                // Debug.Log("Tap");
                circleScreen();
            }

            fingerId = -1;
        }
    }

    void DequeueQueue(GameObject col)
    {
        if (queueOfNotes.Count > 0 && queueOfNotes.Peek().name.Equals(col.gameObject.name)) {
            GameObject note = queueOfNotes.Dequeue();
            Destroy(note);

            status.color = new Color(255f, 0f, 0f);
            status.text = "miss";
            StartCoroutine(showStatus(0));


            PlayerPrefs.SetFloat("combo", 0);
            comboText.gameObject.SetActive(false);
        }
    }

    void Update() {
		foreach(Touch touch in Input.touches)
        {
            // check if touch occurs
            if(screen.Contains(touch.position))
            {
                fingerTouch = true;
                detectTouch(touch);
            }
            
            //SimpleGesture.OnZigZag(zigZagScreen);
            // SimpleGesture.OnTap(circleScreen);
            // SimpleGesture.On4AxisSwipeLeft(swipeLeftScreen);
            //SimpleGesture.On4AxisSwipeRight(swipeRightScreen);
            // SimpleGesture.On4AxisSwipeUp(swipeUpScreen);
            // SimpleGesture.On4AxisSwipeDown(swipeDownScreen);
            //SimpleGesture.OnTap(tapScreen);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Food"))
        {
            queueOfNotes.Enqueue(col.gameObject);
            // 0: ceker, 1: kerupuk, 2: siomay, 3: bakso
            /* if (col.gameObject.name.Equals("Bakso(Clone)"))
            {
                // objectAbove[3] = col.gameObject;
                // foodStatus[3] = true;
            } else if (col.gameObject.name.Equals("Ceker(Clone)"))
            {
                // objectAbove[0] = col.gameObject;
                // foodStatus[0] = true; 
            } else if (col.gameObject.name.Equals("Kerupuk(Clone)"))
            {
                // objectAbove[1] = col.gameObject;
                // foodStatus[1] = true;
            } else // Siomay
            {
                // objectAbove[2] = col.gameObject;
                // foodStatus[2] = true;
            } */
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        /*if (col.gameObject.tag.Equals("Food"))
        {
            if (queueOfNotes.Count > 0 && queueOfNotes.Peek().name.Equals(col.gameObject.name)) {
                GameObject note = queueOfNotes.Dequeue();
                Destroy(note);

                status.text = "miss";

                PlayerPrefs.SetFloat("combo", 0);
                comboText.gameObject.SetActive(false);
            }
        }*/

        //comboText.text = "0".ToString();
        /*int code = -1;
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

        //Destroy(col.gameObject);*/
    }

    // Melakukan perbaharuan score
    // foodNumber 0: ceker, 1: kerupuk, 2: siomay, 3: bakso
    // match 0: perfect, 1:good
    void UpdateScore(int foodNumber, int match) {
        combo = PlayerPrefs.GetFloat("combo") + 1;
        comboText.text = combo.ToString();

        if (combo == 1)
        {
            StartCoroutine(showCombo());
        } 

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

    IEnumerator showCombo()
    {
        comboText.transform.localScale += new Vector3(-0.5f, -0.5f, 0);
        comboText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        comboText.transform.localScale += new Vector3(0.5f, 0.5f, 0);
    }

    IEnumerator showStatus(int code)
    {
        /*Vector3 posLeft = new Vector3(-110.0f, -90.0f);
        Vector3 posRight = new Vector3(100.0f, -90.0f);

        Vector3 posLeftDestination = new Vector3(-90.0f, -90.0f);
        Vector3 posRightDestination = new Vector3(120.0f, -90.0f);

        if (left_or_right == 0) // left
        {
            Debug.Log("left");
            status.transform.position = posLeft;
            status.gameObject.SetActive(true);
            status.transform.position = Vector3.Lerp(posLeft, posLeftDestination, Time.deltaTime * 100.0f);
            status.gameObject.SetActive(false);
        } else // right
        {
            Debug.Log("right");
            status.transform.position = posRight;
            status.gameObject.SetActive(true);
            status.transform.position = Vector3.Lerp(posRight, posRightDestination, Time.deltaTime * 100.0f);
            status.gameObject.SetActive(false);
        }*/
        
        float posXLeft = -110.0f;
        float posXRight = 100.0f;

        // 0: miss
        // 1: good
        // 2: perfect

        if (left_or_right == 0) // left
        {
            status.transform.position = new Vector3(posXLeft, -80.0f);
        }
        else // right
        {
            status.transform.position = new Vector3(posXRight, -80.0f);
        }

        status.gameObject.SetActive(true);

        for (int i = 0; i < 30; i++)
        {
            if (left_or_right == 0) // left
            {
                status.transform.position = new Vector3(posXLeft, -80.0f);
            }
            else // right
            {
                status.transform.position = new Vector3(posXRight, -80.0f);
            }

            posXLeft += 0.2f;
            posXRight += 0.2f;

            yield return new WaitForSeconds(0.001f);
        }

        status.gameObject.SetActive(false);
    }
}