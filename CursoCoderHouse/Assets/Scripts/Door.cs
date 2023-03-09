using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject roomMovePoint1;
    [SerializeField] GameObject roomMovePoint2;
    [SerializeField] Collider roomCollider1;
    [SerializeField] Collider roomCollider2;
    [SerializeField] ItemData _requiredItem;
    [SerializeField] bool isLocked;
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

    public void Interact(ItemData item)
    {
        if (_requiredItem == item) 
        { 
            isLocked = false;
        } else
        {
            Debug.Log("Puerta cerrada");
        }

        if (!isLocked) EnterToRoom();
    }
}
