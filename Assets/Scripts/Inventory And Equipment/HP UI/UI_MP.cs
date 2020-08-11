
using UnityEngine;
using UnityEngine.UI;

public class UI_MP : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Image image;

    private float newFillAmount;
    float speed = 1f;
    private void Start()
    {
        UIEvent.OnPlayerManaChanged += UpdateMana;
    }

    private void FixedUpdate()
    {
        if (newFillAmount < this.image.fillAmount)
        {
            this.image.fillAmount -= speed * Time.fixedDeltaTime;
        }
    }
    private void UpdateMana(float mp, float maxMp)
    {    
        newFillAmount = (float)(mp / maxMp);
        if (this.text != null)
        {
            this.text.text = ((int)mp).ToString() + "/" + ((int)maxMp).ToString();
        }
        
        if (newFillAmount > this.image.fillAmount)
        {
            if (this.image != null)
            {
                this.image.fillAmount = newFillAmount;
            }
        }
    }
}
