using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AboutScript : MonoBehaviour {
    public Button Back_B;
	// Use this for initialization
	void Start () {
        Back_B = Back_B.GetComponent<Button>();
	}
    public void Back_OnClick()
    {
        SceneManager.LoadScene("StartMenu_Andr");
    }

}
