using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInspector : MonoBehaviour
{
    [SerializeField]
    Image background;

    [SerializeField]
    Image itemSprite;

    [SerializeField]
    List<TextMeshProUGUI> texts;

    [SerializeField]
    ItemInspectorSO itemInspectorSO;

    private void Start()
    {
        background.gameObject.SetActive(false);
        itemInspectorSO.OnInspectItem += ShowInfo;
        itemInspectorSO.OnInspectItemEnd += HideInfo;
    }

    void ShowInfo(ItemsSO item)
    {
        background.gameObject.SetActive(true);

        texts[0].text = $"Price : {item.price}";
        texts[1].text = $"Name : {item.alias}";
        texts[2].text = $"Cd : {item.cd}";
        itemSprite.sprite = item.sprite;
    }

    void HideInfo()
    {
        background.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        itemInspectorSO.OnInspectItem -= ShowInfo;
        itemInspectorSO.OnInspectItemEnd -= HideInfo;
    }
}
