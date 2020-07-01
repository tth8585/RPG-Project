using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandSlider : MonoBehaviour,IDragHandler
{
    public event EventHandler OnValueChanged;
    [SerializeField] private RectTransform dragRectTransform;
    [SerializeField] float maxHandPos, minHandPos;
    [SerializeField] float windowScale;
    private Canvas canvas;

    [SerializeField] private Image fillImage;
    float currentPercent;
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

    private void Start()
    {
        SetUp();
    }
    public void OnDrag(PointerEventData eventData)
    {
        float value = eventData.delta.x / (canvas.scaleFactor*windowScale);
        dragRectTransform.anchoredPosition = new Vector2(Mathf.Clamp(value+ dragRectTransform.anchoredPosition.x, minHandPos,maxHandPos),0);

        fillImage.fillAmount = CalFillAmount();
        //for sound value
        if (OnValueChanged != null)
        {
            OnValueChanged(this, EventArgs.Empty);
        }
    }
    private void SetUp()
    {
        //float rectX;
        //Vector2 start = new Vector2(minHandPos, 0);
        //Vector2 end = new Vector2(ref rectX, 0);
        //currentPercent * (maxHandPos - minHandPos);
    }
    public void GetVolumeValue(float value)
    {
        currentPercent = value;
        Debug.Log(value);
    }
    private float CalFillAmount()
    {
        Vector2 start = new Vector2(minHandPos, 0);
        Vector2 end = new Vector2(dragRectTransform.anchoredPosition.x, 0);
        float distance = Vector2.Distance(start, end);
        float percent = (float)(distance / (maxHandPos - minHandPos));

        return percent;
    }
    public float GetValue()
    {
        Vector2 start = new Vector2(minHandPos, 0);
        Vector2 end = new Vector2(dragRectTransform.anchoredPosition.x, 0);
        float distance = Vector2.Distance(start, end);
        float percent = (int)(distance / (maxHandPos - minHandPos) * 100/2.5)-20;
        return percent;
    }

    private float MagnitudeReveser(float value)
    {
        float result =0;
        result = value * (maxHandPos - minHandPos);
        return result;
    }
}
