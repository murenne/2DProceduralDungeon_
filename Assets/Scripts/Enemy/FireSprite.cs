using System.Collections;
using UnityEngine;

public class EnemyControl : EnemyBase, IGetDamage
{
    [Header("component")]
    private SpriteRenderer _sp;

    [Header("attack")]
    [SerializeField] private float _attackRange;
    [SerializeField] private int _physicsDamage;
    [SerializeField] private float _shootIntervalTime;
    private float _shootIntervalTimer;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _fireMuzzle;

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

    // Start is called before the first frame update
    void Start()
    {
        _sp = GetComponent<SpriteRenderer>();
        _currentHP = _maxHP;
        _attackedIntervalTimer = _attackedIntervalTime;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _roomManagement = GameObject.Find("RoomManagement").GetComponent<RoomManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, _playerTransform.position) <= _attackRange)
        {
            // move to player
            transform.position = Vector2.MoveTowards(transform.position, _playerTransform.position, _moveSpeed * Time.deltaTime);
            
            // flip
            gameObject.transform.localScale = _playerTransform.position.x > transform.position.x ? new Vector3(1, 1, 1):new Vector3(-1, 1, 1);

            // shoot logic
            _shootIntervalTimer -= Time.fixedDeltaTime;
            if (_shootIntervalTimer <= 0)
            {
                Instantiate(_bulletPrefab, _fireMuzzle.position, Quaternion.identity);
                _shootIntervalTimer = _shootIntervalTime;
            }
        }
        else
        {
            // cannot find player, reset shoot interval timer
            _shootIntervalTimer = _shootIntervalTime;
        }        
        
        _hurtFlashEffectTimer = _hurtFlashEffectTimer > 0 ? _hurtFlashEffectTimer - Time.deltaTime : 0;
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

    private void HurtEffect()
    {
        _sp.material.SetFloat("_FlashAmount", 1);
        _shootIntervalTimer = _shootIntervalTime;
    }

    public void GetDamage(int _amount)
    {
        // fire sprite get hurt
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
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().AddExp(12);

            // fire sprite destroy
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);

            // drop items
            var temp = Random.Range(1, 100);
            if (temp <= 5)
            {
                Instantiate(_dropItems[0], transform.position, Quaternion.identity);
            }
            else if (temp > 5 && temp <= 30)
            {
                Instantiate(_dropItems[1], transform.position, Quaternion.identity);
            }
            else if (temp > 30 && temp <= 35)
            {
                Instantiate(_dropItems[2], transform.position, Quaternion.identity);
            }
        }
    }
}



