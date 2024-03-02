using TMPro;
using UnityEngine;

public class SmoothTurnNumber : MonoBehaviour
{
    private int _step;
    private float _angle;
    private TextMeshPro _tm;

    private void Awake()
    {
        _tm = GetComponent<TextMeshPro>();
    }

    public void OnStepChanged(int step)
    {
        _step = 350 - step;
        // _tm.text = $"{_step}/{_angle:f0}";
        _tm.text = $"{_step}";
    }

    public void OnAngleChanged(float angle, float delta)
    {
        _angle = angle;
        _tm.text = $"{_step}/{_angle:f0}";
    }
}
