using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon Item Data", menuName = "Inventory/New Weapon Item Data")]
public class WeaponItemData : ItemData
{
    public int id = 0;
    public WeaponItemData()
    {
        base._itemType = ItemType.weapon;
    }
}
