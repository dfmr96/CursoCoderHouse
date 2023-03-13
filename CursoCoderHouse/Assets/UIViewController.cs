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

    private void Start()
    {
        selector.SetActive(false);
    }

    public void HidePressStart()
    {
        pressStart.SetActive(false);
        menuButtons.SetActive(true);
        selector.SetActive(true);
        EventSystem.current.SetSelectedGameObject(defaultMenuButton);
    }

    public void DifficultySelection()
    {
        menuButtons.SetActive(false);
        newGameButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(normalDifficultyButton);

    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
