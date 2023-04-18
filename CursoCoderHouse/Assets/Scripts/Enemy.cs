using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth;
    [SerializeField] AudioClip[] zombieHitSfx;
    [SerializeField] AudioSource audioSource;
    private void Start()
    {
        health = maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (!audioSource.isPlaying)
        {
        randomClip();
        audioSource.Play();
        }
        health -= damage;
        if (health <= 0)
        {
            if (AutoAim.instance != null) AutoAim.instance.RemoveFromList(gameObject);
            Destroy(gameObject);
        }
    }

    public void randomClip()
    {
        audioSource.clip = zombieHitSfx[Random.Range(0,zombieHitSfx.Length)];
    }
}
