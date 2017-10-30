using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    #region Singleton

    public static GunController instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public Transform gunPosition;
    //private PlayerController _player;
    public Gun startingGun;
    public Gun equippedGun;
    public GameObject equippedGunGO;

    public GameObject Pistol;
    public GameObject Shotgun;

    public GameObject PistolModelGO;
    public GameObject ShotgunModelGO;

    // Use this for initialization
    void Start ()
    {
        //_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		if (startingGun != null)
        {
            EquipGun(startingGun.weaponType);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void EquipGun(WeaponType gunType)
    {
        if (equippedGun != null)
        {
            Destroy(equippedGunGO);
        }
        if (gunType == WeaponType.Pistol)
        {
            PistolModelGO.SetActive(true);
            ShotgunModelGO.SetActive(false);
            GameObject gunToEquip = Pistol;
            equippedGunGO = Instantiate(gunToEquip, gunPosition.position, gunPosition.rotation);
            equippedGun = equippedGunGO.GetComponent<GunPistol>();
            equippedGunGO.transform.parent = gunPosition;
            //_player.SetWeaponAnimator(_equippedGun.weaponType);
            //_equippedGun.transform.localScale = _player.transform.localScale;
            equippedGunGO.transform.Rotate(0, -90, 0);
        }
        else if (gunType == WeaponType.Shotgun)
        {
            PistolModelGO.SetActive(false);
            ShotgunModelGO.SetActive(true);
            GameObject gunToEquip = Shotgun;
            equippedGunGO = Instantiate(gunToEquip, gunPosition.position, gunPosition.rotation);
            equippedGun = equippedGunGO.GetComponent<GunShotgun>();
            equippedGunGO.transform.parent = gunPosition;
            //_player.SetWeaponAnimator(_equippedGun.weaponType);
            //_equippedGun.transform.localScale = _player.transform.localScale;
            equippedGunGO.transform.Rotate(0, -90, 0);

        }

    }

    public void UnequipGun()
    {
        if (equippedGun != null)
        {
            Destroy(equippedGunGO);
            PistolModelGO.SetActive(false);
            ShotgunModelGO.SetActive(false);
        }
    }

    public void OnTriggerHold()
    {
        if (equippedGun != null)
        {
            equippedGun.OnTriggerHold();
        }
    }

    public void OnTriggerRelease()
    {
        if (equippedGun != null)
        {
            equippedGun.OnTriggerRelease();
        }
    }

    internal void ShootContinuous()
    {
        if (equippedGun != null)
        {
            equippedGun.ShootContinuous();
        }
    }





}
