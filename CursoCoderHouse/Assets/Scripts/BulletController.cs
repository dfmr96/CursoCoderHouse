using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 dir;
    [SerializeField] int damage;
    [SerializeField] float timeToDestroy;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Destroy(gameObject, timeToDestroy);
    }
    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            AudioManager.sharedInstance.bulletImpact.PlayScheduled(0.5);
            Destroy(gameObject);
        }
    }
}
