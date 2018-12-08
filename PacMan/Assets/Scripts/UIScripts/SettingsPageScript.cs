using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsPageScript : MonoBehaviour {
    public Button Dpad_B;
    public Button Swipe_B;
    public Button Joystick_B;
    public Button Back_B;
    public Button ResetHighScores_B;
    public Text SelInputTX;
    // Use this for initialization
    void Start () {
        Dpad_B = Dpad_B.GetComponent<Button>();
        Swipe_B = Swipe_B.GetComponent<Button>();
        Back_B = Back_B.GetComponent<Button>();
        ResetHighScores_B = ResetHighScores_B.GetComponent<Button>();
        Joystick_B = Joystick_B.GetComponent<Button>();
        SelInputTX = SelInputTX.GetComponent<Text>();

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
    public void Back_OnClick()
    {
        SceneManager.LoadScene("StartMenu_Andr");
    }
    private void GetSelectedInput()
    {
        PlayerSettingsScript.InputChoices cur = PlayerSettingsScript.PlayerSettings.SelChoice;
        if(cur==PlayerSettingsScript.InputChoices.dpad)
            SelInputTX.text="Directional Pad";
        else if (cur == PlayerSettingsScript.InputChoices.swipe)
            SelInputTX.text = "Swipe";
        else if (cur == PlayerSettingsScript.InputChoices.accel)
            SelInputTX.text = "Joystick/Accelerometer";
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
        
    }
}
