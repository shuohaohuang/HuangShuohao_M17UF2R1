using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    Collider2D roomCollider2D;

    public HashSet<Vector3Int> doors = new();
    HashSet<AEnemy> aEnemies = new();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Equals(other.gameObject.GetComponent<PlayerInputs>(), null))
        {
            transform.parent.parent.GetComponent<Generator>().Close(doors);
        }

        if (other.gameObject.GetComponent<AEnemy>() is AEnemy enemy)
        {
            aEnemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<AEnemy>() is AEnemy enemy)
        {
            aEnemies.Remove(enemy);
        }
    }

    private void Update()
    {
        if (aEnemies.Count == 0)
        {
            transform.parent.parent.GetComponent<Generator>().Open(doors);
        }
    }
}
