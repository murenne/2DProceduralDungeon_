using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Wizard : EnemyBase, IGetDamage
{
    [Header("component")]
    private Animator _animator;
    private SpriteRenderer _sp;

    [Header("attack")]
    [SerializeField] private bool _canMove;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _physicsDamage;
    [SerializeField] private float _attackIntervalTime;
    private float _attackIntervalTimer;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _bulletPrefab;

    [Header("hurt")]
    [SerializeField] private float _hurtFlashEffectTime;
    private float _hurtFlashEffectTimer;
    [SerializeField] private float _attackedIntervalTime;
    private float _attackedIntervalTimer;
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private GameObject[] _dropItems;
    public bool HasAttacked { get; private set; }
    
    [Header("room manager")]
    private RoomManagement _roomManagement;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _sp = GetComponent<SpriteRenderer>();
        _currentHP = _maxHP;
        _attackIntervalTimer = _attackIntervalTime;
        _attackedIntervalTimer = _attackedIntervalTime;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _roomManagement = GameObject.Find("RoomManagement").GetComponent<RoomManagement>();
    }

    private void Update()
    {
        if(_canMove)
        {
            Move();
        }

        _hurtFlashEffectTimer = _hurtFlashEffectTimer > 0 ? _hurtFlashEffectTimer - Time.deltaTime: 0;
        if (_hurtFlashEffectTimer <= 0)
        {
            _sp.material.SetFloat("_FlashAmount", 0);
        }

        _attackIntervalTimer = _attackIntervalTimer > 0 ? _attackIntervalTimer - Time.deltaTime : 0;
        if (_attackIntervalTimer <= 0)
        {
            _canMove = false;
            _animator.SetTrigger("Attack");
        }
        
        _attackedIntervalTimer = _attackedIntervalTimer > 0 ? _attackedIntervalTimer - Time.deltaTime : 0;
        if(_attackedIntervalTimer <= 0 )
        {
            HasAttacked = false; 
        }
    }

    private void Move()
    {
        if (Vector2.Distance(transform.position, _playerTransform.position) <= _attackRange)
        {
            // wizard move to player
            transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _moveSpeed * Time.deltaTime);

            // wizard flip
            gameObject.transform.eulerAngles = _playerTransform.position.x > transform.position.x ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0);
        }
    }

    public void GetDamage(int _amount)
    {
        // wizard get hurt
        _currentHP -= _amount;
        HurtEffect();

        // damage interval
        HasAttacked = true;
        _attackedIntervalTimer = _attackedIntervalTime;

        if (_currentHP <= 0)
        {
            // calculate enemy count in this room
            _roomManagement._roomList[enemyRoomId].GetComponent<Room>().EnemyDefeated();

            // player get exp
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().AddExp(15);

            // wizard destroy
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            // drop items
            var temp = Random.Range(1, 100);
            if (temp >= 50)
            {
                Instantiate(_dropItems[0], transform.position, Quaternion.identity);
            }
            else if (temp > 10 && temp <= 20)
            {
                Instantiate(_dropItems[1], transform.position, Quaternion.identity);
            }
            else if (temp > 20 && temp <= 30)
            {
                Instantiate(_dropItems[2], transform.position, Quaternion.identity);
            }
        }
    }

    private void HurtEffect()
    {
        _sp.material.SetFloat("_FlashAmount", 1);
        _hurtFlashEffectTimer = _hurtFlashEffectTime;
    }

    IEnumerator isAttackCo()
    {
        yield return new WaitForSeconds(0.2f);
        HasAttacked = false;
    }

    /// <summary>
    /// attack ( animation event )
    /// </summary>
    public void AnimAttack()
    {
        for (var i = 0; i < 4; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<WizardBullet>().bulletID = i;
        }

        _canMove = true;
        _attackIntervalTimer = _attackIntervalTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            // calculatie collision damage 
            int damage = Mathf.Max(1, _physicsDamage - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().def);

            // execute
            playerController.playerCurrentHP -= damage;
            playerController.GetHurtAnim();

            // camera shake
            FindObjectOfType<CameraControl>().SetCameraShakeAmplify(0.2f);
        }
    }
}
