using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;

public class Register : MonoBehaviour {
	public Text email;
	public Text password;
	public Text dummy;
	public Firebase.Auth.FirebaseAuth auth;

	// Use this for initialization
	void Start () {
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Registration() {
		auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task => {
			if (task.IsCanceled) {
				Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
				return;
			}

			// Firebase user has been created.
			Firebase.Auth.FirebaseUser newUser = task.Result;
			Debug.LogFormat("Firebase user created successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);
			PlayerPrefs.SetString("user_id", newUser.UserId);
			dummy.text = PlayerPrefs.GetString("user_id");
			SceneManager.LoadScene(8);
		});
	}
}
