using UnityEngine;

public class Slime : EnemyController
{
    public override void Start()
    {
        base.Start();
        ID = 0;
        enemyStats.speedWander = 1f;
    }

    //public override void PerformAttack()
    //{
        
    //}

    public override void TakeDamage(float amount, bool isCrit)
    {
        if (runBack)
        {
            if (floatingText)
            {
                ShowFloatingText(0, isCrit);
            }
            return;
        }

        base.TakeDamage(amount, isCrit);

        amount -= enemyStats.Armor;

        enemyStats.currentHP -= (int)amount;

        if (floatingText)
        {
            ShowFloatingText((int)amount, isCrit);
        }

        if (enemyStats.currentHP <= 0)
        {
            Die();
        }

        UIEvent.EnemyHPChange((float)enemyStats.currentHP / (float)enemyStats.maxHP, this.GetInstanceID());
    }
    [SerializeField] private GameObject floatingText;
    private void ShowFloatingText(int amount, bool isCrit)
    {
        var go = Instantiate(floatingText, transform.position, Quaternion.identity, transform);
        go.GetComponent<FloatingTextController>().SetText(amount.ToString(), isCrit);
    }
}
