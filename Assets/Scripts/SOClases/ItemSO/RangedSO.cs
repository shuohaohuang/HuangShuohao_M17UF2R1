using UnityEngine;

[CreateAssetMenu(fileName = "ARangedSO", menuName = "Scriptable Objects/ARangedSO")]
public class RangedSO : ItemsSO
{
    public int satiety;
    public float projectileSpeed;
    public AProjectile projectile;
}
