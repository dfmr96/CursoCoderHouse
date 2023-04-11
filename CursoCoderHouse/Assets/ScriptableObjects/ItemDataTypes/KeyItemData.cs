using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key Item Data", menuName = "Inventory/New Key Item Data")]
public class KeyItemData : ItemData
{
    private void Awake()
    {
        base._itemType = ItemType.Key;
        base.SetColor();
    }
    
}