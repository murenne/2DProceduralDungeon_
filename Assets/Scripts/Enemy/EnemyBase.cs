using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("status")]
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected int _maxHP;
    [SerializeField] protected int _currentHP;

    public int enemyRoomId;
}
