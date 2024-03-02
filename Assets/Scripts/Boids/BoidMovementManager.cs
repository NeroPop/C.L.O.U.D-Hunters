using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMovementManager : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float maxCubeRadiusToMove = 50.0f;
    [SerializeField] private float sphereCheckRadius = 5.0f;
    [SerializeField] private Transform upwardsDirection;

    private Vector3 boidVelocity;
    private Quaternion lookRotation;

    [Header("Rules")]
    [SerializeField] private float alignmentStrength;
    [SerializeField] private float cohesionStrength;
    [SerializeField] private float seperationStength;

    private Rigidbody rigidbody;

    private Vector3 turn;

    Vector3 alignment;
    Vector3 cohesion;
    Vector3 separation;

    private List<GameObject> boidsNearby;

    private void OnDrawGizmosSelected()
    {
        if(boidsNearby.Count > 0)
        {
            foreach (var boid in boidsNearby)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, boid.transform.position);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        boidVelocity.x = Random.Range(0, 10);
        boidVelocity.y = Random.Range(0, 10);
        boidVelocity.z = Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        boidsNearby = CheckForNearbyBoids();

        alignment = Alignment() * alignmentStrength;
        cohesion = Cohesion() * cohesionStrength;
        separation = Separation() * seperationStength; 

        boidVelocity += cohesion + separation;
        lookRotation = Quaternion.LookRotation(boidVelocity, upwardsDirection.rotation.eulerAngles);
        transform.rotation = lookRotation;
        transform.position += boidVelocity * speed * Time.deltaTime;

        Teleport();
    }

    private List<GameObject> CheckForNearbyBoids()
    {
        List<GameObject> boids = new List<GameObject>();

        Collider[] boidColliders = Physics.OverlapSphere(transform.position, sphereCheckRadius);

        foreach(Collider boidCollider in boidColliders)
        {
            GameObject collisionObject = boidCollider.gameObject;

            if (collisionObject.tag == "boid" && collisionObject != gameObject)
                boids.Add(collisionObject);
        }

        return boids;
    }

    private Vector3 Alignment()
    {
        Vector3 perceivedVelocity = new Vector3();

        if (boidsNearby.Count > 0)
        {
            foreach(var boid in boidsNearby)
            {
                perceivedVelocity += boid.GetComponent<BoidMovementManager>().boidVelocity;
            }

            perceivedVelocity /= boidsNearby.Count;
        }

        return perceivedVelocity / 8;
    }

    private Vector3 Cohesion()
    {
        Vector3 perceivedCenter = new Vector3();

        if (boidsNearby.Count > 0)
        {
            foreach (var boid in boidsNearby)
            {
                perceivedCenter += boid.transform.position;
            }

            perceivedCenter /= boidsNearby.Count;
        }

        return perceivedCenter / 100;
    }

    private Vector3 Separation()
    {
        Vector3 separationVector = new Vector3();

        if(boidsNearby.Count > 0)
        {
            foreach (var boid in boidsNearby)
            {
                if (Vector3.Distance(boid.transform.position, transform.position) < 0.2f)
                {
                    separation -= (boid.transform.position - transform.position);
                }
            }
        }

        return separationVector;
    }

    /// <summary>
    /// Only use until collision avoidance is completed.
    /// </summary>
    private void Teleport()
    {
        if(transform.position.x >= maxCubeRadiusToMove)
        {
            transform.position = new Vector3((0 - maxCubeRadiusToMove), transform.position.y, transform.position.z);
        }
        else if (transform.position.x <= -maxCubeRadiusToMove)
        {
            transform.position = new Vector3(maxCubeRadiusToMove, transform.position.y, transform.position.z);
        }
        else if (transform.position.y >= maxCubeRadiusToMove)
        {
            transform.position = new Vector3(transform.position.x, (0 - maxCubeRadiusToMove), transform.position.z);
        }
        else if (transform.position.y <= -maxCubeRadiusToMove)
        {
            transform.position = new Vector3(transform.position.x, maxCubeRadiusToMove, transform.position.z);
        }
        else if (transform.position.z >= maxCubeRadiusToMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, (0 - maxCubeRadiusToMove));
        }
        else if (transform.position.z <= -maxCubeRadiusToMove)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxCubeRadiusToMove);
        }
    }
}
