// ini buat nambahin duit kalo menang
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class MoneyManager : MonoBehaviour {

	public String UserId;
	public int UserMoney;
	public int UserDiamond;
	public int AddMoney;
	public int AddDiamond;

	void Start () {
// harus diganti
PlayerPrefs.SetInt("Money", 10);
PlayerPrefs.SetInt("Diamond", 2);
		AddMoney = PlayerPrefs.GetInt("Money");
		AddDiamond = PlayerPrefs.GetInt("Diamond");
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://djseblak-diamondproduction.firebaseio.com/");
		UserId = PlayerPrefs.GetString("Id");
		DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Users").Child(UserId);
		reference.GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				UserMoney = 0;
				UserDiamond = 0;
			} else if (task.IsCompleted) {
				DataSnapshot snapshot = task.Result;
				UserMoney = Int32.Parse(snapshot.Child("Money").Value.ToString());
				UserDiamond = Int32.Parse(snapshot.Child("Diamond").Value.ToString());
			}
		});
		reference.Child("Money").SetValueAsync(UserMoney + AddMoney);
		reference.Child("Diamond").SetValueAsync(UserDiamond + AddDiamond);
	}
	
}
