using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than once instance of Inventory found!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public GameObject itemOnGround;

    [System.NonSerialized] public int space = 10;

    public List<Item> items = new List<Item>();

    public bool AddItem (Item itemToAdd)
    {
        //add to already existing stack if possible
        if (itemToAdd is Stackable)
        {
            bool itemAdded = false;
            foreach (Item item in items)
            {
                //is common stack found, add to it
                if (itemToAdd.name == item.name)
                {
                    Stackable itemStack = item as Stackable;
                    Stackable itemToAddStack = itemToAdd as Stackable;
                    itemStack.quantity += itemToAddStack.quantity;
                    itemAdded = true;
                    break;
                }
            }
            if (itemAdded == false)
            {
                if (SpaceAvailable())
                {
                    //Item itemCopy = itemToAdd.Copy();
                    items.Add(itemToAdd);
                }
            }

        }
        else if (SpaceAvailable() == false)
        {
            return false;
        }
        else
        {
            //Item itemCopy = itemToAdd.Copy();
            items.Add(itemToAdd);
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void RemoveItem(Item item)
    {
        //Debug.Log(item.name);
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        GameObject itemOnGroundGO = Instantiate(itemOnGround, this.transform.position, this.transform.rotation);
        Debug.Log("dropping item: " + item.name);


        if (item is Weapon)
        {
            WeaponPickup weaponPickup = itemOnGroundGO.AddComponent<WeaponPickup>();
            weaponPickup.ConfigurePickup(item);
        }
        else if (item is Equipment)
        {
            EquipmentPickup equipmentPickup = itemOnGroundGO.AddComponent<EquipmentPickup>();
            equipmentPickup.ConfigurePickup(item);
        }
        else if (item is Ammo)
        {
            AmmoPickup ammoPickup = itemOnGroundGO.AddComponent<AmmoPickup>();
            ammoPickup.ConfigurePickup(item);
        }
        else if (item is Stackable)
        {
            StackablePickup stackablePickup = itemOnGroundGO.AddComponent<StackablePickup>();
            stackablePickup.ConfigurePickup(item);
        }
        else
        {
            ItemPickup itemPickup = itemOnGroundGO.AddComponent<ItemPickup>();
            itemPickup.ConfigurePickup(item);
        }

        //Debug.Log(item);
    }

    public void DestroyItem(Stackable stackableItem)
    {
        items.Remove(stackableItem);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void UpdateUI()
    {
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    private bool SpaceAvailable()
    {
        if (items.Count >= space)
        {
            Debug.Log("No more space in inventory");
            return false;
        }
        else
            return true;
    }
}
