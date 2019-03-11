using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CoinSpawner : MonoBehaviour
{
    private string LevelName;
public Material Material1;
//in the editor this is what you would set as the object you wan't to change
private GameObject[] Object = new GameObject[62];
private int numPowerUpsLeft;
private int numCoinsLeft;

private readonly Vector3[] L1pu = { new Vector3(12.5f, .5f, 13), new Vector3(-12.5f, .5f, 13), new Vector3(12.5f, .5f, -7), new Vector3(-12.5f, .5f, -7) };
private readonly Vector3[] L2pu = { new Vector3(12.5f, .5f, 13), new Vector3(-12.5f, .5f, 13), new Vector3(12.5f, .5f, -13), new Vector3(-12.5f, .5f, -13) };
private readonly Vector3[] L3pu = { new Vector3(12.5f, .5f, 13), new Vector3(-12.5f, .5f, 13) };
private readonly string PUpath = "Prefabs/PickUps/";

void Start()
{

    LevelName = SceneManager.GetActiveScene().name;
    numCoinsLeft = GameObject.FindGameObjectsWithTag("Coin").Length;
    numPowerUpsLeft = GameObject.FindGameObjectsWithTag("PowerUp").Length;
    Object = GameObject.FindGameObjectsWithTag("Wall");

    //Debug.Log(LevelName);
    if (LevelName == "L1")
        CreatePrefab(-12.5f, -14, L1pu);
    else if (LevelName == "L2")
        CreatePrefab(-12.5f, -14, L2pu);
    else if (LevelName == "L3")
        CreatePrefab(-12.5f, -14, L3pu);
}
/// <summary>
/// Takes in any valid point that is part of the hallway, and recursively adds coins that are precisely one space apart. Will occupy all halls.
/// </summary>
/// <param name="x">X coordinate of the point to start</param>
/// <param name="z">Y coordinate of the point to start</param>
private void CreatePrefab(float x, float z, Vector3[] powerUps)
{
    bool isPowerUpLoc = false;
    if (powerUps != null)
        for (int i = 0; i < powerUps.Length; i++)
        {
            if (powerUps[i].x == x && powerUps[i].z == z)
            {
                isPowerUpLoc = true;
                break;
            }
        }
    GameObject c = (isPowerUpLoc) ? ((GameObject)Instantiate(Resources.Load(PUpath + "PowerUp"))) : ((GameObject)Instantiate(Resources.Load(PUpath + "Coin")));
    Vector3 pos = new Vector3(x, 0.5f, z);
    c.transform.position = pos;
    numCoinsLeft++;
    //Debug.Log(c);

    bool l = Valid(pos, Vector3.left);
    bool r = Valid(pos, Vector3.right);
    bool f = Valid(pos, Vector3.forward);
    bool b = Valid(pos, Vector3.back);
        //Debug.Log(c.transform.position + " L" + l + " R "+r + " F " + f + " b " + b);
//#if UNITY_EDITOR_WIN
//stop. we're creating one prefab to test maze advancement. Otherwise, picking up 300 pickups and not dying is going to be
//time consuming and irritating.
//#else
        if (l)
        CreatePrefab(x - 1, z, powerUps);
    if (r)
        CreatePrefab(x + 1, z, powerUps);
    if (f)
        CreatePrefab(x, z + 1, powerUps);
    if (b)
        CreatePrefab(x, z - 1, powerUps);
//#endif
}
private void CreatePrefabsFromSave()
{

}
private bool Valid(Vector3 start, Vector3 dir)
{
    //Debug.Log("Pos " + pos + "   Dir " + dir);
    int mask = 1 << LayerMask.NameToLayer("Ignore Raycast");
    return !Physics.Linecast(start, start + dir, ~mask);
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
        nextScene = "StartMenu";
    }
    StartCoroutine(FlashWhite(nextScene));

}

private IEnumerator FlashWhite(string level)
{
    foreach (GameObject o in Object)
    {
        o.GetComponent<MeshRenderer>().material.color = Color.white;
    }
    yield return new WaitForSecondsRealtime(3);
    SceneManager.LoadScene(level);
}
}
