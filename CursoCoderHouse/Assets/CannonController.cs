using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] int health = 10;
    [SerializeField] int speed = 10;
    [SerializeField] Vector3 direction = new Vector3 (0.4f,0.1f,0.9f);
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawnPoint;

    [SerializeField] float shootCooldown;
    [SerializeField] float shootTimer;
    [SerializeField] float shootDelay;


    private void Start()
    {
        shootTimer = shootCooldown;
    }
    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        RestoreHealth();
        TakeDamage();
        Shoot();

        shootTimer += Time.deltaTime;
    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    void RestoreHealth()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Input: Tecla 1
        {
            health++;
            Debug.Log("Vida recuperada, vida actual: " + health);
        }
    }

    void TakeDamage()
    {
        if (Input.GetKeyDown(KeyCode.Minus)) // Input: Guion
        {
            health--;
            Debug.Log("Vida perdida, vida actual: " + health);
        }
    }
    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space) && shootTimer >= shootCooldown)
        {
            StartCoroutine(MultipleShoots(1));
        }

        if (Input.GetKeyDown(KeyCode.J) && shootTimer >= shootCooldown)
        {
            StartCoroutine(MultipleShoots(2));
        }

        if (Input.GetKeyDown(KeyCode.K) && shootTimer >= shootCooldown)
        {
            StartCoroutine(MultipleShoots(3));
        }

        if (Input.GetKeyDown(KeyCode.L) && shootTimer >= shootCooldown)
        {
            StartCoroutine(MultipleShoots(4));
        }
    }

    IEnumerator MultipleShoots(int bulletNumber)
    {
        shootTimer = 0;
        for (int i = 0; i < bulletNumber; i++)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(shootDelay);
        }
    }
}
