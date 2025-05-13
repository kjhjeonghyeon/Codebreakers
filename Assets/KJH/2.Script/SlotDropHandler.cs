using UnityEngine;
using UnityEngine.EventSystems;

public class SlotDropHandler : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    // ��� �� �θ� �������� ����
    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag;
        if (dragged == null) return;

        var draggable = dragged.GetComponent<Draggable>();
        if (draggable != null)
            draggable.transform.SetParent(transform);
    }

    // ���� Ŭ�� �� ���� �ڽ�(��)�� ��� �ʱ� ��ġ�� �ǵ���
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
