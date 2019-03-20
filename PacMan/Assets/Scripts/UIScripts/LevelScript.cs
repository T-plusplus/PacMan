using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour {
    /// <summary>
    /// The choice of input being used in this level. It's set in Start().
    /// </summary>
    PlayerSettingsScript.InputChoices cur;
    public Button Up_B;
    public Button Down_B;
    public Button Left_B;
    public Button Right_B;
    /// <summary>
    /// The canvas displayed for Directional Pad Control, as a gameObject. We only need the canvas as a gameObject, so we only use it so to prevent unecessary casting.
    /// </summary>
    public GameObject DPad_Cont;
    /// <summary>
    /// The canvas displayed for Swipe Control, as a gameObject. We only need the canvas as a gameObject, so we only use it so to prevent unecessary casting.
    /// </summary>
    public GameObject Swipe_Cont;
    /// <summary>
    /// The canvas displayed for Joystick/Accelerometer Control, as a gameObject. We only need the canvas as a gameObject, so we only use it so to prevent unecessary casting.
    /// </summary>
    public GameObject Accel_Cont;

    private GameSaveScr gs;
    public Canvas PauseMenu;
    public Button PauseButton;
    public Image js_top;

    private Image L1;
    private Image L2;
    private Image L3;
    private Image L4;
    private Image L5;

    private PlayerScript Player;
    // Use this for initialization
    void Start () {
        gs = GameObject.Find("GameSave").GetComponent<GameSaveScr>();
        gs.SaveGame();

        PauseMenu = GameObject.Find("/PauseMenu").GetComponent<Canvas>();
        PauseButton = PauseButton.GetComponent<Button>();
        PauseMenu.enabled = false;
        Player = GameObject.Find("Player").GetComponent<PlayerScript>();
        //c = GameObject.Find("/Controls").GetComponent<Canvas>();
        cur = PlayerSettingsScript.PlayerSettings.SelChoice;
        Debug.Log("Level Init: control - " + cur);
        if(cur==PlayerSettingsScript.InputChoices.keyboard)
        {
            DPad_Cont.SetActive(true);
            Swipe_Cont.SetActive(false);
            Accel_Cont.SetActive(false);

            Up_B = Up_B.GetComponent<Button>();
            Down_B = Down_B.GetComponent<Button>();
            Left_B = Left_B.GetComponent<Button>();
            Right_B = Right_B.GetComponent<Button>();
        }
        if (cur == PlayerSettingsScript.InputChoices.dpad)
        {
            DPad_Cont.SetActive(true);
            Swipe_Cont.SetActive(false);
            Accel_Cont.SetActive(false);
           
            Up_B = Up_B.GetComponent<Button>();
            Down_B = Down_B.GetComponent<Button>();
            Left_B = Left_B.GetComponent<Button>();
            Right_B = Right_B.GetComponent<Button>();
        }
        else if (cur == PlayerSettingsScript.InputChoices.swipe)
        {
            DPad_Cont.SetActive(false);
            Swipe_Cont.SetActive(true);
            Accel_Cont.SetActive(false);

            //set up swipe arrow on canvas
        }
        else if (cur == PlayerSettingsScript.InputChoices.accel)
        {
            DPad_Cont.SetActive(false);
            Swipe_Cont.SetActive(false);
            Accel_Cont.SetActive(true);
            //set up joystick position display on canvas
            js_top = js_top.GetComponent<Image>();
        }
        //Image stuff
        
        L1 = GameObject.Find("Life1").GetComponent<Image>();
        L2 = GameObject.Find("Life2").GetComponent<Image>();
        L3 = GameObject.Find("Life3").GetComponent<Image>();
        L4 = GameObject.Find("Life4").GetComponent<Image>();
        L5 = GameObject.Find("Life5").GetComponent<Image>();
        
        

    }
    private void FixedUpdate()
    {
        if (gs.PlayerLivesLeft < 5)
        {
            L5.gameObject.SetActive(false);
            if (gs.PlayerLivesLeft < 4)
            {
                L4.gameObject.SetActive(false);
                if (gs.PlayerLivesLeft < 3)
                {
                    L3.gameObject.SetActive(false);
                    if (gs.PlayerLivesLeft < 2)
                    {
                        L2.gameObject.SetActive(false);
                        if (gs.PlayerLivesLeft < 1)
                            L1.gameObject.SetActive(false);
                    }
                }
            }
        }
        if (cur == PlayerSettingsScript.InputChoices.keyboard)
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
        else if (cur==PlayerSettingsScript.InputChoices.accel)
        {
            //Here's a link for help: https://docs.unity3d.com/ScriptReference/Input-acceleration.html
            //Here's a link for help: https://docs.unity3d.com/Manual/MobileInput.html
            //criteria:
            bool forwardCri = Input.acceleration.y > -.7;
            bool backCri = Input.acceleration.y < -.97;
            bool leftCri = Input.acceleration.x < -.5;
            bool rightCri = Input.acceleration.x > .5;
            //if(tilt up){
            //Debug.Log(Input.acceleration.x+"    "+Input.acceleration.y+ "      "+Input.acceleration.z);
            //parallel to eyesight(if this is the best description of what I mean)
            bool forwardBack = Input.acceleration.y>-.7 || Input.acceleration.y < -.97;
            bool leftRight = Input.acceleration.x>.5 || Input.acceleration.x<-.5 ;
            //Debug.Log(" F-B " + forwardBack + " l-r " + leftRight);
            Vector3 offset = new Vector3(0, 0, -21);
            if (leftRight)
            {
                
                if(leftCri)
                {
                    LeftClick();
                    js_top.gameObject.transform.position = Vector3.left+offset;
                }
                else if(rightCri)
                {
                    RightClick();
                    js_top.gameObject.transform.position = Vector3.right+offset;
                }
            }
           else if(forwardBack)
            {
                if (forwardCri)
                {
                    UpClick();
                    js_top.gameObject.transform.position = Vector3.forward+offset;
                }
                else if (backCri)
                {
                    DownClick();
                    js_top.gameObject.transform.position = Vector3.back+offset;
                }
            }
            else
            {
                js_top.gameObject.transform.position = offset;
            }
            Debug.Log(js_top.gameObject.transform.position);
         //stick.transform.position= vec3(x, y, z+an amount);
         //UpClick();}
         //else if(tilt down)
         //DownClick();
         //else if(tilt left)
         //LeftClick():
         //else if(tilt right)
         //RightClick();


            
        }
        else if(cur==PlayerSettingsScript.InputChoices.swipe)
        {
            if (Input.touchCount > 0)
            {
                Vector2 thisTouch = Input.GetTouch(0).deltaPosition;
                if(System.Math.Abs(thisTouch.x)<System.Math.Abs(thisTouch.y))
                {
                    //have swipe arrow match the swipe
                    Debug.Log("Vertical");
                    //if(swipe up)
                    if (thisTouch.y>0)
                    {
                        UpClick();
                    }
                    //else if(swipe down)
                    else
                    {
                        DownClick();
                    }
                }
                else
                {
                    Debug.Log("Horiz");
                    //else if(swipe right)
                    if (thisTouch.x>0)
                    {
                        RightClick();
                    }
                    //else if(swipe left)
                    else
                    {
                        LeftClick();
                    }
                }
            }
        }
        //else: dpad: do nothing, not dpad, then error handle
        else
        {
            if(cur==PlayerSettingsScript.InputChoices.dpad)
            {
                //Do Nothing
            }
            else
            {
                Debug.Log("Something is wrong. Failed to get the correct input method");
            }
        }
    }
    public void UpClick()
    {
        Debug.Log("up clicked");
        Player.UpdateDir(Vector3.forward);
    }
    public void DownClick()
    {
        Debug.Log("down clicked");
        Player.UpdateDir(Vector3.back);
    }

    public void LeftClick()
    {
        Debug.Log("left clicked");
        Player.UpdateDir(Vector3.left);
    }
    public void RightClick()
    {
        Debug.Log("right clicked");
        Player.UpdateDir(Vector3.right);

    }
    public void PausePress() //this function will be used on our Exit button

    {
        Time.timeScale = 0;
        PauseMenu.enabled = true; //enable the Quit menu when we click the Exit button
        PauseButton.enabled = false; //then disable the Play and Exit buttons so they cannot be clicked
    }

    public void ReturnToGamePress() //this function will be used for our "NO" button in our Quit Menu
    {
        Time.timeScale = 1;
        PauseMenu.enabled = false; //we'll disable the quit menu, meaning it won't be visible anymore
        PauseButton.enabled = true; //enable the Play and Exit buttons again so they can be clicked
    }
    public void SaveAndExitPress() //this function will be used for our "NO" button in our Quit Menu
    {
        Time.timeScale = 1;
        PlayerSettingsScript.PlayerSettings.SaveSettings();
        SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
        gs.SaveGame();
    }
    
}
