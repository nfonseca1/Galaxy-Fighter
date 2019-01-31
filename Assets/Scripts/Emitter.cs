using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : MonoBehaviour
{
    [SerializeField] Transform asteroid;
    Quaternion rotation;
    float time = 0f;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        
        if(time >= 1f)
        {
            time = 0;
            
            Instantiate(asteroid, transform.position, transform.rotation);
        }
    }
}
