using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;

public class PlayerSettingsScript : MonoBehaviour {

    public static PlayerSettingsScript PlayerSettings;

    public enum InputChoices { dpad = 1, swipe, accel };
    public InputChoices SelChoice;
    public int score;
    
    public int HighScore1;
    public int HighScore2;
    public int HighScore3;
    public int HighScore4;
    public int HighScore5;
    public int HighScore6;
    public int HighScore7;
    public int HighScore8;
    public int HighScore9;
    public int HighScore10;
    //public GameData prevGame;
    private readonly String PlaySetFile = "/playerSettings.dat";
    private readonly String GameFile = "/prevGame.dat";
    private readonly string CoinTag = "Coin";
    private readonly string PUTag = "PowerUp";
    private readonly string PlayerTag = "Player";
    private readonly string GhostTag = "Ghost";
    // Use this for initialization
    void Awake () {
        //Debug.Log(PlayerSettings);
		if(PlayerSettings==null)
        {
            DontDestroyOnLoad(gameObject);
            PlayerSettings = this;
            //pull from internal using persistence
            LoadSettings();
            LoadSettings();
        }
        else if(PlayerSettings!=this)
        {
            //Debug.Log(SelChoice);
            Destroy(gameObject);
        }
	}

    public void SaveSettings()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + PlaySetFile);
        P_Settings data = new P_Settings();
        data.SelChoice = SelChoice;
        data.score = score;
        data.HighScore1 = HighScore1;
        data.HighScore2 = HighScore2;
        data.HighScore3 = HighScore3;
        data.HighScore4 = HighScore4;
        data.HighScore5 = HighScore5;
        data.HighScore6 = HighScore6;
        data.HighScore7 = HighScore7;
        data.HighScore8 = HighScore8;
        data.HighScore9 = HighScore9;
        data.HighScore10 = HighScore10;
        bf.Serialize(file, data);
        file.Close();
    }
    private void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + PlaySetFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + PlaySetFile, FileMode.Open);
            P_Settings data = (P_Settings)bf.Deserialize(file);
            file.Close();
            HighScore1= data.HighScore1;
            HighScore2 = data.HighScore2;
            HighScore3 = data.HighScore3;
            HighScore4 = data.HighScore4;
            HighScore5 = data.HighScore5;
            HighScore6 = data.HighScore6;
            HighScore7 = data.HighScore7;
            HighScore8 = data.HighScore8;
            HighScore9 = data.HighScore9;
            HighScore10 = data.HighScore10;

            SelChoice = data.SelChoice;
            score=data.score;
        }
        else
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + PlaySetFile);
            P_Settings data = new P_Settings();
            data.SelChoice = InputChoices.dpad;
            data.score = 0;
            data.HighScore1 = 0;
            data.HighScore2 = 0;
            data.HighScore3 = 0;
            data.HighScore4 = 0;
            data.HighScore5 = 0;
            data.HighScore6 = 0;
            data.HighScore7 = 0;
            data.HighScore8 = 0;
            data.HighScore9 = 0;
            data.HighScore10 = 0;
            bf.Serialize(file, data);
            file.Close();
        }
    }

    [Serializable]
    class P_Settings
    {
        public InputChoices SelChoice;
        public int score;
        public int HighScore1;
        public int HighScore2;
        public int HighScore3;
        public int HighScore4;
        public int HighScore5;
        public int HighScore6;
        public int HighScore7;
        public int HighScore8;
        public int HighScore9;
        public int HighScore10;

    }

}
