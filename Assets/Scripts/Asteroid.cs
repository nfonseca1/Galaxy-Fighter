using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3(0, 0, 10);
        transform.Rotate(rotation * Time.deltaTime);
    }
}
