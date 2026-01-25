using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<PlayerItems>(out PlayerItems playerItems))
        {
            playerItems.goldCoinNum ++;
            Destroy(gameObject);
        }
    }
}
