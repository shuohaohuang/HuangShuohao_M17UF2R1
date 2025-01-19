using System;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Exhibitor : AInteratacable
{
    [SerializeField]
    FloatVariables pcGold;

    [SerializeField]
    GameObject article;

    [SerializeField]
    ItemsSO current;

    [SerializeField]
    Collider2D exhibitorCollider;

    [SerializeField]
    List<ItemsSO> purchableItems = new();

    [SerializeField]
    ItemInspectorSO inspectorSO;

    public override void Act()
    {
        if (pcGold.Current >= current.price)
        {
            Instantiate(current.Item, transform.position, quaternion.identity);
            Destroy(article);
            exhibitorCollider.enabled = false;
            pcGold.Current -= current.price;
            inspectorSO.EndInspect();
        }
    }

    private void Start()
    {
        current = purchableItems[UnityEngine.Random.Range(0, purchableItems.Count)];
        article.GetComponent<SpriteRenderer>().sprite = current.sprite;
    }

    private new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("PLAYER"))
        {
            inspectorSO.InpectItem(current);
        }
    }

    private new void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (other.gameObject.CompareTag("PLAYER"))
        {
            inspectorSO.EndInspect();
        }
    }
}
