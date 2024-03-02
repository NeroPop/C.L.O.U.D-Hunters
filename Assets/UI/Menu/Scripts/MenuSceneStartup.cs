using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneStartup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayShowMenu());
    }

    IEnumerator DelayShowMenu()
    {
        yield return new WaitForEndOfFrame();
        UI_MenuControls menuScript = FindObjectOfType<UI_MenuControls>();
        if (menuScript)
        {
            menuScript.ShowMainMenu();
            Debug.Log("Found Main menu object in scene.");
        }
        else Debug.LogError("UI Menu script not found in scene!");
    }
}
