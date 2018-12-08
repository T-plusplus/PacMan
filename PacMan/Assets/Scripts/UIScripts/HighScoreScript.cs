using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScoreScript : MonoBehaviour
{
    private readonly string hs = "HighScore";
    public Button Back_B;
    
    Text HS1_TXT;
    Text HS2_TXT;
    Text HS3_TXT;
    Text HS4_TXT;
    Text HS5_TXT;
    Text HS6_TXT;
    Text HS7_TXT;
    Text HS8_TXT;
    Text HS9_TXT;
    Text HS10_TXT;
    // Use this for initialization
    void Start()
    {
        Back_B = Back_B.GetComponent<Button>();
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
    }
    public void Back_OnClick()
    {
        SceneManager.LoadScene("StartMenu_Andr");
    }
}
