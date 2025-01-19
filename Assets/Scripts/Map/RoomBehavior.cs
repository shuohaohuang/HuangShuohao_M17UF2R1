using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class RoomBehavior : MonoBehaviour
{
    Collider2D roomCollider2D;

    private HashSet<Vector3Int> doors = new();
    public List<Vector3Int> walkablePositions = new();

    public HashSet<AEnemy> aEnemies = new();

    public UnityAction<HashSet<Vector3Int>> CloseEvent;
    public UnityAction<HashSet<Vector3Int>> OpenEvent;

    public HashSet<Vector3Int> Doors
    {
        get => doors;
        set => doors = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<AEnemy>() is AEnemy enemy)
        {
            aEnemies.Add(enemy);
            enemy.propiety = this;
        }

        if (!Equals(other.gameObject.GetComponent<PlayerInputs>(), null))
        {
            foreach (AEnemy enemyAgent in aEnemies)
            {
                enemyAgent.InitChase(other.gameObject.GetComponent<PC>());
            }
            if (aEnemies.Any(x => x.active == true))
                CloseEvent.Invoke(doors);
        }
    }

    public void GenerateEnemies(List<GameObject> enemies)
    {
        //enemy Generator
        for (int i = 0; i < UnityEngine.Random.Range(1, 5); i++)
        {
            Vector3 position = walkablePositions[
                UnityEngine.Random.Range(0, walkablePositions.Count)
            ];
            GameObject newEnemy = Instantiate(
                enemies[UnityEngine.Random.Range(0, enemies.Count)],
                position,
                quaternion.identity
            );
            aEnemies.Add(newEnemy.GetComponent<AEnemy>());
            newEnemy.GetComponent<AEnemy>().propiety = this;
        }
    }

    public void CheckEnemies()
    {
        if (aEnemies.All(x => x.active == false))
        {
            OpenEvent.Invoke(doors);
        }
    }
}
