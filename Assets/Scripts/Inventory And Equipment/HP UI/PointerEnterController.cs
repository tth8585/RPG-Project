
using UnityEngine;
using UnityEngine.EventSystems;

public class PointerEnterController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject target;
    public void OnPointerEnter(PointerEventData eventData)
    {
        target.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        target.SetActive(false);
    }
}
