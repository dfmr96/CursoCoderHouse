using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextGenerator : MonoBehaviour
{
    [SerializeField] GameObject text;
    [SerializeField] string[] textMsg;
    private void Start()
    {
        StartCoroutine(PrintMsg(textMsg[0]));
    }

    public IEnumerator PrintMsg(string msg)
    {
        text.SetActive(true);
        text.GetComponent<TMP_Text>().text = msg;
        yield return new WaitForSeconds(5f);
        text.GetComponent<TMP_Text>().text = "";

        text.SetActive(false);
    }

    public void ShowMsg(string msg)
    {
        StartCoroutine(PrintMsg(msg));
    }
}
