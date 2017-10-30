using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    public GameObject removeButtonGO;

    private Item item;

    public void Additem(Item newitem)
    {
        item = newitem;

        icon.sprite = item.icon;
        icon.enabled = true;
        removeButtonGO.SetActive(true);
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButtonGO.SetActive(false);
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
