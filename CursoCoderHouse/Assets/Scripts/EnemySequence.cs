using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySequence : MonoBehaviour
{
    public GameObject enemies;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemies.SetActive(true);
            AudioManager.sharedInstance.doorSound.Play();
        }
    }
}
