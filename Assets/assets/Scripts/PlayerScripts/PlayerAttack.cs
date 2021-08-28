using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private WeaponManager weaponManager;

    public float fireRate = 15f;
    public float damage = 20f;

    private float nextTimeFire;

    private Animator zoomCameraAnim;
    private bool zoomed;
    private bool isAiming;

    private Camera mainCamera;

    private GameObject crosshair;

    [SerializeField]
    private GameObject arrowPrefeb;
    [SerializeField]
    private GameObject spearPrefeb;
    [SerializeField]
    private Transform arrowBowSponPosition;

    // Start is called before the first frame update
    void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();

        zoomCameraAnim = transform.Find(Tags.LookRoot).transform.Find(Tags.ZoomCamera).GetComponent<Animator>();

        crosshair = GameObject.FindWithTag(Tags.Crosshair);

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShot();
        ZoomInAndOut();
    }

    private void WeaponShot()
    {
        if (weaponManager.GetCurrentSelectedWeapon().fireType == WeaponFireType.Multiple)
        {
            // if we press and hold left mouseClick And
            //if time is greater then nextTimeToFire
            if (Input.GetMouseButton(0) && Time.time > nextTimeFire)
            {
                nextTimeFire = Time.time + 1 / fireRate;

                weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired();

            }
        }//if we have a weapon which shoots single at a time
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                //handle Axe
                if (weaponManager.GetCurrentSelectedWeapon().tag == Tags.AxeTag)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                //handle shoot
                //bullet
                else if (weaponManager.GetCurrentSelectedWeapon().BulletType == WeaponBulletType.Bullet)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                    BulletFired();

                }
                else
                {
                    //arrow and spear
                    if (isAiming)
                    {
                        weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                        if (weaponManager.GetCurrentSelectedWeapon().BulletType == WeaponBulletType.Arrow)
                        {
                            ThrowArrowOrSpear(true);
                            //throw Arrow
                        }
                        else if (weaponManager.GetCurrentSelectedWeapon().BulletType == WeaponBulletType.Spear)
                        {
                            ThrowArrowOrSpear(false);
                            //throw spear
                        }
                    }
                }
            }
        }
    }

    void ZoomInAndOut()
    {
        //we are going to aim with our camera on the weapon
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.Aim)
        {
            //if we press and hold right mouse button
            if (Input.GetMouseButtonDown(1))
            {
                zoomCameraAnim.Play(AnimationTag.ZoomInAnim);

                crosshair.SetActive(false);
            }
            //when we release the right  mouse button
            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnim.Play(AnimationTag.ZoomOutAnim);

                crosshair.SetActive(true);
            }
        }
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SelfAim)
        {
            if (Input.GetMouseButtonDown(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(true);
                isAiming = true;
            }
            if (Input.GetMouseButtonUp(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(false);
                isAiming = false;
            }
        }
    }

    void ThrowArrowOrSpear(bool throwArrow)
    {
        if (throwArrow)
        {
            GameObject arrow = Instantiate(arrowPrefeb);
            arrow.transform.position = arrowBowSponPosition.position;

            arrow.GetComponent<ArrowBowSripts>().Launch(mainCamera);

        }
        else
        {
            GameObject spear = Instantiate(spearPrefeb);
            spear.transform.position = arrowBowSponPosition.position;

            spear.GetComponent<ArrowBowSripts>().Launch(mainCamera);
        } 
    }

    private void BulletFired()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
        {
            if (hit.transform.tag == Tags.EnemyTag)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    }

}
