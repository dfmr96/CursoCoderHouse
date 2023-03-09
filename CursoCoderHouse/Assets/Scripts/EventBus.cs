using System;
using UnityEngine;

public class EventBus : MonoBehaviour
{
    public static EventBus Instance { get; private set; }

    public event Action onOpenInventory;
    public event Action onCloseInventory;
    public event Action onGameplayResumed;
    public event Action onGameplayPaused;
    public event Action<ItemData> onItemUsed;
    
    public event Action<ItemData> onItemPickUp;


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

    public void UseItem(ItemData item)
    {
        onItemUsed?.Invoke(item);
        Debug.Log("Evento accionado con" + item.Name);
    }
}
