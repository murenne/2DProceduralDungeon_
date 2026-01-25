using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSceneEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            FindObjectOfType<SceneFader>().FadeTo();
        }
    }
}
