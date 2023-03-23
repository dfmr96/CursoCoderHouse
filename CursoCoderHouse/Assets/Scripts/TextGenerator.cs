using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextGenerator : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] string[] textMsg;
    [SerializeField] float fxSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float minSpeed;

    private void OnEnable()
    {
        EventBus.Instance.onDoorClosed.AddListener(DoorClosed);
    }

    private void OnDisable()
    {
        EventBus.Instance.onDoorClosed.RemoveListener(DoorClosed);

    }
    private void Start()
    {
        StartCoroutine(PrintMsg(textMsg[0],2));
    }

    private void Update()
    {
        Debug.Log(Time.timeScale);

        if (Input.GetKey(KeyCode.Space)) fxSpeed = maxSpeed;
        if (Input.GetKeyUp(KeyCode.Space)) fxSpeed = minSpeed;
    }

    public IEnumerator PrintMsg(string msg, float delay)
    {
        text.gameObject.SetActive(true);

        text.SetText(string.Empty);
        Time.timeScale = 0;

        for (int i = 0; i < msg.Length; i++)
        {
            char character = msg[i];
            text.SetText(text.text + character);

            yield return new WaitForSecondsRealtime(fxSpeed);
        }


        //text.SetText(msg);
        Debug.Log("Esperando por pulsar E" + Time.timeScale);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        Debug.Log("He pulsado" + Time.timeScale);
        Time.timeScale = 1;
        text.SetText(string.Empty);
        text.gameObject.SetActive(false);
    }

    public void ShowMsg(string msg, float delay)
    {
        StartCoroutine(PrintMsg(msg, delay));
    }

    public void DoorClosed()
    {
        ShowMsg(textMsg[1], 1f);
    }
}
