using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextGenerator : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] string[] textMsg;

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

    public IEnumerator PrintMsg(string msg, float delay)
    {
        text.SetActive(true);
        text.GetComponent<TMP_Text>().text = msg;
        yield return new WaitForSeconds(delay);
        text.GetComponent<TMP_Text>().text = "";

        text.SetActive(false);
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
