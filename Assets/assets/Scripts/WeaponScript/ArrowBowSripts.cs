using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBowSripts : MonoBehaviour
{

    private Rigidbody myBody;

    public float speed = 30f;

    public float deactivatorTimer = 3f;

    public float damage = 15f;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameObject", deactivatorTimer);
    }

    private void DeactivateGameObject()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        //AFTER WE TOUCH THE ENEMY THEN deactivate the spear or arrow
        if (other.tag == Tags.EnemyTag)
        {
            other.GetComponent<HealthScript>().ApplyDamage(damage);

            gameObject.SetActive(false);
        }
    }

    public void Launch(Camera mainCamera)
    {
        myBody.velocity = mainCamera.transform.forward * speed;

        transform.LookAt(transform.position + myBody.velocity);
    }

}
