using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In m/s")] [SerializeField] float xSpeed = 5f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    
    // Update is called once per frame
    void Update()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffsetPerFrame = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffsetPerFrame;
        float clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);

        transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);
    }
}
