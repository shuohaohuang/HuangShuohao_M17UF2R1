using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<PlaceHolder> PlaceHolders = new List<PlaceHolder> { null, null, null };

    int current = 0;
    public PlaceHolder currentPlaceHolder;
    public List<Image> images = new();

    [SerializeField]
    public void GetItem(PlaceHolder item)
    {
        if (item == null)
            return;

        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;

        if (PlaceHolders[current] != null)
            PlaceHolders[current].transform.SetParent(null);

        PlaceHolders[current] = item;
        currentPlaceHolder = PlaceHolders[current];
    }

    public void Switch(int next)
    {
        if (PlaceHolders[current] != null)
        {
            PlaceHolders[current].gameObject.SetActive(false);
        }

        if (current + next > PlaceHolders.Count - 1)
        {
            current = 0;
        }
        else if (current + next < 0)
        {
            current = PlaceHolders.Count - 1;
        }
        else
        {
            current += next;
        }

        if (PlaceHolders[current] != null)
        {
            PlaceHolders[current].gameObject.SetActive(true);
            currentPlaceHolder = PlaceHolders[current];
        }
    }
}
