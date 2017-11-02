using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Stackable {

    public AmmoType ammoType;

    public override Item Copy()
    {
        Ammo copiedItem = base.Copy() as Ammo;
        copiedItem.ammoType = this.ammoType;
        return copiedItem;
    }
}
