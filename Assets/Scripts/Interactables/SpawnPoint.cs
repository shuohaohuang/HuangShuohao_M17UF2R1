using Unity.Mathematics;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    GameObject player;

    void Start()
    {
        if (PC.instance != null)
        {
            PC.instance.transform.position = transform.position;
        }
        else
        {
            Instantiate(player, transform.position, quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update() { }
}
