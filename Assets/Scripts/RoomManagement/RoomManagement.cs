using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManagement : MonoBehaviour
{
    [Header("room")]
    [SerializeField] private GameObject _roomFloor;
    [SerializeField] private int _roomCount;
    [SerializeField] private Color _startRoomColor;
    [SerializeField] private Color _endRoomColor;
    [SerializeField] private Room _startRoom;
    [SerializeField] private Room _endRoom;

    [Header("creat room")]
    [SerializeField] private Transform _generatorTransform;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private LayerMask _roomLayer;    
    [SerializeField] private RoomDirection _roomDirection;
    public List<Room> _roomList = new();
    [SerializeField] private WallType _wallTpyes;

    [Header("end room")]
    [SerializeField] private int _maxStepNumber;
    List<Room> _farthestRoomList = new();// farthest room list
    List<Room> _secondFarthestRoomList = new();// second farthest room list
    List<Room> _oneDoorRoomList = new();
    
    [Header("enemy")]
    [SerializeField] private GameObject[] _enemyArray;

    private enum RoomDirection
    {   
        Up,
        Down,
        Left,
        Right 
    };

    // Start is called before the first frame update
    void Awake()
    {
        // generate room 
        GenerateRoomLayout();
        // set up every room's wall
        SetUpRoomWall();
        // set up start room's and end room's color
        SetupStartAndEndRooms();
        // spawn enemy
        SpawnEnemy();
    }

    /// <summary>
    /// generate room
    /// </summary>
    private void GenerateRoomLayout()
    {
        _generatorTransform.position = Vector3.zero;
        
        for (int i = 0; i < _roomCount; i++)
        {
            Room room = Instantiate(_roomFloor, _generatorTransform.position, Quaternion.identity).GetComponent<Room>();
            room.roomId = i;
            _roomList.Add(room);

            MoveGeneratorToNextPosition();
        }
    }

    /// <summary>
    /// find next generate position
    /// </summary>
    /// <returns></returns>
    private void MoveGeneratorToNextPosition()
    {
        do
        {
            MoveGeneratorRandomly();
        }
        while (Physics2D.OverlapCircle(_generatorTransform.position, 0.2f, _roomLayer));
        //return _generatorTransform.position;
    }

    /// <summary>
    /// move randomly in one direction and determine if a room can be generated
    /// </summary>
    private void MoveGeneratorRandomly()
    {
        _roomDirection = (RoomDirection)Random.Range(0, 4);
        switch (_roomDirection)
        {
            case RoomDirection.Up:
                {
                    _generatorTransform.position += new Vector3(0, _yOffset, 0);
                    break;
                }
            case RoomDirection.Down:
                {
                    _generatorTransform.position += new Vector3(0, -_yOffset, 0);
                    break;
                }
            case RoomDirection.Left:
                {
                    _generatorTransform.position += new Vector3(-_xOffset, 0, 0);
                    break;
                }
            case RoomDirection.Right:
                {
                    _generatorTransform.position += new Vector3(_xOffset, 0, 0);
                    break;
                }
        }
    }

    /// <summary>
    /// set up room wall
    /// </summary>
    private void SetUpRoomWall()
    {
        foreach (var room in _roomList)
        {
            // set up room's wall by neighbor
            room.GetRoomData(room.transform.position, _xOffset, _yOffset, _roomLayer);
            switch (room.neighborNumber)
            {
                case 1:
                    {
                        if (room.hasNeighborUp)
                            Instantiate(_wallTpyes.WallU, room.transform.position ,Quaternion.identity);
                        if (room.hasNeighborRight)
                            Instantiate(_wallTpyes.WallR, room.transform.position, Quaternion.identity);
                        if (room.hasNeighborDown)
                            Instantiate(_wallTpyes.WallD, room.transform.position, Quaternion.identity);
                        if (room.hasNeighborLeft)
                            Instantiate(_wallTpyes.WallL, room.transform.position, Quaternion.identity);
                        break;
                    }

                case 2:
                    {
                        if (room.hasNeighborUp && room.hasNeighborRight)
                            Instantiate(_wallTpyes.WallUR, room.transform.position, Quaternion.identity);
                        if (room.hasNeighborUp && room.hasNeighborDown)
                            Instantiate(_wallTpyes.WallUD, room.transform.position, Quaternion.identity);
                        if (room.hasNeighborUp && room.hasNeighborLeft)
                            Instantiate(_wallTpyes.WallUL, room.transform.position, Quaternion.identity);
                        if (room.hasNeighborRight && room.hasNeighborDown)
                            Instantiate(_wallTpyes.WallRD, room.transform.position, Quaternion.identity);
                        if (room.hasNeighborRight && room.hasNeighborLeft)
                            Instantiate(_wallTpyes.WallRL, room.transform.position, Quaternion.identity);
                        if (room.hasNeighborDown && room.hasNeighborLeft)
                            Instantiate(_wallTpyes.WallDL, room.transform.position, Quaternion.identity);
                        break;
                    }

                case 3:
                    {
                        if (room.hasNeighborUp && room.hasNeighborRight && room.hasNeighborDown)
                            Instantiate(_wallTpyes.WallURD, room.transform.position, Quaternion.identity);
                        if(room.hasNeighborUp && room.hasNeighborRight && room.hasNeighborLeft)
                            Instantiate(_wallTpyes.WallURL, room.transform.position, Quaternion.identity);
                        if (room.hasNeighborRight && room.hasNeighborDown && room.hasNeighborLeft)
                            Instantiate(_wallTpyes.WallRDL, room.transform.position, Quaternion.identity);
                        if (room.hasNeighborDown && room.hasNeighborLeft && room.hasNeighborUp)
                            Instantiate(_wallTpyes.WallDLU, room.transform.position, Quaternion.identity);
                        break;
                    }

                case 4:
                    {
                        if (room.hasNeighborUp && room.hasNeighborRight && room.hasNeighborDown && room.hasNeighborLeft)
                        Instantiate(_wallTpyes.WallURDL, room.transform.position, Quaternion.identity);
                        break;
                    }
            }
        }
    }

    /// <summary>
    /// find the end room 
    /// </summary>
    private void SetupStartAndEndRooms()
    {
        if (_roomList.Count == 0) 
        {
            return;
        }

        // start room
        _startRoom = _roomList[0];
        _startRoom.GetComponent<SpriteRenderer>().color = _startRoomColor;

        // find every room's distance from start room and set up _maxStepNumber
        for (int i = 0; i < _roomList.Count; i++)
        {
            if (_roomList[i].stepToStartRoom > _maxStepNumber)
            {
                _maxStepNumber = _roomList[i].stepToStartRoom;
            }
        }

        // find farthest and second farthest rooms
        foreach (var room in _roomList)
        {
            // farthest room list
            if (room.stepToStartRoom == _maxStepNumber)
            {
                _farthestRoomList.Add(room);
            }
            // second farthest room list
            if (room.stepToStartRoom == _maxStepNumber - 1)
            {
                _secondFarthestRoomList.Add(room);
            }
        }
        
        // find one door room in farthest and second farthest room list
        for (int i = 0; i <_farthestRoomList.Count; i++)
        {
            if (_farthestRoomList[i].GetComponent<Room>().neighborNumber == 1)
            {
                _oneDoorRoomList.Add(_farthestRoomList[i]);      
            }         
        }
        for (int i = 0; i < _secondFarthestRoomList.Count; i++)
        {           
            if (_secondFarthestRoomList[i].GetComponent<Room>().neighborNumber == 1)
            {
                _oneDoorRoomList.Add(_secondFarthestRoomList[i]);
            }
        }

        // decide the end room
        if (_oneDoorRoomList.Count != 0)
        {
            _endRoom = _oneDoorRoomList[Random.Range(0, _oneDoorRoomList.Count)];
        }
        else
        {
            _endRoom = _farthestRoomList[Random.Range(0, _farthestRoomList.Count)];
        }

        _endRoom.GetComponent<SpriteRenderer>().color = _endRoomColor;
    }

    /// <summary>
    /// spawn enemy
    /// </summary>
    private void SpawnEnemy()
    {
        // generate enemy in every room
        foreach (var room in _roomList)
        {
            if (_enemyArray.Length > 0)
            {
                var generateEnemyNumber = Random.Range(1, 6);
                for(int i = 0; i< generateEnemyNumber; i++)
                {
                    var enemy = Instantiate(_enemyArray[Random.Range(0, 3)], (Vector2)room.transform.position + Random.insideUnitCircle * 2f, Quaternion.identity);
                    if(enemy.TryGetComponent<EnemyBase>(out var enemybase))
                    {
                        enemybase.enemyRoomId = room.roomId;
                    }
                    
                    room.enemyCount++; 
                }
            }
        }
        
        // boss in end room 
        if (_endRoom != null)
        {
            var boss = Instantiate(_enemyArray[3], (Vector2)_endRoom.transform.position + Random.insideUnitCircle * 2f, Quaternion.identity);
            if(boss.TryGetComponent<EnemyBase>(out var enemybase))
            {
                enemybase.enemyRoomId = _endRoom.roomId;
            }
            
            _endRoom.enemyCount++; 
        }
    }
}

[System.Serializable]
public class WallType
{
    public GameObject WallU, WallR, WallD, WallL,
                    WallUR, WallUD, WallUL, WallRD, WallRL, WallDL,
                    WallURD, WallURL, WallRDL, WallDLU,
                    WallURDL;
}
