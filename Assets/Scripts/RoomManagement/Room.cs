using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject _doorLeft;
    [SerializeField] private GameObject _doorRight;
    [SerializeField] private GameObject _doorUp;
    [SerializeField] private GameObject _doorDown;

    [Header("neighbor")]
    public bool hasNeighborUp;
    public bool hasNeighborDown;
    public bool hasNeighborLeft;
    public bool hasNeighborRight;
    public int neighborNumber;

    [Header("distance")]
    public int stepToStartRoom;

    [Header("enemy")]
    public int roomId;
    public int enemyCount;

    public void GetRoomData(Vector3 roomPosition, float xOffset, float yOffset, LayerMask roomLayer)
    {
        // determine if there are rooms above, below, left, and right
        hasNeighborUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0, yOffset, 0), 0.2f, roomLayer);
        hasNeighborDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0, -yOffset, 0), 0.2f, roomLayer);
        hasNeighborLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffset, 0, 0), 0.2f, roomLayer);
        hasNeighborRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffset, 0, 0), 0.2f, roomLayer);

        // how far from start room
        stepToStartRoom = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / yOffset));

        // doors count
        neighborNumber = 0;
        neighborNumber += hasNeighborUp ? 1 : 0;
        neighborNumber += hasNeighborDown ? 1 : 0;
        neighborNumber += hasNeighborLeft ? 1 : 0;
        neighborNumber += hasNeighborRight ? 1 : 0;
    }

    /// <summary>
    /// go into this room
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // setup door
        if (collision.CompareTag("Player"))
        {
            // camera move
            CameraControl.Instance.ChangeRoom(transform);

            if (enemyCount > 0)
            {
                StartCoroutine(CloseDoorsWithDelay(0.5f));
            }
            else
            {
                SetDoorsActive(false);
            }
        }
    }

    private IEnumerator CloseDoorsWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (enemyCount > 0)
        {
            SetDoorsActive(true);
        }
    }

    /// <summary>
    ///  when enemy died
    /// </summary>
    public void EnemyDefeated()
    {
        enemyCount--;
        if (enemyCount <= 0)
        {
            SetDoorsActive(false);
        }
    }

    /// <summary>
    /// setup doors in this room
    /// </summary>
    /// <param name="isActive"></param>
    private void SetDoorsActive(bool isActive)
    {
        if (hasNeighborUp) _doorUp.SetActive(isActive);
        if (hasNeighborDown) _doorDown.SetActive(isActive);
        if (hasNeighborLeft) _doorLeft.SetActive(isActive);
        if (hasNeighborRight) _doorRight.SetActive(isActive);
    }
}




