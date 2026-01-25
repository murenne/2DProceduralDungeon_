using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : MonoBehaviour
{
    [SerializeField] private GameObject _healingEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            playerController.playerCurrentHP += 5;
            Instantiate(_healingEffect, playerController.gameObject.transform);
            Destroy(gameObject);
        }
    }
}
