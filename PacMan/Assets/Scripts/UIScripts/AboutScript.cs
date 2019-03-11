using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AboutScript : MonoBehaviour {
    private Button Back_B;

    public Canvas WinCanvas;
    public Canvas AndrCanvas;

    private readonly string Back_name = "Back";

    private void Awake()
    {
        //As new devices are added, remember to look at what's needed for UI, and deactivate those panels and bind the relevant pieces. Also as new controls 
        //are added, add those as well.
#if UNITY_EDITOR_WIN
#if UNITY_ANDROID
        Debug.Log("About Page: Android Edit");
        WinCanvas.gameObject.SetActive(false);
#elif UNITY_WEBGL
        Debug.Log("About Page: WebGL Edit");
        AndrCanvas.gameObject.SetActive(false);
#else
        Debug.Log("About Page: Win Edit");
        AndrCanvas.gameObject.SetActive(false);
#endif //andr-webgl-else this tests the platform settings being used in the Unity window. 
#endif //UNITY_EDITOR_WIN
#if UNITY_ANDROID
        Debug.Log("About Page: Android");
        WinCanvas.gameObject.SetActive(false);
#endif
#if UNITY_STANDALONE_WIN
        Debug.Log("About Page: Win");
        AndrCanvas.gameObject.SetActive(false);
#endif
#if UNITY_WEBGL
        Debug.Log("About Page: WebGL");
        AndrCanvas.gameObject.SetActive(false);
#endif
    }
    void Start () {
        Back_B = GameObject.Find(Back_name).GetComponent<Button>();
        Back_B.onClick.AddListener(Back_OnClick);
	}
    public void Back_OnClick()
    {
        SceneManager.LoadScene("StartMenu");
    }

}
