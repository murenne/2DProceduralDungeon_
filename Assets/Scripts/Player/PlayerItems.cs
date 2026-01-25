using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [Header ("Health Potion")]
    public int healthPotionNum;
    [SerializeField] private GameObject _healingEffect;

    [Header("Coin")]
    public int goldCoinNum;

    // Update is called once per frame
    void Update()
    {
        UseHealthPotion();
    }

    /// <summary>
    /// use health potion
    /// </summary>
    void UseHealthPotion()
    {
        if (healthPotionNum > 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                healthPotionNum--;
                Instantiate(_healingEffect, transform);
                GetComponent<PlayerController>().playerCurrentHP += 40;
            }
        }
    }
}
