using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    // Inspector Fields
    [Header("Player Stats Configuration")]
    [SerializeField] private Image xpBarImage;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private float xpStart = 100f;

    // Private Variables
    private float currentLevel;
    private float currentXP;
    private float xpAmountForNextLevel;

    private void Awake()
    {
        currentXP = 0;
        currentLevel = 1;
        xpAmountForNextLevel = xpStart;
        UpdateXPBar();
    }

    public void AddXP(float amount)
    {
        float newXP = currentXP + amount;

        if (newXP >= xpAmountForNextLevel)
        {
            LevelUp();
        }
        else
        {
            currentXP = newXP;
            UpdateXPBar();
        }
    }

    private void UpdateXPBar()
    {
        xpBarImage.fillAmount = currentXP / xpAmountForNextLevel;
        levelText.SetText(currentLevel.ToString());
    }

    private void LevelUp()
    {
        currentXP = 0;
        xpAmountForNextLevel = xpStart * currentLevel;
        currentLevel++;
        GameManager.Instance.PlayerRef.strengthStat.BaseValue += 0.5f;
        UpdateXPBar();
    }
}
