using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootSteps : MonoBehaviour
{

    private AudioSource footStepSound;

    [SerializeField ]
    private AudioClip[] footStepsClip;

    private CharacterController characterController;

    [HideInInspector]
    public float volumeMin;
    [HideInInspector]
    public float volumeMax;

    private float accumulatedDistance;

    [HideInInspector]
    public float stepDistance;

    // Start is called before the first frame update
    void Awake()
    {
        footStepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootStepSound();
    }

    private void CheckToPlayFootStepSound()
    {
        if (!characterController.isGrounded)
        {
            return; 
        }

        if (characterController.isGrounded && characterController.velocity.sqrMagnitude > 0)
        {
            //accumulated distance is the value how far we can go
            //e.g make a step or sprint, or move while croching
            //until we play the footstep sound

            //stepdistance is the max accumulation distance
            accumulatedDistance += Time.deltaTime;
            if (accumulatedDistance > stepDistance)
            {
                footStepSound.volume = Random.Range(volumeMin, volumeMax);
                footStepSound.clip = footStepsClip[Random.Range(0, footStepsClip.Length)];
                footStepSound.Play();

                accumulatedDistance = 0f;

            }
        }
        else
        {
            accumulatedDistance = 0f;
        }

    }
}
