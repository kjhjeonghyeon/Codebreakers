using UnityEngine;
using UnityEngine.EventSystems;

public class SlotDropHandler : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    // 드롭 시 부모를 슬롯으로 변경
    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag;
        if (dragged == null) return;

        var draggable = dragged.GetComponent<Draggable>();
        if (draggable != null)
            draggable.transform.SetParent(transform);
    }

    // 슬롯 클릭 시 슬롯 자식(들)을 모두 초기 위치로 되돌림
    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i);
            var draggable = child.GetComponent<Draggable>();
            if (draggable != null)
                draggable.ResetToInitial();
        }
    }
}
