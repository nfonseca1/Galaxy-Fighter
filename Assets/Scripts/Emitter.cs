using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    [SerializeField] Asteroid asteroid;
    [SerializeField] float minXSpeed = -10;
    [SerializeField] float maxXSpeed = 10;
    [SerializeField] float minYSpeed = -10;
    [SerializeField] float maxYSpeed = 10;

    Vector3 velocity;
    float zSpeed = 5f;
    float xSpeed = 0;
    float ySpeed = 0;
    Quaternion rotation;
    float time = 0f;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if(time >= 1f)
        {
            time = 0;

            xSpeed = Random.Range(minXSpeed, maxXSpeed);
            ySpeed = Random.Range(minYSpeed, maxYSpeed);
            velocity = new Vector3(xSpeed, ySpeed, zSpeed);

            Asteroid thisAsteroid = Instantiate(asteroid, transform.position, transform.rotation);
            thisAsteroid.SetVelocity(velocity);
            Destroy(thisAsteroid.gameObject, 5f);
        }
    }
}
