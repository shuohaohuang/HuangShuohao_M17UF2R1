using System.Collections.Generic;
using System.Numerics;
using NavMeshPlus;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    int minRooms = 5;

    [SerializeField]
    int maxRooms = 11;
    int roomNums;

    [SerializeField]
    HashSet<Vector2Int> positionsSet = new() { new(0, 0) };

    [SerializeField]
    Generator generator;

    [SerializeField]
    NavMeshPlus.Components.NavMeshSurface navMeshSurface;

    private void Start()
    {
        roomNums = Random.Range(minRooms, maxRooms);
        GenerateMap();

        generator.GenerateRooms(positionsSet);

        navMeshSurface.BuildNavMesh();

        generator.GenerateRoomsEnemies();
    }

    void GenerateMap()
    {
        Vector2Int mainRoomPosition = Vector2Int.zero;

        List<Vector2Int> roomPositions = new() { mainRoomPosition };

        for (int i = 0; i < roomNums; i++)
        {
            Vector2Int newRoomPosition;
            do
            {
                Vector2Int MainRoom = roomPositions[Random.Range(0, roomPositions.Count)];
                Vector2Int direction = GetDirection();
                newRoomPosition = MainRoom + direction;
            } while (positionsSet.Contains(newRoomPosition));

            positionsSet.Add(newRoomPosition);
            roomPositions.Add(newRoomPosition);
        }
    }

    Vector2Int GetDirection()
    {
        Vector2Int[] directions =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right,
        };

        return directions[Random.Range(0, directions.Length)];
    }
}
