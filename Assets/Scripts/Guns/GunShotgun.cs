using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GunShotgun: Gun
{
    public GunShotgun()
    {
        shotTime = 0.6f;
        muzzleVelocity = 120f;
        reloadTime = 1f;
        weaponType = WeaponType.Pistol;
        fireMode = FireMode.Single;
        burstCount = 0;
        shotsRemainingInBurst = burstCount;
    }

    public float pumpTime = 0.6f;
    public float loadShellTime = 0.6f;
    public float reloadEndTime = 0.6f;

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
                Vector3 projectileAngle = new Vector3(0, UnityEngine.Random.Range(-30, 30), 0);
                Projectile newProjectile = Instantiate(projectile, projectileSpawn[i].position, projectileSpawn[i].rotation) as Projectile;
                newProjectile.transform.Rotate(projectileAngle);
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
