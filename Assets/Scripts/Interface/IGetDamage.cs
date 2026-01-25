using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetDamage 
{
    bool HasAttacked { get; }
    void GetDamage(int _amount);
}
