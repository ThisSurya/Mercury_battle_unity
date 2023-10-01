using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor.EditorTools;

public class PlayerControll : MonoBehaviour
{
    [Header("General Setup")]
    [Tooltip("Controll speed move by x, y")]
    [SerializeField] float controlspeed = 10f;
    [Tooltip("Range player can move in x direction")]
    [SerializeField] float xRange = 10f;
    [Tooltip("Range player can move in y direction")]
    [SerializeField] float yRange = 7f;

    [Header("Rotation Effect while tilting")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = -2f;
    [SerializeField] float controllerPitchFactor = -10f;
    [SerializeField] float controllerRollFactor = 2f;

    [Header("Place laser object here")]
    [SerializeField] GameObject[] lasers;

    float xThrow, yThrow;

    // Update is called once per frame
    void Update()
    {
        ProcessTransform();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessRotation()
    {
        float pitchDueLocation = transform.localPosition.y * positionPitchFactor;
        float pitchDueControlThrow = yThrow * controllerPitchFactor;

        float yawDueLocation = transform.localPosition.x * positionPitchFactor;
        
        float rollDueControlThrow = xThrow * controllerRollFactor;

        float pitch = pitchDueLocation + pitchDueControlThrow;
        float yaw = yawDueLocation + 180f;
        float roll = rollDueControlThrow;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTransform()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xoffset = -xThrow * Time.deltaTime * controlspeed;
        // New position of Ship
        float rawXPos = transform.localPosition.x + xoffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yoffset = yThrow * Time.deltaTime * controlspeed;
        // New position of Ship
        float rawYPos = transform.localPosition.y + yoffset;
        // Limit the movement from the camera
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if(Input.GetButton("Fire1"))
        {
            SetLaser(true);
        }
        else
        {
            SetLaser(false);
        }
    }


    private void SetLaser(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
