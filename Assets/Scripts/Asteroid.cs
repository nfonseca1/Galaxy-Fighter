using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Vector3 velocity;

    private void Update()
    {
        transform.position = transform.position + transform.forward + velocity * Time.deltaTime;
    }

    public void SetVelocity(Vector3 inputVelocity)
    {
        velocity = inputVelocity;
    }
}
