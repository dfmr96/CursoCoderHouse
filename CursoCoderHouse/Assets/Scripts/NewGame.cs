using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour
{
    [SerializeField] GameObject newGameIntro;
    private void Start()
    {
        Time.timeScale = 0;
        newGameIntro.SetActive(true);
    }

    private void Update()
    {
        if (newGameIntro.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Time.timeScale = 1f;
                newGameIntro.SetActive(false);
            }
        }
    }
}
