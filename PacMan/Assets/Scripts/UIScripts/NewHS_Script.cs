using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewHS_Script : MonoBehaviour
{
    public Canvas WinCanvas;
    public Canvas AndrCanvas;

    /// <summary>
    /// The index of the initial we are currently modifying.
    /// </summary>
    private int cursor = 0;
    /// <summary>
    /// An array indicating which letters have been selected(each dex is the integer value telling where it is in our array of ValidChars).
    /// </summary>
    private int[] dexes = {0, 0, 0};
    /// <summary>
    /// The valid characters to use as initials when getting a high score.
    /// </summary>
    private readonly char[] ValidChars = {'A', 'B', 'C', 'D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
    private readonly string Initials_label = "Initials";
    private readonly string Score_label = "Score";

    private Button UpB;
    private Button DownB;
    private Button LeftB;
    private Button RightB;
    private Button DoneB;

    private Text ScoreTXT;
    private Text InitialsTXT;
    private Text SpotTXT;

    private GameObject[] tmp;
    private Text[] UICursor;

    int place=0;
    private void Awake()
    {
#if UNITY_EDITOR_WIN
#if UNITY_ANDROID
        Debug.Log("High Scores Page: Android Edit");
        WinCanvas.gameObject.SetActive(false);
#elif UNITY_WEBGL
        Debug.Log("High Scores Page: WebGL Edit");
        AndrCanvas.gameObject.SetActive(false);
#else
        Debug.Log("High Scores Page: Win Edit");
        AndrCanvas.gameObject.SetActive(false);
#endif //andr-webgl-else this tests the platform settings being used in the Unity window. 
#endif //UNITY_EDITOR_WIN
#if UNITY_ANDROID
        Debug.Log("High Scores Page: Android");
        WinCanvas.gameObject.SetActive(false);
#endif
#if UNITY_STANDALONE_WIN
        Debug.Log("High Scores Page: Win");
        AndrCanvas.gameObject.SetActive(false);
#endif
#if UNITY_WEBGL
        Debug.Log("High Scores Page: WebGL");
        AndrCanvas.gameObject.SetActive(false);
#endif
    }
    // Start is called before the first frame update
    void Start()
    {
        ScoreTXT = GameObject.Find(Score_label).GetComponent<Text>();
        InitialsTXT = GameObject.Find(Initials_label).GetComponent<Text>();
        SpotTXT = GameObject.Find("Spot").GetComponent<Text>();
        //There's probably a better way to do this next part. 
        tmp = GameObject.FindGameObjectsWithTag("HS_Sel");
        UICursor = new Text[tmp.Length];
        for (int i = 0; i < tmp.Length; i++)
            UICursor[i] = tmp[i].GetComponent<Text>();

#if UNITY_ANDROID
        //Buttons: Bind and add listeners
        UpB = GameObject.Find("UpB").GetComponent<Button>();
        UpB.onClick.AddListener(UpClick);
        DownB = GameObject.Find("DownB").GetComponent<Button>();
        DownB.onClick.AddListener(DownClick);
        LeftB = GameObject.Find("LeftB").GetComponent<Button>();
        LeftB.onClick.AddListener(LeftClick);
        RightB = GameObject.Find("RightB").GetComponent<Button>();
        RightB.onClick.AddListener(RightClick);
#endif
#if UNITY_STANDALONE_WIN
        InvokeRepeating("GetKeyboardInput", 0, .5f);
#endif
#if UNITY_WEBGL
        InvokeRepeating("GetKeyboardInput", 0, .5f);
#endif
        DoneB = GameObject.Find("DoneB").GetComponent<Button>();
        DoneB.onClick.AddListener(DoneClick);

        //Recieve the score and place from the Level
        ScoreTXT.text=PlayerPrefs.GetInt("score")+"";
        place = PlayerPrefs.GetInt("place");

        SpotTXT.text = place + "";

    }

    // Update is called once per frame
    void Update()
    {
        InitialsTXT.text = ""+ValidChars[dexes[0]] + ValidChars[dexes[1]] + ValidChars[dexes[2]];
        //Set UICursor to the position in our backend
        for (int i = 0; i < UICursor.Length; i++)
            UICursor[i].gameObject.SetActive(i == cursor);
#if UNITY_STANDALONE_WIN
        
#endif
    }
    /// <summary>
    /// Function that will handle which digit is being edited. The direction is which way we want to move the cursor.
    /// </summary>
    /// <param name="dir">true to increment, false to decrement.</param>
    private void MoveCursor(bool dir)
    {
        cursor += (dir) ? (1) : (-1);
        //wrap around the back
        if (cursor == 3)
            cursor = 0;
        //wrap around the back
        if (cursor == -1)
            cursor = 2;
    }
    private void CycleAlphabet( bool dir)
    {
        dexes[cursor] += (dir) ? (1) : (-1);
        //wrap around the back
        if (dexes[cursor] == ValidChars.Length)
            dexes[cursor] = 0;
        //wrap around the back
        if (dexes[cursor] == -1)
            dexes[cursor] = ValidChars.Length-1;
    }
    private void UpClick()
    {
        CycleAlphabet(true);
    }
    private void DownClick()
    {
        CycleAlphabet(false);
    }
    private void LeftClick()
    {
        MoveCursor(false);
    }
    private void RightClick()
    {
        MoveCursor(true);
    }
    private void GetKeyboardInput()
    {
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        if (vert != 0)
        {
            if (vert > 0)
                UpClick();
            else if (vert < 0)
                DownClick();
        }
        else
        {
            if (horiz > 0)
                RightClick();
            else if (horiz < 0)
                LeftClick();
        }
    }
    private void DoneClick()
    {
        //Save high scores and Initials
        switch(place)
        {
            case 1:
                PlayerSettingsScript.PlayerSettings.Name1 = InitialsTXT.text;
                break;
            case 2:
                PlayerSettingsScript.PlayerSettings.Name2 = InitialsTXT.text;
                break;
            case 3:
                PlayerSettingsScript.PlayerSettings.Name3 = InitialsTXT.text;
                break;
            case 4:
                PlayerSettingsScript.PlayerSettings.Name4 = InitialsTXT.text;
                break;
            case 5:
                PlayerSettingsScript.PlayerSettings.Name5 = InitialsTXT.text;
                break;
            case 6:
                PlayerSettingsScript.PlayerSettings.Name6 = InitialsTXT.text;
                break;
            case 7:
                PlayerSettingsScript.PlayerSettings.Name7 = InitialsTXT.text;
                break;
            case 8:
                PlayerSettingsScript.PlayerSettings.Name8 = InitialsTXT.text;
                break;
            case 9:
                PlayerSettingsScript.PlayerSettings.Name9 = InitialsTXT.text;
                break;
            case 10:
                PlayerSettingsScript.PlayerSettings.Name10 = InitialsTXT.text;
                break;

        }
        PlayerSettingsScript.PlayerSettings.SaveSettings();
        //Advance to High Score Page.
        SceneManager.LoadScene("High Score Page", LoadSceneMode.Single);
    }
}
