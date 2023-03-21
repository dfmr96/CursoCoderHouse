using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData _itemData;
    // public InventoryObject inventory;

    private void OnEnable()
    {
        EventBus.Instance.onItemPicked += DestroyItem;
    }

    private void OnDisable()
    {
        EventBus.Instance.onItemPicked -= DestroyItem;

    }
    public void Interact(ItemData item)
    {
        //EventBus.Instance.PickUpPrompt(_itemData, () => Debug.Log($"{_itemData.name} ha sido tomado"), () => Debug.Log("No tomaste el item"));
        EventBus.Instance.PickUpItem(_itemData);
        InventoryViewController.lastInteracted = this.gameObject;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
