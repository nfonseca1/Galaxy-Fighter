using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In m/s")] [SerializeField] float xSpeed = 15f;
    [Tooltip("In m")] [SerializeField] float xRange = 5f;
    [Tooltip("In m/s")] [SerializeField] float ySpeed = 15f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;
    [SerializeField] GameObject[] guns;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -5f;
    [SerializeField] float controlRollFactor = -30f;

    ParticleSystem leftGun, rightGun;
    float xThrow, yThrow;
    bool isControlDisabled = false;
    enum Blaster {LeftBlaster, RightBlaster }
    Blaster currentBlaster = Blaster.LeftBlaster;
    float blasterTime = 0f;

    private void Start()
    {
        leftGun = guns[0].GetComponent<ParticleSystem>();

        rightGun = guns[1].GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isControlDisabled)
        {
            MoveOnXAxis();
            MoveOnYAxis();
            ProcessRotation();
        }
        ProcessFiring();
    }

    void OnPlayerDeath()
    {
        isControlDisabled = true;
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        
        float yaw = transform.localPosition.x * positionYawFactor;

        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void MoveOnXAxis()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffsetPerFrame = xThrow * xSpeed * Time.deltaTime;
        float rawNewXPos = transform.localPosition.x + xOffsetPerFrame;
        float clampedXPos = Mathf.Clamp(rawNewXPos, -xRange, xRange);

        transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z);
    }

    private void MoveOnYAxis()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffsetPerFrame = yThrow * ySpeed * Time.deltaTime;
        float rawNewYPos = transform.localPosition.y + yOffsetPerFrame;
        float clampedYPos = Mathf.Clamp(rawNewYPos, -yRange, yRange);

        transform.localPosition = new Vector3(transform.localPosition.x, clampedYPos, transform.localPosition.z);
    }

    private void ProcessFiring()
    {
        blasterTime += Time.deltaTime;
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            if(blasterTime >= .1f)
            {
                if(currentBlaster == Blaster.LeftBlaster)
                {
                    leftGun.Play();
                    
                    currentBlaster = Blaster.RightBlaster;
                    blasterTime = 0;
                }
                else
                {
                    rightGun.Play();
                    currentBlaster = Blaster.LeftBlaster;
                    blasterTime = 0;
                }
            }
        }
    }
}
