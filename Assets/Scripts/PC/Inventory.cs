#nullable enable
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public AItem[] aItems = new AItem[3];
    int current = 0;

    public InventoryUISO? ui;

    private void Start()
    {
        for (int i = 0; i < aItems.Length; i++)
        {
            current = i;
            GetItem(aItems[i]);
            if (aItems[i] != null)
                aItems[i].SwapOut();
        }
        Switch(1);
    }

    [SerializeField]
    public void GetItem(AItem item)
    {
        if (item == null)
            return;
        item.HoldNew(transform);

        if (aItems[current] != null && aItems[current] != item)
        {
            aItems[current].transform.SetParent(null);
            aItems[current].transform.position = transform.position;
            aItems[current].transform.Rotate(0, 0, 0);
            SceneManager.MoveGameObjectToScene(
                aItems[current].gameObject,
                SceneManager.GetActiveScene()
            );
        }

        aItems[current] = item;
        ui?.SetItem(current, item);
    }

    public AItem? GetCurrentItem()
    {
        return aItems[current];
    }

    public void Switch(int next)
    {
        if (!ReferenceEquals(aItems[current], null))
        {
            aItems[current]?.SwapOut();
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
            aItems[current]?.SwapIn();
        }
    }
}
