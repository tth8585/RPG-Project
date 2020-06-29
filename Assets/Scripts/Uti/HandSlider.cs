using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandSlider : MonoBehaviour,IDragHandler
{
    public event EventHandler OnValueChanged;
    [SerializeField] private RectTransform dragRectTransform;
    [SerializeField] float maxHandPos, minHandPos;
    [SerializeField] float windowScale;
    private Canvas canvas;

    private void Awake()
    {
        if (canvas == null)
        {
            Transform testCanvas = transform.parent;
            while (testCanvas != null)
            {
                canvas = testCanvas.GetComponent<Canvas>();
                if (canvas != null)
                {
                    break;
                }
                testCanvas = testCanvas.parent;
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        float value = eventData.delta.x / (canvas.scaleFactor*windowScale);
        dragRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(value+ dragRectTransform.anchoredPosition.x, minHandPos,maxHandPos),0);
        if (OnValueChanged != null)
        {
            OnValueChanged(this, EventArgs.Empty);
        }
    }
    //private float CalulatePercent(float pos)
    //{
    //    Vector2 start = new Vector2(minHandPos, 0);
    //    Vector2 end = new Vector2(pos, 0);
    //    float distance =   Vector2.Distance(start, end);
    //    float percent = (int)(distance / (maxHandPos - minHandPos)*100)-80;
    //    return percent;
    //}

    public float GetValue()
    {
        Vector2 start = new Vector2(minHandPos, 0);
        Vector2 end = new Vector2(dragRectTransform.anchoredPosition.x, 0);
        float distance = Vector2.Distance(start, end);
        float percent = (int)(distance / (maxHandPos - minHandPos) * 100/2.5)-20;
        return percent;
    }
}
