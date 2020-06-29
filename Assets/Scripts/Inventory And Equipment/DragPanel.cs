using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragPanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform rectTransformDrag;
    [SerializeField] private Canvas canvas;

    private Image[] imagesList;

    private void Awake()
    {
        if (rectTransformDrag == null)
        {
            rectTransformDrag = transform.parent.GetComponent<RectTransform>();
        }

        if (canvas == null)
        {
            Transform transformTest = transform.parent;
            while (rectTransformDrag != null)
            {
                canvas = transformTest.GetComponent<Canvas>();
                if (canvas != null)
                {
                    break;
                }
                transformTest = transformTest.parent;
            }
        }

        imagesList = rectTransformDrag.GetComponentsInChildren<Image>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        foreach(Image image in imagesList)
        {
            Color colorWhenDrag = image.color;
            if(colorWhenDrag.a ==1) colorWhenDrag.a = 0.6f;
            image.color = colorWhenDrag;
        }
        UIEvent.PlayerDragging(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransformDrag.anchoredPosition += eventData.delta/canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        foreach (Image image in imagesList)
        {
            Color colorWhenDrag = image.color;
            if(colorWhenDrag.a ==0.6f) colorWhenDrag.a = 1f;
            image.color = colorWhenDrag;
        }
        UIEvent.PlayerDragging(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        rectTransformDrag.SetAsLastSibling();
    }
}
