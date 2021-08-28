using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float sprintSpeed = 10f;
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;

    private Transform lookRoot;
    private float standHight = 1.6f;
    private float crouchHight = 1f;

    private bool isCrouching;

    private PlayerFootSteps playerFootSteps;

    private float sprintVolume = 1f;
    private float crouchVolume = 0.1f;
    private float walkVolumeMin = 0.2f;
    private float walkVolumeMax = 0.6f;

    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.25f;
    private float crouchStepDistance = 0.5f;

    private PlayerStats playerStats;
    private float sprintValue = 100f;
    public float sprintTrashold = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        lookRoot = transform.GetChild(0);
        playerFootSteps = GetComponentInChildren<PlayerFootSteps>();

        playerStats = GetComponent<PlayerStats>();

    }

    private void Start()
    {
        stepDistanceAndVolumeManager(walkStepDistance, walkVolumeMin, walkVolumeMax);
    }
    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    private void Sprint()
    {
        //if we have stamina to sprint
        if(sprintValue > 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
            {
                playerMovement.speed = sprintSpeed;
                stepDistanceAndVolumeManager(sprintStepDistance, sprintVolume, sprintVolume);
                //Debug.Log("running");
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = moveSpeed;
            stepDistanceAndVolumeManager(walkStepDistance, walkVolumeMin, walkVolumeMax);
            //Debug.Log("walking");
        }

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching && playerMovement.isMoving)
        {
            sprintValue -= sprintTrashold * Time.deltaTime;
            if (sprintValue <= 0f)
            {
                sprintValue = 0f;

                //reset speed and sound
                playerMovement.speed = moveSpeed;
                stepDistanceAndVolumeManager(walkStepDistance, walkVolumeMin, walkVolumeMax);
            }
            playerStats.DisplayStaminaStats(sprintValue);
        }
        else
        { 
            if (sprintValue != 100)
            {
                sprintValue += (sprintTrashold / 2f) * Time.deltaTime;
                playerStats.DisplayStaminaStats(sprintValue);
                if (sprintValue > 100f)
                {
                    sprintValue = 100f; 
                }
            }
        }

    }

    void Crouch()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                isCrouching = false;
                lookRoot.localPosition = new Vector3(0f, standHight, 0f);
                playerMovement.speed = moveSpeed;
                stepDistanceAndVolumeManager(walkStepDistance, walkVolumeMin, walkVolumeMax);
                //Debug.Log("is not Crouching");
            }
            else
            {
                isCrouching = true;
                lookRoot.localPosition = new Vector3(0f, crouchHight, 0f);
                playerMovement.speed = crouchSpeed;
                stepDistanceAndVolumeManager(crouchStepDistance, crouchVolume, crouchVolume);
                //Debug.Log("is Crouching");
            }
        }
    }


    private void stepDistanceAndVolumeManager(float distance, float minVol, float maxVol)
    {
        playerFootSteps.stepDistance = distance;
        playerFootSteps.volumeMin = minVol;
        playerFootSteps.volumeMax = maxVol;
    }

}
