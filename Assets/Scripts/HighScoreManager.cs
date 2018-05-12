// ini buat get high score
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class HighScoreManager : MonoBehaviour {

	public Text Song;
	public Text Easy1Name;
	public Text Easy1Score;
	public Text Easy2Name;
	public Text Easy2Score;
	public Text Easy3Name;
	public Text Easy3Score;
	public Text Medium1Name;
	public Text Medium1Score;
	public Text Medium2Name;
	public Text Medium2Score;
	public Text Medium3Name;
	public Text Medium3Score;
	public Text Hard1Name;
	public Text Hard1Score;
	public Text Hard2Name;
	public Text Hard2Score;
	public Text Hard3Name;
	public Text Hard3Score;

	void Start () {
// harus diganti
PlayerPrefs.SetString("Song", "Spectre");
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://djseblak-diamondproduction.firebaseio.com/");
		Song.text = PlayerPrefs.GetString("Song");
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Score").Child(PlayerPrefs.GetString("Song"));
		reference.GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				Easy1Name.text = "-";
				Easy1Score.text = "0";
				Easy2Name.text = "-";
				Easy2Score.text = "0";
				Easy3Name.text = "-";
				Easy3Score.text = "0";

				Medium1Name.text = "-";
				Medium1Score.text = "0";
				Medium2Name.text = "-";
				Medium2Score.text = "0";
				Medium3Name.text = "-";
				Medium3Score.text = "0";

				Hard1Name.text = "-";
				Hard1Score.text = "0";
				Hard2Name.text = "-";
				Hard2Score.text = "0";
				Hard3Name.text = "-";
				Hard3Score.text = "0";
			} else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				Easy1Name.text = snapshot.Child("Easy").Child("1").Child("Name").Value.ToString();
				Easy1Score.text = snapshot.Child("Easy").Child("1").Child("Score").Value.ToString();
				Easy2Name.text = snapshot.Child("Easy").Child("2").Child("Name").Value.ToString();
				Easy2Score.text = snapshot.Child("Easy").Child("2").Child("Score").Value.ToString();
				Easy3Name.text = snapshot.Child("Easy").Child("3").Child("Name").Value.ToString();
				Easy3Score.text = snapshot.Child("Easy").Child("3").Child("Score").Value.ToString();

				Medium1Name.text = snapshot.Child("Medium").Child("1").Child("Name").Value.ToString();
				Medium1Score.text = snapshot.Child("Medium").Child("1").Child("Score").Value.ToString();
				Medium2Name.text = snapshot.Child("Medium").Child("2").Child("Name").Value.ToString();
				Medium2Score.text = snapshot.Child("Medium").Child("2").Child("Score").Value.ToString();
				Medium3Name.text = snapshot.Child("Medium").Child("3").Child("Name").Value.ToString();
				Medium3Score.text = snapshot.Child("Medium").Child("3").Child("Score").Value.ToString();

				Hard1Name.text = snapshot.Child("Hard").Child("1").Child("Name").Value.ToString();
				Hard1Score.text = snapshot.Child("Hard").Child("1").Child("Score").Value.ToString();
				Hard2Name.text = snapshot.Child("Hard").Child("2").Child("Name").Value.ToString();
				Hard2Score.text = snapshot.Child("Hard").Child("2").Child("Score").Value.ToString();
				Hard3Name.text = snapshot.Child("Hard").Child("3").Child("Name").Value.ToString();
				Hard3Score.text = snapshot.Child("Hard").Child("3").Child("Score").Value.ToString();
			}
		});
	}

	// ini dipake kalo suatu saat mau nambah lagu baru
	void AddSong (string Song) {
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Score");
		reference.Child(Song).Child("Easy").Child("1").Child("Name").SetValueAsync("-");
		reference.Child(Song).Child("Easy").Child("1").Child("Score").SetValueAsync(0);
		reference.Child(Song).Child("Easy").Child("2").Child("Name").SetValueAsync("-");
		reference.Child(Song).Child("Easy").Child("2").Child("Score").SetValueAsync(0);
		reference.Child(Song).Child("Easy").Child("3").Child("Name").SetValueAsync("-");
		reference.Child(Song).Child("Easy").Child("3").Child("Score").SetValueAsync(0);

		reference.Child(Song).Child("Medium").Child("1").Child("Name").SetValueAsync("-");
		reference.Child(Song).Child("Medium").Child("1").Child("Score").SetValueAsync(0);
		reference.Child(Song).Child("Medium").Child("2").Child("Name").SetValueAsync("-");
		reference.Child(Song).Child("Medium").Child("2").Child("Score").SetValueAsync(0);
		reference.Child(Song).Child("Medium").Child("3").Child("Name").SetValueAsync("-");
		reference.Child(Song).Child("Medium").Child("3").Child("Score").SetValueAsync(0);

		reference.Child(Song).Child("Hard").Child("1").Child("Name").SetValueAsync("-");
		reference.Child(Song).Child("Hard").Child("1").Child("Score").SetValueAsync(0);
		reference.Child(Song).Child("Hard").Child("2").Child("Name").SetValueAsync("-");
		reference.Child(Song).Child("Hard").Child("2").Child("Score").SetValueAsync(0);
		reference.Child(Song).Child("Hard").Child("3").Child("Name").SetValueAsync("-");
		reference.Child(Song).Child("Hard").Child("3").Child("Score").SetValueAsync(0);
	}
}