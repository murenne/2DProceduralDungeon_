using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicAttack : MonoBehaviour
{
    [SerializeField] private GameObject _magicAttactAnimation;
    public  float magicAttackTime;
    public  float timer;

    // Start is called before the first frame update
    void Start()
    {
        magicAttackTime = timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (magicAttackTime >= timer)
        {
            magicAttackTime = timer;

            if (Input.GetMouseButtonDown(1))
            {
                MagicAttack();
                magicAttackTime = 0;
            }
        }
        else
        {
            magicAttackTime += Time.fixedDeltaTime;
        }
    }

    /// <summary>
    /// magic attack in mouse position
    /// </summary>
    private void MagicAttack()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var magicPositiin = new Vector3(mousePosition.x, mousePosition.y, mousePosition.z + 10);
        Instantiate(_magicAttactAnimation, magicPositiin, Quaternion.identity);
    }
}
