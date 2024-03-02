using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorClamp : MonoBehaviour
{
    public GameObject _tab;
    public float _minX;
    public float _maxX;

    private void FixedUpdate()
    {
        Vector3 tab = _tab.transform.localPosition;
        float xPos = Mathf.Clamp(tab.x, _minX, _maxX);

        this.transform.localPosition = new Vector3(xPos, 0, 0);

        if (this.transform.localPosition.x < _minX)
        {
            this.transform.localPosition = new Vector3(_minX, 0, 0);
        }

        else if (this.transform.localPosition.x > _maxX)
        {
            this.transform.localPosition = new Vector3(_maxX, 0, 0);
        }
    }
}
