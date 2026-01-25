using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    [SerializeField] private GameObject _dialogBotton;
    [SerializeField] private GameObject _textWindowPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _dialogBotton.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _dialogBotton.SetActive(false);
    }

    private void Update()
    {
        if (_dialogBotton.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            _textWindowPanel.SetActive(true);
        }
    }

}
