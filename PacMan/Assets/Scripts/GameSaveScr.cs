using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveScr : MonoBehaviour {
    private readonly string GameFile = "/prevGame.dat";

    private readonly char del = 'Q';

    public static GameSaveScr prevGame;
    public string CurLevelName;
    //private string LeftoverPU_JSON = "";
    //public string LeftoverCoins_JSON = "";
    public int score;
    public int PlayerLivesLeft;

    void Awake()
    {
        Debug.Log(prevGame);
        if (prevGame == null)
        {
            DontDestroyOnLoad(gameObject);
            prevGame = this;
            LoadGame();
        }
        else if (prevGame != this)
        {
            //Debug.Log(SelChoice);
            Destroy(gameObject);
        }
    }
    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + GameFile);
        GameData data = new GameData();
        
        data.score = score;
        data.PlayerLivesLeft = prevGame.PlayerLivesLeft;
        //Level
        data.CurLevelName = SceneManager.GetActiveScene().name;
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + GameFile))
        {
            
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + GameFile, FileMode.Open);
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();
            score = data.score;
            //Level
            CurLevelName = data.CurLevelName;
            PlayerLivesLeft = data.PlayerLivesLeft;
            if (CurLevelName != "") 
                StartGame();
            
        }
        else
        {
            StartGame();
        }
    }
    private void StartGame()
    {
        score = 0;
        CurLevelName = "L1";
        PlayerLivesLeft = 2;
    }
    
    
    [Serializable]
    public class GameData
    {
        public string CurLevelName;
        
        public int score;
        public int PlayerLivesLeft;

    }
 

}

