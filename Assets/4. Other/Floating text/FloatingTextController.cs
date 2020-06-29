
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextController : MonoBehaviour
{
    private const float DISAPPEAR_TIME_MAX = 1f;
    private float timeDisappear;
    private float scaleCrit = 0.03f;
    private TextMesh damageText;
    Vector3 offset = new Vector3(0, 0, 0);
    Vector3 randomizeIntensity = new Vector3(0.5f, 0f, 0f);

    Color textColor;
    private void OnEnable()
    {
        timeDisappear = DISAPPEAR_TIME_MAX;
        //Destroy(gameObject, timeDisappear);

        transform.localPosition += new Vector3(Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
                                              Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
                                              Random.Range(-randomizeIntensity.z, randomizeIntensity.z));
        transform.localPosition += offset;

        damageText = GetComponentInChildren<TextMesh>();
    }

    public void SetText(string text, bool isCrit)
    {
        damageText.text = text;
        if (isCrit)
        {
            textColor = Color.red;
            damageText.color = textColor;
            Vector3 tempScale = damageText.transform.localScale;
            transform.localScale = tempScale + new Vector3(scaleCrit,scaleCrit,scaleCrit);
        }
        else
        {
            textColor = damageText.color;
        }
    }

    private void Update()
    {
        damageText.transform.LookAt(Camera.main.transform.position);
        damageText.transform.Rotate(0, 180, 0);


        float moveYSpeed = 2f;
        transform.position += new Vector3(0, moveYSpeed, 0) * Time.deltaTime;

        if (timeDisappear > DISAPPEAR_TIME_MAX * 0.5f)
        {
            float increaseScaleAmount = 0.01f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 0.06f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        timeDisappear -= Time.deltaTime;
        if(timeDisappear < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            damageText.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
        
    }
}
