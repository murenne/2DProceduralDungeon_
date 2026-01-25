using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    GameObject _mapSprite;

    private void OnEnable()
    {
        // room's map will be hidden at first time
        _mapSprite = transform.parent.GetChild(0).gameObject;
        _mapSprite.SetActive(false);
    }

    /// <summary>
    /// when player go into this room, then this room will appear in map
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _mapSprite.SetActive(true);
        }
    }

}
