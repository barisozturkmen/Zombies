using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public GameObject removeButtonGO;
    public Text quantity;

    private Item item;

    public void Additem(Item newitem)
    {

        //Debug.Log(newitem.name);
        item = newitem;
        //Debug.Log(item.name);
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButtonGO.SetActive(true);

        if (item is Stackable)
        {
            Stackable stackableItem = item as Stackable;
            if (stackableItem.quantity <= 0)
            {
                Inventory.instance.DestroyItem(stackableItem);
            }
            else
            {
                quantity.enabled = true;
                quantity.text = stackableItem.quantity.ToString();
            }
        }
        else if (item is Weapon)
        {
            Weapon weapon = item as Weapon;
            //Debug.Log(weapon.gun.name);
            //Debug.Log(weapon.gun.ammoInMagazine);
            weapon.ammo = weapon.gun.ammoInMagazine;
            quantity.enabled = true;
            quantity.text = weapon.ammo.ToString();
        }
        else
        {
            quantity.enabled = false;
        }
        //Debug.Log(item.name);
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButtonGO.SetActive(false);
        quantity.enabled = false;
    }

    public void OnRemoveButton()
    {
        if (item is Weapon)
        {
            GunController.instance.UnequipGun();
        }
        Inventory.instance.RemoveItem(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }
}
