using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPageScript : MonoBehaviour {
    private Button Dpad_B;
    private Button Swipe_B;
    private Button Joystick_B;
    private Button Keyboard_B;
    private Button Back_B;
    private Button ResetHighScores_B;
    private Text SelInputTX;

    public Canvas WinCanvas;
    public Canvas AndrCanvas;

    private readonly string Dpad_name = "DPad";
    private readonly string Swipe_name = "Swipe";
    private readonly string Joystick_name = "Accel";
    private readonly string Keyboard_name = "Keyboard";
    private readonly string Back_name = "Back";
    private readonly string ResetHighScores = "ResetHighScores";
    private readonly string SelInput = "Selected";
    
    //Use Awake to manage which screen to use. Use start to manage the elements within the screen(for example, if we want to deactivate one thing from the
    //Windows screen, or for adding the click listeners.
    private void Awake()
    {
            //As new devices are added, remember to look at what's needed for UI, and deactivate those panels and bind the relevant pieces. Also as new controls 
            //are added, add those as well.
#if UNITY_EDITOR_WIN
#if UNITY_ANDROID
        Debug.Log("Settings Page: Android Edit");
        WinCanvas.gameObject.SetActive(false);
#elif UNITY_WEBGL
        Debug.Log("Settings Page: WebGL Edit");
        AndrCanvas.gameObject.SetActive(false);
#else
            Debug.Log("Settings Page: Win Edit");
            AndrCanvas.gameObject.SetActive(false);
#endif //andr-webgl-else this tests the platform settings being used in the Unity window. 
#endif //UNITY_EDITOR_WIN
#if UNITY_ANDROID
        Debug.Log("Settings Page: Android");
        WinCanvas.gameObject.SetActive(false);
#endif
#if UNITY_STANDALONE_WIN
            Debug.Log("Settings Page: Win");
            AndrCanvas.gameObject.SetActive(false);
#endif
#if UNITY_WEBGL
        Debug.Log("Settings Page: WebGL");
        AndrCanvas.gameObject.SetActive(false);
#endif
    }
    //Use start to manage the components within the screens.
    void Start () {
        Dpad_B = GameObject.Find(Dpad_name).GetComponent<Button>();
        Back_B = GameObject.Find(Back_name).GetComponent<Button>();
        ResetHighScores_B = GameObject.Find(ResetHighScores).GetComponent<Button>();
        SelInputTX = GameObject.Find(SelInput).GetComponent<Text>();

        //Add Listeners
        Dpad_B.onClick.AddListener(DPad_OnClick);
        Back_B.onClick.AddListener(Back_OnClick);
        ResetHighScores_B.onClick.AddListener(ClearHighScores);
#if UNITY_ANDROID
        Swipe_B = GameObject.Find(Swipe_name).GetComponent<Button>();
        Joystick_B = GameObject.Find(Joystick_name).GetComponent<Button>();
        Swipe_B.onClick.AddListener(Swipe_OnClick);
        Joystick_B.onClick.AddListener(JS_OnClick);
#endif
#if UNITY_STANDALONE_WIN
        Keyboard_B = GameObject.Find(Keyboard_name).GetComponent<Button>();
        Keyboard_B.onClick.AddListener(Keyboard_OnClick);
#endif
        GetSelectedInput();
    }
    public void DPad_OnClick()
    {
        //change in player settings
        PlayerSettingsScript.PlayerSettings.SelChoice = PlayerSettingsScript.InputChoices.dpad;
        //Save
        PlayerSettingsScript.PlayerSettings.SaveSettings();
        //this is costly, but it will basically check that we are saving, and send a debug message if we aren't getting it.
        GetSelectedInput();
    }
    public void Swipe_OnClick()
    {
        PlayerSettingsScript.PlayerSettings.SelChoice = PlayerSettingsScript.InputChoices.swipe;
        PlayerSettingsScript.PlayerSettings.SaveSettings();
        GetSelectedInput();
    }
    public void JS_OnClick()
    {
        PlayerSettingsScript.PlayerSettings.SelChoice = PlayerSettingsScript.InputChoices.accel;
        PlayerSettingsScript.PlayerSettings.SaveSettings();
        GetSelectedInput();
    }
    public void Keyboard_OnClick()
    {
        PlayerSettingsScript.PlayerSettings.SelChoice = PlayerSettingsScript.InputChoices.keyboard;
        PlayerSettingsScript.PlayerSettings.SaveSettings();
        GetSelectedInput();
    }
    public void Back_OnClick()
    {
        SceneManager.LoadScene("StartMenu");
    }
    private void GetSelectedInput()
    {
        //Debug.Log(PlayerSettingsScript.PlayerSettings.SelChoice);
        PlayerSettingsScript.InputChoices cur = PlayerSettingsScript.PlayerSettings.SelChoice;
        if (cur == PlayerSettingsScript.InputChoices.dpad)
            SelInputTX.text = "Directional Pad";
        else if (cur == PlayerSettingsScript.InputChoices.swipe)
            SelInputTX.text = "Swipe";
        else if (cur == PlayerSettingsScript.InputChoices.accel)
            SelInputTX.text = "Joystick/Accelerometer";
        else if (cur == PlayerSettingsScript.InputChoices.keyboard)
            SelInputTX.text = "Keyboard(WASD)";
        //If we get here, something is wrong
        else
            Debug.Log("cannot get input from persistence");
    }
    public void ClearHighScores()
    {
        //for(int x=0; x<10; x++)
        PlayerSettingsScript.PlayerSettings.HighScore1 = 00000;
        PlayerSettingsScript.PlayerSettings.HighScore2 = 00000;
        PlayerSettingsScript.PlayerSettings.HighScore3 = 00000;
        PlayerSettingsScript.PlayerSettings.HighScore4 = 00000;
        PlayerSettingsScript.PlayerSettings.HighScore5 = 00000;
        PlayerSettingsScript.PlayerSettings.HighScore6 = 00000;
        PlayerSettingsScript.PlayerSettings.HighScore7 = 00000;
        PlayerSettingsScript.PlayerSettings.HighScore8 = 00000;
        PlayerSettingsScript.PlayerSettings.HighScore9 = 00000;
        PlayerSettingsScript.PlayerSettings.HighScore10 = 00000;

        PlayerSettingsScript.PlayerSettings.Name1 = "AAA";
        PlayerSettingsScript.PlayerSettings.Name2 = "AAA";
        PlayerSettingsScript.PlayerSettings.Name3 = "AAA";
        PlayerSettingsScript.PlayerSettings.Name4 = "AAA";
        PlayerSettingsScript.PlayerSettings.Name5 = "AAA";
        PlayerSettingsScript.PlayerSettings.Name6 = "AAA";
        PlayerSettingsScript.PlayerSettings.Name7 = "AAA";
        PlayerSettingsScript.PlayerSettings.Name8 = "AAA";
        PlayerSettingsScript.PlayerSettings.Name9 = "AAA";
        PlayerSettingsScript.PlayerSettings.Name10 = "AAA";

    }
}
