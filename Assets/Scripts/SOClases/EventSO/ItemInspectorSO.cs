using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ItemInspectorSO", menuName = "Scriptable Objects/ItemInspectorSO")]
public class ItemInspectorSO : ScriptableObject
{
    public UnityAction<ItemsSO> OnInspectItem;
    public UnityAction OnInspectItemEnd;

    public void InpectItem(ItemsSO item)
    {
        OnInspectItem.Invoke(item);
    }

    public void EndInspect()
    {
        OnInspectItemEnd.Invoke();
    }
}
