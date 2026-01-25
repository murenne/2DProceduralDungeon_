using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [Header("player level")]
    public int playerLevel;
    [SerializeField] private int maxLevel;

    [Header("player exp")]
    public int currentExp;
    public int[] nextLevelExp;

    [Header("player default status")]
    public int atk;
    public int def;
    public int ats;
    public int adf;

    [Header("player current status")]
    public int currentATK;
    public int currentDEF;
    public int currentATS;
    public int currentADF;

    [Header("player previous status")]
    public int previousATK;
    public int previousDEF;
    public int previousATS;
    public int previousADF;

    [Header("player effect")]
    [SerializeField] private GameObject _playerLevelUpEffect;

    private void Awake()
    {
        // exp setting
        nextLevelExp = new int[maxLevel + 1];
        nextLevelExp[1] = 300;

        for (int i = 2; i < maxLevel; i++)
        {
            nextLevelExp[i] = Mathf.RoundToInt(nextLevelExp[i - 1] * 1.1f);
        }

        // status setting
        currentATK = atk;
        currentDEF = def;
        currentATS = ats;
        currentADF = adf;

        previousATK = 0;
        previousDEF = 0;
        previousATS = 0;
        previousADF = 0;
    }

    /// <summary>
    /// add exp
    /// </summary>
    /// <param name="amount"></param>
    public void AddExp(int amount)  
    {
        // save previous status
        previousATK = atk;
        previousDEF = def;
        previousATS = ats;
        previousADF = adf;

        // add exp and level
        currentExp += amount;
        if (currentExp >= nextLevelExp[playerLevel] && playerLevel < maxLevel)
        {
            // level up logic
            LevelUp();

            // level up effect
            Instantiate(_playerLevelUpEffect, transform.position, Quaternion.identity);
        }

        // reach max level
        if (playerLevel >= maxLevel)
        {
            currentExp = 0;
        }
    }

    /// <summary>
    /// level up
    /// </summary>
    private void LevelUp()
    {
        // reset exp and level up
        currentExp -= nextLevelExp[playerLevel];
        playerLevel++;

        // update status
        atk = Mathf.CeilToInt(atk * 1.15f);
        def = Mathf.RoundToInt(def * 1.1f);
        ats = Mathf.RoundToInt(ats * 1.2f);
        adf += 2;

        // update current status
        currentATK = atk;
        currentDEF = def;
        currentATS = ats;
        currentADF = adf;

        // find and play UI animation
        FindObjectOfType<UIManagememt>().LevelupAnimation();
    }
}
