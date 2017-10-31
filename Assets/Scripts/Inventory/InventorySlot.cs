using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public GameObject removeButtonGO;
    public Text quantity;

    private Item item;

    public void Additem(Item newitem)
    {
        item = newitem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButtonGO.SetActive(true);

        if (item is Stackable)
        {
            Stackable stackableItem = item as Stackable;
            quantity.enabled = true;
            quantity.text = stackableItem.quantity.ToString();
        }
        else
        {
            quantity.enabled = false;
        }

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
