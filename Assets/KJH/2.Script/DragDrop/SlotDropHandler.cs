using UnityEngine.EventSystems;
using UnityEngine;

public class SlotDropHandler : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public SequenceChecker sequenceChecker; // 인스펙터 연결

    public void OnDrop(PointerEventData eventData)
    {
        var dragged = eventData.pointerDrag;
        if (dragged == null) return;

        var draggable = dragged.GetComponent<Draggable>();
        if (draggable == null) return;

        if (transform.childCount == 0)
        {
            draggable.transform.SetParent(transform);
            sequenceChecker.RegisterBlock(draggable.GetComponent<Block>()); // ✅ 등록
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i);
            var draggable = child.GetComponent<Draggable>();
            if (draggable != null)
            {
                sequenceChecker.UnregisterBlock(draggable.GetComponent<Block>()); // ✅ 제거
                draggable.ResetToInitial();
            }
        }
    }
}
