// UI_MenuControls.cs
// Starting point for a UI control system.
// This version borrows some code from from the StartUI.cs script that comes with the 3D lite kit.
// By Paul Hedley: 20/02/2021
// Contains quit function that works in build and editor.
// Basic features to disable / enable main menu and quit confirmation panels ( coroutines running LeanTween commands would replace these).

// * 01/3/2021 replaced enable/disable ui commands with simple Lean Tween functions.

// * 07/3/2021 * Added ".setIgnoreTimeScale(true)" to all UI tweens so they ignore any 
//             "Pausing or time dilation from changing Time.TimeScale.

//             * Forcing UIControls to be a singleton so it can persist through levels, while still allowing
//             us to have a dummy UI prefab in scenes in the editor.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

#if UNITY_EDITOR
using UnityEditor;
#endif
public enum SceneIndexes
{
    Intro = 0,
    Menu = 1,
    Tutorial = 2,
    LevelZero = 3,
    FirstLevel = 4
}

public class UI_MenuControls : MonoBehaviour
{
    #region Public references that need to be assigned on the UIControls prefab.
    [Header("UI Controls Main Settings")]
    public float panelTweenTime = 0.5f;
    public float screenFadeTime = 1.5f;
    public GameEvent onPauseEvent;
    public GameEvent onUnpauseEvent;
    [Space]
    public CanvasGroup UI_loadingCanvas;
    public CanvasGroup UI_gameOverCanvas;
    [Space]
    [Header("Main Menu UI Elements")]
    public GameObject UI_mainMenuButtonPanel;
    [Space]
    public GameObject UI_newGamePanel;
    public GameObject UI_loadGamePanel;
    public GameObject UI_creditsPanel;
    public GameObject UI_mainQuitPanel;
    [Space]
    [Header("Pause Menu UI Elements")]
    public GameObject UI_pauseMenuButtonPanel;
    public GameObject UI_pauseQuitPanel;
    [Space]
    [Header("Settings Menu UI Elements.")]
    public GameObject UI_optionsButtonPanel;
    public GameObject UI_optionsControlPanel;
    [Space]
    public GameObject UI_gameplayPanel;
    public GameObject UI_graphicsPanel;
    public GameObject UI_audioPanel;
    #endregion

    #region Private properties
    static UI_MenuControls instance;
    protected bool isPaused;
    protected PlayableDirector[] m_Directors;

    GameObject currentPanel;
    GameObject currentSettingsPanel;
    private bool dialogueInProgress = false;
    #endregion

    //Initialize the UI here as soon as the scene loads.
    private void Awake()
    {
        #region Singleton Pattern
        if (instance !=null && instance!=this )
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        #endregion

        DontDestroyOnLoad(gameObject);
        HideAllPanels();
        currentSettingsPanel = UI_gameplayPanel;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    //Using update to check for pause key being pressed
    private void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex > (int)SceneIndexes.Menu)
        {
            if(!isPaused && !dialogueInProgress && (Input.GetKeyUp(KeyCode.Pause)|| Input.GetKeyUp(KeyCode.Escape)))
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        if(onPauseEvent!=null)
        {
            onPauseEvent.Raise();
        }

        isPaused = true;
        Time.timeScale = 0;
        SwitchToPanel(UI_pauseMenuButtonPanel);
    }



    #region Public functions
    // functions called from this prefabs game event listeners
    public void OnDialogueStart()
    {
        dialogueInProgress = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void OnDialogueEnd()
    {
        dialogueInProgress = true;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    public void UnPauseGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        HidePanelAnimation(UI_pauseMenuButtonPanel);
        currentPanel = null;

        if (onUnpauseEvent != null)
        {
            onUnpauseEvent.Raise();
        }

    }

    // This function is here purely so we can get the UI controls to open
    //when we want them to in the main menu scene.
    public void ShowMainMenu()
    {
        //Invoke("DelayShowMenu", 0.05f);
        Debug.Log("Showing main menu for " + instance.name);
        currentPanel = null;
        Time.timeScale = 1;
        SwitchToPanel(UI_mainMenuButtonPanel);
    }

    /*
    void DelayShowMenu()
    {
        Debug.Log("Showing main menu for " + instance.name);
        currentPanel = null;
        Time.timeScale = 1;
        SwitchToPanel(UI_mainMenuButtonPanel);
    }
    */
    //This is the main function for displaying menu panels.
    //Can be called externally to make the UI change the active panel
    //after hiding the previously active panel (if there was one).
    public void SwitchToPanel(GameObject newPanel)
    {
        if (currentPanel != null)
        {
            HidePanelAnimation(currentPanel);
        }
        currentPanel = newPanel;
        ShowPanelAnimation(currentPanel, panelTweenTime);
    }

    // The settings panel has several subsections.
    // This script tracks the currently active settings panel (gameplay, graphics, audio etc.)
    // allowing us to control it seperately from the main menu controls.
    // This function is called from buttons on the options panel of the UI Prefab.
    public void SwitchSettingsPanel(GameObject newPanel)
    {
        if (newPanel != currentSettingsPanel)
        {
            if (currentSettingsPanel != null)
            {
                HidePanelAnimation(currentSettingsPanel);
                currentSettingsPanel = newPanel;
                ShowPanelAnimation(currentSettingsPanel, panelTweenTime);
            }
            else
            {
                currentSettingsPanel = newPanel;
                ShowPanelAnimation(currentSettingsPanel, 0);
            }
        }
    }
    #endregion

    #region Button Press Functions
    public void OnPressQuitButton()
    {
        HidePanelAnimation(UI_mainMenuButtonPanel);
        ShowPanelAnimation(UI_mainQuitPanel);
    }
    public void OnPressQuitConfirm()
    {
        
        ExitApplication();
    }
    public void OnPressedMainQuitCancel()
    {
        HidePanelAnimation(UI_mainQuitPanel);
        ShowPanelAnimation(UI_mainMenuButtonPanel);
    }
    public void OnPressedPauseQuitCancel()
    {
        HidePanelAnimation(UI_pauseQuitPanel);
        ShowPanelAnimation(UI_pauseMenuButtonPanel);
    }
    public void OnPressedStartTutorial() 
    {
        HidePanelAnimation(currentPanel);
        LoadLevel(SceneIndexes.Tutorial);
    }
    public void OnPressedStartNewGame() 
    {
        HidePanelAnimation(currentPanel);
        LoadLevel(SceneIndexes.FirstLevel);
    }
    public void OnPressedStartLevelZero() 
    {
        HidePanelAnimation(currentPanel);
        LoadLevel(SceneIndexes.LevelZero);
    }   

    public void OnPressedQuitToMainMenu()
    {
        Time.timeScale = 1;
        isPaused = false;
        HidePanelAnimation(currentPanel);
        LoadMenuScene();
        currentPanel = null;
        SwitchToPanel(UI_mainMenuButtonPanel);
    }

    public void OnHideLoadGameList()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndexes.Menu)
        {
            SwitchToPanel(UI_mainMenuButtonPanel);
        }
        else
        {
            SwitchToPanel(UI_pauseMenuButtonPanel);
        }
    }

    public void OnShowOptionsMenu()
    {
        SwitchToPanel(UI_optionsButtonPanel);
        ShowPanelAnimation(UI_optionsControlPanel,panelTweenTime);
        ShowPanelAnimation(currentSettingsPanel);
    }
    public void OnHideOptionsMenu()
    {
        HidePanelAnimation(UI_optionsControlPanel);
        HidePanelAnimation(currentSettingsPanel);
        if (SceneManager.GetActiveScene().buildIndex==(int)SceneIndexes.Menu)
        {
            SwitchToPanel(UI_mainMenuButtonPanel);
        }
        else
        {
            SwitchToPanel(UI_pauseMenuButtonPanel);
        }
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene((int)SceneIndexes.Menu);
    }

    public void LoadIntroScene()
    {
        HidePanelAnimation(UI_mainMenuButtonPanel);
        currentPanel = null;
        SceneManager.LoadScene((int)SceneIndexes.Intro);
    }

    #endregion

    #region Functions for showing and hiding specific full screen canvases using Alpha
    public void ShowGameOverScreen()
    {
        UI_gameOverCanvas.gameObject.SetActive(true);
        LeanTween.alphaCanvas(UI_gameOverCanvas, 1, screenFadeTime)
            .setIgnoreTimeScale(true);
    }

    public void HideGameOverScreen()
    {
        LeanTween.alphaCanvas(UI_gameOverCanvas, 0, screenFadeTime)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => UI_gameOverCanvas.gameObject.SetActive(false));
    }

    public void ShowLoadingScreen()
    {
        dialogueInProgress = true;
        UI_loadingCanvas.gameObject.SetActive(true);
        LeanTween.alphaCanvas(UI_loadingCanvas, 1, screenFadeTime)
            .setIgnoreTimeScale(true);
    }

    public void HideLoadingScreen()
    {
        LeanTween.alphaCanvas(UI_loadingCanvas, 0, screenFadeTime)
            .setIgnoreTimeScale(true)
            .setOnComplete(() => {  UI_loadingCanvas.gameObject.SetActive(false);
                dialogueInProgress = false;
             });
    }
    #endregion


    #region Private functions
    // This function will control how any panel will animate when coming into view
    // when the SwitchToPanel function is called.
    void ShowPanelAnimation(GameObject showPanel, float delay=0)
    {
        LeanTween.scale(showPanel,Vector3.one,panelTweenTime)
                        .setDelay(delay)
                        .setEase(LeanTweenType.easeOutCubic)
                        .setIgnoreTimeScale(true);
    }

    // This function will control how any panel will animate when leaving the view
    // when the SwitchToPanel function is called.
    void HidePanelAnimation(GameObject hidePanel,float delay = 0)
    {
        LeanTween.scale(hidePanel,
                        Vector3.zero,
                        panelTweenTime)
                        .setDelay(delay)
                        .setEase(LeanTweenType.easeInCubic)
                        .setIgnoreTimeScale(true);
    }

    //Instantly set all the ui panels scale to zero so they are ready to animate.
    void HideAllPanels()
    {
        //Set alpha of game over canvas and loading canvas to 0
        LeanTween.alphaCanvas(UI_gameOverCanvas, 0, 0)
            .setIgnoreTimeScale(true);

        LeanTween.alphaCanvas(UI_loadingCanvas, 0, 0)
            .setIgnoreTimeScale(true);

        // Set the scale of the main menu ui elements
        UI_mainMenuButtonPanel.transform.localScale = Vector3.zero;

        UI_newGamePanel.transform.localScale = Vector3.zero; 
        UI_loadGamePanel.transform.localScale = Vector3.zero;
        UI_creditsPanel.transform.localScale = Vector3.zero;
        UI_mainQuitPanel.transform.localScale = Vector3.zero;

        // Set the scale of the pause menu ui elements
        UI_pauseMenuButtonPanel.transform.localScale = Vector3.zero;
        UI_pauseQuitPanel.transform.localScale = Vector3.zero;

        // Set the scale of the options ui elements
        UI_optionsControlPanel.transform.localScale = Vector3.zero;
        UI_optionsButtonPanel.transform.localScale = Vector3.zero;

        UI_gameplayPanel.transform.localScale = Vector3.zero;
        UI_graphicsPanel.transform.localScale = Vector3.zero;
        UI_audioPanel.transform.localScale = Vector3.zero;
    }

    void LoadLevel(SceneIndexes newSceneIndex)
    {
        SceneManager.LoadScene((int)newSceneIndex);
    }

    // Quit the game or stop the editor from playing if in edit mode.
    void ExitApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    #endregion
}