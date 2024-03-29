using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sharedInstance;
    public AudioSource bulletImpact;
    public AudioSource stepSound;
    public AudioSource doorSound;
    public AudioSource retreteSound;
    public AudioSource clickSound;
    public AudioSource doorLockedSound;
    public AudioSource dryWeaponSound;
    public AudioSource reloadWeaponSound;
    public AudioSource slotsSound;
    public AudioSource openContextMenuSound;
    public AudioSource submitContextMenuSound;
    public AudioSource noteSound;
    // Start is called before the first frame update
    private void OnEnable()
    {
        EventBus.Instance.onDoorClosed.AddListener(PlayDoorLocked);
    }

    private void OnDisable()
    {
        EventBus.Instance.onDoorClosed.RemoveListener(PlayDoorLocked);
    }

    void Start()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void PlayDoorLocked()
    {
        doorLockedSound.Play();
    }
}
