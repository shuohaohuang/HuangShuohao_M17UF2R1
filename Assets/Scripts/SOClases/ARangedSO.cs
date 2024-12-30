using UnityEngine;

[CreateAssetMenu(fileName = "ARangedSO", menuName = "Scriptable Objects/ARangedSO")]
public class ARangedSO : ScriptableObject
{
    public int price;
    public int satiety;
    public float cd;
    public float projectileSpeed;
    public float minDistance;
    public string alias;
    public AudioClip shotAudio;
    public GameObject projectile;
}
