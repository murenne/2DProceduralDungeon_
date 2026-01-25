using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : EnemyBase, IGetDamage
{
    [Header("component")]
    private Animator _animator;
    private SpriteRenderer _sp;

    [Header("attack")]
    [SerializeField] private float attackRange;
    [SerializeField] private int physicsDamage;
    [SerializeField] private Transform _playerTransform;

    [Header("hurt")]
    [SerializeField] private float _hurtFlashEffectTime; 
    private float _hurtFlashEffectTimer;
    [SerializeField] private float _attackedIntervalTime;
    private float _attackedIntervalTimer;
    [SerializeField] private GameObject _deathEffect;
    public bool HasAttacked { get; private set; }
    
    [Header("room manager")]
    private RoomManagement _roomManagement;

    // Start is called before the first frame update
    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _roomManagement = GameObject.Find("RoomManagement").GetComponent<RoomManagement>();
        _animator = GetComponent<Animator>();
        _attackedIntervalTimer = _attackedIntervalTime;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _currentHP = _maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        // follow player when distance is less than attack range
        if (Vector2.Distance(transform.position, _playerTransform.position) <= attackRange)
        {
            // knight move to player
            transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _moveSpeed * Time.deltaTime);

            // knight flip
            gameObject.transform.eulerAngles = _playerTransform.position.x > transform.position.x ? new Vector3(0, 180, 0) : new Vector3(0, 0, 0);

            //  knight attack
            if (Vector2.Distance(transform.position, _playerTransform.position) <= 2)
            {
                KngihtAttack();
            }
            else
            {
                _animator.SetBool("attack", false);
                _moveSpeed = 2;
            }
        }

        _hurtFlashEffectTimer = _hurtFlashEffectTimer > 0 ? _hurtFlashEffectTimer - Time.deltaTime: 0;
        if (_hurtFlashEffectTimer <= 0)
        {
            _sp.material.SetFloat("_FlashAmount", 0);
        }

        _attackedIntervalTimer = _attackedIntervalTimer > 0 ? _attackedIntervalTimer - Time.deltaTime : 0;
        if(_attackedIntervalTimer <= 0 )
        {
            HasAttacked = false; 
        }
    }

    /// <summary>
    /// collision damage
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<PlayerController>(out PlayerController playerController))
        {
            // calculatie collision damage 
            int damage = Mathf.Max(1, physicsDamage - GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().def);

            // execute
            playerController.playerCurrentHP -= damage;
            playerController.GetHurtAnim();
        }
    }

    /// <summary>
    /// knight attack
    /// </summary>
    public void KngihtAttack()
    {
        _animator.SetBool("attack", true);
        _moveSpeed = 0;
    }

    /// <summary>
    /// knight get damage
    /// </summary>
    /// <param name="_amount"></param>
    public void GetDamage(int _amount)
    {
        // knight hurt
        _currentHP -= _amount;
        HurtEffect();

        //  damage interval
        HasAttacked = true;
        _attackedIntervalTimer = _attackedIntervalTime;

        if (_currentHP <= 0)
        {
            // calculate enemy count in this room
            _roomManagement._roomList[enemyRoomId].GetComponent<Room>().EnemyDefeated();

            // player get exp
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().AddExp(100);

            // knight destroy
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// knight hurt effect
    /// </summary>
    private void HurtEffect()
    {
        _sp.material.SetFloat("_FlashAmount", 1);
        _hurtFlashEffectTimer = _hurtFlashEffectTime;
    }
}
