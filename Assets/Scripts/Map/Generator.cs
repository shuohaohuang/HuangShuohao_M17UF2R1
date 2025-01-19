using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{
    [SerializeField]
    private Tilemap terrainTilemap;

    [SerializeField]
    private Tilemap obstacleTilemap;

    [SerializeField]
    private TileBase terrainTile;

    [SerializeField]
    private TileBase obstacleTile;

    [SerializeField]
    private TileBase wallTile;

    [SerializeField]
    private int width;

    [SerializeField]
    private int height;

    [SerializeField]
    [Range(0, 1)]
    private float specialRoomsRate;

    [SerializeField]
    private GameObject spawnPoint;

    [SerializeField]
    private GameObject portal;

    [SerializeField]
    private List<GameObject> specialRooms = new();

    [SerializeField]
    private List<RoomBehavior> enemiesRoom = new();

    public List<GameObject> enemies;

    public void GenerateRooms(HashSet<Vector2Int> roomsPositions)
    {
        List<Vector2Int> randomPortal = new List<Vector2Int>(roomsPositions);
        Vector2Int portal;
        bool isSpecialRoom = false;
        do
        {
            portal = randomPortal[UnityEngine.Random.Range(0, randomPortal.Count)];
        } while (portal.Equals(new(0, 0)));

        foreach (Vector2Int position in roomsPositions)
        {
            // obstacleMap = new bool[width, height];
            Vector2Int TileOffset = new(position.x * width, position.y * height);
            Vector3 RoomOffset = new(
                position.x * width + width / 2f,
                position.y * height + height / 2f,
                0
            );

            Vector3 CamOffSet = new(
                position.x * width + width / 2f,
                position.y * height + height / 2f,
                -10
            );

            GridLayout grid = GetComponentInParent<GridLayout>();

            GameObject roomObject = new GameObject($"Room_{position.x}_{position.y}");
            roomObject.transform.position = RoomOffset;
            roomObject.transform.SetParent(transform);

            BoxCollider2D roomCamCollider = roomObject.AddComponent<BoxCollider2D>();
            RoomCam roomScrip = roomObject.AddComponent<RoomCam>();
            roomScrip.RoomId = position;

            roomCamCollider.isTrigger = true;
            roomCamCollider.size = new Vector2(width - 1, height - 1);

            GameObject roomBehavoir = new GameObject($"Room_{position.x}_{position.y}_Behavior");

            roomBehavoir.transform.position = RoomOffset;
            roomBehavoir.transform.SetParent(roomObject.transform);

            BoxCollider2D roombCollider = roomBehavoir.AddComponent<BoxCollider2D>();
            RoomBehavior roomBehavoirScrip = roomBehavoir.AddComponent<RoomBehavior>();
            roombCollider.isTrigger = true;
            roombCollider.size = new Vector2(width - 3.5f, height - 3.5f);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    terrainTilemap.SetTile(
                        new Vector3Int(x + TileOffset.x, y + TileOffset.y, 0),
                        terrainTile
                    );

                    if (!(x < 2 || x >= width - 2 || y < 2 || y >= height - 2))
                    {
                        roomBehavoirScrip.walkablePositions.Add(
                            new Vector3Int(x + TileOffset.x, y + TileOffset.y, 0)
                        );
                    }
                }
            }
            if (position.Equals(new(0, 0)))
            {
                Camera.main.transform.position = CamOffSet;
                Instantiate(spawnPoint, RoomOffset, quaternion.identity);
            }
            else if (position.Equals(portal))
            {
                Instantiate(this.portal, RoomOffset, quaternion.identity);
            }
            else
            {
                isSpecialRoom = UnityEngine.Random.Range(0f, 1f) < specialRoomsRate;
                if (isSpecialRoom)
                {
                    Instantiate(
                        specialRooms[UnityEngine.Random.Range(0, specialRooms.Count)],
                        RoomOffset,
                        quaternion.identity
                    );
                }
                else
                {
                    enemiesRoom.Add(roomBehavoirScrip);
                }
            }

            foreach (Vector3Int door in CreateWallsWithGaps(roomsPositions, position, TileOffset))
            {
                roomBehavoirScrip.Doors.Add(door);
                roomBehavoirScrip.CloseEvent += Close;
                roomBehavoirScrip.OpenEvent += Open;
            }
        }
    }

    private HashSet<Vector3Int> CreateWallsWithGaps(
        HashSet<Vector2Int> roomPositions,
        Vector2Int currentRoom,
        Vector2Int offset
    )
    {
        HashSet<Vector3Int> DoorPositions = new HashSet<Vector3Int>();

        int midX = width / 2;
        int midY = height / 2;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    if (
                        (
                            x >= midX - 1
                            && x <= midX + 1
                            && y == 0
                            && roomPositions.Contains(currentRoom + Vector2Int.down)
                        )
                        || (
                            x >= midX - 1
                            && x <= midX + 1
                            && y == height - 1
                            && roomPositions.Contains(currentRoom + Vector2Int.up)
                        )
                        || (
                            y >= midY - 1
                            && y <= midY + 1
                            && x == 0
                            && roomPositions.Contains(currentRoom + Vector2Int.left)
                        )
                        || (
                            y >= midY - 1
                            && y <= midY + 1
                            && x == width - 1
                            && roomPositions.Contains(currentRoom + Vector2Int.right)
                        )
                    )
                    {
                        DoorPositions.Add(new Vector3Int(x + offset.x, y + offset.y, 0));
                        continue;
                    }

                    obstacleTilemap.SetTile(
                        new Vector3Int(x + offset.x, y + offset.y, 0),
                        wallTile
                    );
                }
            }
        }

        return DoorPositions;
    }

    public void GenerateRoomsEnemies()
    {
        foreach (RoomBehavior room in enemiesRoom)
        {
            room.GenerateEnemies(enemies);
        }
    }

    public void Close(HashSet<Vector3Int> doors)
    {
        foreach (Vector3Int door in doors)
            obstacleTilemap.SetTile(door, wallTile);
    }

    public void Open(HashSet<Vector3Int> doors)
    {
        foreach (Vector3Int door in doors)
            obstacleTilemap.SetTile(door, null);
    }

    private void OnDestroy()
    {
        foreach (RoomBehavior room in enemiesRoom)
        {
            room.OpenEvent -= Open;
            room.CloseEvent -= Open;
        }
    }
}
