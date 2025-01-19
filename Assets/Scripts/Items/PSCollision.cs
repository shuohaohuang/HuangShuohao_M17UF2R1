using UnityEngine;

public class PSCollision : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("ENEMY"))
        {
            other.GetComponent<AEnemy>().GetStun(0.5f, 0f);
        }
    }
}
