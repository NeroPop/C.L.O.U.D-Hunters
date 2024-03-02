using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawneer : MonoBehaviour
{
    [SerializeField] private GameObject boid;
    [SerializeField] private int spawnAmount;
    [SerializeField] private float spawnRange;

    private Vector3 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        float pos_X;
        float pos_Y;
        float pos_Z;

        for(int i = 0; i < spawnAmount; i++)
        {
            pos_X = Random.Range(-spawnRange / 2, spawnRange / 2);
            pos_Y = Random.Range(-spawnRange / 2, spawnRange / 2);
            pos_Z = Random.Range(-spawnRange / 2, spawnRange / 2);

            Instantiate(boid, new Vector3(pos_X, pos_Y, pos_Z), Quaternion.identity);
        }
    }
}
