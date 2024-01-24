using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;


public class WordUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler 
{
    [SerializeField] private Canvas canvas;
    [SerializeField, Range(0,2)] private float smoothTime;
    
    private RectTransform rectTransform;
    private Vector3 initialRectPos;
    public CanvasGroup canvasGroup;
    private Image image;

    public bool isPlaced;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
        initialRectPos = rectTransform.position;
    }

    private void Update()
    {
        if (isPlaced)
        {
            image.raycastTarget = false;
        }
        else
        {
            image.raycastTarget = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(isPlaced) return;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        if(isPlaced) return;
        rectTransform.position += (Vector3)eventData.delta / canvas.scaleFactor;
        //rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        if (!isPlaced)
        {
            ResetPosition();
        }
        
    }

    public void ResetPosition()
    {
        Vector3 targetPos = initialRectPos;
        rectTransform.DOMove(initialRectPos, smoothTime);
        
    }

    
}
