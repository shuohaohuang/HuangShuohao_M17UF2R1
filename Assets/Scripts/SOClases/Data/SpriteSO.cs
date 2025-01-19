using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteSO", menuName = "Scriptable Objects/SpriteSO")]
public class SpriteSO : ScriptableObject
{
    public List<Sprite> sprites;
}
