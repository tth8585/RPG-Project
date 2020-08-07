
using System.Collections;
using UnityEngine;

public class ChooseSystem : MonoBehaviour
{
    [SerializeField] GameObject[] listSelection;
    [SerializeField] Camera cam;
    [SerializeField] GameObject nextBtn;
    [SerializeField] GameObject imageFade;
    [SerializeField] GameObject imageFadeMain;

    private void Start()
    {
        nextBtn.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1))
        {
            SelectTarget();
        }
    }

    void SelectTarget()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000))
        {
            if (hit.transform.tag == "Player")
            {
                for(int i = 0; i < listSelection.Length; i++)
                {
                    listSelection[i].transform.Find("CircleItemYellow").gameObject.SetActive(false);
                }
                nextBtn.SetActive(false);

                ActiveTarget(hit.transform);             
            }
        }
    }

    void ActiveTarget(Transform target)
    {
        target.Find("CircleItemYellow").gameObject.SetActive(true);
        if (target.Find("Unlock") != null)
        {
            nextBtn.SetActive(true);
        }
    }
}
