using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;

    private void Update()
    {
        if (gameOverScreen.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                Application.Quit();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0;
            gameOverScreen.SetActive(true);
        }
    }
}
