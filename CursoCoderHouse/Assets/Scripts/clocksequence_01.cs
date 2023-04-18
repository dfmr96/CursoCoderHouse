using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clocksequence_01 : MonoBehaviour
{
    public ClockSequence clockSec;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            clockSec.soundCanBeActivated = true;
        }
    }
}
