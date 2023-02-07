using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float angularSpeed;
    [SerializeField] CharacterController player;
    [SerializeField] int playerAmmo;
    public bool isRunning;
    public bool isAiming;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawn;
    [SerializeField] AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponent < CharacterController>();
    }
    private void Update()
    {
        Movement();
        RotatePlayer();
        Aim();
    }
    void Movement()
    {
        if (isAiming) return;
        if(Input.GetKey(KeyCode.W))
        {
            //Caminando hacia adelante
            if (!AudioManager.sharedInstance.stepSound.isPlaying)
            {
            AudioManager.sharedInstance.stepSound.Play();
            }
            player.Move(transform.forward * speed * Time.deltaTime);
            isRunning = true;
        } else if (Input.GetKey(KeyCode.S))
        {
            if (!AudioManager.sharedInstance.stepSound.isPlaying)
            {
                AudioManager.sharedInstance.stepSound.Play();
            }
            player.Move(-transform.forward * speed * Time.deltaTime);
            isRunning = true;
        } else
        {
            AudioManager.sharedInstance.stepSound.Stop();
            isRunning = false;
        }

    }

    void RotatePlayer()
    {
        Vector3 rotateVector = new Vector3(0, 1, 0);
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-rotateVector * angularSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(rotateVector * angularSpeed * Time.deltaTime);
        }
    }


    private void Aim()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isAiming = true;
        } else
        {
            isAiming = false;
        }

        Shoot();
    }

    void Shoot()
    {
        if (!isAiming) return;

        if (Input.GetKeyDown(KeyCode.Space) && playerAmmo > 0)
        {
            playerAmmo--;
            audioSource.Play();
            Instantiate(bulletPrefab, bulletSpawn.transform);
        }
    }
}
