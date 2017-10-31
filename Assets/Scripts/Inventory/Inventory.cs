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
            Debug.Log("Recognised object is stackable");
            foreach (Item item in items)
            {
                //is common stack found, add to it
                if (itemToAdd.name == item.name)
                {
                    Debug.Log("Match found");
                    Stackable itemStack = item as Stackable;
                    Stackable itemToAddStack = itemToAdd as Stackable;
                    itemStack.quantity += itemToAddStack.quantity;
                    itemAdded = true;
                    break;
                }
            }
            if (itemAdded == false)
            {
                Debug.Log("Match not found");
                if (SpaceAvailable())
                {
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
            Debug.Log("Shouldnt see this");
            items.Add(itemToAdd);
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }

    public void RemoveItem (Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        GameObject itemOnGroundGO = Instantiate(itemOnGround, this.transform.position, this.transform.rotation);
        itemOnGroundGO.GetComponent<ItemOnGround>().item = item;
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
