using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroll_texture : MonoBehaviour
{
    public float scrollSpeedX;
    public float scrollSpeedY;
    private MeshRenderer MeshRenderer;

    private float offset;
    private float movement;

    private float timeInterval = 11.1111f;
    private bool locked = false;
    private int offsetChoice;
    private float scrollTime;

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
        offsetChoice = (Random.Range(1, 9));

        if(offsetChoice == 1)
        {
            offset = 0;
        }
        else if (offsetChoice == 2)
        {
            offset = 0.1111f;
        }
        else if (offsetChoice == 3)
        {
            offset = 0.2222f;
        }
        else if (offsetChoice == 4)
        {
            offset = 0.3333f;
        }
        else if (offsetChoice == 5)
        {
            offset = 0.4444f;
        }
        else if (offsetChoice == 6)
        {
            offset = 0.5555f;
        }
        else if (offsetChoice == 7)
        {
            offset = 0.6666f;
        }
        else if (offsetChoice == 8)
        {
            offset = 0.7777f;
        }
        else if (offsetChoice == 9)
        {
            offset = 0.8888f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(movement >= timeInterval)
        {
            movement = -0.1111f;
            Interval();
        }

        if ((int)Time.realtimeSinceStartup % 0.5 == 0 && !locked)
        {
            MeshRenderer.material.mainTextureOffset = new Vector2((scrollTime * scrollSpeedX) + ((scrollTime * 0.5f * scrollSpeedX)),
                                                                  (scrollTime * scrollSpeedY) + ((scrollTime * 0.5f * scrollSpeedX) + offset));

            scrollTime += UnityEngine.Time.deltaTime;
          //  Debug.Log(Time.realtimeSinceStartup);
        }

        movement += UnityEngine.Time.deltaTime;
    }

    private void Interval()
    {
        if (!locked)
        {
            locked = true;
        }
        else
        {
            locked = false;
        }
    }
}
