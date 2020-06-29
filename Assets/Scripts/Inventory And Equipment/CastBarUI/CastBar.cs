
using UnityEngine;
using UnityEngine.UI;

public class CastBar : MonoBehaviour
{
    public static CastBar Instance { get; set; }
    [SerializeField] private Image imageFill;

    private bool isCasting;
    private float castingTimer;
    private float maxCastingTimer;
    private Vector3 castPos;
    public GameObject player;
    public GameObject castBarUI;

    Animator animator;
    bool isActive;
   
    private void Awake()
    {
        animator = castBarUI.GetComponent<Animator>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void CastSpell(float duration)
    {
        if (!isCasting)
        {
            Debug.Log("casting ...");
            castingTimer = duration;
            maxCastingTimer = duration;
            castPos = player.transform.position;
            isCasting = true;
        }
    }

    private void Update()
    {
        if (isCasting)
        {
            //show cast bar if cast bar is hidden
            if(isActive == false)
            {
                //castBarUI.SetActive(true);
                ShowBar();
            }
            if (castingTimer > 0)
            {
                castingTimer -= Time.deltaTime;
                imageFill.fillAmount = (float)(1 - castingTimer / maxCastingTimer);
            }
            else
            {
                isCasting = false;
                //hide cast bar
                //castBarUI.SetActive(false);
                HideBar();
            }

            if (player.transform.position != castPos)
            {
                isCasting = false;
                Debug.Log("move when casting");
                //castBarUI.SetActive(false);
                HideBar();
            }
        }
    }

    void ShowBar()
    {
        isActive = true;
        animator.SetBool("show",isActive);
    }

    void HideBar()
    {
        isActive = false;
        animator.SetBool("show", isActive);
    }

    void BarDefault()
    {

    }
}
