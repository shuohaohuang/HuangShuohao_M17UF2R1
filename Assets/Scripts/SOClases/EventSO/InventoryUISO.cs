using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InventoryUISO", menuName = "Scriptable Objects/InventoryUISO")]
public class InventoryUISO : ScriptableObject
{
    public UnityAction OnItemsUpdated;
    private AItem[] items = new AItem[3];

    public AItem[] Items => items;

    public UnityAction<int, AItem> OnGetItem;

    public void SetItem(int index, AItem item)
    {
        if (index >= 0 && index < items.Length)
        {
            items[index] = item;
            OnItemsUpdated?.Invoke();
        }
    }

    public AItem GetItem(int index)
    {
        if (index >= 0 && index < items.Length)
        {
            return items[index];
        }
        return null;
    }
}
