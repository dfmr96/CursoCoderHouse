using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BathroomEvents : MonoBehaviour
{
    [SerializeField] GameObject bathroomLight;
    [SerializeField] Collider eventTrigger;
    [SerializeField] UnityEvent OnSecuenceStart;

    private void Start()
    {
        OnSecuenceStart.AddListener(SecuenceHandler);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BeginSecuence();
        }
    }
    public void BeginSecuence()
    {
        StartCoroutine(bathroomSecuence());
    }

    public void SecuenceHandler()
    {
        Debug.Log("Secuencia iniciada");
    }

    public IEnumerator bathroomSecuence()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.sharedInstance.clickSound.Play();
        yield return new WaitForSeconds(0.2f);
        bathroomLight.SetActive(true);
        yield return new WaitForSeconds(2f);
        AudioManager.sharedInstance.retreteSound.Play();
        yield return new WaitForSeconds(2f);
        AudioManager.sharedInstance.clickSound.Play();
        yield return new WaitForSeconds(0.2f);
        bathroomLight.SetActive(false);
    }
}
