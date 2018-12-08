using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour {
    public Button startTX;
    public Button settingsTX;
    public Button aboutTX;
    public Button HighScoresTX;

    //PlayerSettingsScript.
    // Use this for initialization
    void Start () {
        startTX = startTX.GetComponent<Button>();
        settingsTX = settingsTX.GetComponent<Button>();
        aboutTX = aboutTX.GetComponent<Button>();
        HighScoresTX = HighScoresTX.GetComponent<Button>();
    }
	
	public void StartClick()
    {
        //if no saved game
        GameSaveScr gss = GameObject.Find("GameSave").GetComponent<GameSaveScr>();
        gss.LoadGame();
        Debug.Log(gss.CurLevelName);
        if (gss.CurLevelName == "")
            SceneManager.LoadScene("L1", LoadSceneMode.Single);
        //else
        else
            SceneManager.LoadScene(gss.CurLevelName);
    }
    public void SettingsClick()
    {
        SceneManager.LoadScene("Settings Page", LoadSceneMode.Single);
    }
    public void AboutClick()
    {
        SceneManager.LoadScene("About Page", LoadSceneMode.Single);
    }
    public void HighScoresClick()
    {
        SceneManager.LoadScene("High Score Page", LoadSceneMode.Single);
    }
}
