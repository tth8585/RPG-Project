
using UnityEngine;
using UnityEngine.UI;

public class UI_HP : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Image image;
    [SerializeField] private Image imageLip;

    private float newFillAmount;
    float speed = 1f;
    private void Start()
    {
        UIEvent.OnPlayerHealthChanged += UpdateHealth;
    }

    private void LateUpdate()
    {
        //if (newFillAmount < this.image.fillAmount)
        //{
        //    this.image.fillAmount -= speed * Time.deltaTime;
        //}

        //imageLip.fillAmount = image.fillAmount + 0.01f;
    }
    private void UpdateHealth(float hp, float maxHp)
    {
        newFillAmount = (float)(hp / maxHp);

        this.text.text = ((int)hp).ToString()+"/"+ ((int)maxHp).ToString();
        this.image.fillAmount = newFillAmount;
        imageLip.fillAmount = image.fillAmount + 0.01f;

        //if (newFillAmount > this.image.fillAmount)
        //{
        //    this.image.fillAmount = newFillAmount;
        //}
    }
}
