// ini buat get profile
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class ProfileManager : MonoBehaviour
{
    public Text Name;
    public Text Country;
    public Text JoinDate;
    public Text Money;
    public Text Diamond;

    void Start()
    {
        FirebaseApp app = FirebaseApp.DefaultInstance;
        app.SetEditorDatabaseUrl("https://djseblak-diamondproduction.firebaseio.com/");
        if (app.Options.DatabaseUrl != null)
        {
            app.SetEditorDatabaseUrl(app.Options.DatabaseUrl);
        }
        startListener();
    }

    void startListener()
    {
        // harus diganti
PlayerPrefs.SetString("Id", "1");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("Users").Child(PlayerPrefs.GetString("Id"));
        reference.GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Name.text = "User";
		        Country.text = "Indonesia";
		        JoinDate.text = "January 1, 2018";
		        Money.text = "0";
		        Diamond.text = "0";
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Value != null)
                {
                    Name.text = (string)snapshot.Child("Name").Value.ToString();
                    Country.text = (string)snapshot.Child("Country").Value.ToString();
                    JoinDate.text = (string)snapshot.Child("JoinDate").Value.ToString();
                    Money.text = (string)snapshot.Child("Money").Value.ToString();
                    Diamond.text = (string)snapshot.Child("Diamond").Value.ToString();
                }
            }
        });
        reference.ValueChanged += HandleValueChanged;
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }
        DataSnapshot snapshot = args.Snapshot;
        Name.text = (string)snapshot.Child("Name").Value.ToString();
        Country.text = (string)snapshot.Child("Country").Value.ToString();
        JoinDate.text = (string)snapshot.Child("JoinDate").Value.ToString();
        Money.text = (string)snapshot.Child("Money").Value.ToString();
        Diamond.text = (string)snapshot.Child("Diamond").Value.ToString();
    }

    void Update()
    {

    }
}