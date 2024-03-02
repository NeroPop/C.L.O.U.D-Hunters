using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneloadtrigger : MonoBehaviour
{
    [SerializeField]
    int Level;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(Level, LoadSceneMode.Additive);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
