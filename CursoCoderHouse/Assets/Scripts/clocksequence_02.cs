using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clocksequence_02 : MonoBehaviour
{
    public ClockSequence clockSec;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && clockSec.soundCanBeActivated)
        {
            clockSec.clockCooCooSound.Play();
        }
    }
}
