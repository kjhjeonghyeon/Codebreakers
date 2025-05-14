using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class Draggable : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    // 최초 부모·위치 (reset 용)
    private Transform initialParent;
    private Vector2 initialPosition;

    // 드래그 중 플래그
    private bool isDragged;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();

        initialParent = transform.parent;
        initialPosition = rectTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 슬롯에 들어가 있는 상태면 드래그 금지
        if (transform.parent != initialParent)
            return;

        isDragged = true;
        // 드래그 시작 시점 부모·위치 저장
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragged) return;

        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            rectTransform.anchoredPosition = localPoint;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
       
        if (!isDragged) return;
        isDragged = false;
        canvasGroup.blocksRaycasts = true;

        // 드롭 후 부모 검사
        if (transform.parent == initialParent)
        {
            // 슬롯에 드롭되지 않았다면 원위치
            rectTransform.anchoredPosition = initialPosition;
        }
        else
        {
            // 슬롯에 드롭됐다면 슬롯 기준(0,0)에 스냅
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    // 슬롯 클릭으로 호출: 무조건 초기 상태로 복귀
    public void ResetToInitial()
    {
        isDragged = false;
        transform.SetParent(initialParent);
        rectTransform.anchoredPosition = initialPosition;
    }
}
