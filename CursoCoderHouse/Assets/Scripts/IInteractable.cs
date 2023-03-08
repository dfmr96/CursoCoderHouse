using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    Door,
    Note,
    Item
}
public interface IInteractable
{
    void Interact();
}
