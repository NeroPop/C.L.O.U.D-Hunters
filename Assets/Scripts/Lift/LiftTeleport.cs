using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LiftTeleport : MonoBehaviour
{

    [SerializeField] private GameObject _player;

    public GameObject _LiftTele;



    // Start is called before the first frame update
    void Start()
    {
        //_player = GameObject.FindGameObjectWithTag("Player");
        //_player.GetComponent<TemperatureManager>().newDeathSystem = this.gameObject;
        _LiftTele = GameObject.Find("TP_Pos");
    }

    public void setSystem()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Teleport()
    {
        _player.transform.position = _LiftTele.transform.position;
    }

}
