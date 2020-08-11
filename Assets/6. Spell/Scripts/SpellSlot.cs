
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Text indexText;
    [SerializeField] Image bg;
    public Image icon;
    public GameObject auraIcon;
    public int indexSlot;
    public string idSlot;

    protected Color normalColor = Color.white;
    protected Color disableColor = Color.clear;
    private Color bgColor= new Color(0.33f,0.33f,0.33f,1);

    public int spellLevel;
    [SerializeField] GameObject[] listSpellLevelIcon;

    private Spell _spell;
    public Spell Spell
    {
        get { return _spell; }
        set
        {
            _spell = value;

            if (_spell == null)
            {
                bg.color = disableColor;
                icon.color = disableColor;
                indexText.text = string.Empty;
                idSlot = string.Empty;
            }
            else
            {

                bg.sprite = _spell.spellIcon;
                bg.color = bgColor;

                icon.sprite = _spell.spellIcon;
                icon.color = normalColor;

                indexText.text = indexSlot.ToString();

                idSlot = _spell.spellId;
            }
        }
    }
    public void ToggleAuraIcon(bool isActive)
    {
        auraIcon.SetActive(isActive);
        icon.gameObject.SetActive(!isActive);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("pointer click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointer enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("pointer exit");
    }

    public void SpellLock()
    {
        icon.color = bgColor;
    }

    public void SpellUnlock()
    {
        icon.color = normalColor;
        for(int i = 0; i < spellLevel; i++)
        {
            listSpellLevelIcon[i].SetActive(true);
        }
    }
}
