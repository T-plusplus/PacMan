using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour
{
    private Button startTX;
    private Button settingsTX;
    private Button aboutTX;
    private Button HighScoresTX;

    private readonly string startT = "PlayButton";
    private readonly string setT = "SettingsButton";
    private readonly string abT = "AboutButton";
    private readonly string hsT = "HighScoresButton";
    public Canvas WinCanvas;
    public Canvas AndrCanvas;
    //PlayerSettingsScript.
    
    private void Awake()
    {
        //As new devices are added, we want to print to the debug console what version, and deactivate the necessary UI panels.
#if UNITY_EDITOR_WIN
#if UNITY_ANDROID
        Debug.Log("Android-Edit");
        WinCanvas.gameObject.SetActive(false);
#elif UNITY_WEBGL
        Debug.Log("WebGL Edit");
#else
        Debug.Log("Win Edit");
        AndrCanvas.gameObject.SetActive(false);
#endif //andr-webgl-else this tests the platform settings being used in the Unity window. 
#endif //UNITY_EDITOR_WIN
#if UNITY_ANDROID
        Debug.Log("Android");
        WinCanvas.gameObject.SetActive(false);
#endif
#if UNITY_STANDALONE_WIN
        Debug.Log("Win");
        AndrCanvas.gameObject.SetActive(false);
#endif
    }
    void Start()
    {
        startTX = GameObject.Find(startT).GetComponent<Button>();
        //startTX = startTX.GetComponent<Button>();
        settingsTX = GameObject.Find(setT).GetComponent<Button>();
        aboutTX = GameObject.Find(abT).GetComponent<Button>();
        HighScoresTX = GameObject.Find(hsT).GetComponent<Button>();
        startTX.onClick.AddListener(StartClick);
        settingsTX.onClick.AddListener(SettingsClick);
        aboutTX.onClick.AddListener(AboutClick);
        HighScoresTX.onClick.AddListener(HighScoresClick);
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
