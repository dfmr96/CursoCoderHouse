using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo Data", menuName = "Inventory/New Ammo Data")]
public class AmmoItemData : ItemData
{
    private void Awake()
    {
        base._itemType = ItemType.Ammo;
        base.SetColor();
    }
}
