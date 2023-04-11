using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon Item Data", menuName = "Inventory/New Weapon Item Data")]
public class WeaponItemData : ItemData
{
    private void Awake()
    {
        base._itemType = ItemType.Weapon;
        base.SetColor();
    }
}