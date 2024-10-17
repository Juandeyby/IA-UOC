using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves : MonoBehaviour
{
    public GameObject target;
    public float maxVelocity = 7f;
    public float turnSpeed = 2f;
    public float maxDistance = 3f;
    public bool vehicle = true;
    Vector3 movement = Vector3.zero;
    Quaternion rotation;  
    float freq = 0f;

    void Start()
    {
        Seek();
    }
    
    void Seek()
    {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0f;
        movement = direction.normalized * maxVelocity;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(movement.x, movement.z);
        rotation = Quaternion.AngleAxis(angle, Vector3.up);
    }

    void updateAnimal(float dt)
    {
        if (Mathf.Abs(Vector3.Angle(transform.forward, movement)) > 5) 
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, dt * turnSpeed);
        else
            transform.position += movement * dt;
    }

    void updateVehicle(float dt)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, dt * turnSpeed);
        transform.position += transform.forward.normalized * maxVelocity * dt;
    }
    
    void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < maxDistance) return;

        freq += Time.deltaTime;
        if (freq > 0.5)
        {
            freq -= 0.5f;
            Seek(); 
        }

        if (vehicle)
            updateVehicle(Time.deltaTime);
        else
            updateAnimal(Time.deltaTime);
    }
}
