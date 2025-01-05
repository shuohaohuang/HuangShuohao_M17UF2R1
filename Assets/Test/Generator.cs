using System.Collections.Generic;
using Unity.Mathematics;
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
    private int width; // Ancho del mapa

    [SerializeField]
    private int height; // Alto del mapa
    private bool[,] obstacleMap;

    [SerializeField]
    private GameObject SpawnPoint;

    [SerializeField]
    private GameObject Portal;

    public void GenerateRooms(HashSet<Vector2Int> roomsPositions)
    {
        List<Vector2Int> randomPortal = new List<Vector2Int>(roomsPositions);
        Vector2Int portal;
        do
        {
            portal = randomPortal[UnityEngine.Random.Range(0, randomPortal.Count)];
        } while (portal.Equals(new(0, 0)));

        foreach (Vector2Int position in roomsPositions)
        {
            obstacleMap = new bool[width, height];
            Vector2Int offset = new(position.x * width, position.y * height);

            GridLayout grid = GetComponentInParent<GridLayout>();

            GameObject roomObject = new GameObject($"Room_{position.x}_{position.y}");
            roomObject.transform.position = grid.CellToWorld((Vector3Int)offset);
            roomObject.transform.SetParent(transform);

            BoxCollider2D roomCollider = roomObject.AddComponent<BoxCollider2D>();
            RoomCam roomScrip = roomObject.AddComponent<RoomCam>();
            roomScrip.RoomId = position;
            roomScrip.CameraPosition = new(position.x * 19 + 9.5f, position.y * 11f + 5.5f, -10);
            ;

            roomCollider.isTrigger = true;
            roomCollider.size = new Vector2(width - 1, height - 1);
            roomCollider.offset = new Vector2(roomCollider.size.x / 2, roomCollider.size.y / 2);

            GameObject Roomb = new GameObject($"Room_{position.x}_{position.y}_Behavior");

            Roomb.transform.position = new(position.x * 19 + 9.5f, position.y * 11f + 5.5f, -10);
            Roomb.transform.SetParent(roomObject.transform);

            BoxCollider2D RoombCollider = Roomb.AddComponent<BoxCollider2D>();
            RoomBehavior RoombScrip = Roomb.AddComponent<RoomBehavior>();
            RoombCollider.isTrigger = true;
            RoombCollider.size = new Vector2(width - 2, height - 2);

            if (position.Equals(new(0, 0)))
            {
                Camera.main.transform.position = new(
                    position.x * 19 + 9.5f,
                    position.y * 9 + 5.5f,
                    -10
                );
                Instantiate(
                    SpawnPoint,
                    new(position.x * 19 + 9.5f, position.y * 11 + 5.5f, 0),
                    quaternion.identity
                );
            }
            if (position.Equals(portal))
            {
                Instantiate(
                    Portal,
                    new(position.x * 19 + 9.5f, position.y * 11 + 5.5f, 0),
                    quaternion.identity
                );
            }

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    terrainTilemap.SetTile(
                        new Vector3Int(x + offset.x, y + offset.y, 0),
                        terrainTile
                    );
                }
            }

            foreach (Vector3Int door in CreateWallsWithGaps(roomsPositions, position, offset))
            {
                RoombScrip.doors.Add(door);
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
}
