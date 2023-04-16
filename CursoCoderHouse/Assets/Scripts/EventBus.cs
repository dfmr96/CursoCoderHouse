using System;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{
    public static EventBus Instance { get; private set; }

    public event Action OnOpenInventory;
    public event Action OnCloseInventory;
    public event Action<ItemData> OnItemUsed;
    public event Action<ItemData> OnItemPickUp;
    public event Action<int> OnWeaponEquipped;
    public UnityEvent onDoorClosed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void OpenInventory()
    {
        OnOpenInventory?.Invoke();
    }

    public void CloseInventory()
    {
        OnCloseInventory?.Invoke();
    }

    public void UseItem(ItemData item)
    {
        OnItemUsed?.Invoke(item);
        Debug.Log("Evento accionado con" + item.Name);
    }
    public void PickUpItem(ItemData item)
    {
        OnItemPickUp?.Invoke(item);
    }

    public void DoorLocked()
    {
        onDoorClosed?.Invoke();
        Debug.Log("Puerta Cerrada");
    }

    public void EquipWeapon(int weaponId)
    {
        OnWeaponEquipped?.Invoke(weaponId);
    }
}
