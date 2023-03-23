using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour
{
    public string text;
    [SerializeField] TextGenerator textGenerator;

    private void OnTriggerEnter(Collider other)
    {
      //
      //
      if (other.gameObject.CompareTag("Player"))  textGenerator.ShowMsg(text, 0f);
    }
}
