using UnityEngine;

public class PlayerSlashAttack : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SlashAttack();
        }
    }

    /// <summary>
    /// Attack
    /// </summary>
    private void SlashAttack()
    {
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

        // slash rotation
        var difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        var rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;//Radius -> Degree
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
