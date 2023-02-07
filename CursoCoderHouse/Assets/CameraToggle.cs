using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    [SerializeField] GameObject camOne;
    [SerializeField] GameObject camTwo;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeCamera();
        }
    }

    private void ChangeCamera()
    {
        if (camOne.activeInHierarchy)
        {
            camOne.SetActive(false);
            camTwo.SetActive(true);
        } else
        {
            camOne.SetActive(true);
            camTwo.SetActive(false);
        }
    }
}
