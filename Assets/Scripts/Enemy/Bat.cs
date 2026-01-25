using System.Collections;
using UnityEngine;

public class Bat : EnemyBase, IGetDamage
{
    [Header("component")]
    [SerializeField] private SpriteRenderer _sp;

    [Header("attack")]
    [SerializeField] private float attackRange;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private int _physicsDamage;

    [Header("Hurt")]
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
        _currentHP = _maxHP;
        _sp = GetComponent<SpriteRenderer>();
        _roomManagement = GameObject.Find("RoomManagement").GetComponent<RoomManagement>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        FollowPlayer();

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
    /// emeny follow player
    /// </summary>
    private void FollowPlayer()
    {
        // follow player when distance is less than attack range
        if (Vector2.Distance(transform.position, _playerTransform.position) <= attackRange)
        {
            // enemy move to player
            transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _moveSpeed * Time.deltaTime);
            
            // enemy flip
            gameObject.transform.localScale = _playerTransform.position.x > transform.position.x ? new Vector3(1, 1, 1):new Vector3(-1, 1, 1);
        }
    }

    /// <summary>
    /// bat get damage
    /// </summary>
    /// <param name="_amount"></param>
    public void GetDamage(int _amount)
    {
        // bat get hurt
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().AddExp(8);

            // bat destroy
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            // drop item
            var temp = Random.Range(1, 100);
            if (temp <= 5)
            {
                Instantiate(_dropItems[0], transform.position, Quaternion.identity);
            }
            else if (temp <= 20)
            {
                Instantiate(_dropItems[1], transform.position, Quaternion.identity);
            }
            else if (temp <= 50)
            {
                Instantiate(_dropItems[2], transform.position, Quaternion.identity);
            }

        }
    }

    /// <summary>
    /// bat hurt effect
    /// </summary>
    private void HurtEffect()
    {
        _sp.material.SetFloat("_FlashAmount", 1);
        _hurtFlashEffectTimer = _hurtFlashEffectTime;
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
