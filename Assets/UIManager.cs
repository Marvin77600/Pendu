using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup easyButton;
    [SerializeField]
    private CanvasGroup mediumButton;
    [SerializeField]
    private CanvasGroup hardButton;
    [SerializeField]
    private TMP_Text difficultyRect;
    [SerializeField]
    private TMP_Text wordRect;
    [SerializeField]
    private TMP_Text lettersRect;
    [SerializeField]
    private CanvasGroup retryRect;
    [SerializeField]
    private TMP_Text faultsRect;

    public void SetDifficultyText(string str)
    {
        difficultyRect.SetText(str);
    }

    public void SetWordText(string str)
    {
        wordRect.SetText(str);
    }

    public void SetLettersText(string str)
    {
        lettersRect.SetText(str);
    }

    public void SetFaultsText(string str)
    {
        faultsRect.SetText(str);
    }

    public string GetDifficultyText => difficultyRect.text;

    public string GetWordText => wordRect.text;

    public string GetLettersText => lettersRect.text;

    public string GetFaultsText => faultsRect.text;

    public void DisableDifficultyChoices()
    {
        CanvasGroup[] buttons1 = new CanvasGroup[]
        {
            easyButton,
            mediumButton,
            hardButton
        };
        DisableDisplay(buttons1, 1);
    }

    public void EnableDifficultyChoices()
    {
        CanvasGroup[] buttons1 = new CanvasGroup[]
        {
            easyButton,
            mediumButton,
            hardButton
        };
        EnableDisplay(buttons1, 1);
    }

    public void EnableRetryChoice()
    {
        CanvasGroup[] buttons1 = new CanvasGroup[]
        {
            retryRect
        };
        EnableDisplay(buttons1, 1);
    }

    public void DisableRetryChoice()
    {
        CanvasGroup[] buttons1 = new CanvasGroup[]
        {
            retryRect
        };
        DisableDisplay(buttons1, 1);
    }

    private void DisableDisplay(CanvasGroup[] _canvasGroups, float _duration)
    {
        StartCoroutine(DoButtonsFade(_canvasGroups, 1, 0, false, _duration));
    }

    private void EnableDisplay(CanvasGroup[] _canvasGroups, float _duration)
    {
        foreach (var item in _canvasGroups)
        {
            item.gameObject.SetActive(true);
        }
        StartCoroutine(DoButtonsFade(_canvasGroups, 0, 1, true, _duration));
    }

    private IEnumerator DoButtonsFade(CanvasGroup[] _canvasGroups, float _start, float _end, bool _flag, float _duration)
    {
        float counter = 0f;

        while (counter < _duration)
        {
            counter += Time.deltaTime;
            for (int i = 0; i < _canvasGroups.Length; i++)
            {
                _canvasGroups[i].alpha = Mathf.Lerp(_start, _end, counter / _duration);
            }
            yield return null;
        }
        for (int i = 0; i < _canvasGroups.Length; i++)
        {
            _canvasGroups[i].gameObject.SetActive(_flag);
        }
    }
}
