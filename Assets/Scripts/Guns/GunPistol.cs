using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class GunPistol : Gun {

    public GunPistol()
    {
        shotTime = 0.6f;
        muzzleVelocity = 120f;
        reloadTime = 1f;
        weaponType = WeaponType.Pistol;
        fireMode = FireMode.Single;
        burstCount = 3;
        shotsRemainingInBurst = burstCount;
    }

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        muzzleFlash = this.GetComponent<MuzzleFlash>();
    }

    public override void Shoot()
    {
        if (CanShoot() == true)
        {
            if (fireMode == FireMode.Burst)
            {
                if (shotsRemainingInBurst == 0)
                {
                    return;
                }
                shotsRemainingInBurst--;
            }
        else if (fireMode == FireMode.Single)
        {
            if (!triggerReleasedSinceLastShot)
            {
                return;
            }
        }
            for (int i = 0; i < projectileSpawn.Length; i++)
            {
                Projectile newProjectile = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;
                newProjectile.SetSpeed(muzzleVelocity);
                nextPossibleShootTime = Time.time + shotTime;
            }
            audioSource.Play();
            Instantiate(shell, shellEjector.position, shellEjector.rotation);
            muzzleFlash.Activate();
            isShooting = true;
        }
    }

    public override void ShootContinuous()
    {
        if (fireMode == FireMode.Auto)
        {
            Shoot();
        }
    }

    public override bool CanShoot()
    {
        bool canShoot = true;
        if (Time.time < nextPossibleShootTime)
        {
            canShoot = false;
        }
        return canShoot;
    }

    public override void OnTriggerHold()
    {
        Shoot();
        triggerReleasedSinceLastShot = false;
    }

    public override void OnTriggerRelease()
    {
        triggerReleasedSinceLastShot = true;
        shotsRemainingInBurst = burstCount;
    }
}
