using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    public static List<PlaceHolder> List = new();
    public string _name = "a";

    public Vector2 direccion;

    public static PlaceHolder FirstHolder()
    {
        if (List.Count == 0)
            return null;
        PlaceHolder aux = List[0];
        List.RemoveAt(0);

        return aux;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PLAYER"))
        {
            List.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PLAYER"))
            List.Remove(this);
    }
}
