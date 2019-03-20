using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewHS_Script : MonoBehaviour
{
    public Canvas WinCanvas;
    public Canvas AndrCanvas;

    private Color DefColor = Color.green;
    private void Awake()
    {
#if UNITY_EDITOR_WIN
#if UNITY_ANDROID
        Debug.Log("High Scores Page: Android Edit");
        WinCanvas.gameObject.SetActive(false);
#elif UNITY_WEBGL
        Debug.Log("High Scores Page: WebGL Edit");
        AndrCanvas.gameObject.SetActive(false);
#else
        Debug.Log("High Scores Page: Win Edit");
        AndrCanvas.gameObject.SetActive(false);
#endif //andr-webgl-else this tests the platform settings being used in the Unity window. 
#endif //UNITY_EDITOR_WIN
#if UNITY_ANDROID
        Debug.Log("High Scores Page: Android");
        WinCanvas.gameObject.SetActive(false);
#endif
#if UNITY_STANDALONE_WIN
        Debug.Log("High Scores Page: Win");
        AndrCanvas.gameObject.SetActive(false);
#endif
#if UNITY_WEBGL
        Debug.Log("High Scores Page: WebGL");
        AndrCanvas.gameObject.SetActive(false);
#endif
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
