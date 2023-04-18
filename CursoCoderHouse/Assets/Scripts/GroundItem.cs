using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class GroundItem : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private ParticleSystem _ps;
    // public InventoryObject inventory;

    private void Start()
    {
        SetParticleColor();
    }

    public void Interact(ItemData item)
    {
        //EventBus.Instance.PickUpPrompt(_itemData, () => Debug.Log($"{_itemData.name} ha sido tomado"), () => Debug.Log("No tomaste el item"));
        EventBus.Instance.PickUpItem(_itemData);
        InventoryViewController.lastInteracted = this.gameObject;
    }

    private void SetParticleColor()
    {
        var main = _ps.main;
        main.startColor = _itemData.GetColor();
        //_ps.main.startColor = Color.white;
    }
}
