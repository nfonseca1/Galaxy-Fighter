using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Vector3 speed;
    float zSpeed = 5f;
    float xSpeed = 0;
    float ySpeed = 0;

    [SerializeField] float minXSpeed = -10;
    [SerializeField] float maxXSpeed = 10;
    [SerializeField] float minYSpeed = -10;
    [SerializeField] float maxYSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        xSpeed = Random.Range(minXSpeed, maxXSpeed);
        ySpeed = Random.Range(minYSpeed, maxYSpeed);
        speed = new Vector3(xSpeed, ySpeed, zSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward + speed * Time.deltaTime;
    }
}
