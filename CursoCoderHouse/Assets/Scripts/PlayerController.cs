using UnityEngine;
using UnityEngine.Rendering;

public enum PlayerState
{
    Idle,
    Walking,
    Running,
    Aiming,
    Interacting,
    CheckingInventory
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerState state;
    [SerializeField] float currentSpeed;
    [SerializeField] float speed;
    [SerializeField] float runningSpeed;
    [SerializeField] float angularSpeed;
    [SerializeField] CharacterController player;
    [SerializeField] int playerAmmo;
    [SerializeField] PlayerInventory inventory;
    [SerializeField] bool allowInteraction;
    [SerializeField] bool flashlightTaken;
    public bool isRunning;
    public bool isAiming;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawn;
    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponent<CharacterController>();
        
    }
    private void Update()
    {

        Movement();
        RotatePlayer();
        Aim();
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }
    void Movement()
    {
        if (state != PlayerState.Aiming)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                state = PlayerState.Walking;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    state = PlayerState.Running;
                }
            }
            else 
            {
                state = PlayerState.Idle;
            }
        }
        
        switch (state)
        {
            case PlayerState.Idle:
                AudioManager.sharedInstance.stepSound.Stop();
                isRunning = false;
                break;
            case PlayerState.Walking:
                if (!AudioManager.sharedInstance.stepSound.isPlaying) AudioManager.sharedInstance.stepSound.Play();
                if (Input.GetKey(KeyCode.W)) currentSpeed = speed;
                if (Input.GetKey(KeyCode.S)) currentSpeed = -speed;
                player.Move(transform.forward * currentSpeed * Time.deltaTime);
                isRunning = true;
                break;
            case PlayerState.Running:
                if (!AudioManager.sharedInstance.stepSound.isPlaying) AudioManager.sharedInstance.stepSound.Play();
                currentSpeed = runningSpeed;
                player.Move(transform.forward * currentSpeed * Time.deltaTime);
                isRunning = true;
                break;
            case PlayerState.Aiming:
                isRunning = false;
                if (Input.GetKeyDown(KeyCode.Space) && playerAmmo > 0) Shoot();
                return;

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
            state = PlayerState.Aiming;
            isAiming = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            state = PlayerState.Idle;
            isAiming = false;
        }
    }

    void Shoot()
    {
        if (!isAiming) return;

        {
            playerAmmo--;
            audioSource.Play();
            Instantiate(bulletPrefab, bulletSpawn.transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Door>() != null)
        {
            Debug.Log(other.name + "is triggered");
            if (allowInteraction)
            {
                other.gameObject.GetComponent<Door>().EnterToRoom(other);
                Debug.Log("E apretada");
                allowInteraction= false;
            }
        }

        if (other.gameObject.GetComponent<Flashlight>() != null && !flashlightTaken && allowInteraction)
        {
            flashlightTaken= true;
            inventory.TakeFlashlight();
        }
    }

    public void Interact()
    {
        allowInteraction = true;
    }
}
