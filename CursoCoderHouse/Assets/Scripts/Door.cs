using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject roomMovePoint1;
    [SerializeField] GameObject roomMovePoint2;
    [SerializeField] Collider roomCollider1;
    [SerializeField] Collider roomCollider2;

    public void EnterToRoom(Collider col)
    {
        AudioManager.sharedInstance.doorSound.Play();
        if (col ==roomCollider1)
        {
            player.transform.position = roomMovePoint1.transform.position;
        }
        if (col == roomCollider2)
        {
            player.transform.position = roomMovePoint2.transform.position;
        }
    }
}
