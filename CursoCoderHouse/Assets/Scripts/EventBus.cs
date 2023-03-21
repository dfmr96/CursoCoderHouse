using System;
using UnityEngine;
using UnityEngine.Events;

public class EventBus : MonoBehaviour
{
    public static EventBus Instance { get; private set; }

    public event Action onOpenInventory;
    public event Action onCloseInventory;
    public event Action onGameplayResumed;
    public event Action onGameplayPaused;
    public event Action<ItemData> onItemUsed;
    
    public event Action<ItemData> onItemPickUp;
    public event Action onItemPicked;
    public event Action<ItemData, Action, Action> onPickUpPrompt;

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
        onOpenInventory?.Invoke();
    }

    public void CloseInventory()
    {
        onCloseInventory?.Invoke();
    }

    public void GameplayResume()
    {
        onGameplayResumed?.Invoke();
    }

    public void GameplayPause()
    { 
        onGameplayPaused?.Invoke(); 
    }

    public void PickUpItem(ItemData item)
    {
        onItemPickUp?.Invoke(item);
    }

    public void ItemPicked()
    {
        onItemPicked?.Invoke();
    }

    public void UseItem(ItemData item)
    {
        onItemUsed?.Invoke(item);
        Debug.Log("Evento accionado con" + item.Name);
    }

    public void DoorLocked()
    {
        onDoorClosed?.Invoke();
        Debug.Log("Puerta Cerrada");
    }

    public void PickUpPrompt(ItemData item, Action yesAction, Action noAction)
    {
        onOpenInventory?.Invoke();
        onPickUpPrompt?.Invoke(item, yesAction, noAction);
        Debug.Log("PickUpPromt llamado");
    }
}
