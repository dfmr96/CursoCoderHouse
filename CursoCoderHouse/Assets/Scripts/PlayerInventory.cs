using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public GameObject flashlight;
    public GameObject flashlightOnPlayer;
    //public InventoryObject inventory;

    public void TakeFlashlight()
    {
        flashlight.SetActive(false);
        flashlightOnPlayer.SetActive(true);
    }

    private void OnApplicationQuit()
    {
     //   inventory.Container.Items = new InventorySlot[6];
    }
}
