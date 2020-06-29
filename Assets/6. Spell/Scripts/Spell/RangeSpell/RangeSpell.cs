
using System.Collections;
using UnityEngine;

public class RangeSpell : MonoBehaviour
{
    public GameObject target;
    Vector3 _velocity = Vector3.zero;
    float spellSpeed = 30f;
    private float damage;
    public bool isCrit;
 
    void Update()
    {
        if (target != null )
        {
            float distance = Vector3.Distance(target.transform.position, this.transform.position);

            if (distance > 2f)
            {
                transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref _velocity, Time.deltaTime * spellSpeed);
            }
            else
            {
                StartCoroutine(DestroyFX());
                HitTarget();
            }
        }
        
    }

    void HitTarget()
    {
        target.gameObject.GetComponent<IEnemy>().TakeDamage(damage, isCrit);
        Debug.Log("chua xu ly crit dame magic target spell");
        Destroy(this.gameObject);
    }

    public void SetSpell(float value, GameObject tar)
    {
        target = tar;
        damage = value;
    }

    private IEnumerator DestroyFX()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
