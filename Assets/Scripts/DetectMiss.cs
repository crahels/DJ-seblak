using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectMiss : MonoBehaviour {
    public Text comboText;
    public Text status;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Food"))
        {
            if (this.tag.Equals("PanciLeft"))
            {
                GameObject.Find("LeftActivator").SendMessage("DequeueQueue", col.gameObject);
            } else if (this.tag.Equals("PanciRight"))
            {
                GameObject.Find("RightActivator").SendMessage("DequeueQueue", col.gameObject);
            }
        }
  
        /*if (col.gameObject.tag.Equals("Food"))
        {
            string item = "";
            // 0: ceker, 1: kerupuk, 2: siomay, 3: bakso
            if (col.gameObject.name.Equals("Bakso(Clone)"))
            {
                item = "bakso";
            }
            else if (col.gameObject.name.Equals("Ceker(Clone)"))
            {
                item = "ceker";
            }
            else if (col.gameObject.name.Equals("Kerupuk(Clone)"))
            {
                item = "kerupuk";
            }
            else // Siomay
            {
                item = "siomay";
            }

            item = "";

            status.text = item + " miss";

            PlayerPrefs.SetFloat("combo", 0);
            comboText.gameObject.SetActive(false);
            //comboText.text = "0".ToString();
        }

        Destroy(col.gameObject);*/
    }
}
