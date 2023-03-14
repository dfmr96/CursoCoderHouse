using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UIViewController : MonoBehaviour
{
    [SerializeField] GameObject pressStart;
    [SerializeField] GameObject menuButtons;
    [SerializeField] GameObject newGameButtons;
    [SerializeField] GameObject defaultMenuButton;
    [SerializeField] GameObject normalDifficultyButton;
    [SerializeField] GameObject selector;

    [SerializeField] AudioSource audioUISource;
    [SerializeField] AudioSource audioBGMSource;
    [SerializeField] AudioClip[] uiSFX;
    [SerializeField] AudioClip[] bgm;
    private void Start()
    {
        selector.SetActive(false);
        PlayAudio(audioBGMSource, bgm[0]);
    }

    public void HidePressStart()
    {
        PlayAudio(audioUISource ,uiSFX[0]);
        PlayAudio(audioBGMSource, bgm[1]);
        pressStart.SetActive(false);
        menuButtons.SetActive(true);
        selector.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultMenuButton);
    }

    public void ReturnToMainMenu()
    {
        newGameButtons.SetActive(false);
        menuButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultMenuButton);
    }

    public void DifficultySelection()
    {
        Debug.Log("Choose difficulty");
        menuButtons.SetActive(false);
        newGameButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(normalDifficultyButton);

    }

    public void NewGame()
    {
        PlayAudio(audioUISource, bgm[3]);
        StartCoroutine(NewGameCO());
    }

    public void Credits()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PlayAudio(AudioSource audioSource,AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public IEnumerator NewGameCO()
    {
        PlayAudio(audioBGMSource, bgm[2]);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
