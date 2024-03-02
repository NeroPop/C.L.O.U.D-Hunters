using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class addativeload : MonoBehaviour
{
    [SerializeField]
    int Level;

    [SerializeField]
    [Tooltip("True = Load/ False = Unload")]
    public bool loadUnloadToggle;

    // Start is called before the first frame update
    void OnEnable()
    {
        if (loadUnloadToggle == true)
            SceneManager.LoadScene(Level, LoadSceneMode.Additive);
        else if (loadUnloadToggle == false)
            SceneManager.UnloadSceneAsync(Level);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
