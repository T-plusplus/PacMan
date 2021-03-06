﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    //Notes for sites for additional features. These features do not necessarily have to be implemented here:
    // Glowing wall outlines
    //Flames: https://unity3d.com/learn/tutorials/topics/graphics/flame-particles-overview?playlist=17102
    public float speed;
    /// <summary>
    /// Vector representing the current direction.
    /// </summary>
    private Vector3 dir;
    private Rigidbody rb;

    private CoinSpawner CS_scriptRef;
    private readonly string PowUpTag = "PowerUp";
    private readonly string GhostTag = "Ghost";
    private readonly string CoinTag = "Coin";
    
    private readonly string curGameLabel = "CG_Score";
    private readonly string HSLabel = "GH_Score";
    private readonly string GameSaveLabel = "GameSave";
    private readonly string CoinSpawnerLabel = "CoinSpawn";
    /// <summary>
    /// The place where the player starts and respawns.
    /// </summary>
    private Vector3 startVec;
    private float respawnTime=0f;
    public Material mat1;
    public Material mat2;

    private readonly int PowUpSc = 50;
    private readonly int CoinSc = 10;

    private Text gameScore;
    /// <summary>
    /// The score of this game.
    /// </summary>
    private int LocalScore;
    private Text HS_TXT;
    
    // Use this for initialization
    void Start()
    {
        //Debug.Log(Application.persistentDataPath);
        rb = GetComponent<Rigidbody>();
        //start right
        dir = Vector3.right;
        startVec = gameObject.transform.position;
        
        gameScore = GameObject.Find(curGameLabel).GetComponent<Text>();
        // Assign new string to "Text" field in that component
        //gameScore.text = PlayerSettingsScript.PlayerSettings.score.ToString();
        GameSaveScr gss = GameObject.Find(GameSaveLabel).GetComponent<GameSaveScr>();
        LocalScore = gss.score;
        gameScore.text = LocalScore.ToString();
        CS_scriptRef = GameObject.Find(CoinSpawnerLabel).GetComponent<CoinSpawner>();
        HS_TXT = GameObject.Find(HSLabel).GetComponent<Text>();
        


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //dir will be set by UpdateDir
        rb.velocity = speed * dir; 
        
        //no need to update score here. The trigger will do that.
        if(respawnTime>0)
        {
            respawnTime -= Time.deltaTime;
            gameObject.GetComponent<Renderer>().material = mat2;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = mat1;
        }
    }
    /// <summary>
    /// Checks the Validity of PacMan moving one space in that direction.
    /// </summary>
    /// <param name="dir">The direction we wish to check</param>
    /// <returns>True if PacMan can move in that direction, false if he is obstructed</returns>
    bool Valid(Vector3 dir)
    {
        Vector3 pos = transform.position;
        int mask = 1 << LayerMask.NameToLayer("CoinPU");
        //Debug.Log("Pos " + pos + "   Dir " + dir);
        return !Physics.Linecast(pos, pos + dir,~mask);
    }
    /// <summary>
    /// Updates the direction, if PacMan is not obstructed in that direction
    /// </summary>
    /// <param name="toDir">the direction we wish to change PacMan's direction to.</param>
    public void UpdateDir(Vector3 toDir)
    {
        if (Valid(toDir))
        {
            dir = toDir;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject);
        if (other.gameObject.CompareTag(PowUpTag))
        {
            other.gameObject.SetActive(false);
            UpdateScore(PowUpSc);
            //set Ghosts to frightened mode
            GameObject[] g = GameObject.FindGameObjectsWithTag(GhostTag);
            for (int i = 0; i < g.Length; i++)
            {
                //Debug.Log(g[i]);
                //Debug.Log(i+" "+g.Length+g);
                g[i].GetComponent<GhostScript>().SetFrightenedState();
            }
        }
        else if(other.gameObject.CompareTag(CoinTag))
        {
            other.gameObject.SetActive(false);
            //increment score
            UpdateScore(CoinSc);
            //gameLogic for finished maze in UpdateScore()
            
        }
        else if(other.gameObject.CompareTag(GhostTag))
        {
            Debug.Log("here");
            //script instance
            GhostScript gs = other.gameObject.GetComponent<GhostScript>();
            //if the Ghost is frightened
            if(gs.GetState()==GhostScript.States.frightened)
            {
                //kill ghost
                //determine if ghost has already been killed in succession
                //other.transform.position = new Vector3(0, .5f, 0);
                UpdateScore(200);
                other.GetComponent<GhostScript>().ResetGhost();
            }
            else
            {
                //kill pacman, deduct one life, reload scene
                gameObject.transform.position = startVec;
                respawnTime = 5f;
                //Decrement Lives
                GameSaveScr gss = GameObject.Find("GameSave").GetComponent<GameSaveScr>();
                gss.PlayerLivesLeft--;
                Debug.Log(gss.PlayerLivesLeft);
                if (gss.PlayerLivesLeft == 0)
                {
                    gss.score=LocalScore;
                    //GameOver
                    gss.CurLevelName = "";
                    bool nhs = false;//new high score
                    int place = 0;
                    if (gss.score < PlayerSettingsScript.PlayerSettings.HighScore1)
                    {
                        if (gss.score < PlayerSettingsScript.PlayerSettings.HighScore2)
                        {
                            if (gss.score < PlayerSettingsScript.PlayerSettings.HighScore3)
                            {
                                if (gss.score < PlayerSettingsScript.PlayerSettings.HighScore4)
                                {
                                    if (gss.score < PlayerSettingsScript.PlayerSettings.HighScore5)
                                    {
                                        if (gss.score < PlayerSettingsScript.PlayerSettings.HighScore6)
                                        {
                                            if (gss.score < PlayerSettingsScript.PlayerSettings.HighScore7)
                                            {
                                                if (gss.score < PlayerSettingsScript.PlayerSettings.HighScore8)
                                                {
                                                    if (gss.score < PlayerSettingsScript.PlayerSettings.HighScore9)
                                                    {

                                                        if (gss.score < PlayerSettingsScript.PlayerSettings.HighScore10)
                                                        {
                                                            //Do nothing, not a high score
                                                        }
                                                        else
                                                        {
                                                            PlayerSettingsScript.PlayerSettings.HighScore10 = gss.score;
                                                            nhs = true;
                                                            place = 10;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        PlayerSettingsScript.PlayerSettings.HighScore10 = PlayerSettingsScript.PlayerSettings.HighScore9;
                                                        PlayerSettingsScript.PlayerSettings.HighScore9 = gss.score;
                                                        nhs = true;
                                                        place = 9;
                                                    }
                                                }
                                                else
                                                {
                                                    PlayerSettingsScript.PlayerSettings.HighScore9 = PlayerSettingsScript.PlayerSettings.HighScore8;
                                                    PlayerSettingsScript.PlayerSettings.HighScore10 = PlayerSettingsScript.PlayerSettings.HighScore9;
                                                    PlayerSettingsScript.PlayerSettings.HighScore8 = gss.score;
                                                    nhs = true;
                                                    place = 8;
                                                }
                                            }
                                            else
                                            {
                                                PlayerSettingsScript.PlayerSettings.HighScore10 = PlayerSettingsScript.PlayerSettings.HighScore9;
                                                PlayerSettingsScript.PlayerSettings.HighScore9 = PlayerSettingsScript.PlayerSettings.HighScore8;
                                                PlayerSettingsScript.PlayerSettings.HighScore8 = PlayerSettingsScript.PlayerSettings.HighScore7;
                                                PlayerSettingsScript.PlayerSettings.HighScore7 = gss.score;
                                                nhs = true;
                                                place = 7;
                                            }
                                        }
                                        else
                                        {
                                            PlayerSettingsScript.PlayerSettings.HighScore10 = PlayerSettingsScript.PlayerSettings.HighScore9;
                                            PlayerSettingsScript.PlayerSettings.HighScore9 = PlayerSettingsScript.PlayerSettings.HighScore8;
                                            PlayerSettingsScript.PlayerSettings.HighScore8 = PlayerSettingsScript.PlayerSettings.HighScore7;
                                            PlayerSettingsScript.PlayerSettings.HighScore7 = PlayerSettingsScript.PlayerSettings.HighScore6;
                                            PlayerSettingsScript.PlayerSettings.HighScore6 = gss.score;
                                            nhs = true;
                                            place = 6;
                                        }
                                    }
                                    else
                                    {
                                        PlayerSettingsScript.PlayerSettings.HighScore10 = PlayerSettingsScript.PlayerSettings.HighScore9;
                                        PlayerSettingsScript.PlayerSettings.HighScore9 = PlayerSettingsScript.PlayerSettings.HighScore8;
                                        PlayerSettingsScript.PlayerSettings.HighScore8 = PlayerSettingsScript.PlayerSettings.HighScore7;
                                        PlayerSettingsScript.PlayerSettings.HighScore7 = PlayerSettingsScript.PlayerSettings.HighScore6;
                                        PlayerSettingsScript.PlayerSettings.HighScore6 = PlayerSettingsScript.PlayerSettings.HighScore5;
                                        PlayerSettingsScript.PlayerSettings.HighScore5 = gss.score;
                                        nhs = true;
                                        place = 5;
                                    }
                                }
                                else
                                {
                                    PlayerSettingsScript.PlayerSettings.HighScore10 = PlayerSettingsScript.PlayerSettings.HighScore9;
                                    PlayerSettingsScript.PlayerSettings.HighScore9 = PlayerSettingsScript.PlayerSettings.HighScore8;
                                    PlayerSettingsScript.PlayerSettings.HighScore8 = PlayerSettingsScript.PlayerSettings.HighScore7;
                                    PlayerSettingsScript.PlayerSettings.HighScore7 = PlayerSettingsScript.PlayerSettings.HighScore6;
                                    PlayerSettingsScript.PlayerSettings.HighScore6 = PlayerSettingsScript.PlayerSettings.HighScore5;
                                    PlayerSettingsScript.PlayerSettings.HighScore5 = PlayerSettingsScript.PlayerSettings.HighScore4;
                                    PlayerSettingsScript.PlayerSettings.HighScore4 = gss.score;
                                    nhs = true;
                                    place = 4;
                                }
                            }
                            else
                            {
                                PlayerSettingsScript.PlayerSettings.HighScore10 = PlayerSettingsScript.PlayerSettings.HighScore9;
                                PlayerSettingsScript.PlayerSettings.HighScore9 = PlayerSettingsScript.PlayerSettings.HighScore8;
                                PlayerSettingsScript.PlayerSettings.HighScore8 = PlayerSettingsScript.PlayerSettings.HighScore7;
                                PlayerSettingsScript.PlayerSettings.HighScore7 = PlayerSettingsScript.PlayerSettings.HighScore6;
                                PlayerSettingsScript.PlayerSettings.HighScore6 = PlayerSettingsScript.PlayerSettings.HighScore5;
                                PlayerSettingsScript.PlayerSettings.HighScore5 = PlayerSettingsScript.PlayerSettings.HighScore4;
                                PlayerSettingsScript.PlayerSettings.HighScore4 = PlayerSettingsScript.PlayerSettings.HighScore3;
                                PlayerSettingsScript.PlayerSettings.HighScore3 = gss.score;
                                nhs = true;
                                place = 3;
                            }
                        }
                        else
                        {
                            PlayerSettingsScript.PlayerSettings.HighScore10 = PlayerSettingsScript.PlayerSettings.HighScore9;
                            PlayerSettingsScript.PlayerSettings.HighScore9 = PlayerSettingsScript.PlayerSettings.HighScore8;
                            PlayerSettingsScript.PlayerSettings.HighScore8 = PlayerSettingsScript.PlayerSettings.HighScore7;
                            PlayerSettingsScript.PlayerSettings.HighScore7 = PlayerSettingsScript.PlayerSettings.HighScore6;
                            PlayerSettingsScript.PlayerSettings.HighScore6 = PlayerSettingsScript.PlayerSettings.HighScore5;
                            PlayerSettingsScript.PlayerSettings.HighScore5 = PlayerSettingsScript.PlayerSettings.HighScore4;
                            PlayerSettingsScript.PlayerSettings.HighScore4 = PlayerSettingsScript.PlayerSettings.HighScore3;
                            PlayerSettingsScript.PlayerSettings.HighScore3 = PlayerSettingsScript.PlayerSettings.HighScore2;
                            PlayerSettingsScript.PlayerSettings.HighScore2 = gss.score;
                            nhs = true;
                            place = 2;
                        }
                    }
                    else
                    {
                        PlayerSettingsScript.PlayerSettings.HighScore10 = PlayerSettingsScript.PlayerSettings.HighScore9;
                        PlayerSettingsScript.PlayerSettings.HighScore9 = PlayerSettingsScript.PlayerSettings.HighScore8;
                        PlayerSettingsScript.PlayerSettings.HighScore8 = PlayerSettingsScript.PlayerSettings.HighScore7;
                        PlayerSettingsScript.PlayerSettings.HighScore7 = PlayerSettingsScript.PlayerSettings.HighScore6;
                        PlayerSettingsScript.PlayerSettings.HighScore6 = PlayerSettingsScript.PlayerSettings.HighScore5;
                        PlayerSettingsScript.PlayerSettings.HighScore5 = PlayerSettingsScript.PlayerSettings.HighScore4;
                        PlayerSettingsScript.PlayerSettings.HighScore4 = PlayerSettingsScript.PlayerSettings.HighScore3;
                        PlayerSettingsScript.PlayerSettings.HighScore3 = PlayerSettingsScript.PlayerSettings.HighScore2;
                        PlayerSettingsScript.PlayerSettings.HighScore2 = PlayerSettingsScript.PlayerSettings.HighScore1;
                        PlayerSettingsScript.PlayerSettings.HighScore1 = gss.score;
                        nhs = true;
                        place = 1;
                    }
                    gss.score = 0;
                    PlayerSettingsScript.PlayerSettings.SaveSettings();
                    gss.SaveGame();
                    if(nhs)
                    {
                        PlayerPrefs.SetInt("score", LocalScore);
                        PlayerPrefs.SetInt("place", place);
                        SceneManager.LoadScene("NewHS Page", LoadSceneMode.Single);
                    }
                    else
                    SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
                }
                else
                {
                    //Keep on keeping on
                }
                
                
            }
            
        }
    }
    private void UpdateScore(int scoreToAdd)
    {
        /*PlayerSettingsScript.PlayerSettings.prevGame.score += scoreToAdd;
        gameScore.text = PlayerSettingsScript.PlayerSettings.prevGame.score.ToString();
        if (PlayerSettingsScript.PlayerSettings.prevGame.score >= PlayerSettingsScript.PlayerSettings.HighScores[0])
        {
            PlayerSettingsScript.PlayerSettings.HighScores[0] = PlayerSettingsScript.PlayerSettings.prevGame.score;
            HS_TXT.text = PlayerSettingsScript.PlayerSettings.HighScores[0].ToString();
        }*/
        GameSaveScr gss = GameObject.Find("GameSave").GetComponent<GameSaveScr>();
        //PlayerSettingsScript.PlayerSettings.score += scoreToAdd;
        LocalScore += scoreToAdd;
        //gameScore.text = PlayerSettingsScript.PlayerSettings.score.ToString();
        gameScore.text = LocalScore.ToString();
        //if (PlayerSettingsScript.PlayerSettings.score >= PlayerSettingsScript.PlayerSettings.HighScore1)
        if(LocalScore>=PlayerSettingsScript.PlayerSettings.HighScore1)
        {
            //PlayerSettingsScript.PlayerSettings.HighScore1 = PlayerSettingsScript.PlayerSettings.score;
            //HS_TXT.text = PlayerSettingsScript.PlayerSettings.HighScore1.ToString();
            HS_TXT.text = LocalScore.ToString();
        }
        else
        {
            HS_TXT.text = PlayerSettingsScript.PlayerSettings.HighScore1.ToString();
        }
        //Let's manage the number left here:
        if (scoreToAdd==PowUpSc)
        {
            CS_scriptRef.DeductPU();
        }
        else if(scoreToAdd==CoinSc)
        {
            CS_scriptRef.DeductCoin();
        }
        else
        {
            //ghost or tampered score
            
        }
        //check for win
        if(CS_scriptRef.LeftoverCPU()==0)
        {
            //win, stop game, do whatever we need to do to signal the end of the game, save, then load next level.
            CS_scriptRef.OnWin();
        }
           
    }
}
