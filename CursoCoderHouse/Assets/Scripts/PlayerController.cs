using UnityEngine;

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
    [SerializeField] int weaponDamage;
    [SerializeField] bool allowInteraction;
    [SerializeField] GameObject interactionObject;
    [SerializeField] GameObject fireOrigin;
    [SerializeField] int weaponRange;
    [SerializeField] int maxDistance;
    public bool isRunning;
    public bool isAiming;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSpawn;
    [SerializeField] AudioSource audioSource;

    private void OnEnable()
    {
        EventBus.Instance.onOpenInventory += () => state = PlayerState.CheckingInventory;
        EventBus.Instance.onCloseInventory += () => state = PlayerState.Idle;
        EventBus.Instance.onItemUsed += Interact;
    }

    private void OnDisable()
    {
        EventBus.Instance.onOpenInventory -= () => state = PlayerState.CheckingInventory;
        EventBus.Instance.onCloseInventory -= () => state = PlayerState.Idle;
        EventBus.Instance.onItemUsed -= Interact;

    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GetComponent<CharacterController>();

    }
    private void Update()
    {
        if (Time.timeScale == 0) return;
        Movement();
        Aim();

        if (Input.GetKeyDown(KeyCode.E) && state != PlayerState.CheckingInventory)
        {
            Interact(null);
        }
    }
    private void Movement()
    {
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
                Debug.Log("Puede disparar");
                isRunning = false;
                RotatePlayer();
                break;
            case PlayerState.CheckingInventory:
                break;
        }

        if (state == PlayerState.Aiming || state == PlayerState.CheckingInventory) return;

        RotatePlayer();
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

    private void RotatePlayer()
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

            RaycastHit hit;
            if (Input.GetKeyDown(KeyCode.Space) && playerAmmo > 0)
            {
                Shoot();

                if (Physics.Raycast(fireOrigin.transform.position, transform.forward, out hit, weaponRange))
                {
                    DealDamage(hit);
                }
            }

        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            state = PlayerState.Idle;
            isAiming = false;
        }
    }

    private void Shoot()
    {
        Debug.Log("Disparó");
        playerAmmo--;
        audioSource.Play();
        //if (!isAiming) return;
    }

    private void DealDamage(RaycastHit hit)
    {
        var enemy = hit.collider.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("Enemigo Impactado");
            enemy.TakeDamage(weaponDamage);
        }
    }

    public void Interact(ItemData item)
    {
        allowInteraction = true;
        Debug.Log("E apretado");
        RaycastHit hit;

        if (Physics.Raycast(interactionObject.transform.position, interactionObject.transform.forward, out hit, maxDistance))
        {
            Debug.Log("Ha golpeado" + hit.transform.name + hit.collider);
            Debug.DrawRay(interactionObject.transform.position, interactionObject.transform.forward, Color.red, 0.5f);
            var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable == null) return;
            if (allowInteraction)
            {
                interactable.Interact(item);
                allowInteraction = false;
            }
            //state = PlayerState.Idle;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(interactionObject.transform.position, interactionObject.transform.forward);
        Gizmos.DrawRay(fireOrigin.transform.position, transform.forward * weaponRange);
    }
}

