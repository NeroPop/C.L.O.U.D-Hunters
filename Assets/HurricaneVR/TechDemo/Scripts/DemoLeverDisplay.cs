using TMPro;
using UnityEngine;

namespace HurricaneVR.TechDemo.Scripts
{
    public class DemoLeverDisplay : MonoBehaviour
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
            _step = 100 - step;
           // _tm.text = $"{_step}/{_angle:f0}";
            _tm.text = $"{_step}";
        }

        public void OnAngleChanged(float angle, float delta)
        {
            _angle = angle;
            _tm.text = $"{_step}/{_angle:f0}";
        }
    }
}
