using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashExecute : MonoBehaviour
{
    [SerializeField] private int _atkDamage;
    [SerializeField] private GameObject _damageCanvas;
    [SerializeField] private float _minDamage;
    [SerializeField] private float _maxDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            IGetDamage enemy = collision.gameObject.GetComponent<IGetDamage>();

            _atkDamage = (int)Random.Range(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().atk - _minDamage , GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>().atk + _maxDamage);

            if (!enemy.HasAttacked)
            {
                enemy.GetDamage(_atkDamage);
                FindObjectOfType<CameraControl>().SetCameraShakeAmplify(0.4f);
                DamageNumber damagenumberCanvas = Instantiate(_damageCanvas, collision.transform.position, Quaternion.identity).GetComponent<DamageNumber>();
                damagenumberCanvas.ShowUIDamage(Mathf.RoundToInt(_atkDamage));

                Vector2 difference = collision.transform.position - transform.position;
                difference.Normalize();
                collision.transform.position = new Vector2(collision.transform.position.x + difference.x/2, collision.transform.position.y + difference.y/2);
            }         
        }
    }

    public void AnimEndAttack()
    {
        gameObject.SetActive(false);
    }
}



