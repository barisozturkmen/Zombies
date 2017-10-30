using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    public enum PlayerAnimations
    {
        Default,
        Pistol_Aim, Pistol_Shoot, Pistol_Reload,
        Shotgun_Aim, Shotgun_Shoot, Shotgun_Pump, Shotgun_ReloadMain, Shotgun_ReloadShell, Shotgun_ReloadEnd
    }

    private Animator _animator;
    private PlayerController _playerController;
    private GunController _gunController;
    private Gun _equippedGun;

    public float animationSpeedPercent = 1f;

    void Start ()
    {
        _animator = GetComponent<Animator>();
        _playerController = GetComponent<PlayerController>();
        _gunController = GunController.instance;
    }
	
	void Update ()
    {
        _animator.SetFloat("speedPercent", animationSpeedPercent);

    }

    //public void AnimatePlayer()
    //{
    //    if (_playerController.isAiming == false && _playerController.playingAnimation == false)
    //    {
    //        _animationToPlay = Animations.Default;
    //        _animator.Play(_animationToPlay.ToString());
    //    }
    //    else if (Input.GetButton("Aim") && _playerController.playingAnimation == false)
    //    {
    //        if (Input.GetButtonDown("Reload"))
    //        {
    //            StartCoroutine(PlayReloadAnimation());
    //        }
    //        else if (_playerController.playingAnimation == false)
    //        {
    //            StartCoroutine(PlayAimAnimation());
    //        }
    //    }
    //}



    public void SetAnimationSpeed(float animSpeedPercent)
    {
        animationSpeedPercent = animSpeedPercent;
    }

    public void PlayDefault()
    {
        _animator.Play(PlayerAnimations.Default.ToString());
    }

    public void PlayAim()
    {
        if (_gunController.equippedGun != null)
            _equippedGun = _gunController.equippedGun;

        if (_gunController.equippedGun.weaponType == WeaponType.Pistol)
        {
            StartCoroutine(PlayPistolAim());
        }
        else if (_equippedGun.weaponType == WeaponType.Shotgun)
        {
            StartCoroutine(PlayShotgunAim());
        }
    }


    public void PlayShoot()
    {

        if (_gunController.equippedGun != null)
            _equippedGun = _gunController.equippedGun;

        if (_equippedGun.weaponType == WeaponType.Pistol)
        {
            StartCoroutine(PlayPistolShoot());
        }
        else if (_equippedGun.weaponType == WeaponType.Shotgun)
        {
            StartCoroutine(PlayShotgunShoot());
        }
    }

    public void PlayReload()
    {
        if (_gunController.equippedGun != null)
            _equippedGun = _gunController.equippedGun;

        if (_equippedGun.weaponType == WeaponType.Pistol)
        {
            StartCoroutine(PlayPistolReload());
        }
        else if (_equippedGun.weaponType == WeaponType.Shotgun)
        {
            StartCoroutine(PlayShotgunReload());
        }
    }

    private IEnumerator PlayUnequppedDefault()
    {
        _animator.Play(PlayerAnimations.Default.ToString());
        yield return null;
    }

    private IEnumerator PlayPistolAim()
    {
        //Debug.Log("PLAYING AIM?");
        _playerController.isAiming = true;
        _animator.Play(PlayerAnimations.Pistol_Aim.ToString());
        yield return null;
    }

    private IEnumerator PlayPistolShoot()
    {

        //Debug.Log("PLAYING SHOOT?");
        _playerController.playingAnimation = true;
        float completionAmount = 0.1f;
        while (completionAmount < _equippedGun.shotTime)
        {
            completionAmount += Time.deltaTime;
            _animator.Play(PlayerAnimations.Pistol_Shoot.ToString());
            yield return null;
        }
        _playerController.playingAnimation = false;
        _equippedGun.isShooting = false;
    }

    private IEnumerator PlayPistolReload()
    {
        //Debug.Log("PLAYING RELOAD?");
        _playerController.playingAnimation = true;
        yield return new WaitForSeconds(0.2f);
        float completionAmount = 0;
        while (completionAmount < _equippedGun.reloadTime)
        {
            completionAmount += Time.deltaTime;
            _animator.Play(PlayerAnimations.Pistol_Reload.ToString());
            yield return null;
        }
        _playerController.playingAnimation = false;
    }

    private IEnumerator PlayShotgunAim()
    {
        //Debug.Log("PLAYING AIM?");
        _playerController.isAiming = true;
        _animator.Play(PlayerAnimations.Shotgun_Aim.ToString());
        yield return null;
    }

    private IEnumerator PlayShotgunShoot()
    {
        GunShotgun equippedShotgun = _equippedGun as GunShotgun;
        _playerController.playingAnimation = true;
        float completionAmount = 0f;
        while (completionAmount < equippedShotgun.shotTime)
        {
            completionAmount += Time.deltaTime;
            _animator.Play(PlayerAnimations.Shotgun_Shoot.ToString());
            yield return null;
        }
        completionAmount = 0f;
        float pumpTime = equippedShotgun.pumpTime;
        while (completionAmount < pumpTime)
        {
            completionAmount += Time.deltaTime;
            _animator.Play(PlayerAnimations.Shotgun_Pump.ToString());
            yield return null;
        }
        _playerController.playingAnimation = false;
        equippedShotgun.isShooting = false;
    }

    private IEnumerator PlayShotgunReload()
    {
        GunShotgun equippedShotgun = _equippedGun as GunShotgun;
        _playerController.playingAnimation = true;
        yield return new WaitForSeconds(0.1f);
        float completionAmount = 0;
        while (completionAmount < equippedShotgun.reloadTime)
        {
            completionAmount += Time.deltaTime;
            _animator.Play(PlayerAnimations.Shotgun_ReloadMain.ToString());
            yield return null;
        }

        for (int i = 0; i < 5; i++)
        {
            completionAmount = 0;
            while (completionAmount < equippedShotgun.loadShellTime)
            {
                completionAmount += Time.deltaTime;
                _animator.Play(PlayerAnimations.Shotgun_ReloadShell.ToString());
                yield return null;
            }
            _animator.Play(PlayerAnimations.Shotgun_ReloadShell.ToString(), -1, 0f);
        }

        completionAmount = 0f;
        float endTime = equippedShotgun.reloadEndTime;
        while (completionAmount < endTime)
        {
            completionAmount += Time.deltaTime;
            _animator.Play(PlayerAnimations.Shotgun_ReloadEnd.ToString());
            yield return null;
        }

        completionAmount = 0f;
        float pumpTime = equippedShotgun.pumpTime;
        while (completionAmount < pumpTime)
        {
            completionAmount += Time.deltaTime;
            _animator.Play(PlayerAnimations.Shotgun_Pump.ToString());
            yield return null;
        }

        _playerController.playingAnimation = false;
    }

}
