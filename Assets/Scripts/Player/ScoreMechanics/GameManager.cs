using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } // Using static instance to store a reference to this game manager

   // public GameOverScreen GameOverScreen; //Reference Game over screen
   // public GameWonScreen GameWonScreen; //Reference Game won screen 

    public UnityEvent OnWin;  //Using Unity events to reference their conditionals
    public UnityEvent OnLose;

    /// <summary>
    /// Prevents multiple Game Managers from being active at one time
    /// </summary>
    private void Awake()
    {
        Time.timeScale = 1.0f;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void WinGame(float timeTaken) //Invoke event
    {
        Debug.Log("You have won!");
        Debug.Log("Time Taken " + timeTaken);
        //GameWonScreen.Setup(timeTaken);
        OnWin?.Invoke();
        Time.timeScale = 0f;
    }

    public void LoseGame() //Invoke event
    {
        
        Debug.Log("You Crashed!");
        //GameOverScreen.Setup();
        OnLose?.Invoke();
        Time.timeScale = 0f;
       
    }
     /*
    public void WinGameTest() //Invoke event
    {
        Debug.Log("You have won!");
        
        OnWin?.Invoke();
    }
  */
     //function which reloads the scene using scenemanagent namespace
    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
