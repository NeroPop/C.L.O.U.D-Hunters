using System.Collections;
using System.Collections.Generic;
using Player.Stats;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using HexabodyVR.PlayerController;
using HurricaneVR.Framework.Core.Grabbers;

public class newDeathSystem : MonoBehaviour
{
    private float spawnTime = 0.0f;


    [SerializeField] private Transform[] checkPoints;

    public int _activeCheckPoint;

    [SerializeField] private GameObject _oculusPlayer;

    [SerializeField] private GameObject _openXRPlayer;

    [SerializeField] private HexaBodyPlayer4 Oculus_Hexa;

    [SerializeField] private HexaBodyPlayer4 OpenXR_Hexa;

    private GameObject _player;

    private HexaBodyPlayer4 _pbody;

    public GameObject _deathScreen;

    [SerializeField] private UnityEvent OnDeath;
    [SerializeField] private Transform TeleportPosition;
    public List<HVRHandGrabber> Hands = new List<HVRHandGrabber>();

   // public Transform DeathPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (_oculusPlayer.activeInHierarchy)
        {
            _player = _oculusPlayer;
            _pbody = Oculus_Hexa;
        }
        else if (_openXRPlayer.activeInHierarchy)
        {
            _player = _openXRPlayer;
            _pbody = OpenXR_Hexa;
        }

        //_player = GameObject.FindGameObjectWithTag("Player");
        //_player.GetComponent<TemperatureManager>().newDeathSystem = this.gameObject;
       // _deathScreen = GameObject.Find("Death Box");
    }

    public void setSystem()
    {
      //  _player = GameObject.FindGameObjectWithTag("Player");

    }

    public void Death()
    {
        float lifeTime = Time.realtimeSinceStartup - spawnTime;

        StatsManager.Instance.Stats["deaths"] += 1.0f;
        StatsManager.Instance.Stats["longest life"] = lifeTime;

        _player.transform.position = _deathScreen.transform.position;
        _pbody.transform.position = _deathScreen.transform.position;
        OnDeath.Invoke();
    }

    public void doRespawn()
    {
        this.spawnTime = Time.realtimeSinceStartup;

        TeleportPosition.position = checkPoints[_activeCheckPoint].position;

        //MoveToPosition is based on the ball bottom, so we capture the ball bottom before teleporting
        var start = _pbody.LocoBall.transform.position + Vector3.down * _pbody.LocoCollider.radius;
        _pbody.MoveToPosition(TeleportPosition.position);
        var delta = TeleportPosition.position - start;

        for (int i = 0; i < Hands.Count; i++)
        {
            var hand = Hands[i];
            if (hand && hand.HeldObject && hand.HeldObject.Rigidbody)
            {
                var rb = hand.HeldObject.Rigidbody;
                rb.transform.position += delta;
                rb.position += delta;
            }
        }

      //  _player.transform.position = checkPoints[_activeCheckPoint].position;
      //  _pbody.transform.position = new Vector3(0, 0, 0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
