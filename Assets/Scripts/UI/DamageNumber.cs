using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private Text _damageText;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _upSpeed;


    // Start is called before the first frame update
    void Start()
    {
        // wait _lifeTime and destroy itself
        Destroy(gameObject, _lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, _upSpeed * Time.fixedDeltaTime , 0);
    }

    public void ShowUIDamage(float _amount)
    {
        _damageText.text = _amount.ToString ();
    }
}
