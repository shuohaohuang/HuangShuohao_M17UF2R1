#nullable enable
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public AItems?[] aItems = new AItems[3];
    int current = 0;
    public List<Image> images = new();

    [SerializeField]
    public void GetItem(AItems item)
    {
        if (item == null)
            return;

        item.HoldNew(transform);

        images[current].transform.GetChild(0).GetComponent<Image>().sprite =
            item.GetComponent<SpriteRenderer>().sprite;

        if (!ReferenceEquals(aItems[current], null))
        {
            aItems[current]?.transform.SetParent(null);
        }

        aItems[current] = item;
    }

    public AItems? GetCurrentItem()
    {
        return aItems[current];
    }

    public void Switch(int next)
    {
        if (!ReferenceEquals(aItems[current], null))
        {
            aItems[current]?.gameObject.SetActive(false);
        }

        if (current + next > aItems.Length - 1)
        {
            current = 0;
        }
        else if (current + next < 0)
        {
            current = aItems.Length - 1;
        }
        else
        {
            current += next;
        }

        if (!ReferenceEquals(aItems[current], null))
        {
            aItems[current]?.gameObject.SetActive(true);
        }
    }
}
