using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpriteBullet : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _lifeTime;
    private float _lifeTimer;
    [SerializeField] private int magicDamage;
    [SerializeField] private GameObject _bulletDestroyEffect;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _lifeTimer = 0;
    }

    private void Update()
    {
        Track();

        _lifeTimer += Time.deltaTime;
        if(_lifeTimer >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// tracking to player
    /// </summary>
    private void Track()
    {
        transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// damage
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            // calculatie collision damage 
            int damage = Mathf.Max(1, magicDamage - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().adf);

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
