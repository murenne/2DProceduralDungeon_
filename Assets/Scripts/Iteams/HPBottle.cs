using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBottle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerItems>(out PlayerItems playerItems))
        {
            playerItems.healthPotionNum ++;
            Destroy(gameObject);
        }
    }
}
