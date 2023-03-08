using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject roomMovePoint1;
    [SerializeField] GameObject roomMovePoint2;
    [SerializeField] Collider roomCollider1;
    [SerializeField] Collider roomCollider2;
    bool isPlayerInside;

    public void EnterToRoom()
    {
        AudioManager.sharedInstance.doorSound.Play();
        if (!isPlayerInside)
        {
            player.transform.position = roomMovePoint1.transform.position;
            isPlayerInside = true;
        }
        else
        {
            isPlayerInside = false;
            player.transform.position = roomMovePoint2.transform.position;
        }
    }

    public void Interact()
    {
        EnterToRoom();
    }
}
