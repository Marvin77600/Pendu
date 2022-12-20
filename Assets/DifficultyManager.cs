using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField]
    private int minimumEasyLength;
    [SerializeField]
    private int maximumEasyLength;
    [SerializeField]
    private int minimumMediumLength;
    [SerializeField]
    private int maximumMediumLength;
    [SerializeField]
    private int minimumHardLength;
    [SerializeField]
    private int maximumHardLength;
    [SerializeField]
    private GameManager gameManager;
    [SerializeField]
    private UIManager UIManager;

    public void SetEasyDifficulty()
    {
        UIManager.SetDifficultyText("Facile");
        gameManager.GetWord(minimumEasyLength, maximumEasyLength);
    }

    public void SetMediumDifficulty()
    {
        UIManager.SetDifficultyText("Moyen");
        gameManager.GetWord(minimumMediumLength, maximumMediumLength);
    }

    public void SetHardDifficulty()
    {
        UIManager.SetDifficultyText("Difficile");
        gameManager.GetWord(minimumHardLength, maximumHardLength);
    }

    public int GetEasyMinLength => minimumEasyLength;
    public int GetEasyMaxLength => maximumEasyLength;

    public int GetMediumMinLength => minimumMediumLength;
    public int GetMediumMaxLength => maximumMediumLength;

    public int GetHardMinLength => minimumHardLength;
    public int GetHardMaxLength => maximumHardLength;
}
