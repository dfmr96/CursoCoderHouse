using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScreenBlinker : MonoBehaviour
{
    [SerializeField] Image bg;

    public void Blink()
    {
        bg.gameObject.SetActive(true);
        StartCoroutine(BlinkCO());
    }

    IEnumerator BlinkCO()
    {
        for (int i = 0; i < 7; i++)
        {
            bg.color = new Color(1, 1, 1, 1);
            yield return new WaitForSeconds(0.2f);
            bg.color = new Color(1, 1, 1, 0);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
