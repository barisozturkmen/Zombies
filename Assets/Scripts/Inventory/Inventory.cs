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

    public bool AddItem (Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("No more space in inventory");
            return false;
        }
        items.Add(item);
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
}
