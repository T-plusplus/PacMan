using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CoinSpawner : MonoBehaviour {

    private string LevelName;
    public Material Material1;
    //in the editor this is what you would set as the object you wan't to change
    private GameObject[] Object = new GameObject[62];
    private int numPowerUpsLeft = 0;
    private int numCoinsLeft;//TODO change to be determined by whether this is level 2 or another level
                                   // Use this for initialization

    void Start() {
        LevelName = SceneManager.GetActiveScene().name;
        if (LevelName == "L1")
        {
            numCoinsLeft = GameObject.FindGameObjectsWithTag("Coin").Length;
            for (int i = 2; i <= 55; i++)
            {
                Object[i - 1] = GameObject.Find("Cube (" + i + ")");
            }
        }
        if (LevelName == "L2")
        {
            numPowerUpsLeft = 4;
            numCoinsLeft = 282;
            for (int i = 1; i <= 62; i++)
            {
                Object[i - 1] = GameObject.Find("Cube (" + i + ")");
            }
        }
        if (LevelName == "L3")
        {
            numCoinsLeft = GameObject.FindGameObjectsWithTag("Coin").Length;
            for (int i = 1; i <= 61; i++)
            {
                Object[i - 1] = GameObject.Find("Cube (" + i + ")");
            }
        }

        //Debug.Log(LevelName);
        if (LevelName != "L2")
        {
            //Debug.Log("Level 3?");
            CreatePrefab(-12.5f, -14);

        }

    }
    /// <summary>
    /// Takes in any valid point that is part of the hallway, and recursively adds coins that are precisely one space apart. Will occupy all halls.
    /// </summary>
    /// <param name="x">X coordinate of the point to start</param>
    /// <param name="z">Y coordinate of the point to start</param>
    private void CreatePrefab(float x, float z)
    {
        GameObject c = (GameObject)Instantiate(Resources.Load("Coin"));
        Vector3 pos = new Vector3(x, 0.5f, z);
        c.transform.position = pos;
        numCoinsLeft++;
        //Debug.Log(c);

        bool l = Valid(pos, Vector3.left);
        bool r = Valid(pos, Vector3.right);
        bool f = Valid(pos, Vector3.forward);
        bool b = Valid(pos, Vector3.back);
        Debug.Log(c.transform.position + " L" + l + " R "+r + " F " + f + " b " + b);
        if (l)
            CreatePrefab(x - 1, z);
        if (r)
            CreatePrefab(x + 1, z);
        if (f)
            CreatePrefab(x, z + 1);
        if (b)
            CreatePrefab(x, z - 1);
    }
    private void CreatePrefabsFromSave()
    {

    }
    private bool Valid(Vector3 start, Vector3 dir)
    {
        //Debug.Log("Pos " + pos + "   Dir " + dir);
        int mask = 1 << LayerMask.NameToLayer("Ignore Raycast");
        return !Physics.Linecast(start, start + dir, ~mask);//,mask);
    }
    public void DeductCoin()
    {
        numCoinsLeft--;
    }
    public void DeductPU()
    {
        numPowerUpsLeft--;
    }
    public int LeftoverCPU()
    {
        return numCoinsLeft + numPowerUpsLeft;
    }
    public void OnWin()
    {
        GameSaveScr gss = GameObject.Find("GameSave").GetComponent<GameSaveScr>();
        gss.SaveGame();
        string curLevel = SceneManager.GetActiveScene().name;
        string nextScene;
        if (curLevel == "L2")
        {
            nextScene = "L3";
            gss.CurLevelName = nextScene;
            gss.SaveGame();
        }
        else if (curLevel == "L3")
        {
            nextScene = "L1";
            gss.CurLevelName = nextScene;
            gss.SaveGame();
        }
        else if (curLevel == "L1")
        {
            nextScene = "L2";
            gss.CurLevelName = nextScene;
            gss.SaveGame();
        }
        else
        {
            Debug.Log("Error getting next Level, Scene name Input was : " + curLevel);
            nextScene = "StartMenu_Andr";
        }
        
        StartCoroutine(FlashWhite(nextScene));

    }

    IEnumerator FlashWhite(string level)
    {
        string curLevel = SceneManager.GetActiveScene().name;
        if (curLevel == "L1")
        {
            for (int i = 2; i <= 55; i++)
            {
                Object[i - 1].GetComponent<MeshRenderer>().material.color = Color.white;
            }
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene(level);
        }
        if (curLevel == "L2")
        {
            for (int i = 1; i <= 62; i++)
            {
                Object[i - 1].GetComponent<MeshRenderer>().material.color = Color.white;
            }
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene(level);
        }
        if (curLevel == "L3")
        {
            for (int i = 1; i <= 61; i++)
            {
                Object[i - 1].GetComponent<MeshRenderer>().material.color = Color.white;
            }
            yield return new WaitForSecondsRealtime(3);
            SceneManager.LoadScene(level);
        }

    }
}
