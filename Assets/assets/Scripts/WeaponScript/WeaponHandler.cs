using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim 
{
    None,
    SelfAim,
    Aim
}

public enum WeaponFireType
{
    Single,
    Multiple
}

public enum WeaponBulletType
{
    Bullet,
    Arrow,
    Spear,
    None
}

public class WeaponHandler : MonoBehaviour
{

    private Animator anim;

    public WeaponAim weaponAim;

    [SerializeField]
    private GameObject muzzelFlash;

    [SerializeField]
    private AudioSource shootSound;
    [SerializeField]
    private AudioSource reloadSound;

    public WeaponFireType fireType;
    public WeaponBulletType BulletType;
    public GameObject attackPoint;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ShootAnimation()
    {
        anim.SetTrigger(AnimationTag.ShootTrigger);
    }

    public void Aim(bool canAim)
    {
        anim.SetBool(AnimationTag.AimParameter, canAim);
    }

    private void TurnOnMuzzleFlash()
    {
        muzzelFlash.SetActive(true);
    }
    
    private void TurnOffMuzzleFlash()
    {
        muzzelFlash.SetActive(false);
    }

    private void PlayShootSound()
    {
        shootSound.Play();
    }

    private void PlayRealoadSound()
    {
        reloadSound.Play();
    }

    private void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    private void TurnOffAttackPoint()
    {
        if(attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }

}
