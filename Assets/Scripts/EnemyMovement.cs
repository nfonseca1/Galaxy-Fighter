using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float thrustSpeed = 5f;
    [SerializeField] float rotationSpeed = 45f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, 0, 0));
        transform.Translate(new Vector3(0, -thrustSpeed * Time.deltaTime, 0), Space.Self);
    }
}
