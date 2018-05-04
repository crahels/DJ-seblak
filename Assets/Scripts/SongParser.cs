using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SongParser : MonoBehaviour
{
    public Text textLeft;
    public Text textRight;
    //This structure contains all the information for this track
    public struct Metadata
    {
        //Is the song's structure valid?
        public bool valid;

        //The Title, Subtitle and Artist for the song
        public string title;
        public string subtitle;
        public string artist;

        //The file paths for the related images and song media
        public string bannerPath;
        public string backgroundPath;
        public string musicPath;

        //The offset that the song starts at compared to the step info
        public float offset;

        //The start and length of the sample that is played when selecting a song
        public float sampleStart;
        public float sampleLength;

        //The bpm the song is played at
        public float bpm;

        //The note data for each difficulty, 
        //as well as a boolean to check that data for that difficulty exists
        public NoteData beginner;
        public bool beginnerExists;
        public NoteData easy;
        public bool easyExists;
        public NoteData medium;
        public bool mediumExists;
        public NoteData hard;
        public bool hardExists;
        public NoteData challenge;
        public bool challengeExists;
        public string difficulty;
    }

    //This structure contains all the bars for a song at a single difficulty
    public struct NoteData
    {
        public List<List<Notes>> bars;
    }

    //This structure contains note information for a single 'row' of notes
    //Right now it's just a simple "Is there a note there or not"
    //But this could be modified and expanded to support numerous note types
    public struct Notes
    {
        public int left;
        public int right;
        //public bool up;
        //public bool down;
    }

    private Metadata songData;
    private string difficulty;
    private bool isInit = false;
    private float arrowSpeed;
    private float barTime;
    private float distance = 0.001f; //0.0005
    private float songTimer;
    private float barExecutedTime = 0;
    private int barCount = 0;
    private NoteData noteData;
    private AudioClip sourceAudio;
    private AudioSource songAudio = new AudioSource();
    private string song_source;
    private string song_name;
    private int idx_song;

    // Use this for initialization
    void Start()
    {
        // add song for the game
        idx_song = PlayerPrefs.GetInt("songs");
        songAudio = GetComponent<AudioSource>();

        if (idx_song >= 0 && idx_song <= 3)
        {
            if (idx_song == 0)
            {
                song_name = "fancy";
            } else if (idx_song == 1)
            {
                song_name = "beethoven";
            } else if (idx_song == 2)
            {
                song_name = "elektronokimia";
            } else
            {
                song_name = "spectre";
            }
        } else
        {
            song_name = "spectre";
        }

        sourceAudio = Resources.Load("Sounds/" + song_name, typeof(AudioClip)) as AudioClip;
        songAudio.clip = sourceAudio;

        PlayerPrefs.SetFloat("combo", 0);
        songAudio.Play();
        TextAsset level = Resources.Load("External/" + song_name, typeof(TextAsset)) as TextAsset;
        
        //string filePath = "Assets/Resources/External/demo.sm";
        //Check if the file path is empty

        //Create a boolean variable that we'll use to check whether
        //we're currently parsing the notes or other metadata
        bool inNotes = false;

        Metadata songData = new Metadata();
        //Initialise Metadata
        //If it encounters any major errors during parsing, this is set to false and the song cannot be selected
        songData.valid = true;
        songData.beginnerExists = false;
        songData.easyExists = false;
        songData.mediumExists = false;
        songData.hardExists = false;
        songData.challengeExists = false;

        //Collect the raw data from the sm file all at once
        List<string> fileData = level.text.Split('\n').ToList(); //C#//File.ReadAllLines(filePath).ToList();

        //Get the file directory, and make sure it ends with either forward or backslash
        string fileDir = Path.GetDirectoryName("External/" + song_name + ".txt");
        if (!fileDir.EndsWith("\\") && !fileDir.EndsWith("/"))
        {
            fileDir += "\\";
        }

        //Go through the file data
        for (int i = 0; i < fileData.Count; i++)
        {
            //Parse the data from the document
            string line = fileData[i].Trim();

            if (line.StartsWith("//"))
            {
                //It's a comment, ignore it and go to the next line
                continue;
            }
            else if (line.StartsWith("#"))
            {
                //the # symbol denotes generic metadata for the song
                string key = line.Substring(0, line.IndexOf(':')).Trim('#').Trim(':');

                switch (key.ToUpper())
                {
                    case "TITLE":
                        songData.title = line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        break;
                    case "SUBTITLE":
                        songData.subtitle = line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        break;
                    case "ARTIST":
                        songData.artist = line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        break;
                    case "BANNER":
                        songData.bannerPath = fileDir + line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        break;
                    case "BACKGROUND":
                        songData.backgroundPath = fileDir + line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        break;
                    case "MUSIC":
                        songData.musicPath = fileDir + line.Substring(line.IndexOf(':')).Trim(':').Trim(';');
                        if (!File.Exists(songData.musicPath))
                        {
                            //No music file found!
                            songData.musicPath = null;
                            songData.valid = false;
                        }
                        break;
                    case "OFFSET":
                        if (!float.TryParse(line.Substring(line.IndexOf(':')).Trim(':').Trim(';'), out songData.offset))
                        {
                            //Error Parsing
                            songData.offset = 0.0f;
                        }
                        break;
                    case "SAMPLESTART":
                        if (!float.TryParse(line.Substring(line.IndexOf(':')).Trim(':').Trim(';'), out songData.sampleStart))
                        {
                            //Error Parsing
                            songData.sampleStart = 0.0f;
                        }
                        break;
                    case "SAMPLELENGTH":
                        if (!float.TryParse(line.Substring(line.IndexOf(':')).Trim(':').Trim(';'), out songData.sampleLength))
                        {
                        }
                        break;
                    case "DISPLAYBPM":
                        if (!float.TryParse(line.Substring(line.IndexOf(':')).Trim(':').Trim(';'), out songData.bpm) || songData.bpm <= 0)
                        {
                            //Error Parsing - BPM not valid
                            songData.valid = false;
                            songData.bpm = 0.0f;
                        }
                        break;
                    case "NOTES":
                        inNotes = true;
                        break;
                    default:
                        break;
                }
            }

            if (inNotes)
            {
                //We skip some feature we're not implementing for now
                if (line.ToLower().Contains("dance-double"))
                {
                    //And update the for loop we're in to adequately skip this section
                    for (int j = i; j < fileData.Count; j++)
                    {
                        if (fileData[j].Contains(";"))
                        {
                            i = j - 1;
                            break;
                        }
                    }
                }

                //Check if it's a difficulty
                if (line.ToLower().Contains("beginner") ||
                    line.ToLower().Contains("easy") ||
                    line.ToLower().Contains("medium") ||
                    line.ToLower().Contains("hard") ||
                    line.ToLower().Contains("challenge"))
                {
                    //And if it does have a difficulty declaration
                    //Then we're at the start of a 'step chart' 
                    string difficulty = line.Trim().Trim(':');

                    //We update the parsing for loop to after the current step chart, and also record the note data along the way
                    //We can then analyse the note data and parse it further
                    List<string> noteChart = new List<string>();
                    for (int j = i; j < fileData.Count; j++)
                    {
                        string noteLine = fileData[j].Trim();
                        if (noteLine.EndsWith(";"))
                        {
                            i = j - 1;
                            break;
                        }
                        else
                        {
                            noteChart.Add(noteLine);
                        }
                    }

                    songData.difficulty = difficulty.ToLower().Trim();

                    //Here we determine what difficulty we're in, and begin parsing the accompanied note data
                    switch (songData.difficulty)
                    {
                        case "beginner":
                            songData.beginnerExists = true;
                            songData.beginner = ParseNotes(noteChart);
                            break;
                        case "easy":
                            songData.easyExists = true;
                            songData.easy = ParseNotes(noteChart);
                            break;
                        case "medium":
                            songData.mediumExists = true;
                            songData.medium = ParseNotes(noteChart);
                            break;
                        case "hard":
                            songData.hardExists = true;
                            songData.hard = ParseNotes(noteChart);
                            break;
                        case "challenge":
                            songData.challengeExists = true;
                            songData.challenge = ParseNotes(noteChart);
                            break;
                    }
                }
                if (line.EndsWith(";"))
                {
                    inNotes = false;
                }
            }
        }
        InitSteps(songData, songData.difficulty);
    }

    private NoteData ParseNotes(List<string> notes)
    {
        //We first instantiate our structures
        NoteData noteData = new NoteData();
        noteData.bars = new List<List<Notes>>();

        //And then work through each line of the raw note data
        List<Notes> bar = new List<Notes>();
        for (int i = 0; i < notes.Count; i++)
        {
            //Based on different line properties we can determine what data that
            //line contains, such as a semicolon dictating the end of the note data
            //or a comma indicating the end of that bar
            string line = notes[i].Trim();

            if (line.StartsWith(";"))
            {
                break;
            }

            if (line.EndsWith(","))
            {
                noteData.bars.Add(bar);
                bar = new List<Notes>();
            }
            else if (line.EndsWith(":"))
            {
                continue;
            }
            else if (line.Length >= 2)
            {
                //When we have a single 'note row' such as '0010' or '0110'
                //We check which columns will contain 'steps' and mark the appropriate flags
                Notes note = new Notes();
                note.left = line[0] - '0';
                note.right = line[1] - '0';
                //We then add this information to our current bar and continue until end
                bar.Add(note);
            }
        }

        return noteData;
    }

    public void InitSteps(Metadata newSongData, string newDifficulty)
    {
        songData = newSongData;
        isInit = true;

        //We estimate how many seconds a single 'bar' will be in the song
        //Using the bpm provided in the song data
        barTime = (60.0f / songData.bpm) * 4.0f;

        difficulty = newDifficulty;

        //We then use the provided difficulty to determine how fast the arrows 
        //will be going
        switch (difficulty)
        {
            case "beginner":
                arrowSpeed = 0.007f;
                noteData = songData.beginner;
                break;
            case "easy":
                arrowSpeed = 0.009f;
                noteData = songData.easy;
                break;
            case "medium":
                arrowSpeed = 0.011f;
                noteData = songData.medium;
                break;
            case "hard":
                arrowSpeed = 0.013f;
                noteData = songData.hard;
                break;
            case "challenge":
                arrowSpeed = 0.016f;
                noteData = songData.challenge;
                break;
            default:
                goto case "easy";
        }

        //This variable is needed when we look at changing the speed of the song
        //with the variable BPM mechanic
    }

    IEnumerator PlaceBar(List<Notes> bar)
    {
        for (int i = 0; i < bar.Count; i++)
        {
            if (bar[i].left > 0 && bar[i].left < 5)
            {
                int x = -100;
                int y = 110;
                int z = 10;
                string resource;
                GameObject obj = null;
                if (bar[i].left == 1)
                {
                    textLeft.text = "swipe left";
                    resource = "Prefabs/Ceker";
                    obj = (GameObject)Instantiate(Resources.Load(resource), new Vector3(x, y, z), Quaternion.Euler(new Vector3(0, 0, 45)));
                } else if (bar[i].left == 2)
                {
                    textLeft.text = "swipe up";
                    resource = "Prefabs/Kerupuk";
                    obj = (GameObject)Instantiate(Resources.Load(resource), new Vector3(x, y, z), Quaternion.identity);
                } else if (bar[i].left == 3)
                {
                    textLeft.text = "swipe down";
                    resource = "Prefabs/Siomay";
                    obj = (GameObject)Instantiate(Resources.Load(resource), new Vector3(x, y, z), Quaternion.identity);
                } else
                {
                    textLeft.text = "tap";
                    resource = "Prefabs/Bakso";
                    obj = (GameObject)Instantiate(Resources.Load(resource), new Vector3(x, y, z), Quaternion.identity);
                }
                Debug.Log("left: " + bar[i].left.ToString());
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -50);
            } else if (bar[i].left == 5)
            {
                yield return new WaitForSeconds(5.0f);
                SceneManager.LoadScene(0);
            }
            if (bar[i].right > 0 && bar[i].right < 5)
            {
                int x = 100;
                int y = 110;
                int z = 10;
                string resource;
                GameObject obj = null;
                if (bar[i].right == 1)
                {
                    textRight.text = "swipe left";
                    resource = "Prefabs/Ceker";
                    obj = (GameObject)Instantiate(Resources.Load(resource), new Vector3(x, y, z), Quaternion.Euler(new Vector3(0, 0, 45)));
                }
                else if (bar[i].right == 2)
                {
                    textRight.text = "swipe up";
                    resource = "Prefabs/Kerupuk";
                    obj = (GameObject)Instantiate(Resources.Load(resource), new Vector3(x, y, z), Quaternion.identity);
                }
                else if (bar[i].right == 3)
                {
                    textRight.text = "swipe down";
                    resource = "Prefabs/Siomay";
                    obj = (GameObject)Instantiate(Resources.Load(resource), new Vector3(x, y, z), Quaternion.identity);
                }
                else 
                {
                    textRight.text = "tap";
                    resource = "Prefabs/Bakso";
                    obj = (GameObject)Instantiate(Resources.Load(resource), new Vector3(x, y, z), Quaternion.identity);
                } 
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -50);
                Debug.Log("right: " + bar[i].right.ToString());
            }
            yield return new WaitForSeconds((barTime / bar.Count) - Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If we're done initializing the rest of the world
        //And we havent gone through all the bars of the song yet
        if (isInit && barCount < noteData.bars.Count)
        {
            //We calculate the time offset using the s=d/t equation (t=d/s) 
            //distance = originalDistance;
            float timeOffset = distance / arrowSpeed;

            //Get the current time through the song 
            songTimer = songAudio.time;

            //If the current song time - the time Offset is greater than 
            //the time taken for all executed bars so far 
            //then it's time for us to spawn the next bar's notes 
            if (songTimer - timeOffset >= (barExecutedTime - barTime))
            {
                StartCoroutine(PlaceBar(noteData.bars[barCount++]));
                barExecutedTime += barTime;
            }
        }
    }    
}
