using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class Draggable : MonoBehaviour,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    // ���� �θ���ġ (reset ��)
    private Transform initialParent;
    private Vector2 initialPosition;

    // �巡�� �� �÷���
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
        // ���Կ� �� �ִ� ���¸� �巡�� ����
        if (transform.parent != initialParent)
            return;

        isDragged = true;
        // �巡�� ���� ���� �θ���ġ ����
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

        // ��� �� �θ� �˻�
        if (transform.parent == initialParent)
        {
            // ���Կ� ��ӵ��� �ʾҴٸ� ����ġ
            rectTransform.anchoredPosition = initialPosition;
        }
        else
        {
            // ���Կ� ��ӵƴٸ� ���� ����(0,0)�� ����
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    // ���� Ŭ������ ȣ��: ������ �ʱ� ���·� ����
    public void ResetToInitial()
    {
        isDragged = false;
        transform.SetParent(initialParent);
        rectTransform.anchoredPosition = initialPosition;
    }
}
