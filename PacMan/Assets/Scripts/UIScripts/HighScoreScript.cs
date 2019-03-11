using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour
{
    public Canvas WinCanvas;
    public Canvas AndrCanvas;

    private Button Back_B;
    
    private Text HS1_TXT;
    private Text HS2_TXT;
    private Text HS3_TXT;
    private Text HS4_TXT;
    private Text HS5_TXT;
    private Text HS6_TXT;
    private Text HS7_TXT;
    private Text HS8_TXT;
    private Text HS9_TXT;
    private Text HS10_TXT;

    private readonly string hs = "HS";
    private readonly string hsName_name = "Name";
    private readonly string Back_name = "Back";
    private Text[] Names = new Text[10];
    
    private void Awake()
    {
        //As new devices are added, remember to look at what's needed for UI, and deactivate those panels and bind the relevant pieces. Also as new controls 
        //are added, add those as well.
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
    //We find UI texts and set them like this because of how we save them. The file doesn't like to persist arrays. you have to encapsulate it
    //in a different serializable object. I know how, it's just a pretty massive backend update for something that will not affect the game in 
    //a way that someone would see while playing. 
    void Start()
    {
        Back_B = GameObject.Find(Back_name).GetComponent<Button>();
        Back_B.onClick.AddListener(Back_OnClick);
        //for (int x = 0; x < 10; x++)
        // {
        //   int y = x + 1;
        //   HSGO[x] = GameObject.Find(hs + y);
        //    HS_txt[x] = HSGO[x].GetComponent<Text>();
        //  HS_txt[x].text = PlayerSettingsScript.PlayerSettings.HighScores[x].ToString();
        //}
        Debug.Log(Application.persistentDataPath);
        HS1_TXT = GameObject.Find("HighScore1").GetComponent<Text>();
        HS1_TXT.text = PlayerSettingsScript.PlayerSettings.HighScore1.ToString();

        HS2_TXT = GameObject.Find("HighScore2").GetComponent<Text>();
        HS2_TXT.text = PlayerSettingsScript.PlayerSettings.HighScore2.ToString();
        
        HS3_TXT = GameObject.Find("HighScore3").GetComponent<Text>();
        HS3_TXT.text = PlayerSettingsScript.PlayerSettings.HighScore3.ToString();
        
        HS4_TXT = GameObject.Find("HighScore4").GetComponent<Text>();
        HS4_TXT.text = PlayerSettingsScript.PlayerSettings.HighScore4.ToString();
        
        HS5_TXT = GameObject.Find("HighScore5").GetComponent<Text>();
        HS5_TXT.text = PlayerSettingsScript.PlayerSettings.HighScore5.ToString();
        
        HS6_TXT = GameObject.Find("HighScore6").GetComponent<Text>();
        HS6_TXT.text = PlayerSettingsScript.PlayerSettings.HighScore6.ToString();
        
        HS7_TXT = GameObject.Find("HighScore7").GetComponent<Text>();
        HS7_TXT.text = PlayerSettingsScript.PlayerSettings.HighScore7.ToString();
        
        HS8_TXT = GameObject.Find("HighScore8").GetComponent<Text>();
        HS8_TXT.text = PlayerSettingsScript.PlayerSettings.HighScore8.ToString();
        
        HS9_TXT = GameObject.Find("HighScore9").GetComponent<Text>();
        HS9_TXT.text = PlayerSettingsScript.PlayerSettings.HighScore9.ToString();
        
        HS10_TXT = GameObject.Find("HighScore10").GetComponent<Text>();
        HS10_TXT.text = PlayerSettingsScript.PlayerSettings.HighScore10.ToString();

        for(int i=1; i<=10; i++)
            Names[i-1] = GameObject.Find(hsName_name + i).GetComponent<Text>();
        //Names[0] = GameObject.Find("Name1").GetComponent<Text>();
        Names[0].text = PlayerSettingsScript.PlayerSettings.Name1.ToString();
        Names[1].text = PlayerSettingsScript.PlayerSettings.Name2.ToString();
        Names[2].text = PlayerSettingsScript.PlayerSettings.Name3.ToString();
        Names[3].text = PlayerSettingsScript.PlayerSettings.Name4.ToString();
        Names[4].text = PlayerSettingsScript.PlayerSettings.Name5.ToString();
        Names[5].text = PlayerSettingsScript.PlayerSettings.Name6.ToString();
        Names[6].text = PlayerSettingsScript.PlayerSettings.Name7.ToString();
        Names[7].text = PlayerSettingsScript.PlayerSettings.Name8.ToString();
        Names[8].text = PlayerSettingsScript.PlayerSettings.Name9.ToString();
        Names[9].text = PlayerSettingsScript.PlayerSettings.Name10.ToString();
    }
    public void Back_OnClick()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
