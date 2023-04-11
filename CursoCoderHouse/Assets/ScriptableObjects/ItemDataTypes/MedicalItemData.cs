using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Medical Item Data", menuName = "Inventory/New Medical Item Data")]
public class MedicalItemData : ItemData
{
    private void Awake()
    {
        base._itemType = ItemType.Medical;
        base.SetColor();
    }
}
