using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;

public class Logout : MonoBehaviour {
	public Firebase.Auth.FirebaseAuth auth;

	// Use this for initialization
	void Start () {
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void LoggingOut() {
		auth.SignOut();
		SceneManager.LoadScene(6);
	}
}
