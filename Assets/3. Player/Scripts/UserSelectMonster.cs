
using UnityEngine;

public class UserSelectMonster : MonoBehaviour
{
    public GameObject selectedUnit;
    private bool behindEnemy;
    bool canAttack;
    Vector3 toTarget;
    float distance;
    Vector3 targetDir;
    Vector3 forward;
    float angle;
    float rangeCanSelect = 20;
    float rangeCanTalkToNPC = 5f;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SelectTarget();
        }

        CheckBehind();

        CheckInRangeSelectedUnit();

        //CheckCanAttack();
    }

    private void CheckInRangeSelectedUnit()
    {
        if (selectedUnit != null)
        {
            if (Vector3.Distance(this.transform.position, selectedUnit.transform.position) > rangeCanSelect)
            {
                selectedUnit = null;
                SelectedUnitSystem.Instance.HideUnit();
                DialogSystem.Instance.HideDialogue();
            }
        }
    }

    private void CheckCanAttack()
    {
        distance = Vector3.Distance(this.transform.position, selectedUnit.transform.position);
        targetDir = selectedUnit.transform.position - transform.position;
        forward = transform.forward;
        angle = Vector3.Angle(targetDir, forward);

        if (angle > 60)
        {
            canAttack = false;
        }
        else
        {
            if (distance < 60)
            {
                canAttack = true;
            }
            else
            {
                canAttack = false;
            }
        }
    }

    private void CheckBehind()
    {
        if (selectedUnit != null)
        {
            toTarget = (selectedUnit.transform.position - transform.position).normalized;
            if (Vector3.Dot(toTarget, selectedUnit.transform.forward) < 0)
            {
                behindEnemy = true;
            }
            else
            {
                behindEnemy = false;
            }
        }
    }

    void SelectTarget()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit, 10000))
        {
            
            if (hit.transform.tag == "Enemy")
            {
                selectedUnit = hit.transform.gameObject;
                SelectedUnitSystem.Instance.AddNewUnit(selectedUnit.GetComponent<IEnemy>().icon, selectedUnit.GetComponent<IEnemy>().enemyName, (float)selectedUnit.GetComponent<Slime>().enemyStats.currentHP/ (float)selectedUnit.GetComponent<Slime>().enemyStats.maxHP, selectedUnit.GetComponent<Slime>().GetInstanceID());
                SelectedUnitSystem.Instance.ChangeColor(true);
            }
            else if (hit.transform.tag == "NPC")
            {
                selectedUnit = hit.transform.gameObject;
                SelectedUnitSystem.Instance.AddNewUnit(selectedUnit.GetComponent<NPC>().spriteNPC, selectedUnit.GetComponent<NPC>().nameNPC, 1, selectedUnit.GetComponent<NPC>().GetInstanceID());
                SelectedUnitSystem.Instance.ChangeColor(false);

                if (Vector3.Distance(this.transform.position, selectedUnit.transform.position) <= rangeCanTalkToNPC)
                {
                    selectedUnit.GetComponent<NPC>().Interact();
                }
            }
        }
    }
}
