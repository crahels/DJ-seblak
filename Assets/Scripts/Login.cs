using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Unity.Editor;

public class Login : MonoBehaviour {
	public Text email;
	public Text password;
	public Firebase.Auth.FirebaseAuth auth;

	void Start() {
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
	}

	public void SignIn() {
		auth.SignInWithEmailAndPasswordAsync (email.text, password.text).ContinueWith (task => {
			if (task.IsCanceled) {
				Debug.LogError ("SignInWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				Debug.LogError ("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
				return;
			}

			Firebase.Auth.FirebaseUser newUser = task.Result;
			Debug.LogFormat ("User signed in successfully: {0} ({1})",
				newUser.DisplayName, newUser.UserId);
            PlayerPrefs.SetString("user_id", newUser.UserId);
            SceneManager.LoadScene(4);
		});
	}
}
