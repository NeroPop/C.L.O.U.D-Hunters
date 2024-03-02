using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class respawnManger : MonoBehaviour
{
    public GameObject newDeathSystem;

    public Transform MenuPosition;

    [SerializeField] private GameObject _oculusPlayer;

    [SerializeField] private GameObject _openXRPlayer;

    private GameObject _player;

    [SerializeField]
    private UnityEvent BacktoMenu;

    private void Start()
    {
        if(_oculusPlayer.activeInHierarchy)
        {
            _player = _oculusPlayer;
        }
        else if (_openXRPlayer.activeInHierarchy)
        {
            _player = _openXRPlayer;
        }
    }
    public void Restart()
    {
        newDeathSystem.GetComponent<newDeathSystem>().doRespawn();
    }

    public void MainMenu()
    {
        BacktoMenu.Invoke();
        UnloadAllScenesExcept("0A Master");
    }
    void UnloadAllScenesExcept(string Master)
    {
        int c = SceneManager.sceneCount;
        for (int i = 0; i < c; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            print(scene.name);
            if (scene.name != Master)
            {
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }
}
