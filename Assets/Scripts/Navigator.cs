using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(0, 0, 10);
        transform.Translate(movement * Time.deltaTime, Space.World);
    }
}
