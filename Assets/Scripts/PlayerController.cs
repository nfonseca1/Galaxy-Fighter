using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] Image crosshair;
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
        Vector3 hitpoint = ProcessAiming();
        ProcessFiring(hitpoint);
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

    private void ProcessFiring(Vector3 hitpoint)
    {
        blasterTime += Time.deltaTime;
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            if(blasterTime >= .1f)
            {
                if(currentBlaster == Blaster.LeftBlaster)
                {
                    leftGun.transform.LookAt(hitpoint);
                    leftGun.Play();
                    currentBlaster = Blaster.RightBlaster;
                    blasterTime = 0;
                }
                else
                {
                    rightGun.transform.LookAt(hitpoint);
                    rightGun.Play();
                    currentBlaster = Blaster.LeftBlaster;
                    blasterTime = 0;
                }
            }
        }
    }

    private Vector3 ProcessAiming()
    {
        Vector3 crosshairPosition = Input.mousePosition;
        crosshair.transform.position = crosshairPosition;
        
        Camera mainCamera = Camera.main;
        Vector3 screenPoint = mainCamera.ScreenToWorldPoint(new Vector3(
            crosshairPosition.x, 
            crosshairPosition.y, 
            1
            ));

        Vector3 rayDirection = screenPoint - mainCamera.gameObject.transform.position;
        Ray aimRay = new Ray(screenPoint, rayDirection);
        RaycastHit hitInfo;
        if(Physics.Raycast(aimRay, out hitInfo, 10000))
        {
            return hitInfo.collider.gameObject.transform.position;
        }

        return new Vector3(transform.position.x, transform.position.y, transform.position.z + 1000000);
    }
}
