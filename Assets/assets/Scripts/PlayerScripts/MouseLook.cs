using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    //transform components
    [SerializeField]
    private Transform playerRoot;
    [SerializeField]
    private Transform lookAtRoot;

    // bool velues
    [SerializeField]
    private bool invert;
    [SerializeField]
    private bool canUnlock = true;

    //float values
    [SerializeField]
    private float senstivity = 5f;
    [SerializeField]
    private float smoothWeight = 0.4f;
    [SerializeField]
    private float rollAngle = 10f;
    [SerializeField]
    private float rollSpeed = 3f;
    private float currentRollAngle;

    //intiger values
    [SerializeField]
    private int smoothSteps = 10;
    private int lastLookFrame;

    //vecotr2 velues
    [SerializeField]
    private Vector2 defaultLookLimts = new Vector2(-70f, 80f);
    private Vector2 lookAngles;
    private Vector2 currentMouseLook;
    private Vector2 smoothMove;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }

    }

    //for lock and unlock the cursor
    private void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }
    }

    private void LookAround()
    {
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.mouseY), (Input.GetAxis(MouseAxis.mouseX)));

        lookAngles.x += currentMouseLook.x * senstivity * (invert ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * senstivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimts.x, defaultLookLimts.y);

        //currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw(MouseAxis.mouseX) * rollAngle, Time.deltaTime * rollSpeed);

        lookAtRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);

    }

}
