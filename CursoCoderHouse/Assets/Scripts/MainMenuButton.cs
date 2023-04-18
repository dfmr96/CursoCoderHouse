using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, ISelectHandler, ISubmitHandler
{
    [SerializeField] private AudioClip[] clip;
    [SerializeField] private AudioClip submitClip;
    [SerializeField] AudioSource audioSource;


    public void OnSelect(BaseEventData eventData)
    {
        PlayClip(clip[RandomClip()]);
    }

    private int RandomClip()
    {
        int randomNumber = Random.Range(0, clip.Length);
        return randomNumber;
    }

    private void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        PlayClip(submitClip);
    }
}
