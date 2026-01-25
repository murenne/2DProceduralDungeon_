using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // singleton
    public static PlayerController instance;

    [Header("player status")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _moveH;
    [SerializeField] private float _moveV;
    [SerializeField] private float _moveSpeed;

    [Header("player UIֵ")]
    public float playerMaxHP;
    public float playerCurrentHP;

    private void Awake()
    {
        // singleton
        if (instance == null)   
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        
        // should not destroy when load new scene
        DontDestroyOnLoad(gameObject);
        playerCurrentHP = playerMaxHP;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // input for moveing and flipping
        HandleInput();

        // limit maximum HP
        if (playerCurrentHP > playerMaxHP)
        {
            playerCurrentHP = playerMaxHP;
        }
    }

    void FixedUpdate()
    {
        // smooth moving for each computer
        _rigidbody.velocity = new Vector2(_moveH, _moveV);
    }

    /// <summary>
    /// handle player input
    /// </summary>
    void HandleInput()
    {
        //player movement
        _moveH = Input.GetAxisRaw("Horizontal") * _moveSpeed;
        _moveV = Input.GetAxisRaw("Vertical") * _moveSpeed;

        // player flip based on mouse mouse position
        if (transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void GetHurtAnim()
    {
        _animator.SetBool("gethurt", true);
    }

    void AnimHurtStop()
    {
        _animator.SetBool("gethurt", false);
    }
}
