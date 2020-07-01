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
        currentPercent = OptionsMenu.Instance.GetVolume();
        fillImage.fillAmount = FillAmounteReveser(currentPercent);
        dragRectTransform.anchoredPosition = new Vector2(MagnitudeReveser(currentPercent), 0);
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

    private float FillAmounteReveser(float value)
    {
        float result = 0;
        result = (float)((value + 20) / 40);
        return result;
    }

    private float MagnitudeReveser(float value)
    {
        float result = 0;
        Vector2 start = new Vector2(minHandPos, 0);
        float distance;
        distance = (value + 20) * (maxHandPos - minHandPos) * 0.01f * 2.5f;
        result = SolveQuadratic(1, -2 * minHandPos, minHandPos * minHandPos - distance * distance);
        return result;
    }

    private float SolveQuadratic(double a, double b, double c)
    {
        double sqrtpart = b * b - 4 * a * c;

        double x, x1, x2, img;

        if (sqrtpart > 0)

        {

            x1 = (-b + System.Math.Sqrt(sqrtpart)) / (2 * a);

            x2 = (-b - System.Math.Sqrt(sqrtpart)) / (2 * a);

            //Console.WriteLine("Two Real Solutions: {0,8:f4} or  {1,8:f4}", x1, x2);
            //Debug.Log(x1 + "/"+x2);
            return (float)x1;
        }

        else if (sqrtpart < 0)

        {

            sqrtpart = -sqrtpart;

            x = -b / (2 * a);

            img = System.Math.Sqrt(sqrtpart) / (2 * a);

            ///Console.WriteLine("Two Imaginary Solutions: {0,8:f4} + {1,8:f4} i or {2,8:f4} + {3,8:f4} i", x, img, x, img);
            //Debug.Log("vo nghiem hay gi ?");
            return 0;
        }

        else

        {

            x = (-b + System.Math.Sqrt(sqrtpart)) / (2 * a);

            //Debug.Log(x);
            return (float   )x;

        }
    }
}
