using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton

    public static EquipmentManager instance;

    Inventory inventory;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

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
        else
        {
            Weapon newWeapon = newItem as Weapon;
            _gunController.EquipGun(newWeapon.weaponType);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            currentEquipment[slotIndex] = null;
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
