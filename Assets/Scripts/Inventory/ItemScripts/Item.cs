using UnityEngine;

public class Item {

    new public string name = "Item Name";
    public Sprite icon = null;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        //Debug.Log("LOL");
        //Debug.Log("using " + name);
    }

    public virtual Item Copy()
    {
        Item copiedItem = new Item();
        copiedItem.name = this.name;
        copiedItem.icon = this.icon;
        copiedItem.isDefaultItem = this.isDefaultItem;
        return copiedItem;
    }
    
}
