using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stackable : Item {

    public int quantity;

    public override Item Copy()
    {
        Stackable copiedItem = base.Copy() as Stackable;
        copiedItem.quantity = this.quantity;
        return copiedItem;
    }
}
