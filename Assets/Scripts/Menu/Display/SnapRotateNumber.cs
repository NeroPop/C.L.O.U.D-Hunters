using TMPro;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class SnapRotateNumber : MonoBehaviour
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
            _step = step;
            // _tm.text = $"{_step}/{_angle:f0}";
           // _tm.text = $"{_step}";
            if (step == 5)
            {
                _tm.text = $"10";
            }
            else if (step == 4)
            {
                _tm.text = $"15";
            }
            else if (step == 3)
            {
                _tm.text = $"25";
            }
            else if (step == 2)
            {
                _tm.text = $"30";
            }
            else if (step == 1)
            {
                _tm.text = $"45";
            }
            else if (step == 0)
            {
                _tm.text = $"90";
            }
            else
            {
                _tm.text = $"90";
            }
        }

    }
}
