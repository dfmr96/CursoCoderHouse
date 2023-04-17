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
    [Header("Core")]
    [SerializeField] PlayerState state;
    [SerializeField] CharacterController player;
    [SerializeField] GameObject autoAim;
    [SerializeField] AudioSource audioSource;
    [SerializeField] bool allowInteraction;
    [SerializeField] GameObject interactionObject;
    [SerializeField] int interactMaxDistance;
    [SerializeField] InventoryViewController _inventoryViewController;


    [Space(20)]
    [Header("Speeds")]
    [SerializeField] float currentSpeed;
    [SerializeField] float speed;
    [SerializeField] float runningSpeed;
    [SerializeField] float angularSpeed;
    [Space(20)]
    [Header("Weapon")]
    [SerializeField] GameObject fireOrigin;
    [SerializeField] int weaponRange;
    [SerializeField] int playerAmmo;
    [SerializeField] int weaponDamage;
    [Space(20)]
    [Header("Animation Bool")]
    public bool isRunning;
    public bool isAiming;
    public bool hasWeaponEquipped = false;

    private void OnEnable()
    {
        EventBus.Instance.OnOpenInventory += () => state = PlayerState.CheckingInventory;
        EventBus.Instance.OnCloseInventory += () => state = PlayerState.Idle;
        EventBus.Instance.OnWeaponEquipped += (int i) => hasWeaponEquipped = true;
        EventBus.Instance.OnItemUsed += Interact;
    }

    private void OnDisable()
    {
        EventBus.Instance.OnOpenInventory -= () => state = PlayerState.CheckingInventory;
        EventBus.Instance.OnCloseInventory -= () => state = PlayerState.Idle;
        EventBus.Instance.OnWeaponEquipped -= (i) => hasWeaponEquipped = true;

        EventBus.Instance.OnItemUsed -= Interact;

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
        if (hasWeaponEquipped) Aim();
        if (Input.GetKeyDown(KeyCode.E) && state != PlayerState.CheckingInventory) Interact(null);
        if (Input.GetKeyDown(KeyCode.R) && CurrentWeapon.Instance.stack < CurrentWeapon.Instance.maxStack)
        {
            Reload();
            AudioManager.sharedInstance.reloadWeaponSound.Play();
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

            autoAim.GetComponent<AutoAim>().AimToNearestEnemy(transform);

            RaycastHit hit;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (CurrentWeapon.Instance.stack > 0)
                {
                    Shoot();
                }
                else
                {
                    AudioManager.sharedInstance.dryWeaponSound.Play();
                    return;
                }
                if (Physics.Raycast(fireOrigin.transform.position, transform.forward, out hit, weaponRange)) DealDamage(hit);
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
        CurrentWeapon.Instance.stack--;
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

        if (Physics.Raycast(interactionObject.transform.position, interactionObject.transform.forward, out hit, interactMaxDistance))
        {
            Debug.Log("Ha golpeado" + hit.transform.name + hit.collider);
            Debug.DrawRay(interactionObject.transform.position, interactionObject.transform.forward, Color.red, 0.5f);
            var interactable = hit.collider.gameObject.GetComponent<IInteractable>();
            if (interactable == null) return;
            if (allowInteraction)
            {
                interactable.Interact(item);
            }
            //state = PlayerState.Idle;
        }
        allowInteraction = false;
    }

    public void Reload()
    {
        _inventoryViewController.GetAmmo();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(interactionObject.transform.position, interactionObject.transform.forward * interactMaxDistance);

        if (WeaponsPool.instance != null && WeaponsPool.instance.currentWeapon != null)
        {
            Gizmos.DrawRay(WeaponsPool.instance.GetWeaponOrigin(), transform.forward * weaponRange);
        }
    }
}

