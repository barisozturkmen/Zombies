using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton

    public static EquipmentManager instance;



    private void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    private Inventory inventory;
    private Equipment[] currentEquipment;
    private GunController _gunController;



    private void Start()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        inventory = Inventory.instance;
        _gunController = GunController.instance;
    }

    public void Equip (Equipment newItem)
    {
        Debug.Log("trying to equip item");
        int slotIndex = (int)newItem.equipmentSlot;

        Equipment oldItem = null;

        if (newItem.equipmentSlot != EquipmentSlot.Weapon)
        {
            Inventory.instance.RemoveItem(newItem);
            if (currentEquipment[slotIndex] != null)
            {
                oldItem = currentEquipment[slotIndex];
                inventory.AddItem(oldItem);
            }
        }
        //if (oldItem is Weapon)
        //{
        //
        //}
        if (newItem is Weapon)
        {
            if (currentEquipment[slotIndex] != null)
            {
                Weapon oldWeapon = currentEquipment[slotIndex] as Weapon;
                oldWeapon.ammo = _gunController.equippedGun.ammoInMagazine;
            }

            Debug.Log("trying to equip gun");
            Weapon newWeapon = newItem as Weapon;
            _gunController.EquipGun(newWeapon.weaponType);
            newWeapon.gun = _gunController.equippedGun;
            _gunController.equippedGun.ammoInMagazine = newWeapon.ammo;
        }


        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip (int slotIndex)
    {
        Debug.Log("LLLLLLLLL");
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            currentEquipment[slotIndex] = null;
            if (oldItem is Weapon)
            {
                Debug.Log("saving ammo");
                Weapon oldWeapon = oldItem as Weapon;
                oldWeapon.ammo = _gunController.equippedGun.ammoInMagazine;
            }
            if (inventory.AddItem(oldItem))
            {
                return;
            }
            else
            {
                inventory.RemoveItem(oldItem);
            }
        }
    }
}
