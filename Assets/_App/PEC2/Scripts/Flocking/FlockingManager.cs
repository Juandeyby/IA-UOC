using System.Collections;
using System.Collections.Generic;
using Unity.Muse.Behavior;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class FlockingManager : MonoBehaviour
{
    public Transform queen; // Transform de la reina
    public float queenInfluence = 2.0f; // Influencia de la reina en las reglas de flocking

    [SerializeField] private Flock flockPrefab;
    [SerializeField] private int numFlocks = 20;
    [SerializeField] private float limit = 5f;
    [SerializeField] private Flock[] allFlocks;
    public Flock[] AllFlocks => allFlocks;

    [Header("Flocking Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDistance;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;

    private void Start()
    {
        allFlocks = new Flock[numFlocks];
        for (var i = 0; i < numFlocks; ++i)
        {
            // Aleatorizamos la posicion de los agentes
            var pos = this.transform.position + new Vector3(Random.Range(-limit, limit),
                                                                Random.Range(-limit, limit),
                                                                Random.Range(-limit, limit));
            // Aleatorizamos la rotacion de los agentes
            var randomize = new Vector3 (Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1);
            // Instanciamos los agentes
            allFlocks[i] = Instantiate(flockPrefab, pos, Quaternion.LookRotation(randomize), transform);
            // Asignamos el manager a los agentes
            allFlocks[i].Manager = this;
        }
    }
}
