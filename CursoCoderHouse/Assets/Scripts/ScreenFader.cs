using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public static ScreenFader Instance { get; private set; }
    private Image _fader;
    private bool _isBusy;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _fader = GetComponent<Image>();
    }
    public void FadeToBlack(float duration, Action finishedCallback)
    {
        if (_isBusy) return;
        StartCoroutine(CO_FadeToBlack(duration, finishedCallback));
    }

    public void FadeFromBlack(float duration, Action finishedCallback)
    {
        if (_isBusy) return;
        StartCoroutine(CO_FadeFromBlack(duration, finishedCallback));
    }

    private IEnumerator CO_FadeToBlack(float duration, Action finishedCallback)
    {
        _isBusy= true;
        while (_fader.color.a < 1)
        {
            _fader.color = new Color(0, 0, 0, _fader.color.a + (Time.deltaTime / duration));
            yield return null;
        }
        _fader.color = new Color(0, 0, 0, 1);
        _isBusy= false;
        finishedCallback?.Invoke();
        yield return null;
    }

    private IEnumerator CO_FadeFromBlack(float duration, Action finishedCallback)
    {
        _isBusy = true;
        while (_fader.color.a > 0)
        {
            _fader.color = new Color(0, 0, 0, _fader.color.a - (Time.deltaTime / duration));
            yield return null;
        }
        _fader.color = new Color(0, 0, 0, 0);
        _isBusy = false;
        finishedCallback?.Invoke();
        yield return null;
    }
}
