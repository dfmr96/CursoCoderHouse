using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraToggle : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;

    private void Start()
    {
        cam = transform.parent.gameObject.GetComponent<CinemachineVirtualCamera>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + "ha entrado");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player ha entrado");
            cam.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + "ha salido");

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player ha salido");
            cam.enabled = false;
        }
    }
}
