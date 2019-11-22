using System;

[Serializable]
public class PlayerStatData
{
    public float currentLevel;
    public float currentXP;

    public PlayerStatData(float currentLevel, float currentXP)
    {
        this.currentLevel = currentLevel;
        this.currentXP = currentXP;
    }
}
