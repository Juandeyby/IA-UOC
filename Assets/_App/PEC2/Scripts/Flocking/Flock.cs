using System.Collections;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockingManager Manager { get; set; }
    private float _speed;
    private Vector3 _direction;

    private void Start()
    {
        _speed = Random.Range(Manager.minSpeed, Manager.maxSpeed);
        _direction = Random.insideUnitSphere;
        StartCoroutine(Flocking());
    }

    private void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(_direction),
            Manager.rotationSpeed * Time.deltaTime);
        transform.Translate(0, 0, Time.deltaTime * _speed);
    }

    private IEnumerator Flocking()
    {
        while (true)
        {
            var waitTime = Random.Range(0.3f, 0.5f);
            yield return new WaitForSeconds(waitTime);

            FlockingRules();
        }
    }

    private void FlockingRules()
    {
        var cohesion = Vector3.zero;
        var separation = Vector3.zero;
        var alignment = Vector3.zero;
        var queenAttraction = Vector3.zero;
        var numFlocks = 0;
        
        foreach (var flock in Manager.AllFlocks)
        {
            if (flock != this)
            {
                var distance = Vector3.Distance(flock.transform.position, transform.position);
                if (distance <= Manager.neighbourDistance)
                {
                    cohesion += flock.transform.position;
                    alignment += flock._direction;
                    separation -= (transform.position - flock.transform.position) / (distance * distance);
                    numFlocks++;
                }
            }
        }
        
        if (Manager.queen != null)
        {
            var distanceToQueen = Vector3.Distance(Manager.queen.position, transform.position);
            if (distanceToQueen > 0.1f) // Evita fuerzas extremadamente altas
            {
                queenAttraction = (Manager.queen.position - transform.position).normalized * Manager.queenInfluence;
            }
        } 
        
        if (numFlocks > 0)
        {
            alignment /= numFlocks;
            _speed = Mathf.Clamp(alignment.magnitude, Manager.minSpeed, Manager.maxSpeed);
            cohesion = (cohesion / numFlocks - transform.position).normalized * _speed;
            
            _direction = (cohesion + alignment + separation + queenAttraction).normalized * _speed;
        }
    }
}
