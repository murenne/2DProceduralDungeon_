using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicExecute : MonoBehaviour
{
    private int _attackDamage;
    [SerializeField] private GameObject _damageCanvas;

    [Header("damage range")]
    [SerializeField] private float _minDamage;
    [SerializeField] private float _maxDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            IGetDamage enemy = collision.gameObject.GetComponent<IGetDamage>();

            _attackDamage = (int)Random.Range(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().ats - _minDamage, GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().ats + _maxDamage);

            if (!enemy.HasAttacked)
            {
                enemy.GetDamage(_attackDamage);
                FindObjectOfType<CameraControl>().SetCameraShakeAmplify(0.4f);
                DamageNumber damagenumberCanvas = Instantiate(_damageCanvas, collision.transform.position, Quaternion.identity).GetComponent<DamageNumber>();
                damagenumberCanvas.ShowUIDamage(Mathf.RoundToInt(_attackDamage));

                Vector2 difference = collision.transform.position - transform.position;
                difference.Normalize();
                collision.transform.position = new Vector2(collision.transform.position.x + difference.x / 2, collision.transform.position.y + difference.y / 2);
            }
        }
    }
}
