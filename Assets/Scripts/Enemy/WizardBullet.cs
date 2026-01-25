using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WizardBullet : MonoBehaviour
{
    [HideInInspector] public int bulletID;
    [SerializeField] private float _bulletSpeed = 3;
    [SerializeField] private int _magicDamage;
    [SerializeField] private GameObject _bulletDestroyEffect;

    public void Update()
    {
        switch (bulletID)
        {
            case 0:
                {
                    transform.Translate(-_bulletSpeed * Time.deltaTime, _bulletSpeed * Time.deltaTime, 0);
                    break;
                }
            case 1:
                {
                    transform.Translate(_bulletSpeed * Time.deltaTime, _bulletSpeed * Time.deltaTime, 0);
                    break;
                }
            case 2:
                {
                    transform.Translate(-_bulletSpeed * Time.deltaTime, -_bulletSpeed * Time.deltaTime, 0);
                    break;
                }
            case 3:
                {
                    transform.Translate(_bulletSpeed * Time.deltaTime, -_bulletSpeed * Time.deltaTime, 0);
                    break;
                }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Wall")
        {
            Instantiate(_bulletDestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if(other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            // calculatie collision damage 
            int damage = Mathf.Max(1, _magicDamage - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().adf);

            // execute
            playerController.playerCurrentHP -= damage;
            playerController.GetHurtAnim();

            // camera shake
            FindObjectOfType<CameraControl>().SetCameraShakeAmplify(0.2f);

            // attack effect
            Instantiate(_bulletDestroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
