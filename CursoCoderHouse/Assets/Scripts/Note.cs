using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour, IInteractable
{
    [SerializeField] TextTrigger trigger;
    [SerializeField] TextGenerator generator;
    public void Interact(ItemData item)
    {
        generator.ShowMsg(trigger.text);
    }
}
