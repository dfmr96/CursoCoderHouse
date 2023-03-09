using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData _itemData;
   // public InventoryObject inventory;

    public void Interact()
    {
        EventBus.Instance.PickUpItem(_itemData);
        Destroy(gameObject);
    }
}
