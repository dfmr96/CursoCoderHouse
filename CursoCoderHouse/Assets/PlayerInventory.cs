using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public GameObject flashlight;
    public GameObject flashlightOnPlayer;

    public void TakeFlashlight()
    {
        flashlight.SetActive(false);
        flashlightOnPlayer.SetActive(true);
    }
}
