using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static  CameraControl Instance;

    [Header("Camera Shake")]
    [SerializeField] private Transform _targetRoom;
    [SerializeField] private float _cameraSpeed;
    private Vector3 _shakeActive;
    private float _shakeAmplify;

    // Start is called before the first frame update
    private  void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(_targetRoom != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_targetRoom.position.x, _targetRoom.position.y, transform.position.z), _cameraSpeed * Time.deltaTime);
        }
        
        if (_shakeAmplify > 0)
        {
            // shake amplify will get smaller
            _shakeActive = new Vector3(Random.Range(-_shakeAmplify, _shakeAmplify), Random.Range(-_shakeAmplify, _shakeAmplify), 0f);
            _shakeAmplify -= Time.deltaTime;
        }
        else
        {
            // room's center position
            _shakeActive = Vector3.zero;
        }

        transform.position += _shakeActive;
    }

    /// <summary>
    /// if change room then move camera to new room position
    /// </summary>
    /// <param name="newRoom"></param>
    public void ChangeRoom(Transform newRoom)
    {
        _targetRoom = newRoom;
    }

    /// <summary>
    /// set camera shake amplify
    /// </summary>
    /// <param name="_amount"></param>
    public void SetCameraShakeAmplify(float _amount)
    {
        _shakeAmplify = _amount;
    }
}

