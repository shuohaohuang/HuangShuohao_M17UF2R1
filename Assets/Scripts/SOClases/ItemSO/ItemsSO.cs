using UnityEngine;

[CreateAssetMenu(fileName = "ItemsSO", menuName = "Scriptable Objects/ItemsSO")]
public class ItemsSO : ScriptableObject
{
    public int price;
    public float cd;
    public float minDistance;
    public string alias;
    public Sprite sprite;
    public AudioClip onUseAudio;
    public GameObject Item;
}
