using UnityEngine;

[CreateAssetMenu(fileName = "Item Name", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "Item Name";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        Debug.Log("using " + name);
    }
}
