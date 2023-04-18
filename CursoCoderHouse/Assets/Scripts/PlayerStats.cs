using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] Volume volume;
    [SerializeField] VolumeProfile volumeProfile;
    [SerializeField] LensDistortion lensDistortion;
    [SerializeField] int health = 100;
    [SerializeField] int sanity = 100;

    private void Start()
    {
        volumeProfile = volume.sharedProfile;
        volumeProfile.TryGet<LensDistortion>(out lensDistortion);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Debug.Log("Shift Derecho apretado");
            HeartBeat();
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            Debug.Log("Todas las corrutinas pausadas");
            StopAllCoroutines();
        }
    }

    private void HeartBeat()
    {
        StartCoroutine(CO_Hearbeating());
    }

    IEnumerator CO_Hearbeating()
    {
        while (true)
        {

        Debug.Log("Empieza corrutina");
        lensDistortion.intensity.Override(0.5f);
        Debug.Log("Cambio a 0.5f");
        yield return new WaitForSeconds(.5f);
        Debug.Log("Cambiar a 0");
        lensDistortion.intensity.Override(0f);
        yield return new WaitForSeconds(0.5f);
        }

    }
}
