using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In m/s")] [SerializeField] float xSpeed = 10f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m/s")] [SerializeField] float ySpeed = 10f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;

    // Update is called once per frame
    void Update()
    {
        MoveOnXAxis();
        MoveOnYAxis();
    }

    private void MoveOnXAxis()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffsetPerFrame = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffsetPerFrame;
        float clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);

        transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);
    }

    private void MoveOnYAxis()
    {
        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffsetPerFrame = yThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffsetPerFrame;
        float clampedYPos = Mathf.Clamp(rawNewYPos, -yRange, yRange);

        transform.localPosition = new Vector3(transform.localPosition.x, clampedYPos, transform.localPosition.z);
    }
}
