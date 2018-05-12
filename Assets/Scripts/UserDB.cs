using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class UserDB : MonoBehaviour {
	private string userid;
	public Text name;
	public Text country;
	public Button register;

	// Use this for initialization
	void Start () {

		Button btn = register.GetComponent<Button> ();
		btn.onClick.AddListener (TaskOnClick);

		//FirebaseDatabase.DefaultInstance.GetReference("Users").Child(userid).
		//SceneManager.LoadScene(1);
	}

	void TaskOnClick() {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://djseblak-diamondproduction.firebaseio.com/");
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Users");

		userid = PlayerPrefs.GetString ("user_id");

		reference.Child (userid).Child ("Name").SetValueAsync (name.text);
		reference.Child (userid).Child ("Country").SetValueAsync (country.text);
		reference.Child (userid).Child ("Money").SetValueAsync (0);
		reference.Child (userid).Child ("Diamond").SetValueAsync (0);
		reference.Child (userid).Child ("JoinDate").SetValueAsync (System.DateTime.Today.Date.ToShortDateString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
