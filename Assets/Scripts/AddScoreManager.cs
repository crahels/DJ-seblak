using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

// ini buat nyimpen high score kalo menang
public class AddScoreManager : MonoBehaviour {

	private string Song;
	private string SongLevel;
	private string Name;
	private int Score;

	void Start () {
// harus diganti
PlayerPrefs.SetString("Song", "Spectre");
PlayerPrefs.SetString("SongLevel", "Easy");
PlayerPrefs.SetString("Name", "Rose");
PlayerPrefs.SetInt("Score", 1);
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://djseblak-diamondproduction.firebaseio.com/");
		Song = PlayerPrefs.GetString("Song");
		SongLevel = PlayerPrefs.GetString("SongLevel");
		Name = PlayerPrefs.GetString("Name");
		Score = PlayerPrefs.GetInt("Score");
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Score").Child(Song).Child(SongLevel);
		reference.GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
			} else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				if (Score > Int32.Parse(snapshot.Child("3").Child("Score").Value.ToString())) {
					if (Score > Int32.Parse(snapshot.Child("2").Child("Score").Value.ToString())) {
						if (Score > Int32.Parse(snapshot.Child("1").Child("Score").Value.ToString())) {
							reference.Child("1").Child("Name").SetValueAsync(Name);
							reference.Child("1").Child("Score").SetValueAsync(Score);
						} else {
							reference.Child("2").Child("Name").SetValueAsync(Name);
							reference.Child("2").Child("Score").SetValueAsync(Score);
						}
					} else {
						reference.Child("3").Child("Name").SetValueAsync(Name);
						reference.Child("3").Child("Score").SetValueAsync(Score);
					}
				}
			}
		});
	}

}