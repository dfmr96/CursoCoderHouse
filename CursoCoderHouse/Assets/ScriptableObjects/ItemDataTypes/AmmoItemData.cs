using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo Data", menuName = "Inventory/New Ammo Data")]
public class AmmoItemData : ItemData
{
    public WeaponItemData weaponData;
    public AmmoItemData()
    {
        base._itemType = ItemType.ammo;
    }
}
