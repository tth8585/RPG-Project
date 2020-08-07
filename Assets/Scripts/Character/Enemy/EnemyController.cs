
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyStats))]
public class EnemyController : MonoBehaviour, IEnemy
{
    public EnemyStats enemyStats;
    public LayerMask aggroLayerMask;
    NavMeshAgent navMeshAgent;

    public GameObject enemyTextName;
    public GameObject target;
    public float distanceResetTarget = 30f;
    float timer = 0;
    public float wanderTimer;
    public float wanderRadius = 20;
    Vector3 positionSpawn;
    public bool runBack;

    //===================
    //loot
    public DropTable dropTable;
    public ItemPickup itemPickup;
    public List<LootDrop> lootDrops;
    public ExpFollow expFollow;
    //private Inventory inventory;
    public string enemyName { get { return enemyStats.enemyName; } }

    public Sprite icon { get { return enemyStats.enemyIcon; } }

    public int ID { get; set; } // for quest

    public int Experience { get { return enemyStats.expGive; } }

    public Spawner Spawner {  get; set;  }
    public bool isDead { get ; set ; }
    public bool inCombat { get ; set; }

    public virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyStats = GetComponent<EnemyStats>();
        navMeshAgent.speed = enemyStats.MovementSpeed;

        SetUpLoot();
        isDead = false;
        enemyTextName.GetComponent<TextMesh>().text = enemyStats.enemyName;
        positionSpawn = Spawner.gameObject.transform.position;
    }
    
    public virtual void Die()
    {
        isDead = true;
        GiveExp();
        DropLoot();
        this.Spawner.Respawn();

        Destroy(gameObject);
    }

    public virtual void PerformAttack()
    {
        int damageAmount = Random.Range(enemyStats.minDamage, enemyStats.maxDamage + 1);
        target.GetComponentInParent<PlayerController>().TakeDamage(damageAmount, false);
    }

    public virtual void TakeDamage(float amount, bool isCrit)
    {
        SearchTargetWhenTakedDamage();
    }

    private void SetUpLoot()
    {
        dropTable = new DropTable();
        dropTable.loot = new List<LootDrop>();
        for (int i = 0; i < lootDrops.Count; i++)
        {
            dropTable.loot.Add(lootDrops[i]);
        }
    }

    public void GiveExp()
    {
        CombatEvent.EnemyDied(this);
    }

    private void DropLoot()
    {
        Item item = dropTable.GetDrop();
        if (item != null)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            ItemPickup instance = Instantiate(itemPickup, pos, Quaternion.identity);
            instance.itemDrop = item;
            //instance.inventory = inventory;
            instance.GetComponent<Renderer>().material = instance.material;

            int random = Random.Range(2, 5);
            for(int i = 0; i < random; i++)
            {
                Vector3 expPos = new Vector3(transform.position.x+Random.Range(0,0.5f), transform.position.y+Random.Range(0, 0.5f), transform.position.z+Random.Range(0, 0.5f));
                ExpFollow instanceExp = Instantiate(expFollow, expPos, Quaternion.identity);
                instanceExp.target = target.transform;
            }
        }
    }

    private void Update()
    {
        if (enemyTextName != null)
        {
            if (Camera.main != null)
            {
                enemyTextName.transform.LookAt(Camera.main.transform.position);
                enemyTextName.transform.Rotate(0, 180, 0);
            }
        }

        if (!isDead)
        {
            float distanceFromSpawn = Vector3.Distance(positionSpawn, this.transform.position);
            if (distanceFromSpawn > 50 && !runBack)
            {
                target = null;
                runBack = true;
            }

            if (target == null)
            {
                if (runBack)
                {
                    RunBackToTheSpawn();
                }
                else
                {
                    timer += Time.deltaTime;
                    SearchTarget();
                    Wander();
                }  
            }
            else
            {
                FollowPlayer();
            }
        }
    }
    void RunBackToTheSpawn()
    {
        if (Vector3.Distance(positionSpawn, this.transform.position) < 2)
        {
            runBack = false;
        }
        else
        {
            navMeshAgent.speed = enemyStats.MovementSpeed + 5;
            navMeshAgent.SetDestination(positionSpawn);
            Debug.Log("enemy runback");
        }
    }
    void Wander()
    {
        if (timer > wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            navMeshAgent.speed = enemyStats.speedWander;
            navMeshAgent.SetDestination(newPos);
            timer = 0;
            wanderTimer = Random.Range(5f, 15f);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    void SearchTarget()
    {
        Collider[] withinAggroCollifer = Physics.OverlapSphere(transform.position, enemyStats.rangeDetected, aggroLayerMask);
        int i = 0;
        while (i < withinAggroCollifer.Length)
        {
            if (withinAggroCollifer[i].transform.tag == "Player")
            {
                target = withinAggroCollifer[i].transform.gameObject;
            }
            i++;
        }
    }
    public void SearchTargetWhenTakedDamage()
    {
        Collider[] withinAggroCollifer = Physics.OverlapSphere(transform.position, 1000, aggroLayerMask);
        int i = 0;
        while (i < withinAggroCollifer.Length)
        {
            if (withinAggroCollifer[i].transform.tag == "Player")
            {
                target = withinAggroCollifer[i].transform.gameObject;
            }
            i++;
        }
    }
    float elapsed = 0f;
    void FollowPlayer()
    {
        float distance = Vector3.Distance(target.transform.position, this.transform.position);
        if (  distance > distanceResetTarget)
        {
            target = null;
            return;
        }
        else if (distance < 2f)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= CalculateAS())
            {
                elapsed = elapsed % 1f;
                PerformAttack();
            }
            
        }
       
        navMeshAgent.speed = enemyStats.MovementSpeed;

        navMeshAgent.SetDestination(target.transform.position);
    }

    float CalculateAS()
    {
        float result = 0;
        result = 1/enemyStats.AttackSpeed;
        return result;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyStats.rangeDetected);
    }
}
