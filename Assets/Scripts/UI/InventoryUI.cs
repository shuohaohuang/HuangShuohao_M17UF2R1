using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image[] ItemsUI = new Image[3];
    public InventoryUISO inventoryUISO;

    private void OnEnable()
    {
        inventoryUISO.OnItemsUpdated += UpdateUI;
    }

    private void OnDisable()
    {
        inventoryUISO.OnItemsUpdated -= UpdateUI;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < inventoryUISO.Items.Length; i++)
        {
            AItem item = inventoryUISO.GetItem(i);
            if (item != null)
            {
                ItemsUI[i].sprite = item.sprite;
            }
        }
    }

    private void Start()
    {
        UpdateUI();
    }
}
