using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMelee : MonoBehaviour
{
    [SerializeField] private int _collisionDamage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            // calculatie collision damage 
            int damage = Mathf.Max(1, _collisionDamage - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().def);

            // execute
            playerController.playerCurrentHP -= damage;
            playerController.GetHurtAnim();

            // camera shake
            FindObjectOfType<CameraControl>().SetCameraShakeAmplify(0.2f);
        }
    }
}
