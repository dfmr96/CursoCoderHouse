using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public float batteryDuration;
    public float currentBattery;
    [SerializeField] GameObject lightGO;

    private void Start()
    {
        currentBattery = batteryDuration;
    }

    private void Update()
    {
        if (currentBattery > 0)
        {
        currentBattery -= Time.deltaTime;
        }
        CheckBattery();
    }

    public void CheckBattery()
    {
        if (currentBattery <= 0)
        {
            lightGO.SetActive(false);
        }
    }
}
